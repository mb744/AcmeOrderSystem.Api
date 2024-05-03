using AcmeOrderSystem.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AcmeOrderSystem.Api.Contracts
{
    public class CreateContactRequest
    {
        public CreateContactRequest()
        {
        }

        public CreateContactRequest(string? email, string? phone, int customerId)
        {
            Email = email;
            Phone = phone;
            CustomerId = customerId;
        }

        public int CustomerId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
    
}
