using CRM_MongoDB.DTOs.RegionGroup;
using CRM_MongoDB.Repositories.RegionGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;

        public RegionController(IRegionRepository regionRepository)
        {
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await regionRepository.GetlAllActive());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegionRequestDTO regionDTO)
        {
            regionRepository.CreateAsync(regionDTO);
            return Ok();
        }
    }
}
