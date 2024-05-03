using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface IContactService
    {
        Task<ContactResponse?> AddContact(CreateContactRequest contact);
        Task<bool?> DeleteContact(int id);
        Task<ContactResponse?> UpdateContact(int id, UpdateContactRequest contact);
        Task<List<ContactResponse>> GetAllContacts();
        Task<ContactResponse?> GetSingleContact(int id);
    }
}