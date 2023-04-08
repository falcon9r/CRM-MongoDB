using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.EmployeeGroup
{
    public class EmployeeLoginDTO
    {
        [Exists(Key = "login", CollectionName = "employees")]
        [Required]
        public string Login { get; set; }
        
        [Required]
        public string Password {  get; set; }
    }
}
