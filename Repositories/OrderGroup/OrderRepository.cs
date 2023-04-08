using CRM_MongoDB.DTOs.OrderGroup;
using CRM_MongoDB.Models;
using MongoDB.Driver;

namespace CRM_MongoDB.Repositories.OrderGroup
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orderCollection;

        public OrderRepository(IMongoDatabase mongoDatabase)
        {
            _orderCollection = mongoDatabase.GetCollection<Order>("orders");
        }

        public async Task Create(OrderRequestDTO orderRequestDTO)
        {
            _orderCollection.InsertOne(new Order
            {
                IsActive = true,
                ProductId = orderRequestDTO.ProductId,
                ClientId = orderRequestDTO.ClientId,
                UpdatedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                MoneyType = orderRequestDTO.MoneyType,
                Price = orderRequestDTO.Price
            });
        }

        public async Task<Order> GetOrderByIdAsync(string id)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Id, id);
            return await Body(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            var filter = Builders<Order>.Filter.Empty;
            return await Body(filter).ToListAsync();
        }

        private  IAggregateFluent<Order> Body(FilterDefinition<Order> filter)
        {
            return _orderCollection.Aggregate()
                .Match(filter).Lookup<Order, Order>("products", "product_id", "_id", "product")
                .Unwind<Order, Order>(order => order.Product)
                .Lookup<Order, Order>("clients", "client_id", "_id", "client")
                .Unwind<Order, Order>(order => order.Client)
                .Lookup<Order, Order>("regions", "client.region_id", "_id", "client.region")
                .Unwind<Order, Order>(order => order.Client.Region)
                .Lookup<Order, Order>("categories", "product.categories_ids", "_id", "product.categories");
        }

        public Task Update(string id, OrderRequestUpdateDTO orderRequestUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
