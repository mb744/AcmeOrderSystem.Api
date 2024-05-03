namespace AcmeOrderSystem.Api.Contracts
{
    public class UpdateOrderRequest
    {
        public UpdateOrderRequest()
        {
        }

        public UpdateOrderRequest(int Id, decimal? total, int? customerId, string details)
        {
            Details = details;
            Total = total;
            CustomerId = customerId;
        }

        public int Id { get; set; }
        public string Details { get; set; } = string.Empty;
        public decimal? Total { get; set; }
        public int? CustomerId { get; set; }
    }
}
