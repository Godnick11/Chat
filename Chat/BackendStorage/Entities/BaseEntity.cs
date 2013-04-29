using MongoDB.Bson;

namespace Chat.BackendStorage.Entities
{
  public class BaseEntity
  {
    public ObjectId Id { get; set; }
  }
}