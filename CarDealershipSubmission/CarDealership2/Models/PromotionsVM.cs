using CarDealership.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership2.Models
{
    public class PromotionsVM
    {
        public List<Promotion> Allpromotions { get; set; }
        public List<Promotion> ValidPromotions { get; set; }
        public Promotion NewPromo { get; set; }

    }
}