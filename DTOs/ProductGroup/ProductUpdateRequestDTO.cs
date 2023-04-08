using CRM_MongoDB.Validators;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.ProductGroup
{
    public class ProductUpdateRequestDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal DollarPrice { get; set; }

        [Required]
        public decimal SomonPrice { get; set; }

    }
}
