using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDb.Domain.Entities
{
    public class BaseEntity
    {
        [BsonElement("_id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
