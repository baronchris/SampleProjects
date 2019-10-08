using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CarDealership.Data;
using System.Data.SqlClient;
using CarDealership.Data.carcomponents;

namespace CarDealership.Test
{
    [TestFixture]
    public class ADOtest
    {

        CarDetailed Test = new CarDetailed
        {
            ModelID = 1,
            VIN = "TestVIN1",
            StyleID = 1,
            Color = 1,
            Interior = 1,
            Mileage = 100,
            TransmissionID = 1,
            Saleprice = 60000M,
            MSRP = 65000M,
            IsNew = true,
            IsSold = false,
            IsFeatured = false,
            ModelYear = 2018,
            CarDescription = "TestCar",
            ImageFileName = "Test.png"
        };
        [SetUp]
        public void DataSetup()
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("dbreset", cn);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        [Test]
        public void CanLoadCars()
        {
            CarRepoADO repo = new CarRepoADO();
            List<CarBrief> Cars = repo.GetAllCars();
            Assert.IsNotNull(Cars);
            Assert.AreEqual(Cars[0].CarID, 1);
            Assert.AreEqual(Cars[0].ModelName, "Corvette");
            Assert.AreEqual(Cars[0].MakeName, "Chevrolet");
            Assert.AreEqual(Cars[0].ModelYear, 2017);
            Assert.AreEqual(Cars[0].Saleprice, 55000.00);
        }
        [Test]
        public void CanLoadCarbyID()
        {
            CarRepoADO repo = new CarRepoADO();
            CarDetailed C = repo.GetCarByID(1);
            Assert.IsNotNull(C);
            Assert.AreEqual(C.ModelName, "Corvette");
            Assert.AreEqual(C.StyleID, 1);
            Assert.AreEqual(C.Saleprice, 55000.00);
        }
        [Test]
        public void CanLoadFeatured()
        {
            CarRepoADO repo = new CarRepoADO();
            List<CarBrief> Cars = repo.GetFeaturedCars();
            Assert.AreEqual(11, Cars[0].CarID);
            Assert.AreEqual(6, Cars.Count());
            Assert.AreEqual(5000123.73, Cars.Max(c => c.Saleprice));
            Assert.AreEqual(22799.99M, Cars.Min(c => c.Saleprice));
        }
        [Test]
        [TestCase("fo", 3)]
        [TestCase("z", 0)]
        [TestCase("R", 10)]
        public void CanSearchbyMake(string input, int expected)
        {
            CarRepoADO repo = new CarRepoADO();
            List<CarDetailed> Cars = repo.GetCarsByQuickSearch(input);
            Assert.AreEqual(Cars.Count, expected);
        }
        [Test]
        [TestCase(1999, 2018, 12)]
        [TestCase(2018, null, 6)]
        [TestCase(1999, null, 12)]
        [TestCase(2019, null, 0)]
        [TestCase(2001, 2016, 2)]
        public void SearchbyYearTest(int start, int? finish, int expected)
        {
            CarRepoADO repo = new CarRepoADO();
            List<CarDetailed> Cars = repo.GetCarsByYear(start, finish);
            Assert.AreEqual(Cars.Count, expected);
        }
        [Test]
        public void CanAddCar()
        {
            CarRepoADO repo = new CarRepoADO();
            int initialcount = repo.GetAllCars().Count();
            int initalmaxID = repo.GetAllCars().Max(c => c.CarID);
            repo.AddCar(Test);
            int postaddcount = repo.GetAllCars().Count();
            int postaddmaxid = repo.GetAllCars().Max(c => c.CarID);
            Assert.AreEqual(postaddcount - 1, initialcount);
            Assert.AreEqual(postaddmaxid - 1, initalmaxID);
        }

        [Test]
        public void CanEditCar()
        {
            CarRepoADO repo = new CarRepoADO();
            repo.AddCar(Test);
            Test.CarDescription = "This is the edited description";
            repo.EditCar(Test);
            CarDetailed edittest = repo.GetCarByID(Test.CarID);
            Assert.AreEqual("This is the edited description", edittest.CarDescription);
        }

