using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebApi.MomgoDB.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }
    }
}
