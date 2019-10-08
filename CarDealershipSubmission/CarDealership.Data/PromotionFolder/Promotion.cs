using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class Promotion
    {
        public int PromotionID { get; set; }
        [DisplayName("Promotion Name")]
        [Required(ErrorMessage ="Please enter a name")]
        public string PromotionName { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsForUsed { get; set; }
        public bool IsForNew { get; set; }
        public decimal FlatDiscount { get; set; }
        public int PercentDiscount { get; set; }
        public bool IsCanceled { get; set; }
    }
}
