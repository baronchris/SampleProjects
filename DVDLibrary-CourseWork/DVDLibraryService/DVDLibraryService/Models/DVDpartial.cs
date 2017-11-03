using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DVDLibraryService.Models
{
    public partial class DVD
    {
        public string RatingName { get; set; }
        public string Note { get; set; }
        public virtual Note NotesReal { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Title))
            {
                errors.Add(
                    new ValidationResult(
                        "Please enter the Title.",
                        new[] { "Title" }));
            }
            if (string.IsNullOrWhiteSpace(Director))
            {
                errors.Add(
                    new ValidationResult(
                        "Please enter the Director.",
                        new[] { "Director" }));
            }
            if (RatingID==0)
            {
                errors.Add(
                    new ValidationResult(
                        "Please Select a rating.",
                        new[] { "RatingID" }));
            }
            return errors;
        }
    }
}