using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CRM_MongoDB.Validators
{
    public class PhoneCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string phoneNumber = value.ToString();

            if(phoneNumber.Length != 9) 
            {
                return new ValidationResult("Length of phone must be 9 digits");
            }

            string pattern = "^[0-9]{9}$";

            bool isMatch = Regex.IsMatch(phoneNumber, pattern);

            if (!isMatch)
            {
                return new ValidationResult("The phone must be like (928259006)");
            }

            return ValidationResult.Success;
        }
    }
}
