using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Data
{
    public class CarBrief
    {
        public int CarID { get; set; }
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public int ModelYear { get; set; }
        public decimal Saleprice { get; set; }
        public string ImageFileName { get; set; }

    }
}