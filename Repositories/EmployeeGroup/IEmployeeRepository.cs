using CRM_MongoDB.DTOs.EmployeeGroup;
using CRM_MongoDB.Models;

namespace CRM_MongoDB.Repositories.EmployeeGroup
{
    public interface IEmployeeRepository
    {
        public Task<Employee> Login(EmployeeLoginDTO employeeLoginDTO);

        public Task Create(EmployeeRegisterDTO employeeRegisterDTO);
    }
}
