using CRM_MongoDB.DTOs.OrderGroup;
using CRM_MongoDB.Models;

namespace CRM_MongoDB.Repositories.OrderGroup
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrdersAsync();
        
        public Task<Order> GetOrderByIdAsync(string id);

        public Task Create(OrderRequestDTO orderRequestDTO);

        public Task Update(string id, OrderRequestUpdateDTO orderRequestUpdateDTO);
    }
}
