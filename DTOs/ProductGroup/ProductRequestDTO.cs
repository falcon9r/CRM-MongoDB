using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.ProductGroup
{
    public class ProductRequestDTO
    {
        [Required]
        [IsUnique(Key = "name" , CollectionName = "products")]
        public string Name { get; set; }

        [Required]
        public decimal DollarPrice { get; set; }

        [Required]
        public decimal SomonPrice { get; set; }

    }
}
