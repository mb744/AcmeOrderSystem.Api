using System.Text.Json.Serialization;

namespace AcmeOrderSystem.Api.Entities
{
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(string name, string? city, Contact? contact, DateTime date) 
        {
            Name = name;
            City = city;
            Contact = contact;
            Date = date;
            
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? City { get; set; }
        public Contact? Contact { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
