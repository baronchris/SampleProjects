using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalLibrary.Data.Models
{
    public class Book
    {
        [Required (ErrorMessage ="Please enter a title")]
        public string Title { get; set; }
        public int BookID { get; set; }
        [Required(ErrorMessage = "Please enter an ISBN")]
        public string ISBN { get; set; }
        public int PublicationDate { get; set; }
        public string Publisher { get; set; }
        public bool IsAnthology { get; set; }  //try null aggregator ternary expression in input/output
        public bool IsAutographed { get; set; }
        public List<Author> Authors { get; set; }
        public List<Editor>Editors { get; set; }
        public List<string>Notes { get; set; }
        public List<Genre>Genres { get; set; }
        public List<string> AuthorsToAdd { get; set; }
        public List<string>EditorsToAdd { get; set; }
    }
}
