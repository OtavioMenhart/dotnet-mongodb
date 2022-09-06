using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb.Domain.Entities
{
    public class Book : BaseEntity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
