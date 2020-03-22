using AutoMapper;
using ContactMgmt_REST.Models;
using ContactMgmt_REST.Models.Entities;
using ContactMgmt_REST.Models.Request;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ContactMgmt_REST.Repositories
{
    public class ContactRepository : IContactRepository
    {
        #region Injected Members
        private readonly CmsDbContext _db;
        #endregion

        #region Constructor
        public ContactRepository(CmsDbContext db)
        {
            _db = db;
        }
        #endregion

        #region Public Methods
        public int AddContact(ContactRequest contact)
        {
            Mapper.CreateMap<ContactRequest, Contact>();
            Contact obj = Mapper.Map<ContactRequest, Contact>(contact);
            if (IsDuplicateData(obj))
            {
                return -1;
            }
            _db.Contacts.Add(obj);
            _db.SaveChanges();

            return obj.ContactId;
        }

        public void DeleteContact(Contact contact)
        {
            _db.Entry(contact).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public int EditContact(int id, ContactRequest contact)
        {
            Mapper.CreateMap<ContactRequest, Contact>();
            Contact obj = Mapper.Map<ContactRequest, Contact>(contact);
            obj.ContactId = id;
            if (IsDuplicateData(obj,id))
            {
                return -1;
            }
            _db.Entry(obj).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IsExists(id))
                {
                    return 0;// NotFound();
                }
                else
                {
                    throw;
                }
            }
            return 1;
        }

        public Contact GetContact(int id)
        {
            return _db.Contacts.FirstOrDefault(x => x.ContactId == id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _db.Contacts?.ToList();
        }

        public bool IsExists(int id)
        {
            return _db.Contacts.Count(e => e.ContactId == id) > 0;
        }

        public bool IsDuplicateData(Contact contact, int id = 0)
        {
            return _db.Contacts.Any(x =>
                                    x.ContactId != id &&
                                    string.Equals(x.FirstName + x.LastName, contact.FirstName + contact.LastName) ||
                                    string.Equals(x.Email, contact.Email) ||
                                    string.Equals(x.PhoneNumber, contact.PhoneNumber)
                                   );
        }
        #endregion
    }
}