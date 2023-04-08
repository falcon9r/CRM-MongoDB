using CRM_MongoDB.DTOs.ClientGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;

namespace CRM_MongoDB.Repositories.ClientGroup
{
    public interface IClientRepository
    {
        public Task<IList<Client>> GetAllActive(string employeeId);

        public Task Create(ClientRequestDTO clientRequestDTO);

        public Task<Client> GetById(string id, string employeeId);

        public Task<bool> IsValidPhone(string phone, string clientId);

        public Task Update(string id , string employeeId , ClientRequestUpdateDTO clientRequestDTO);

        public Task<bool> Exists(string clientId, string employeeId);
    }
}
