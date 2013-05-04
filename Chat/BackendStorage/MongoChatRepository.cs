using Chat.BackendStorage.Entities;
using Chat.Models;
using Chat.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat.BackendStorage
{
  public class MongoChatRepository : IChatRepository
  {
    private readonly IConfigurationHelper _configurationHelper;
    private readonly ISecurityManager _securityManager;
    private readonly MongoDatabase _database;

    static MongoChatRepository()
    {
      if (!BsonClassMap.IsClassMapRegistered(typeof(User))) BsonClassMap.RegisterClassMap<User>();
      if (!BsonClassMap.IsClassMapRegistered(typeof(Message))) BsonClassMap.RegisterClassMap<Message>();
    }

    private MongoCollection<User> Users
    {
      get
      {
        var result = _database.GetCollection<User>("users");
        return result;
      }
    }

    private MongoCollection<Message> Messages
    {
      get
      {
        var result = _database.GetCollection<Message>("messages");
        return result;
      }
    }

    public MongoChatRepository(IConfigurationHelper configurationHelper,
      ISecurityManager securityManager)
    {
      _configurationHelper = configurationHelper;
      _securityManager = securityManager;
      _database = ConnectToDatabase();
    }

    public bool IsUserRegistered(string email)
    {
      email = email.ToLowerInvariant();
      var query = Query<User>.EQ(u => u.Email, email);
      var count = Users.Find(query).Count();
      if (count > 1)
      {
        var message = string.Format("there are more that one user with the same email: \"{0}\"", email);
        throw new ApplicationException(message);
      }
      var result = count > 0;
      return result;
    }

    public User GetUserByEmail(string email)
    {
      email = email.ToLowerInvariant();
      var query = Query<User>.EQ(u => u.Email, email);
      var result = Users.FindOne(query);
      return result;
    }

    public void AddNewUser(RegisterUserViewModel userData)
    {
      var user = userData.ToEntity();
      var passwordSalt = _securityManager.GenerateNewSalt();
      var passwordHash = _securityManager.ComputeHash(passwordSalt, userData.Password);
      user.PasswordSalt = new BsonBinaryData(passwordSalt);
      user.PasswordHash = new BsonBinaryData(passwordHash);
      Users.Insert(user);
    }

    public bool IsCredentialsValid(string email, string password)
    {
      var user = GetUserByEmail(email);
      if (user == null)
        return false;
      var result = _securityManager.IsPasswordValid(user.PasswordSalt.AsByteArray, password, user.PasswordHash.AsByteArray);
      return result;
    }

    public Message SaveMessage(string userEmail, string messageBody)
    {
      var user = GetUserByEmail(userEmail);
      if (user == null)
      {
        var errorMessage = string.Format("failed to find user with email \"{0}\"", userEmail);
        throw new ApplicationException(errorMessage);
      }
      var message = new Message { WhoPosted = user.FullName, WhenPosted = DateTime.Now, Text = messageBody };
      Messages.Insert(message);
      return message;
    }

    public IEnumerable<Message> GetLastMessages(int messageCount)
    {
      if (messageCount <= 0)
      {
        var errorMessage = string.Format("wrong messageCount: \"{0}\"", messageCount);
        throw new ArgumentException(errorMessage);
      }
      var result = Messages
            .FindAll()
            .SetSortOrder(SortBy<Message>.Descending(m => m.WhenPosted))
            .Take(messageCount)
            .Reverse()
            .ToList();
      return result;
    }

    public void UpdateUser(User user)
    {
      if (user.Id == null || user.Id == BsonObjectId.Empty)
      {
        var errorMessage = string.Format("user id is not defined: \"{0}\"", user.Id);
        throw new ArgumentException(errorMessage);
      }
      Users.Save(user);
    }

    private MongoDatabase ConnectToDatabase()
    {
      var mongoUrl = MongoUrl.Create(_configurationHelper.MongoDbConnectionString);
      var client = new MongoClient(mongoUrl);
      var server = client.GetServer();
      var result = server.GetDatabase(mongoUrl.DatabaseName);
      return result;
    }
  }
}