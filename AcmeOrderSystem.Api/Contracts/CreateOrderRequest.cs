using AcmeOrderSystem.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace AcmeOrderSystem.Api.Contracts
{
    public class CreateOrderRequest 
    {
        public CreateOrderRequest()
        {
        }

        public CreateOrderRequest(decimal? total, int? customerId, string details)
        {
            Details = details;
            Total = total;
            CustomerId = customerId;
        }

        public string Details { get; set; } = string.Empty;
        public decimal? Total { get; set; }
        public int? CustomerId { get; set; }
    } 
    
}
