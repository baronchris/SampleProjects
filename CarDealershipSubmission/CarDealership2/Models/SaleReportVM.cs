using CarDealership.Data;
using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Models
{
    public class SaleReportVM
    {
      public   List<SalesReport> Reports;
      public List<SelectListItem> SalesStaff; 
        [DisplayName("User")]
      public string SelectedUser { get; set; }
        [DisplayName("From Date")]
        public string FromDateString { get; set; }
        [DisplayName("To Date")]
        public string ToDateString { get; set; }
        public List<InventoryReportItem> InventoryReport { get; set; }

        public void SetSalesStaff(List<SalesReport> sales)
        {
            List<SelectListItem> staff = new List<SelectListItem>();
            foreach(SalesReport r in sales)
            {
                staff.Add(new SelectListItem()
                {
                    Value=r.UserName,
                    Text=r.UserName,
                    Selected=false
                });
            }
            SalesStaff = staff;
        }
    }
}