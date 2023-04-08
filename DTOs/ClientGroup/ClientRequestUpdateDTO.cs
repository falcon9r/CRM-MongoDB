using CRM_MongoDB.Validators;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using MongoDB.Bson;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CRM_MongoDB.DTOs.ClientGroup
{
    public class ClientRequestUpdateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [PhoneCheck]
        public string Phone { get; set; }

        [Required]
        [Exists(Key = "_id", CollectionName = "regions")]
        public string RegionId { get; set; }

    }
}
