using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CRM_MongoDB.Entities
{
    public class BaseEntity
    {
        [BsonElement("created_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("updated_at"), BsonRepresentation(BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }
    }
}
