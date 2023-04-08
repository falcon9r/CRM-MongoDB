using CRM_MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CRM_MongoDB.Models
{
    public enum MoneyType
    {
        Dollar,
        Somoni
    }

    public class Order : BaseEntity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("client_id") , BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; }

        [BsonElement("product_id"), BsonRepresentation(BsonType.ObjectId)]
        public string ProductId { get; set; }

        [BsonElement("price") , BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        [BsonElement("money_type") , BsonRepresentation(BsonType.Int32)]
        public MoneyType MoneyType { get; set; }

        [BsonElement("is_active"), BsonRepresentation(BsonType.Boolean)]
        public bool IsActive { get; set; }

        [BsonIgnoreIfNull, BsonElement("client")]
        public virtual Client Client { get; set; }

        [BsonIgnoreIfNull, BsonElement("product")]
        public virtual Product Product { get; set; }
    }
}
