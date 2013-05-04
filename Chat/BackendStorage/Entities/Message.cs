using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Chat.BackendStorage.Entities
{
  public class Message : BaseEntity
  {
    [BsonRequired]
    public string WhoPosted { set; get; }

    [BsonRequired]
    public DateTime WhenPosted { set; get; }

    [BsonRequired]
    public string Text { set; get; }
  }
}