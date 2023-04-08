    using CRM_MongoDB.Validators;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CRM_MongoDB.DTOs.ClientGroup
{
    public class ClientRequestDTO
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }

        [Required]
        [PhoneCheck]
        [IsUnique(Key = "phone" , CollectionName = "clients")]
        public string Phone { get; set; }

        [Required]
        [Exists(Key = "_id" , CollectionName = "regions")]
        public string RegionId { get; set; }

        [JsonIgnore]
        public string? EmployeeId { get ; set; }
    }
}


