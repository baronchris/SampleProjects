using CarDealership.Data;
using CarDealership.Data.carcomponents;
using CarDealership.Data.People;
using CarDealership2.Models;
using CarDealership2.Models.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IndexVM model = new IndexVM();
            CarRepoADO repoC = new CarRepoADO();
            PromotionRepoADO repoP = new PromotionRepoADO();
            model.Featured = repoC.GetFeaturedCars();
            model.CurrentPromo = repoP.GetPromotionByDate(DateTime.Now);
            return View(model);
        }
        [HttpGet]
        public ActionResult DisplayCarDetail(int ID)
        {
            CarRepoADO repo = new CarRepoADO();
            CarDetailed model = repo.GetCarByID(ID);
            return View(model);

        }
        [HttpGet ]
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginVM model = new LoginVM();
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("Login",model);
            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<Models.AppUser>>();
            var authManager = HttpContext.GetOwinContext().Authentication;
            Models.AppUser user = userManager.Find(model.UserName, model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid username or password");

                return View("Login",model);
            }
            else
            {
               
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Contact()
        {
            Contact model = new Contact();
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                CustomerRepoADO cRepo = new CustomerRepoADO();
                cRepo.AddContact(model);
                return View("ContactConfirm");
            }
            return View("Contact", model);
        }
        [HttpGet]
        public ActionResult Used()
        {
            SalesIndexVM model = new SalesIndexVM();
            CarRepoADO repo =new CarRepoADO();
            model.Inventory = repo.GetInventory().Where(c => c.IsNew == false).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult New()
        {
            SalesIndexVM model = new SalesIndexVM();
            CarRepoADO repo = new CarRepoADO();
            model.Inventory = repo.GetInventory().Where(c => c.IsNew == true).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Promotions()
        {
            PromotionsVM model = new PromotionsVM();
            PromotionRepoADO repoP = new PromotionRepoADO();
            model.Allpromotions = repoP.GetALLpromotions();
            model.ValidPromotions = repoP.GetValidPromotions();
            return View("Special", model);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]       
        public ActionResult DeletePromo(int ID)
        {
            PromotionRepoADO repoP = new PromotionRepoADO();
            repoP.DeletePromotion(ID);
            PromotionsVM model = new PromotionsVM();
            model.Allpromotions = repoP.GetALLpromotions();
            model.ValidPromotions = repoP.GetValidPromotions();
            return View("Special", model);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult AddPromo(Promotion NewPromo)
        {
            PromotionRepoADO repoP = new PromotionRepoADO();
            PromotionsVM model = new PromotionsVM();
            
             if(string.IsNullOrWhiteSpace(NewPromo.PromotionName)|| string.IsNullOrWhiteSpace(NewPromo.Description))
            {
                ModelState.AddModelError("", "Promotion name and description must contain text");
            }

            if (ModelState.IsValid)
            {
                repoP.AddPromotion(NewPromo);
                model.Allpromotions = repoP.GetALLpromotions();
                model.ValidPromotions = repoP.GetValidPromotions();
                return RedirectToAction("Promotions");
             }
            else
            {
                model.Allpromotions = repoP.GetALLpromotions();
                model.ValidPromotions = repoP.GetValidPromotions();
                model.NewPromo = NewPromo;
                 return View("Special",model);
            }

        }
    }

}