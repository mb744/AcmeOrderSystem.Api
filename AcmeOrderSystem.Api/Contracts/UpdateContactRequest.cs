using AcmeOrderSystem.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AcmeOrderSystem.Api.Contracts
{
    public class UpdateContactRequest
    {
        public UpdateContactRequest(int id, int customerId, string email, string phone)
        {
            Id = id;
            CustomerId = customerId;
            Email = email;
            Phone = phone;
        }

        public int Id { get; set; } 
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

}
