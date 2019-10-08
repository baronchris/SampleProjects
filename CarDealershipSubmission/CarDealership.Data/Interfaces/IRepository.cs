using CarDealership.Data.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data
{
    public interface IRepository
    {
        List<CarBrief> GetAllCars();
        CarDetailed GetCarByID(int ID);
        List<CarBrief> GetFeaturedCars();
        List<CarDetailed> GetCarsByQuickSearch(string searchterm);
        List<CarDetailed> GetCarsByYear(int startyear, int? finishyear);
        void AddCar(CarDetailed ToAdd);
        void EditCar(CarDetailed ToEdit);
        void DeleteCar(int CarID);
        void AddMake(Makes ToAdd);
        void AddModel(CarModels ToAdd);
        List<CarDetailed> GetCarsbyPrice(decimal min, decimal? max);
        List<CarDetailed> GetInventory();
        void SellCar(SoldCar car, Customer customer, decimal FinalPrice, int PaymentTypeID, int PromoID, string SoldBy);


    }
}
