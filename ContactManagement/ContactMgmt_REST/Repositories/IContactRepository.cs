using ContactMgmt_REST.Models.Entities;
using ContactMgmt_REST.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactMgmt_REST.Repositories
{
    public interface IContactRepository
    {
        Contact GetContact(int id);
        IEnumerable<Contact> GetContacts();
        int AddContact(ContactRequest contact);
        bool EditContact(int id, ContactRequest contact);
        void DeleteContact(Contact contact);
        bool ContactExists(int id);
    }
}
