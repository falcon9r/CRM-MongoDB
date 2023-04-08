using CRM_MongoDB.DTOs.RegionGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM_MongoDB.Repositories.RegionGroup
{
    public class RegionRepository : IRegionRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Region> regionCollection;

        public RegionRepository(IMongoDatabase mongoDatabase) 
        {
            database = mongoDatabase;
            regionCollection = database.GetCollection<Region>("regions");
        }

        public async Task CreateAsync(RegionRequestDTO regionRequestDTO)
        {
            Region region = new()
            {
                Name = regionRequestDTO.Name,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            await regionCollection.InsertOneAsync(region);

            return;
        }

        public async Task<IList<Region>> GetlAllActive()
        {
            return regionCollection.Find(region => region.IsActive == true).ToList();
        }
    }
}
