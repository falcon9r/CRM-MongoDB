using CRM_MongoDB.DTOs.ProductGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;

namespace CRM_MongoDB.Repositories.ProductGroup
{
    public interface IProductRepository
    {
        public Task<IList<Product>> GetAllActive();

        public Task<Product> GetById(string id);

        public Task Create(ProductRequestDTO productRequestDTO);

        public Task Update(string id , ProductUpdateRequestDTO productUpdateRequestDTO);

        public Task<bool> IsNameValid(string id , string name);

        public Task AddCategory(string categoryId, string productId);

        public Task<bool> Exists(string id);
    }
}
