using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;


namespace AcmeOrderSystem.Api.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ContactRepository> _logger;

        public ContactRepository(ApplicationDbContext context, ILogger<ContactRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Contact?> AddContact(Contact contact)
        {
           
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return contact;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
            
            
            
        }

        public async Task<bool?> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact is null)
                return null;
            try
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<List<Contact>> GetAllContacts()
        {
            int page = 1;
            int pageSize = 10;
            var contacts = new List<Contact>();
            try
            {
                contacts = await _context.Contacts.AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return contacts;
            }
            
            return contacts; 
        }

        public async Task<Contact?> GetSingleContact(int id)
        {
            
            try
            {
                
                var contact = await _context.Contacts
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (contact is null)
                    return null;

                return contact;
               
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }


        public async Task<Contact?> UpdateContact(Contact contact)
        {

            try
            {

                var updatedContact = new Contact(contact.Id, contact.Email, contact.Phone, contact.CustomerId, DateTime.UtcNow);

                _context.Contacts.Update(updatedContact);
                return updatedContact;
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
               
            }
        }

        public void DeleteContact(Contact contact)
        {

            try
            {
                _context.Contacts.Remove(contact);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
        }

    }
}
