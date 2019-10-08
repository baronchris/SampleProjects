using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class CarModels
    {
        [DisplayName("Model Name")]
        [Required(ErrorMessage ="Enter a model name")]
        public string ModelName { get; set; }
        public int ModelID { get; set; }
        [DisplayName("Make")]
        [Required(ErrorMessage = "Select a Make")]
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public DateTime DateAdded { get; set; }
        public string AddedBy { get; set; }
    }
}
