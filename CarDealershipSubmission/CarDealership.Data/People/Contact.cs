using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.People
{
    public class Contact
    {
        public int ContactID { get; set; }
        [DisplayName("Name")]
        [Required(ErrorMessage = "Please enter your name")]
        public string ContactName { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        [DisplayName("Email")]
        [Required(ErrorMessage ="Please enter your email address")]
        public string Email { get; set; }
        [DisplayName("Your Message")]
        [Required(ErrorMessage = "Please Leave a message")]
        public string ContactMessage { get; set; }
        public DateTime ContactDate { get; set; }
        public bool Responded { get; set; }
    }
}
