using CRM_MongoDB.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRM_MongoDB.DTOs.RegionGroup
{
    public class RegionRequestDTO
    {
        [Required]
        [IsUnique(Key = "name" , CollectionName = "regions")]
        public string Name { get; set; }
    }
}
