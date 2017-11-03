using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DVDLibraryService.Models
{
    public class DVDListView
    {
        public int DVDID { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public int realeaseYear { get; set; }
        public string RatingName { get; set; }
        public int RatingID { get; set; }
    }
}