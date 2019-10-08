using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.People
{
    public class Employee
    {
        
        public string UserName { get; set; }
        public string Email { get; set; }
        public string EmployeeID { get; set; }
        [DisplayName("Role")]
        [Required(ErrorMessage ="Please select a role")]
        public string RoleName { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "You must enter a First name")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "You must enter a Last name")]
        [DisplayName("Last Name")]
        public string Lastname { get; set; }
    }
}
