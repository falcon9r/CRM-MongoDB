using CRM_MongoDB.DTOs.EmployeeGroup;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CRM_MongoDB.Models;

namespace CRM_MongoDB.Services
{
    public class JWTService
    {
        private readonly IConfiguration configuration;

        public JWTService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetToken(Employee employee)
        {
            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
               new Claim(ClaimTypes.Name, employee.Login),
               new Claim(ClaimTypes.MobilePhone, employee.Phone),
            };

            var token = new JwtSecurityToken
              (
                       issuer: configuration["Jwt:Issuer"],
                       audience: configuration["Jwt:Audience"],
                       claims: claims,
                       expires: DateTime.Now.AddDays(60),
                       notBefore: DateTime.Now,
                       signingCredentials: new SigningCredentials(
                           new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                           SecurityAlgorithms.HmacSha256)
              );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
