using System.Text.Json.Serialization;

namespace AcmeOrderSystem.Api.Entities
{
    public class Contact
    {
        public Contact()
        {
        }

        public Contact(int id, string email, string phone, int customerId, DateTime dateTime)
        {
            Id = id;    
            Email = email;
            Phone = phone;
            CustomerId = customerId;
            Date = dateTime;
        }

        [JsonIgnore]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
