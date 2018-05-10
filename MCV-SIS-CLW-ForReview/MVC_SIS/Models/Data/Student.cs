using Exercises.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercises.Models.Data
{
    public class Student :IValidatableObject
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal GPA { get; set; }
        public Address Address { get; set; }
        public Major Major { get; set; }
        public List<Course> Courses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> Errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                Errors.Add(new ValidationResult(" Please enter the first name", new[] { "FirstName" }));
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                Errors.Add(new ValidationResult(" Please enter the last name", new[] { "LastName" }));
            }
            if(GPA<0 || GPA > 4.33M)
            {
                Errors.Add(new ValidationResult("GPAs range from 0 to 4.33", new[] { "GPA" }));
            }
            if (Major.MajorId==0||Major.Equals(null))
            {
                Errors.Add(new ValidationResult("Please select a Major", new[] { "MajorID","Major" }));
            }
            
            return Errors;
        }
    }
}