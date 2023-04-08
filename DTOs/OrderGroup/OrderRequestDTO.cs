using CRM_MongoDB.Models;
using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;

namespace CRM_MongoDB.DTOs.OrderGroup
{
    public class OrderRequestDTO
    {
        [Required]
        [Exists(CollectionName = "clients" , Key = "_id")]
        public string ClientId { get; set; }

        [Required]
        [Exists(CollectionName = "products" , Key = "_id")]
        public string ProductId {  get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [Required]
        public MoneyType MoneyType { get; set; }
    }
}
