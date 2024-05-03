
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<Customer?> GetSingleCustomer(int id);
        Task<Customer?> GetSingleCustomer(string name);
        Task<Customer?> AddCustomer(Customer customer);
        Task<Customer?> UpdateCustomer(int id, Customer customer);
        Task<bool?> DeleteCustomer(int id);
    }
}
