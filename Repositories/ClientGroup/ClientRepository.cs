using CRM_MongoDB.DTOs.ClientGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM_MongoDB.Repositories.ClientGroup
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Client> clientCollection;
        private readonly IMongoCollection<Region> regionCollection;

        public ClientRepository(IMongoDatabase database)
        {
            this.database = database;
            clientCollection = database.GetCollection<Client>("clients");
            regionCollection = database.GetCollection<Region>("regions");
        }

        public async Task<IList<Client>> GetAllActive(string employeeId)
        {
            return await clientCollection.Aggregate()
                .Match(client => client.EmployeeId == employeeId)
                .Lookup<Client, Client>("regions", "region_id", "_id", "Region")
                .Unwind<Client, Client>(client => client.Region).ToListAsync();
        }


        public async Task Create(ClientRequestDTO clientRequestDTO)
        {
            clientCollection.InsertOne(new Client()
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = clientRequestDTO.Name,
                Phone = clientRequestDTO.Phone,
                EmployeeId = clientRequestDTO.EmployeeId,
                Surname= clientRequestDTO.Surname,
                RegionId = clientRequestDTO.RegionId,
                IsActive = true
            });
        }

        public async Task<Client> GetById(string id, string employeeId)
        {
            return clientCollection.Aggregate()
                .Match(client => client.Id == id && client.EmployeeId == employeeId)
                .Lookup<Client, Client>("regions", "region_id", "_id", "Region")
                .Unwind<Client, Client>(client => client.Region)
                .Lookup<Client, Client>("employees", "employee_id", "_id", "employee")
                .Unwind<Client, Client>(client => client.Employee).FirstOrDefault();
        }


        public async Task Update(string id , string employeeId , ClientRequestUpdateDTO clientRequestDTO)
        {
            var filterDefinition =  Builders<Client>.Filter.Eq(client => client.Id, id) & Builders<Client>.Filter.Eq(client => client.EmployeeId, employeeId);
            var updateDefinition = Builders<Client>.Update.Set(client => client.Name, clientRequestDTO.Name)
                .Set(client => client.Surname, clientRequestDTO.Surname)
                .Set(client => client.UpdatedAt, DateTime.Now)
                .Set(client => client.Phone, clientRequestDTO.Phone)
                .Set(client => client.RegionId, clientRequestDTO.RegionId);
            clientCollection.UpdateOne(filterDefinition, updateDefinition);
        }

        public async Task<bool> IsValidPhone(string phone, string clientId)
        {
            var filterDefinition = Builders<Client>.Filter.Ne(client => client.Id, clientId) 
                & Builders<Client>.Filter.Eq(client => client.Phone, phone);

            long count = await clientCollection.CountDocumentsAsync(filterDefinition);
            if(count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> Exists(string clientId, string employeeId)
        {
            return clientCollection.Find(client => client.Id == client.Id && client.EmployeeId == employeeId).Any();
        }
    }
}
