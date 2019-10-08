using CarDealership.Data;
using CarDealership.Data.carcomponents;
using CarDealership.Data.People;
using CarDealership2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership2.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {
            
            CarRepoADO repoC = new CarRepoADO();
            SalesIndexVM model = new SalesIndexVM();
            model.Inventory = repoC.GetInventory();
            return View(model);
        }

        //[HttpGet]
        public ActionResult AddCar()
        {
            CarAddEdditVM model = new CarAddEdditVM();
            CarComponentRepoADO repo = new CarComponentRepoADO();
            model.SetSelectList(repo.GetModels(), repo.GetMakes(), repo.GetColors(), repo.GetInteriors(), repo.GetBodies(), repo.GetTrans());
            return View(model);
        }
        [HttpPost]
        public ActionResult AddCar(CarAddEdditVM model)
        {
            CarRepoADO repoc = new CarRepoADO();
            CarComponentRepoADO repo = new CarComponentRepoADO();

            if (model.UploadedFile !=null && model.UploadedFile.ContentLength > 0)
            {
                if (model.UploadedFile.ContentType == "image/jpeg" || model.UploadedFile.ContentType == "image/png")
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                        Path.GetFileName(model.UploadedFile.FileName));

                    model.UploadedFile.SaveAs(path);
                    model.AddCar.ImageFileName = model.UploadedFile.FileName;
                }
                else
                {
                    ModelState.AddModelError("UploadedFile", "The uploaded file must be of type .jpg or .png");
                    
                }
            }
            if (ModelState.IsValid)
            {
                repoc.AddCar(model.AddCar);

                return RedirectToAction("index", "home");
            }
            else
            {
                model.SetSelectList(repo.GetModels(), repo.GetMakes(), repo.GetColors(), repo.GetInteriors(), repo.GetBodies(), repo.GetTrans());
                return View("AddCar", model);
            }
        }
        [HttpGet]
        public ActionResult AddMake()
        {
            AddEdditMake ToAdd = new AddEdditMake();
            CarComponentRepoADO repo = new CarComponentRepoADO();
            ToAdd.MakeList = repo.GetMakes();
            return View(ToAdd);
        }
        [HttpPost]
        public ActionResult AddMake(AddEdditMake ToAdd)
        {
            CarComponentRepoADO repoc = new CarComponentRepoADO();
            ToAdd.MakeList = repoc.GetMakes();
            CarRepoADO repo = new CarRepoADO();
            if (ModelState.IsValid)
            {
                Makes makeAdd = new Makes();
                makeAdd = ToAdd.MakeToAdd;
                repo.AddMake(makeAdd);
                AddEdditMake forlist = new AddEdditMake();
                forlist.MakeList = repoc.GetMakes();
                return View("AddMake", forlist);
            }
            else
            {    
                return View("AddMake", ToAdd);
            }
        }
        [HttpGet]
        public ActionResult AddModel()
        {
            AddEditModel Setupadd = new AddEditModel();
            Setupadd.SetMakeChoices();
            return View(Setupadd);
            
        }

        [HttpPost]
        public ActionResult AddModel(AddEditModel ToAdd)
        {
            CarRepoADO repo = new CarRepoADO();
            AddEditModel forview = new AddEditModel();
            forview.SetMakeChoices();
            if (!string.IsNullOrWhiteSpace(ToAdd.ModelToAdd.ModelName))
            {
                CarModels AddME = new CarModels();
                AddME.ModelName = ToAdd.ModelToAdd.ModelName;
                AddME.MakeID = ToAdd.ModelToAdd.MakeID;
                repo.AddModel(AddME);
                return View("AddModel", forview);
            }
            else
            {
                ToAdd.SetMakeChoices();
                return View("AddModel", ToAdd);
            }
        }
        [HttpPost]
        public ActionResult AddModel2(AddEditModel ToAdd)
        {
            CarRepoADO repo = new CarRepoADO();
            AddEditModel forview = new AddEditModel();
            if (ModelState.IsValid)
            {
                CarModels AddME = new CarModels();
                AddME.ModelName = ToAdd.ModelName;
                AddME.MakeID = ToAdd.MakeID;
                repo.AddModel(AddME);
                forview.SetMakeChoices();
                return View("AddModel", forview);
            }
            else
            {
                ToAdd.SetMakeChoices();
                return View("AddModel", ToAdd);
            }
        }
        [HttpGet]
        public ActionResult CreatePromotion()
        {
            Promotion model = new Promotion();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreatePromotion(Promotion model)
        {

            return View();
        }
        [HttpGet]
        public ActionResult EditCar(int CarID)
        {
            CarAddEdditVM model = new CarAddEdditVM();
            CarComponentRepoADO repo = new CarComponentRepoADO();
            CarRepoADO repoc = new CarRepoADO();
            model.SetSelectList(repo.GetModels(), repo.GetMakes(), repo.GetColors(), repo.GetInteriors(), repo.GetBodies(), repo.GetTrans());
            model.AddCar = repoc.GetCarByID(CarID);
            foreach(SelectListItem make in model.MakeChoices)
            {
                if (int.Parse(make.Value) == model.AddCar.MakeID)
                {
                    make.Selected = true;
                }
            }
            foreach (SelectListItem m in model.ModelChoices)
            {
                if (int.Parse(m.Value) == model.AddCar.ModelID)
                {
                    m.Selected = true;
                }
            }
            foreach(SelectListItem color in model.ColorChoices)
            {
                if (int.Parse(color.Value) == model.AddCar.Color)
                {
                    color.Selected = true;
                }
            }
            foreach(SelectListItem i in model.InteriorChoices)
            {
                if (int.Parse(i.Value) == model.AddCar.Interior)
                {
                    i.Selected = true;
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult EditCar(CarAddEdditVM model)
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            CarRepoADO repoc = new CarRepoADO();
            if (model.UploadedFile != null && model.UploadedFile.ContentLength > 0)
            {
                if (model.UploadedFile.ContentType == "image/jpeg" || model.UploadedFile.ContentType == "image/png")
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"),
                        Path.GetFileName(model.UploadedFile.FileName));

                    model.UploadedFile.SaveAs(path);
                    model.AddCar.ImageFileName = model.UploadedFile.FileName;
                }
                else
                {
                    ModelState.AddModelError("UploadedFile", "The uploaded file must be of type .jpg or .png");
                }
            }

            if (!ModelState.IsValid)
            {
                model.SetSelectList(repo.GetModels(), repo.GetMakes(), repo.GetColors(), repo.GetInteriors(), repo.GetBodies(), repo.GetTrans());
                return View("editcar", model);
            }
           
            repoc.EditCar(model.AddCar);
            SalesIndexVM indexvm = new SalesIndexVM();
            indexvm.Inventory = repoc.GetInventory();
            return RedirectToAction("index", indexvm);
        }
        [HttpGet]
        public ActionResult EmployeeAdmin()
        {
            EmployeeRepo eRepo = new EmployeeRepo();
            List<Employee> Users = eRepo.GetAllEmployees();
            List<Role> Roles = eRepo.GetAllRoles();
            UserAdminVM model = new UserAdminVM();
            model.Users = Users;
            model.Roles = Roles;
            model.SetRoleChoice(Roles);
            return View(model);
        }
        [HttpPost]
        public ActionResult AddEmployee(UserAdminVM model)
        {
            EmployeeRepo eRepo = new EmployeeRepo();

            List<Employee> Users = eRepo.GetAllEmployees();
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            if (string.IsNullOrWhiteSpace(model.NewUser.UserName) || model.NewUser.UserName.Length<5 )
            {
                ModelState.AddModelError("UserName", "Please enter a username at least 5 characters long");

            }
            if (string.IsNullOrWhiteSpace(model.password1 ))
            {
                ModelState.AddModelError("password1", "Please enter a password");
            }
            if (string.IsNullOrWhiteSpace(model.password2))
            {
                ModelState.AddModelError("password2", "Please re-enter your password");
            }
            if (model.password1 != model.password2)
            {
                ModelState.AddModelError("password2", "Passwords must match");
            }
            foreach (Employee e in Users)
            {
                if(e.UserName==model.NewUser.UserName)
                {
                    ModelState.AddModelError("UserName", "Usernames must be unique");
                }
            }

            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = model.NewUser.UserName,
                    Email = model.NewUser.Email,
                    FirstName =model.NewUser.Firstname,
                    LastName=model.NewUser.Lastname,
                };
                userManager.Create(user, model.password1);
                userManager.AddToRole(user.Id, model.NewUser.RoleName);
            }
            UserAdminVM model2 = new UserAdminVM();

           
            List<Role> Roles = eRepo.GetAllRoles();
            model2.Users = Users;
            model2.Roles = Roles;
            model2.SetRoleChoice(Roles);

            return View("EmployeeAdmin", model2);
        }
        [HttpGet]
        public ActionResult EditUser(string UserID)
        {
            EmployeeRepo eRepo = new EmployeeRepo();
            List<Employee> Users = eRepo.GetAllEmployees();
            List<Role> Roles = eRepo.GetAllRoles();
            UserAdminVM model = new UserAdminVM();
            model.Users = Users;
            model.Roles = Roles;
            model.SetRoleChoice(Roles);
            model.NewUser = eRepo.GetEmployeeByID(UserID);
            model.OldRoleName = model.NewUser.RoleName;
            return View(model);
        } 
        [HttpPost]
        public ActionResult EditUser(UserAdminVM model)
        {
            EmployeeRepo eRepo = new EmployeeRepo();
            List<Employee> Users = eRepo.GetAllEmployees();
            List<Role> Roles = eRepo.GetAllRoles();
            model.Users = Users;
            model.Roles = Roles;
            model.SetRoleChoice(Roles);
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            if (ModelState.IsValid)
            {
                eRepo.EditEmployee(model.NewUser);
                if (model.OldRoleName != null)
                {
                    if (model.NewUser.RoleName != model.OldRoleName)
                    {
                        userManager.RemoveFromRole(model.NewUser.EmployeeID, model.OldRoleName);
                        userManager.AddToRole(model.NewUser.EmployeeID, model.NewUser.RoleName);
                    }
                }
                else
                {
                    userManager.AddToRole(model.NewUser.EmployeeID, model.NewUser.RoleName);
                }

                UserAdminVM m2 = new UserAdminVM();
                
                m2.Users = Users;
                m2.Roles = Roles;
                m2.SetRoleChoice(Roles);
                return RedirectToAction("EmployeeAdmin", m2);
            }
            else
            {
                return View("EditUser", model);
            }

        }
        [HttpGet]
        public ActionResult DisableUser(string UserID)
        {

            EmployeeRepo eRepo = new EmployeeRepo();
            List<Employee> Users = eRepo.GetAllEmployees();
            List<Role> Roles = eRepo.GetAllRoles();
            UserAdminVM model = new UserAdminVM();
            UserAdminVM m2 = new UserAdminVM();
            eRepo.DisableUser(UserID);
            m2.Users = Users;
            m2.Roles = Roles;
            m2.SetRoleChoice(Roles);
            return RedirectToAction("EmployeeAdmin", m2);
        }
        [Authorize(Roles = "admin, sales")]
        [HttpGet]
        public ActionResult ChangePassword(string UserName)
        {
            EmployeeRepo eRepo = new EmployeeRepo();
            UserAdminVM model = new UserAdminVM();
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            Employee employee = eRepo.GetEmployeeByName(UserName);
            model.NewUser = employee;
            return View(model);
        }
        [Authorize(Roles = "admin, sales")]
        [HttpPost]
        public ActionResult ChangePassword(UserAdminVM model)
        {
            EmployeeRepo eRepo = new EmployeeRepo();
            if(string.IsNullOrWhiteSpace(model.password1))
            {
                ModelState.AddModelError("password1", "Please enter your current password");
            }
            if (string.IsNullOrWhiteSpace(model.password2))
            {
                ModelState.AddModelError("password2", "Please enter a new password");

            }
            var userManager = HttpContext.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            Models.AppUser user = userManager.Find(model.NewUser.UserName, model.password1);
            
            if (user==null || user.Id.ToString()!=model.NewUser.EmployeeID)
            {
                ModelState.AddModelError("password1", "incorrect password");

            }
            if (ModelState.IsValid)
            {
                userManager.ChangePassword(model.NewUser.EmployeeID, model.password1, model.password2);
                return RedirectToAction("index", "home");
            }
            return View("ChangePassword", model);

        }
        public ActionResult Salesreports(string username)
        {
            EmployeeRepo erepo = new EmployeeRepo();
            SaleReportVM model = new SaleReportVM();
            List<SalesReport> reports = new List<SalesReport>();
            List<Employee> Users = erepo.GetAllEmployees();
            model.InventoryReport = erepo.GetInventoryReport();
            foreach (Employee u in Users.Where(u => u.RoleName.ToLower() == "sales"))
            {
                SalesReport current = erepo.GetSalesReportByName(u.UserName);
                current.Sales = erepo.GetListofSales(u.UserName);
                reports.Add(current);
                

            }
           
            model.Reports = reports;
            model.SetSalesStaff(reports);
            return View(model);
        }

        public ActionResult SpecifiedSalesReport(SaleReportVM model)
        {
            EmployeeRepo erepo = new EmployeeRepo();
            List<SalesReport> reports = new List<SalesReport>();
            SalesReport current = erepo.GetSalesReportByName(model.SelectedUser);
            SaleReportVM model2 = new SaleReportVM();
            model2.SelectedUser = model.SelectedUser;
            model2.FromDateString = model.FromDateString;
            model2.ToDateString = model.ToDateString;
            model2.Reports = new List<SalesReport>();
            List<Sales> sales = erepo.GetListofSales(model2.SelectedUser);
            if (string.IsNullOrWhiteSpace(model.ToDateString) && string.IsNullOrWhiteSpace(model.FromDateString))
            {
                current.Sales = sales;
                model2.Reports.Add(current);
            }

            if (!string.IsNullOrWhiteSpace(model2.FromDateString) && string.IsNullOrWhiteSpace(model2.ToDateString))
            {
                DateTime FromDate = new DateTime();
                bool isdatefrom = DateTime.TryParse(model2.FromDateString, out FromDate);
                List<Sales> salesfrom = new List<Sales>();
                if (isdatefrom)
                {
                   foreach(Sales s in sales)
                    {
                        if (DateTime.Compare(s.DateSold, FromDate) >= 0)
                        {
                            salesfrom.Add(s);
                        }
                    }
                    current.Sales = salesfrom;
                    model2.Reports.Add(current);
                }
                else
                {
                    current.Sales = sales;
                    model2.Reports.Add(current);
                }
            }
            if (!string.IsNullOrEmpty(model2.ToDateString)&& string.IsNullOrWhiteSpace(model2.FromDateString))
            {
                DateTime ToDate = new DateTime();
                List<Sales> salesto = new List<Sales>();
                bool isdateto = DateTime.TryParse(model2.ToDateString, out ToDate);
                if (isdateto)
                {
                    foreach (Sales s in sales)
                    {
                        if (DateTime.Compare(s.DateSold, ToDate) <= 0)
                        {
                            salesto.Add(s);
                        }
                    }
                    current.Sales = salesto;
                    model2.Reports.Add(current);
                }
                else
                {
                    current.Sales = sales;
                    model2.Reports.Add(current);
                }
            }
            if ((!string.IsNullOrEmpty(model.ToDateString) && (!string.IsNullOrWhiteSpace(model.FromDateString))))
            {
                DateTime FromDate = new DateTime();
                DateTime ToDate = new DateTime();
                bool isdatefrom = DateTime.TryParse(model.FromDateString, out FromDate);
                bool isdateto = DateTime.TryParse(model.ToDateString, out ToDate);
                List<Sales> spansales = new List<Sales>();
                if(isdateto && isdatefrom)
                {
                    foreach (Sales s in sales)
                    {
                        if(DateTime.Compare(s.DateSold, ToDate) <= 0 && DateTime.Compare(s.DateSold, FromDate) >= 0)
                        {
                            spansales.Add(s);
                        }
                    }
                    current.Sales = spansales;
                    model2.Reports.Add(current);
                }
                else
                {
                    current.Sales = sales;
                    model2.Reports.Add(current);
                }
            }
            return View(model2);
        }


    }
}