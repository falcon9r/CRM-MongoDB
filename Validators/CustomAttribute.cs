using CRM_MongoDB.Configurations.Mongo;
using MongoDB.Driver;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.Validators
{
    public class CustomAttribute : ValidationAttribute
    {
     
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }
    }
}
