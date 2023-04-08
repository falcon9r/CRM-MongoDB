using CRM_MongoDB.DTOs.RegionGroup;
using CRM_MongoDB.Models;

namespace CRM_MongoDB.Repositories.RegionGroup
{
    public interface IRegionRepository
    {
        public Task CreateAsync(RegionRequestDTO regionRequestDTO);

        public Task<IList<Region>> GetlAllActive();
    }
}
