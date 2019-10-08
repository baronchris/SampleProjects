using CarDealership.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership2.Models
{
    public class IndexVM
    {
        
        public List<CarBrief> Featured  { get; set; }
        public Promotion CurrentPromo { get; set; }

       
    }
}