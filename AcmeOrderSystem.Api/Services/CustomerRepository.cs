using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace AcmeOrderSystem.Api.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(ApplicationDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Customer?> AddCustomer(Customer customer)
        {
           
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            
            
            
        }

        public async Task<bool?> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer is null)
                return null;
            try
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            int page = 1;
            int pageSize = 10;
            var customers = new List<Customer>();
            try
            {
                customers = await _context.Customers.Include(x => x.Contact).AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return customers;
            }
            
            return customers; 
        }

        public async Task<Customer?> GetSingleCustomer(int id)
        {
            
            try
            {
                
                    var customer = await _context.Customers.Include(i => i.Contact)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Id == id);
                    
                if (customer is null)
                    return null;

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Customer?> GetSingleCustomer(string name)
        {

            try
            {
                var customer = await _context.Customers
                        .AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Name == name);
                
                if (customer is null)
                    return null;

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<Customer?> UpdateCustomer(int id, Customer updateCustomer)
        {
            

            customer.Name = updateCustomer.Name;
            customer.City = (updateCustomer.City) ?? customer.City;
            customer.Contact.Phone = (updateCustomer.Contact.Phone) ?? customer.Contact.Phone ;
            customer.Contact.Email = (updateCustomer.Contact.Email) ?? customer.Contact.Email;
            customer.Date = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();

                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }

            
        }
    }
}
