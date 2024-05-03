namespace AcmeOrderSystem.Api.Contracts
{
    public class ContactResponse
    {
        public ContactResponse()
        {
        }

        public ContactResponse(int id, int customerId, string email, string phone, DateTime date)
        {
            Id = id;    
            CustomerId = customerId;
            Email = email;
            Phone = phone;
            Date = date;
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
    }
}
