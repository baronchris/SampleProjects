using CarDealership.Data;
using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Models
{
    public class SellCarVM
    {
        public int SalesID { get; set; }
        public int CarID { get; set; }
        public decimal SalesPrice { get; set; }
        public int PurchaseTypeID { get; set; }
        public string PurchaseTypeName { get; set; }
        public decimal PurchasePrice { get; set; }
        public string StateID { get; set; }
        public DateTime SoldDate { get; set; }
        public string SoldBy { get; set; }
        public Customer customer { get; set; }
        public Promotion promo { get; set; }
        public SoldCar CarSold { get; set; }
        public PaymentType Paytype { get; set; }
        public List<SelectListItem> PurchaseTypes { get; set; }
        public List<SelectListItem>States { get; set; }

        public void FillSelectLists(List<PaymentType>pay, List<State>states)
        {
            List<SelectListItem> selectpay = new List<SelectListItem>();
            foreach (PaymentType p in pay)
            {
                selectpay.Add(new SelectListItem()
                {
                    Value = p.PaymentTypeID.ToString(),
                    Text = p.PaymentTypeName,
                    Selected = false
                });
            }
            PurchaseTypes = selectpay;
            List<SelectListItem> selectstates = new List<SelectListItem>();
            foreach(State s in states)
            {
                selectstates.Add(new SelectListItem()
                {
                    Value = s.StateID,
                    Text = s.StateName,
                    Selected = false
                });
            }
            States = selectstates;
        }
    }
}