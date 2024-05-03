using StackExchange.Redis;
using System.Text.Json.Serialization;

namespace AcmeOrderSystem.Api.Entities
{
    public class Order
    {
        

        public Order(int id, string details, decimal? total, DateTime date, int? customerId)
        {
            Id = id;
            Details = details;
            Total = total;
            Date = date;
            CustomerId = customerId;
        }

        private Order()
        {
        }

        public int Id { get; set; }
        public string Details { get; set; }
        public decimal? Total { get; set; }
        public DateTime Date { get; set; }
        [JsonIgnore]
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


    }
}
