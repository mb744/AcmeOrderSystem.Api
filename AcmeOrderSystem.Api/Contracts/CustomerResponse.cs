namespace AcmeOrderSystem.Api.Contracts
{
    public class CustomerResponse
    {
        public CustomerResponse()
        {
        }

        public CustomerResponse(int id, string name, string city, ContactResponse contact, DateTime date)
        {
            Id = id;
            Name = name;
            City = city;
            Contact = contact;
            Date = date;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public DateTime Date { get; set; }
        public ContactResponse Contact { get; set; }
    }
}
