using ContactMgmt_REST.Controllers;
using ContactMgmt_REST.Models;
using ContactMgmt_REST.Models.Entities;
using ContactMgmt_REST.Models.Request;
using ContactMgmt_REST.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace ContactMgmt_REST.Tests
{
    [TestClass]
    public class ContactUnitTest
    {
        #region Private members
        private Mock<IContactRepository> _mockContactRepository;
        private Mock<CmsDbContext> _mockCmsDbContext;
        private ContactController _controller;
        #endregion

        #region Initialize
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            testContext.WriteLine("Mocking/Initialising objects.");
        }

        [TestInitialize]
        public void Setup()
        {
            //Arrenge
            Initialise();
            MockRepository();
            GetController();
        }
        #endregion

        #region Protected Methods
        protected virtual void Initialise()
        {
            _mockContactRepository = new Mock<IContactRepository>();
            _mockCmsDbContext = new Mock<CmsDbContext>();
        }

        protected virtual void GetController()
        {
            _controller = new ContactController(_mockContactRepository.Object, _mockCmsDbContext.Object);
        }

        protected virtual void MockRepository()
        {
            List<Contact> contacts = new List<Contact>();
            contacts.Add(new Contact() { ContactId = 1, FirstName = "Snehal", LastName = "Pawar", Email = "Snehal@gmail.com", IsActive = true, PhoneNumber = "1234567890" });
            contacts.Add(new Contact() { ContactId = 2, FirstName = "Anvaya", LastName = "Pawar", Email = "Anvaya@gmail.com", IsActive = true, PhoneNumber = "1523455890" });

            _mockContactRepository.Setup(x => x.GetContact(It.IsAny<int>())).Returns(() => contacts[0]);
            _mockContactRepository.Setup(x => x.GetContacts()).Returns(() => contacts);
            _mockContactRepository.Setup(x => x.AddContact(It.IsAny<ContactRequest>())).Returns(() => 3);
            _mockContactRepository.Setup(x => x.EditContact(It.IsAny<int>(), It.IsAny<ContactRequest>())).Returns(() => true);
            _mockContactRepository.Setup(x => x.DeleteContact(It.IsAny<Contact>()));

        }
        #endregion

        #region Test Methods
        [TestMethod]
        [DataRow(1)]
        public void GetContact(int id)
        {
            //Act
            var response = _controller.GetContact(id);

            var contentResult = response as OkNegotiatedContentResult<Contact>;

            // Assert the result  
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.ContactId);
        }

        [TestMethod]
        public void GetContacts()
        {
            //Act
            var response = _controller.GetContacts();

            var contentResult = response as OkNegotiatedContentResult<IEnumerable<Contact>>;

            // Assert the result  
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }


        [TestMethod]
        public void AddContact()
        {
            //Act
            ContactRequest contact = new ContactRequest() { FirstName = "Ramesh", LastName = "Pawar", Email = "Ramesh@gmail.com", IsActive = true, PhoneNumber = "1234237890" };

            var response = _controller.PostContact(contact);

            var contentResult = response as CreatedAtRouteNegotiatedContentResult<ContactRequest>;
            // Assert the result
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        [DataRow(1)]
        public void EditContact(int id)
        {
            //Act
            ContactRequest contact = new ContactRequest() { FirstName = "Suresh", LastName = "Pawar", Email = "Ramesh@gmail.com", IsActive = true, PhoneNumber = "1234237890" };

            var response = _controller.PutContact(id, contact);

            var contentResult = response as StatusCodeResult;
            // Assert the result
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.StatusCode);
            Assert.AreEqual(contentResult.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        [DataRow(1)]
        public void DeleteContact(int id)
        {
            //Act
            ContactRequest contact = new ContactRequest() { FirstName = "Suresh", LastName = "Pawar", Email = "Ramesh@gmail.com", IsActive = true, PhoneNumber = "1234237890" };

            var response = _controller.DeleteContact(id);

            var contentResult = response as OkNegotiatedContentResult<Contact>;
            // Assert the result
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        #endregion
    }
}
