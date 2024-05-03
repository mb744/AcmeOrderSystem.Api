namespace AcmeOrderSystem.Api.Contracts
{
    public class OrderResponse
    {
        public OrderResponse()
        {
        }

        public OrderResponse(int id, decimal? total, int? customerId, string details, DateTime dateTime)
        {
            Id = id;
            CustomerId = customerId;
            Details = details;
            Total = total;
            CustomerId = customerId;
            Date = dateTime;
        }

        public int Id { get; set; }
        public string Details { get; set; } = string.Empty;
        public decimal? Total { get; set; }
        public int? CustomerId { get; set; }
        public CustomerResponse CustomerResponse { get; set; }
        public DateTime Date { get; set; }
    }

    
}
