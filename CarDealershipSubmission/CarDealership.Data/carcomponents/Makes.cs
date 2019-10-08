using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
     public class Makes
    {
        [DisplayName("Make")]
        [Required(ErrorMessage = "Enter the name of the manufacturer")]
        public string MakeName { get; set; }
        public int MakeID { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
    }
}
