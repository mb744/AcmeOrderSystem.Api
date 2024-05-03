using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse?> AddCustomer(CreateCustomerRequest customer);
        Task<bool?> DeleteCustomer(int id);
        Task<List<CustomerResponse>> GetAllCustomers();
        Task<CustomerResponse?> GetSingleCustomer(int id);
        Task<CustomerResponse?> UpdateCustomer(int id, UpdateCustomerRequest customer);
    }
}