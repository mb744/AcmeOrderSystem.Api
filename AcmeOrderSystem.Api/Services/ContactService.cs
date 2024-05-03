using AcmeOrderSystem.Api.Contracts;
using AcmeOrderSystem.Api.Database;
using AcmeOrderSystem.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace AcmeOrderSystem.Api.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<ContactResponse?> AddContact(CreateContactRequest contact)
        {
            
            try
            {
               
                var newContact = new Contact(0,contact.Email, contact.Phone, contact.CustomerId, DateTime.UtcNow);

                var contactAdded = await _contactRepository.AddContact(newContact);

                var contactResponse = new ContactResponse(contactAdded.Id, contactAdded.CustomerId, contactAdded.Email, contactAdded.Phone, contactAdded.Date);

                return contactResponse;

            }
            catch (Exception ex)
            {
                return null;
            }


            
        }

        public async Task<ContactResponse?> UpdateContact(int id, UpdateContactRequest contact)
        {
            

            try
            {
                var gettConatct = _contactRepository.GetSingleContact(id);
                if (gettConatct == null) { return null; }


                var updateContact = new Contact(contact.Id, contact.Email, contact.Phone, contact.CustomerId, DateTime.UtcNow);

                var updatedContact = await _contactRepository.UpdateContact(updateContact);

                var contactResponse = new ContactResponse(updatedContact.Id, updatedContact.CustomerId, updatedContact.Email, updatedContact.Phone, updatedContact.Date);

                return contactResponse;

            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public async Task<bool?> DeleteContact(int id)
        {
            

            try
            {
                var gettConatct = _contactRepository.GetSingleContact(id);
                if (gettConatct == null) { return null; }


                

                var delteddContact = await _contactRepository.DeleteContact(id);
                if (delteddContact == null) { return null; }

                return true;

                

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public async Task<List<ContactResponse>> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContacts();

            var responseContacts = contacts.Select(s => new ContactResponse(s.Id, s.CustomerId, s.Email, s.Phone, s.Date)).ToList();

            return responseContacts; 
        }

        public async Task<ContactResponse?> GetSingleContact(int id)
        {
            var contact = await _contactRepository.GetSingleContact(id);
            if (contact != null) { return null; };

            var responseContact = new ContactResponse(contact.Id, contact.CustomerId, contact.Email, contact.Phone, contact.Date);
                        
            return responseContact;
        }

    }
}