using CRM_MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_MongoDB.Models
{
    public class Client : BaseEntity
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        
        [BsonElement("surname"), BsonRepresentation(BsonType.String)]
        public string Surname { get; set; }

        [BsonElement("phone"), BsonRepresentation(BsonType.String)]
        public string Phone { get; set; }


        [BsonElement("employee_id") , BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }

        [BsonElement("region_id") , BsonRepresentation(BsonType.ObjectId)]
        public string RegionId { get; set; }

        [BsonElement("is_active") , BsonRepresentation(BsonType.Boolean)]
        public bool IsActive { get; set; }

        [BsonIgnoreIfNull, BsonElement("employee")]
        public virtual Employee Employee { get; set; }
        
        [BsonIgnoreIfNull, BsonElement("region")]
        public virtual Region Region { get; set; }
    }
}
