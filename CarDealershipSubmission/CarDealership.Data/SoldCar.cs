using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class SoldCar
    {
        public CarDetailed Car { get; set; }
        public string SoldBy{ get; set; }
        public Customer purchaser { get; set; }
    }
}
