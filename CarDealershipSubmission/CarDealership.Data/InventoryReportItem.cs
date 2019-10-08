using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public class InventoryReportItem
    {
        public string ModelName { get; set; }
        public string MakeName { get; set; }
        public int ModelYear { get; set; }
        public int InStock { get; set; }
    }
}
