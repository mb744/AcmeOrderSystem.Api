using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AcmeOrderSystem.Api.Contracts
{
    public class CreateCustomerRequest
    {
        public CreateCustomerRequest()
        {
        }

        public CreateCustomerRequest(string name, string? city, CreateContactRequest? contact)
        {
            Name = name;
            City = city;
            Contact = contact;
        }

        public string Name { get; set; }
        public string? City { get; set; }
        public CreateContactRequest? Contact { get; set; }

    }
}
