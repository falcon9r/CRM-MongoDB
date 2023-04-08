using CRM_MongoDB.DTOs.CategoryGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CRM_MongoDB.Repositories.CategoryGroup
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Category> categoryCollection;

        public CategoryRepository(IMongoDatabase database)
        {
            this.database = database;
            this.categoryCollection = database.GetCollection<Category>("categories");
        }

        public async Task Create(CategoryRequestDTO categoryRequestDTO)
        {
            await categoryCollection.InsertOneAsync(new Category
            {
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true,
                Name = categoryRequestDTO.Name,
            });
        }

        public async Task<IList<Category>> GetAllActive()
        {
            return await categoryCollection.Find(category => category.IsActive == true).ToListAsync();
        }

        public async Task<Category> GetById(string id)
        {
            return await categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync();
        }
    }
}
