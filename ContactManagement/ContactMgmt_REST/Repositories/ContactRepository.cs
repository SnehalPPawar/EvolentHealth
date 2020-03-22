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
            Contact obj=Mapper.Map<ContactRequest, Contact>(contact);

            _db.Contacts.Add(obj);
            _db.SaveChanges();

            return obj.ContactId;
        }

        public void DeleteContact(Contact contact)
        {
            _db.Entry(contact).State = EntityState.Deleted;
            _db.SaveChanges();
        }

        public bool EditContact(int id, ContactRequest contact)
        {
            Mapper.CreateMap<ContactRequest, Contact>();
            Contact obj = Mapper.Map<ContactRequest, Contact>(contact);
            obj.ContactId = id;

            _db.Entry(obj).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
                {
                    return false;// NotFound();
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public Contact GetContact(int id)
        {
            return _db.Contacts.FirstOrDefault(x=>x.ContactId==id);
        }

        public IEnumerable<Contact> GetContacts()
        {
            return _db.Contacts?.ToList();
        }

        public bool ContactExists(int id)
        {
            return _db.Contacts.Count(e => e.ContactId == id) > 0;
        }
        #endregion
    }
}