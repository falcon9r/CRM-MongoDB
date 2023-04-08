using CRM_MongoDB.DTOs.EmployeeGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM_MongoDB.Repositories.EmployeeGroup
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Employee> employeesColellection;

        public EmployeeRepository(IMongoDatabase mongoDatabase)
        {
            this.database = mongoDatabase;
            employeesColellection = this.database.GetCollection<Employee>("employees");
        }

        public async Task<Employee> Login(EmployeeLoginDTO employeeLoginDTO)
        {
            return employeesColellection.Find(employee => (employee.Login == employeeLoginDTO.Login) && (employee.Password == employeeLoginDTO.Password)).FirstOrDefault();
        }

        public async Task Create(EmployeeRegisterDTO employeeRegisterDTO)
        {
            Employee employee = new Employee()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Login = employeeRegisterDTO.Login,
                Password = employeeRegisterDTO.Password,
                Phone = employeeRegisterDTO.Phone
            };

            employeesColellection.InsertOne(employee);
            return;
        }
    }
}
