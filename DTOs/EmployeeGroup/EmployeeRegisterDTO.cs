using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CRM_MongoDB.DTOs.EmployeeGroup
{
    public class EmployeeRegisterDTO
    {
        [IsUnique(Key = "login" , CollectionName = "employees")]
        [Required]
        public string Login { get; set; }

        [Required]
        [PasswordPropertyText]
        [MinLength(4)]
        public string Password { get; set; }

        [Required]
        [IsUnique(Key = "phone", CollectionName = "employees")]
        [PhoneCheck]
        public string Phone { get; set; }
    }
}
