using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Chat.BackendStorage
{
  public class MongoChatRepository : IChatRepository
  {
    private readonly MongoServer _server;

    static MongoChatRepository()
    {
      //BsonClassMap.RegisterClassMap<>();
    }

    public MongoChatRepository(string connectionString)
    {
      var client = new MongoClient(connectionString);
      _server = client.GetServer();
    }
  }
}