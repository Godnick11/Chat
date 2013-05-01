using Chat.BackendStorage.Entities;
using Chat.Models;
using Chat.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;

namespace Chat.BackendStorage
{
  public class MongoChatRepository : IChatRepository
  {
    private readonly IConfigurationHelper _configurationHelper;
    private readonly ISecurityManager _securityManager;
    private readonly MongoDatabase _database;

    private MongoCollection<User> Users
    {
      get
      {
        var result = _database.GetCollection<User>("users");
        return result;
      }
    }

    static MongoChatRepository()
    {
      if (!BsonClassMap.IsClassMapRegistered(typeof(User)))
        BsonClassMap.RegisterClassMap<User>();
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
        var message = string.Format("there are more that one users with the same email: \"{0}\"", email);
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