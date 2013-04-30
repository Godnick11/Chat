using MongoDB.Bson;

namespace Chat.BackendStorage.Entities
{
  public abstract class BaseEntity
  {
    public ObjectId Id { get; set; }
  }
}