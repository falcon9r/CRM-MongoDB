using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.CategoryGroup
{
    public class CategoryRequestDTO
    {
        [Required]
        [IsUnique(Key = "name" , CollectionName = "categories")]
        public string Name { get; set; }
    }
}
