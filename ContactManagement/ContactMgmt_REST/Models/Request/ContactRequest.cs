using System.ComponentModel.DataAnnotations;

namespace ContactMgmt_REST.Models.Request
{
    public class ContactRequest
    {
        [Required]
        [MaxLength(20)]
        [RegularExpression("^[a-zA-Z]+(([a-zA-Z ])?[a-zA-Z]*)*$")]
        public string FirstName { get; set; }

        [MaxLength(20)]
        [RegularExpression("^[a-zA-Z]+(([a-zA-Z ])?[a-zA-Z]*)*$")]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^[0-9]{10}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}