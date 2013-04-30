using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Chat.BackendStorage.Entities
{
  public class User : BaseEntity
  {
    [BsonRequired]
    public string Email { set; get; }

    [BsonRequired]
    public string FirstName { set; get; }

    [BsonRequired]
    public string LastName { set; get; }

    [BsonRequired]
    public BsonBinaryData PasswordSalt { set; get; }

    [BsonRequired]
    public BsonBinaryData PasswordHash { set; get; }
  }
}