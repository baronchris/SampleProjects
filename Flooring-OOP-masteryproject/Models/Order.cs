using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order //add date param -use substring displayed in getallfiles 
    {
        public int OrderNumber { get; set;}
        public string CustomerName { get; set; }
        public string State { get; set; }
        public  decimal TaxRate { get; set; }
        public  string Product { get; set; }
        public decimal Area { get; set; }
        public decimal CostPerSquareFoot { get; set; } //product.Cost
        public decimal MaterialCost { get; set; } //product.Cost*area
        public decimal LaborCostPerSquareFoot { get; set; } //product.LaborCost
        public decimal LaborCost { get; set; }
        public decimal Tax { get; set; } //state.Tax applied to order
        public decimal Total { get; set; }
        public string OrderDate { get; set; }

        public Order(int ordernumber, string customername, string state, decimal tax,string product, decimal area, decimal costperunit, decimal laborcostperunit,string orderdate)
        {
            OrderNumber = ordernumber;
            CustomerName = customername;
            State = state;
            TaxRate = tax;
            Product = product;
            Area = area;
            CostPerSquareFoot = costperunit;
            MaterialCost = Area * CostPerSquareFoot;
            LaborCostPerSquareFoot = LaborCost;
            LaborCost = LaborCostPerSquareFoot * Area;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = (MaterialCost + LaborCost) + Tax;
            OrderDate = orderdate;
        }
        public Order(int ordernumber, string customername, string state, decimal taxrate, string product, decimal area, decimal costperunit, decimal laborcostperunit, decimal materialcost, decimal laborcost, decimal tax, decimal total, string orderdate )
        {
            OrderNumber = ordernumber;
            CustomerName = customername;
            State = state;
            TaxRate = taxrate;
            Product = product;
            Area = area;
            CostPerSquareFoot = costperunit;
            MaterialCost = materialcost;
            LaborCostPerSquareFoot = laborcostperunit;
            LaborCost = laborcost;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = LaborCost+MaterialCost+Tax;
            OrderDate = orderdate;
        }
        public Order(int ordernumber, string customername, State state, Product product, decimal area,string orderdate)
        {
            OrderNumber = ordernumber;
            CustomerName = customername;
            State = state.StateCode;
            TaxRate = state.TaxRate;
            Product = product.ProductName;
            Area = area;
            CostPerSquareFoot = product.CostperSquare;
            MaterialCost = CostPerSquareFoot * Area;
            LaborCostPerSquareFoot = product.LaborCost;
            LaborCost = LaborCostPerSquareFoot * Area;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = LaborCost + MaterialCost + Tax;
            OrderDate = orderdate;

        }
        public Order(int ordernumber, string customername, string state, decimal taxrate, string product, decimal area, decimal costperunit, decimal laborcostperunit, decimal materialcost, decimal laborcost, decimal tax, decimal total)
        {
            OrderNumber = ordernumber;
            CustomerName = customername;
            State = state;
            TaxRate = taxrate;
            Product = product;
            Area = area;
            CostPerSquareFoot = costperunit;
            MaterialCost = materialcost;
            LaborCostPerSquareFoot = laborcostperunit;
            LaborCost = laborcost;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = LaborCost + MaterialCost + Tax;
            
        }
        public Order()
        {

        }
        public void ComputOrderValues(Product product, State state)
        {
            CostPerSquareFoot = product.CostperSquare;
            MaterialCost = CostPerSquareFoot * Area;
            LaborCostPerSquareFoot = product.LaborCost;
            LaborCost = LaborCostPerSquareFoot * Area;
            Tax = (MaterialCost + LaborCost) * (TaxRate / 100);
            Total = LaborCost + MaterialCost + Tax;
        }
    }

}
