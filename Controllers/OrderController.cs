using CRM_MongoDB.DTOs.OrderGroup;
using CRM_MongoDB.Models;
using CRM_MongoDB.Repositories.ClientGroup;
using CRM_MongoDB.Repositories.OrderGroup;
using CRM_MongoDB.Repositories.ProductGroup;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IOrderRepository orderRepository;
        private readonly IClientRepository clientRepository;
        private readonly IProductRepository productRepository;

        public OrderController(IHttpContextAccessor httpContextAccessor , IOrderRepository orderRepository , IClientRepository clientRepository , IProductRepository productRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.orderRepository = orderRepository;
            this.clientRepository = clientRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await orderRepository.GetOrdersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Order order = await orderRepository.GetOrderByIdAsync(id);
            if(order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequestDTO orderRequestDTO)
        {
            orderRepository.Create(orderRequestDTO);
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] OrderRequestUpdateDTO orderRequestUpdateDTO)
        {
            if (!await clientRepository.Exists(orderRequestUpdateDTO.ClientId, this.GetUserId()))
            {
                ModelState.AddModelError("clientId", "Client does not exist");
            }
            if (! await productRepository.Exists(orderRequestUpdateDTO.ProductId))
            {
                ModelState.AddModelError("productId", "Product does not exist");
            }
            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem(ModelState);
            }
            else
            {
                orderRepository.Update(id, orderRequestUpdateDTO);
                return Ok();
            }
        }

        private string GetUserId()
        {
            return this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
