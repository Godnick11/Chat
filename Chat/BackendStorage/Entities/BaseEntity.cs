using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chat.BackendStorage.Entities
{
  public abstract class BaseEntity
  {
    [BsonId]
    [BsonIgnoreIfDefault]
    public BsonObjectId Id { get; set; }
  }
}