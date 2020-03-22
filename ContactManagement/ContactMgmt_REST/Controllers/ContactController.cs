using ContactMgmt_REST.Filters;
using ContactMgmt_REST.Models;
using ContactMgmt_REST.Models.Entities;
using ContactMgmt_REST.Models.Request;
using ContactMgmt_REST.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace ContactMgmt_REST.Controllers
{
    [JWTAuthenticationFilter]
    public class ContactController : ApiController
    {
        #region Injected Members
        private readonly IContactRepository _repo;
        #endregion

        #region Constructor
        public ContactController(IContactRepository repo, CmsDbContext db)
        {
            _repo = repo;
        }
        #endregion

        #region Actions
        // GET: api/Contact
        public IHttpActionResult GetContacts()
        {
            IEnumerable<Contact> contacts = _repo.GetContacts();
            if (contacts == null || contacts.Count() == 0)
            {
                return NotFound();
            }

            return Ok(contacts);
        }

        // GET: api/Contact/5
        public IHttpActionResult GetContact(int id)
        {
            Contact contact = _repo.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // PUT: api/Contact/5
        public IHttpActionResult PutContact(int id, [FromBody]ContactRequest contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = _repo.EditContact(id, contact);

            if (!result)
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Contact
        public IHttpActionResult PostContact([FromBody]ContactRequest contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int contactId = _repo.AddContact(contact);

            if (contactId == 0)
            {
                return BadRequest("Recored not created");
            }

            return CreatedAtRoute("DefaultApi", new { id = contactId }, contact);
        }

        // DELETE: api/Contact/5
        public IHttpActionResult DeleteContact(int id)
        {
            Contact contact = _repo.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            _repo.DeleteContact(contact);
            return Ok(contact);
        }
        #endregion
    }
}