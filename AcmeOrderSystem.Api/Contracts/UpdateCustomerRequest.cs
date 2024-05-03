using System.ComponentModel.DataAnnotations;

namespace AcmeOrderSystem.Api.Contracts
{
    public class UpdateCustomerRequest
    {
        public UpdateCustomerRequest(int id, string name, string city, UpdateContactRequest contact)
        {
            Id = id;
            Name = name;
            City = city;
            Contact = contact;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public UpdateContactRequest Contact { get; set; }
    }
    
}
