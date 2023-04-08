using CRM_MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRM_MongoDB.Models
{
    public class Product : BaseEntity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("dollar_price") , BsonRepresentation(BsonType.Decimal128)]
        public decimal DollarPrice { get; set; }

        [BsonElement("somoni_price"), BsonRepresentation(BsonType.Decimal128)]
        public decimal SomonPrice { get; set; }

        [BsonElement("is_active"), BsonRepresentation(BsonType.Boolean)]
        public bool IsActive {  get; set; } = true;

        [BsonElement("categories_ids")]
        [JsonIgnore]
        public List<ObjectId> CategoriesIds { get; set; } = new List<ObjectId>();

        [BsonElement("categories")]
        [BsonIgnoreIfNull]
        public virtual List<Category> Categories { get; set; }
    }
}
