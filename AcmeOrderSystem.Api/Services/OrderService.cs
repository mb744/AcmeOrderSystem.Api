using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public OrderService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponse?> AddOrder(CreateOrderRequest order)
        {

            try
            {
                var customerExist = await _customerRepository.GetSingleCustomer(order.CustomerId.Value);
                if (customerExist is null)
                {
                    return null;
                }
                var newOrder = new Order(0, order.Details , order.Total, DateTime.UtcNow, order.CustomerId);

                var addOrder = await _orderRepository.AddOrder(newOrder);
                var addedOrder = new OrderResponse(addOrder.Id, addOrder.Total, addOrder.CustomerId, addOrder.Details, DateTime.UtcNow);
                return addedOrder;

            }
            catch (Exception ex)
            {
                return null;
            }



        }

        public async Task<bool?> DeleteOrder(int id)
        {
            var order = await _orderRepository.DeleteOrder(id);
            if (order == null) { return null; }

            return true;
        }

        public async Task<List<OrderResponse>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();

            return orders.Select(s => new OrderResponse
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                Details = s.Details,
                Total = s.Total,
                Date = s.Date

            }).ToList();
        }

        public async Task<OrderResponse?> GetSingleOrder(int id)
        {
            var order = await _orderRepository.GetSingleOrder(id);

            if (order == null) { return null; };

            return new OrderResponse(order.Id, order.Total, order.CustomerId, order.Details, order.Date);
        }

        public async Task<List<OrderResponse>> GetAllOrdersByCustomerId(int customerId)
        {
            var orders = await _orderRepository.GetAllOrdersByCustomerId(customerId);

            return orders.Select(s => new OrderResponse
            {
                Id = s.Id,
                CustomerId = s.CustomerId,
                Details = s.Details,
                Total = s.Total,
                Date = s.Date

            }).ToList();

        }

        public async Task<OrderResponse?> UpdateOrder(int id, UpdateOrderRequest updateOrder)
        {
            var customerExist = await _customerRepository.GetSingleCustomer(updateOrder.CustomerId.Value);
            if (customerExist is null)
            {
                return null;
            }

            var order = await _orderRepository.GetSingleOrder(id);
            if (order is null)
                return null;

            order.CustomerId = updateOrder.CustomerId;
            order.Details = updateOrder.Details;
            order.Total = updateOrder.Total;
            order.Date = DateTime.UtcNow;
            order.Details = updateOrder.Details;

            var updatedOrder = await _orderRepository.UpdateOrder(id, order);

            var orderResponse = new OrderResponse(updatedOrder.Id, updatedOrder.Total, updatedOrder.CustomerId, updatedOrder.Details, updatedOrder.Date);

            orderResponse.CustomerResponse = new CustomerResponse(customerExist.Id, customerExist.Name, customerExist.City,
                new ContactResponse(customerExist.Contact.Id, customerExist.Id, customerExist.Contact.Email, customerExist.Contact.Phone, customerExist.Contact.Date), 
                customerExist.Date);

            return orderResponse;

        }
    }
}