        [Test]
        public void Candeletecar()
        {
            CarRepoADO repo = new CarRepoADO();
            repo.AddCar(Test);
            int initialcount = repo.GetAllCars().Count();
            Assert.AreEqual(13, initialcount);
            repo.DeleteCar(13);
            int finalcount = repo.GetAllCars().Count();
            Assert.AreEqual(12, finalcount);

        }

        [Test]
        public void CanFindPromotion()
        {
            PromotionRepoADO repo = new PromotionRepoADO();
            Promotion p = repo.GetPromotionByDate(DateTime.Parse("10/31/2017"));
            Assert.AreEqual(1, p.PromotionID);
            Assert.AreEqual("Halloween Sale", p.PromotionName);
            Assert.AreEqual(10, p.PercentDiscount);
        }

        [Test]
        public void CanLoadBodies()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<BodyStyles> Styles = repo.GetBodies();
            Assert.IsNotNull(Styles);
            Assert.AreEqual(1, Styles[0].StyleID);
            Assert.AreEqual("Coupe", Styles[0].BodyStyle);
        }
        [Test]
        public void CanLoadColors()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<Colors> colors = repo.GetColors();
            Assert.IsNotNull(colors);
            Assert.AreEqual(1, colors[0].ColorID);
            Assert.AreEqual("Silver", colors[0].ColorName);
        }
        [Test]
        public void CanLoadInteriors()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<Interiors> interiors = repo.GetInteriors();
            Assert.IsNotNull(interiors);
            Assert.AreEqual(1, interiors[0].InteriorID);
            Assert.AreEqual("grey fabric", interiors[0].InteriorName);
        }
        [Test]
        public void CanLoadMakes()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<Makes> makes = repo.GetMakes();
            Assert.IsNotNull(makes);
            Assert.AreEqual(1, makes[0].MakeID);
            Assert.AreEqual("Chevrolet", makes[0].MakeName);
            Assert.AreEqual("ChrisW", makes[0].AddedBy);
            Assert.AreEqual(8, makes.Count());
        }
        [Test]
        public void CanLoadModels()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<CarModels> models = repo.GetModels();
            Assert.IsNotNull(models);
            Assert.AreEqual(1, models[0].ModelID);
            Assert.AreEqual(1, models[0].MakeID);
            Assert.AreEqual("Corvette", models[0].ModelName);
            Assert.AreEqual("dbo", models[0].AddedBy);
            Assert.AreEqual(16, models.Count());

        }
        [Test]
        public void CanLoadTrans()
        {
            CarComponentRepoADO repo = new CarComponentRepoADO();
            List<Transmission> trans = repo.GetTrans();
            Assert.IsNotNull(trans);
            Assert.AreEqual(1, trans[0].TransmissionID);
            Assert.AreEqual("Automatic", trans[0].TransmissionName);
            Assert.AreEqual(4, trans.Count());

        }
        [Test]
        public void CanCreatePromotion()
        {
            PromotionRepoADO repo =new  PromotionRepoADO();
            Promotion promo = new Promotion
            {
                PromotionName = "Test Promo",
                Description = "This is a test Promotion",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddYears(1),
                IsForNew = true,
                IsForUsed = true,
                FlatDiscount = 0,
                PercentDiscount = 10
            };
            repo.AddPromotion(promo);
            Promotion p2 = repo.GetPromotionByDate(DateTime.Parse("2/03/2018").Date);
            Assert.AreEqual(p2.Description.ToLower(), "this is a test promotion");
        }
        [Test]
        public void CanGetAllPromotions()
        {
            PromotionRepoADO repo = new PromotionRepoADO();
            Promotion promo = new Promotion
            {
                PromotionName = "Test Promo",
                Description = "This is a test Promotion",
                IsForNew = true,
                IsForUsed = true,
                FlatDiscount = 0,
                PercentDiscount = 10
            };
            List<Promotion> ps1 = repo.GetValidPromotions();
            Assert.AreEqual(1, ps1.Count);
            repo.AddPromotion(promo);
            List<Promotion> ps2 = repo.GetALLpromotions();
            Assert.AreEqual(5, ps2.Count);
            List<Promotion> ps3 = repo.GetValidPromotions();
            Assert.AreEqual(2, ps3.Count);

        }
    }
}
