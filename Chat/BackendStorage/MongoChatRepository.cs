using Chat.BackendStorage.Entities;
using Chat.Models;
using Chat.Utils;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

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

    public User GetUserByEmail(string email)
    {
      email = email.ToLowerInvariant();
      var query = Query<User>.EQ(u => u.Email.ToLowerInvariant(), email);
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