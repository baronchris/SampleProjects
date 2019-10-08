using CarDealership.Data;
using CarDealership.Data.People;
using CarDealership2.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Controllers
{
    [Authorize(Roles = "sales")]

    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            SalesIndexVM model = new SalesIndexVM();
            CarRepoADO repoC = new CarRepoADO();
            model.Inventory = repoC.GetInventory();
            

            return View("saleindex", model);
        }
        [HttpGet]
        public ActionResult SellCar(int CarID)
        {
            CarRepoADO repoc = new CarRepoADO();
            PromotionRepoADO repop = new PromotionRepoADO();
            CustomerRepoADO custrep = new CustomerRepoADO();
            SoldCar carsold = new SoldCar();
            carsold.Car = repoc.GetCarByID(CarID);
            SellCarVM model = new SellCarVM();
            model.CarSold = carsold;
            Promotion promo = repop.GetPromotionByDate(DateTime.Now); //need null promo
            model.promo = promo;
            model.SoldBy= User.Identity.GetUserName();
            List<State> states = custrep.GetStates();
            List<PaymentType> pay = custrep.GetPaymentTypes();
            model.FillSelectLists(pay, states);
            //fill in select lists
            return View(model);
        }
        [HttpPost]
        public ActionResult SellCar(SellCarVM model)
        {
            if (!ModelState.IsValid)
            {
                return View("SellCar", model);
            }
            CarRepoADO repoc = new CarRepoADO();
            model.CarSold.Car = repoc.GetCarByID(model.CarID);
            PromotionRepoADO repop = new PromotionRepoADO();
            Promotion promo = repop.GetPromotionByDate(DateTime.Now); //need null promo
            model.promo = promo;
           if (model.promo == null)
            {
                repoc.SellCarNoPromo(model.CarSold, model.customer, model.PurchasePrice, model.PurchaseTypeID, model.SoldBy);

            }
            else
            {
                repoc.SellCar(model.CarSold, model.customer, model.PurchasePrice, model.PurchaseTypeID, model.promo.PromotionID, model.SoldBy);
            }
            return RedirectToAction("index");
        }
    }
}