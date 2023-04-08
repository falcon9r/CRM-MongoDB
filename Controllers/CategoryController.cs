using CRM_MongoDB.DTOs.CategoryGroup;
using CRM_MongoDB.Repositories.CategoryGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await categoryRepository.GetAllActive());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryRequestDTO categoryRequestDTO)
        {
            categoryRepository.Create(categoryRequestDTO);
            return Ok();
        }
    }
}
