using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Product
    {
        public string ProductName { get; set; }
        public decimal CostperSquare { get; set; }
        public decimal LaborCost { get; set; }

        public Product(string productName, decimal cost, decimal laborcost)
        {
            ProductName = productName;
            CostperSquare = cost;
            LaborCost = laborcost;
        }
    }
}
