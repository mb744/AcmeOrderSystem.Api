using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface IOrderService
    {
        Task<List<OrderResponse>> GetAllOrders();
        Task<List<OrderResponse>> GetAllOrdersByCustomerId(int customerId);
        Task<OrderResponse?> GetSingleOrder(int id);
        Task<OrderResponse?> AddOrder(CreateOrderRequest order);
        Task<OrderResponse?> UpdateOrder(int id, UpdateOrderRequest order);
        Task<bool?> DeleteOrder(int id);
    }
}