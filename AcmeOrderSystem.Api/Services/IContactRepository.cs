
using AcmeOrderSystem.Api.Entities;

namespace AcmeOrderSystem.Api.Services
{
    public interface IContactRepository
    {
        Task<List<Contact>> GetAllContacts();
        Task<Contact?> GetSingleContact(int id);
        Task<Contact?> AddContact(Contact prodcut);
        Task<Contact?> UpdateContact(Contact contact);
        Task<bool?> DeleteContact(int id);
    }
}
