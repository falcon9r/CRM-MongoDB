using CRM_MongoDB.Entities;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace CRM_MongoDB.Models
{
    public class Employee : BaseEntity 
    {
        [BsonId, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("login") , BsonRepresentation(BsonType.String)]
        public string Login { get; set; }


        [BsonElement("password") , BsonRepresentation(BsonType.String)]
        [JsonIgnore]
        public string Password {  get; set; }
        
        [BsonElement("phone"), BsonRepresentation(BsonType.String)]
        public string Phone { get; set; }

        [BsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
