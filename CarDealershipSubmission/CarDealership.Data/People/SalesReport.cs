using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.People
{
    public class SalesReport
    {
        public string UserName { get; set; }
        public int TotalCarsSold { get; set; }
        public decimal NetSales { get; set; }
        public List<Sales> Sales { get; set; }
    }
}
