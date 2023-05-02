using CRM_MongoDB.DTOs.EmployeeGroup;
using CRM_MongoDB.Models;
using CRM_MongoDB.Repositories.EmployeeGroup;
using CRM_MongoDB.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CRM_MongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly JWTService jWTService;

        public AuthController(IEmployeeRepository employeeRepository, JWTService jWTService)
        {
            this.employeeRepository = employeeRepository;
            this.jWTService = jWTService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(EmployeeLoginDTO employeeLoginDTO)
        {
            Employee employee = await employeeRepository.Login(employeeLoginDTO);
            if (employee is not null)
            {
                return Ok(new { 
                    token = jWTService.GetToken(employee)
                });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Register(EmployeeRegisterDTO employeeRegisterDTO)
        {
            await this.employeeRepository.Create(employeeRegisterDTO);
            return Ok(new { 
                Message = "Employee added"
            });
        }
    }
}
