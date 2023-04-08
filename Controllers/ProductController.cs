using CRM_MongoDB.DTOs.CategoryGroup;
using CRM_MongoDB.DTOs.ProductGroup;
using CRM_MongoDB.Models;
using CRM_MongoDB.Repositories.CategoryGroup;
using CRM_MongoDB.Repositories.ProductGroup;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository categoryRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _productRepository.GetAllActive());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Product product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(product);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductRequestDTO productRequestDTO)
        {
            await _productRepository.Create(productRequestDTO);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, ProductUpdateRequestDTO productUpdateRequestDTO)
        {

            if(! await _productRepository.IsNameValid(id , productUpdateRequestDTO.Name))
            {
                ModelState.AddModelError("Name" , "Name is invalid");    
            }

            if(ModelState.IsValid)
            {
                _productRepository.Update(id, productUpdateRequestDTO);
                return Ok();
            }

            return ValidationProblem(ModelState);
        }

        [HttpPut("{categoryId},{productId}")]
        public async Task<IActionResult> AddCategories(string categoryId, string productId)
        {
            Category category = await categoryRepository.GetById(categoryId);
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                await _productRepository.AddCategory(productId: productId, categoryId: categoryId);
                return Ok();
            }
        }
    }
}
