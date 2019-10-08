namespace CarDealership2.Migrations
{
    using CarDealership2.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealership2.Models.CarDealershipDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealership2.Models.CarDealershipDbContext context)
        {
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));
            if (roleMgr.RoleExists("admin"))
            {
                return;
            }
            if (roleMgr.RoleExists("sales"))
            {
                return;
            }
            roleMgr.Create(new AppRole() { Name = "admin" });
            roleMgr.Create(new AppRole() { Name = "sales" });

            var user = new AppUser()
            {
                UserName = "admin",
                FirstName="Cut-me-own-throat",
                LastName="Dibbler",
                Email= "admin@cmotcars.com"
            };


            var user1 = new AppUser()
            {
                UserName = "JRDobbs",
                FirstName="Bob",
                LastName="Dobbs",
                Email= "JRDobbs@cmotcars.com"

            };

            var user2 = new AppUser()
            {
                UserName = "SalesBot",
                FirstName= "Malfunctioning ",
                LastName="Eddie",
                Email= "Salesbot@cmotcars.com"

            };
            userMgr.Create(user, "testing123");
            userMgr.Create(user1, "testing123");
            userMgr.Create(user2, "testing123");
            userMgr.AddToRole(user.Id, "admin");
            userMgr.AddToRole(user1.Id, "sales");
            userMgr.AddToRole(user2.Id, "sales");
        }
    }
}
