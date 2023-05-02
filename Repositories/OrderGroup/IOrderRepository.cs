using CRM_MongoDB.DTOs.OrderGroup;
using CRM_MongoDB.Filter;
using CRM_MongoDB.Models;

namespace CRM_MongoDB.Repositories.OrderGroup
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrdersAsync(string employee_id, PaginationFilter filter);
        
        public Task<Order> GetOrderByIdAsync(string employee_id , string id);

        public Task Create(OrderRequestDTO orderRequestDTO);

        public Task Update(string id, OrderRequestUpdateDTO orderRequestUpdateDTO);
    }
}
