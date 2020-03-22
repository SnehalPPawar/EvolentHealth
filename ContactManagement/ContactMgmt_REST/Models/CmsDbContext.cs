using ContactMgmt_REST.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactMgmt_REST.Models
{
    public class CmsDbContext : DbContext
    {
        #region Constructor
        public CmsDbContext() : base("name=DefaultConnection")
        {

        }
        #endregion Constructor

        #region Public Properties
        public DbSet<Contact> Contacts { get; set; }
        #endregion Public Properties
    }
}