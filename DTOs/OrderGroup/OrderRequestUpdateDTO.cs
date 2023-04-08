using CRM_MongoDB.Models;
using CRM_MongoDB.Validators;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.OrderGroup
{
    public class OrderRequestUpdateDTO
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ProductId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public MoneyType MoneyType { get; set; }

    }
}
