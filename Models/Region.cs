using CRM_MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CRM_MongoDB.Models
{
    public class Region : BaseEntity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("is_active") , BsonRepresentation(BsonType.Boolean) , BsonDefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
