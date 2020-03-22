using ContactMgmt_REST.Models.Entities;
using ContactMgmt_REST.Models.Request;
using System.Collections.Generic;

namespace ContactMgmt_REST.Repositories
{
    public interface IContactRepository
    {
        Contact GetContact(int id);
        IEnumerable<Contact> GetContacts();
        int AddContact(ContactRequest contact);
        int EditContact(int id, ContactRequest contact);
        void DeleteContact(Contact contact);
        bool IsExists(int id);
        bool IsDuplicateData(Contact contact, int id=0);
    }
}
