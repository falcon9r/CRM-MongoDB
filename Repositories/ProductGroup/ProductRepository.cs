using CRM_MongoDB.DTOs.ProductGroup;
using CRM_MongoDB.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM_MongoDB.Repositories.ProductGroup
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<Product> productCollection;

        public ProductRepository(IMongoDatabase database)
        {
            this.database = database;
            productCollection = database.GetCollection<Product>("products");
        }

        public async Task AddCategory(string categoryId, string  productId)
        {

            var filterDefinition = Builders<Product>.Filter.Eq(p => p.Id, productId);
            var UpdateDefinition = Builders<Product>.Update.Push(p => p.CategoriesIds, ObjectId.Parse(categoryId));
                
             await productCollection.UpdateOneAsync(filterDefinition, UpdateDefinition);
        }

        public async Task Create(ProductRequestDTO productRequestDTO)
        {
            await productCollection.InsertOneAsync(new Product
            {
                DollarPrice = productRequestDTO.DollarPrice,
                IsActive = true,
                Name = productRequestDTO.Name,
                SomonPrice = productRequestDTO.SomonPrice,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            });
        }

        public async Task<bool> Exists(string id)
        {
            return productCollection.Find(x => x.Id == id).Any();
        }

        public async Task<IList<Product>> GetAllActive()
        {
            var filterDefinition = Builders<Product>.Filter.Eq(p => p.IsActive, true);

            return await productCollection.Aggregate().Match(filterDefinition)
                .Lookup<Product, Product>("categories", "categories_ids", "_id", "categories").ToListAsync();
            
        }

        public async Task<Product> GetById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            return await productCollection.Aggregate().Match(filter).Lookup<Product, Product>("categories", "categories_ids", "_id", "categories").FirstOrDefaultAsync();
        }

        public async Task<bool> IsNameValid(string id, string name)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Name, name) & Builders<Product>.Filter.Ne(p => p.Id, id);
            long count = productCollection.CountDocuments(filter);
            return count == 0;
        }

        public async Task Update(string id, ProductUpdateRequestDTO productUpdateRequestDTO)
        {
            var filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            var update = Builders<Product>.Update.Set(p => p.Name, productUpdateRequestDTO.Name)
                .Set(p => p.DollarPrice, productUpdateRequestDTO.DollarPrice)
                .Set(p => p.SomonPrice, productUpdateRequestDTO.SomonPrice);
            productCollection.UpdateOne(filter, update);
        }
    }
}
