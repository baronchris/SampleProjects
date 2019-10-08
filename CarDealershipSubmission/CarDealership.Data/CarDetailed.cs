using CarDealership.Data.carcomponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.Data
{
    public class CarDetailed
    {
        public int CarID { get; set; }
        [DisplayName("Model")]
        [Required(ErrorMessage ="Please select a model")]
        public int ModelID { get; set; }
        [DisplayName("Make")]
        [Required(ErrorMessage = "Please select a make")]
        public int MakeID { get; set; }
        [DisplayName("Body Style")]
        [Required(ErrorMessage = "Please select a body style")]
        public int StyleID { get; set; }
        public string BodyStyle { get; set; }
        [Required(ErrorMessage = "Please enter the mileage")]
        public int Mileage { get; set; }
        [DisplayName("Color")]
        [Required(ErrorMessage = "Please select a color")]
        public int Color { get; set; }
        [DisplayName("SalePrice")]
        [Required(ErrorMessage = "enter a sale price")]
        public decimal Saleprice { get; set; }
        [DisplayName("Type")]
        [Required(ErrorMessage = "Please select a new or used")]
        public bool IsNew { get; set; }
        public bool IsSold { get; set; }
        [DisplayName("Feature This Vehicle?")]
        public bool IsFeatured { get; set; }
        [DisplayName("Picture")]
        public string ImageFileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AddedBy { get; set; }
        public string ColorName { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        [DisplayName("Transmission")]
        public int TransmissionID { get; set; }
        public string TransmissionName { get; set; }
        [Required(ErrorMessage = "Please select an interior")]
        public int Interior { get; set; }
        public string InteriorDescription { get; set; }
        [Required(ErrorMessage = "Please enter the MSRP")]
        public decimal MSRP { get; set; }
        public int ModelYear { get; set; }
        [DisplayName("Description")]
        public string CarDescription { get; set; }
        [DisplayName("VIN #")]
        public string VIN { get; set; }

        
    }
}