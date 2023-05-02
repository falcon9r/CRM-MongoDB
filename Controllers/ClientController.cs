using CRM_MongoDB.DTOs.ClientGroup;
using CRM_MongoDB.Models;
using CRM_MongoDB.Repositories.ClientGroup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClientController(IClientRepository clientRepository , 
                                IHttpContextAccessor httpContextAccessor)
        {
            this.clientRepository = clientRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await clientRepository.GetAllActive(GetUserId()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Client details = await clientRepository.GetById(id, GetUserId());
            if (details is null)
            {
                return NotFound();
            }
            else
            {
                return Ok(details);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClientRequestDTO clientRequestDTO)
        {
            clientRequestDTO.EmployeeId = GetUserId();
            await clientRepository.Create(clientRequestDTO);
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(string id, ClientRequestUpdateDTO clientRequestDTO)
        {
            if (await clientRepository.IsValidPhone(phone: clientRequestDTO.Phone, clientId: id))
            {
                await clientRepository.Update(id, GetUserId(), clientRequestDTO);
                return Ok();
            }
            else
            {
                ModelState.AddModelError("Phone", "Phoen is invalid");
                return ValidationProblem(ModelState);
            }
        }

        private string GetUserId()
        {
            return this.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
