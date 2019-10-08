using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Models
{
    public class UserAdminVM //:IValidatableObject
    {
       public List<Employee> Users { get; set; }
       public  List<Role> Roles { get; set; }
        public Employee NewUser { get; set; }
        [DisplayName("Please enter Password")]
    
        public string password1 { get; set; }
        [DisplayName("Please reenter your Password")]
       
        public string password2 { get; set; }
        public List<SelectListItem>RoleChoice { get; set; }
        public string OldRoleName { get; set; }
        public void SetRoleChoice(List<Role> roles)
        {
            List<SelectListItem> rolechoice = new List<SelectListItem>();
            foreach(Role r in roles)
            {
                rolechoice.Add(new SelectListItem()
                {
                    Value = r.RoleName,
                    Text = r.RoleName,
                    Selected = false
                });
            }
            RoleChoice = rolechoice;
        }
        
        
         
            
               
            
        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
        if(employeeID==null && password1 !=password2)
        {}
            throw new NotImplementedException();
        }
        */
    }
}