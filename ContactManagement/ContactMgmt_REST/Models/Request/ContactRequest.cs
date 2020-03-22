using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ContactMgmt_REST.Models.Request
{
    public class ContactRequest
    {
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}