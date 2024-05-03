using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<List<Order>> GetAllOrdersByCustomerId(int customerId);
        Task<Order?> GetSingleOrder(int id);
        Task<Order?> AddOrder(Order order);
        Task<Order?> UpdateOrder(int id, Order order);
        Task<bool?> DeleteOrder(int id);
    }
}
