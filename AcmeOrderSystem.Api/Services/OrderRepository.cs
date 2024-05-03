using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcmeOrderSystem.Api.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Order?> GetSingleOrder(int id)
        {
            try
            {
                return await _context.Orders
                .Include(o => o.Customer)
                .SingleOrDefaultAsync(o => o.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;

            }

            
        }

        public async Task<Order?> AddOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
                
            }
        }

        public async Task<List<Order>> GetAllOrdersByCustomerId(int id)
        {
            int page = 1;
            int pageSize = 10;
            var orders = new List<Order>();
            try
            {
                orders = await _context.Orders.Include(x => x.Customer).AsNoTracking().Where(w => w.CustomerId == id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return orders;
            }

            return orders;


        }

        public async Task<List<Order>> GetAllOrders()
        {
            int page = 1;
            int pageSize = 10;
            var orders = new List<Order>();
            try
            {
                orders = await _context.Orders.Include(x => x.Customer).AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return orders;
            }

            return orders;
        }

        public async Task<Order?> UpdateOrder(int id, Order updateOrder)
        {
            try
            {
                var order = await _context.Orders.FindAsync(id);
                if (order is null)
                    return null;

                order.Id = id;
                order.CustomerId = updateOrder.CustomerId;
                order.Details = updateOrder.Details;
                order.Total = updateOrder.Total;
                order.Date = DateTime.UtcNow;

                _context.Orders.Update(order);

            
                await _context.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }


        }

        public async Task<bool?> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null)
                return null;
            try
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }


    }
}
