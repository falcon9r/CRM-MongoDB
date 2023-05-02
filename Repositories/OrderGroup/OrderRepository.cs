using CRM_MongoDB.DTOs.OrderGroup;
using CRM_MongoDB.DTOs.ProductGroup;
using CRM_MongoDB.Filter;
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

        public async Task<Order> GetOrderByIdAsync(string empoyeeId, string id)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Id, id) & Builders<Order>.Filter.Eq(order => order.Client.EmployeeId, empoyeeId);
            return await Body(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrdersAsync(string employeeId, PaginationFilter Pfilter)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Client.EmployeeId, employeeId);
            return await Body(filter).Skip((Pfilter.PageNumber - 1) * Pfilter.PageSize).Limit(Pfilter.PageSize).ToListAsync();
        }

        private  IAggregateFluent<Order> Body(FilterDefinition<Order> filter)
        {
            return _orderCollection.Aggregate()
                .Lookup<Order, Order>("products", "product_id", "_id", "product")
                .Unwind<Order, Order>(order => order.Product)
                .Lookup<Order, Order>("clients", "client_id", "_id", "client")
                .Unwind<Order, Order>(order => order.Client)
                .Lookup<Order, Order>("regions", "client.region_id", "_id", "client.region")
                .Unwind<Order, Order>(order => order.Client.Region)
                .Lookup<Order, Order>("categories", "product.categories_ids", "_id", "product.categories").Match(filter);
        }

        public async Task Update(string id, OrderRequestUpdateDTO orderRequestUpdateDTO)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, id);
            var update = Builders<Order>.Update
                .Set(o => o.Price, orderRequestUpdateDTO.Price)
                .Set(o => o.ProductId, orderRequestUpdateDTO.ProductId)
                .Set(o => o.ClientId, orderRequestUpdateDTO.ClientId);
            _orderCollection.UpdateOne(filter, update);

        }
    }
}
