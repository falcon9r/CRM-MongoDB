using CRM_MongoDB.DTOs.CategoryGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;

namespace CRM_MongoDB.Repositories.CategoryGroup
{
    public interface ICategoryRepository
    {
        public Task Create(CategoryRequestDTO categoryRequestDTO);

        public Task<IList<Category>> GetAllActive();

        public Task<Category> GetById(string id);
    }
}
