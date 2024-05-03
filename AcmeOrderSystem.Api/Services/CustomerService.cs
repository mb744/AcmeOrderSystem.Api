using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace AcmeOrderSystem.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IContactRepository _contactRepository;
        public CustomerService(ICustomerRepository customerRepository, IContactRepository contactRepository)
        {
            _customerRepository = customerRepository;
            _contactRepository = contactRepository;
        }

        public async Task<CustomerResponse?> AddCustomer(CreateCustomerRequest customer)
        {
            var existingCustomer = await _customerRepository.GetSingleCustomer(customer.Name);

            if (existingCustomer != null) 
            { 
                return null; 
            }

            try
            {
                var newContact = new Contact { Email = customer?.Contact?.Email, Phone = customer?.Contact?.Phone };
                var newCustomer = new Customer(customer.Name, customer.City, newContact, DateTime.UtcNow);

                var contactResponse = new ContactResponse();
                var customerAdded = await _customerRepository.AddCustomer(newCustomer);
                if (customerAdded?.Contact is null)
                {
                    contactResponse = null;
                }
                else
                {
                    var contact = await _contactRepository.GetSingleContact(customerAdded.Contact.Id);

                    if (contact is null)
                    {
                        contactResponse = null;
                    }
                    else
                    {
                        contactResponse.Id = contact.Id;
                        contactResponse.CustomerId = contact.CustomerId;
                        contactResponse.Email = contact.Email;
                        contactResponse.Phone = contact.Phone;
                        contactResponse.Date = contact.Date;
                    }
                }
               
                var customerResponse = new CustomerResponse(customerAdded.Id, customerAdded.Name, customerAdded.City, contactResponse, customerAdded.Date);

                return customerResponse;

            }
            catch (Exception ex)
            {
                return null;
            }


            
        }

        public async Task<bool?> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.DeleteCustomer(id);
            if (customer == null)
                return null;

            return true;
        }

        public async Task<List<CustomerResponse>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllCustomers();

            return customers.Select(s => new CustomerResponse()
            {
                Id = s.Id,
                Name = s.Name,
                City = s.City,
                Contact = new ContactResponse
                {
                    Id = s.Contact.Id,
                    CustomerId = s.Contact.CustomerId,
                    Email = s.Contact.Email,
                    Phone = s.Contact.Phone,
                    Date = s.Contact.Date,
                }
            }).ToList();
        }

        public async Task<CustomerResponse?> GetSingleCustomer(int id)
        {
            var contactResponse = new ContactResponse();
            var customer = await _customerRepository.GetSingleCustomer(id);

            if (customer?.Contact is null)
            {
                contactResponse = null;
            }
            else
            {
                var contact = await _contactRepository.GetSingleContact(customer.Contact.Id);

                if (contact is null)
                {
                    contactResponse = null;
                }
                else
                {
                    contactResponse.Id = contact.Id;
                    contactResponse.CustomerId = contact.CustomerId;
                    contactResponse.Email = contact.Email;
                    contactResponse.Phone = contact.Phone;
                    contactResponse.Date = contact.Date;
                }
            }

            var customerResponse = new CustomerResponse(customer.Id, customer.Name, customer.City, contactResponse, customer.Date);

            return customerResponse;
        }

        public async Task<CustomerResponse?> UpdateCustomer(int id, UpdateCustomerRequest updateCustomer)
        {
            var customer = await _customerRepository.GetSingleCustomer(id);
            if (customer is null)
                return null;

            customer.Name = updateCustomer.Name;
            customer.City = (updateCustomer.City) ?? customer.City;
            customer.Contact.Phone = (updateCustomer.Contact.Phone) ?? customer.Contact.Phone;
            customer.Contact.Email = (updateCustomer.Contact.Email) ?? customer.Contact.Email;
            customer.Date = DateTime.UtcNow;

            var updatedCustomer = await _customerRepository.UpdateCustomer(id, customer);

            return new CustomerResponse(updateCustomer.Id, updateCustomer.Name, updateCustomer.City, new ContactResponse(updateCustomer.Contact.Id, updateCustomer.Contact.CustomerId
                , updateCustomer.Contact.Email, updateCustomer.Contact.Phone, customer.Date), customer.Date);  


        }
    }
}