
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CarDealership.Data.People;

namespace CarDealership.Data
{
    public class CarRepoADO:IRepository
    {
        public void AddCar(CarDetailed ToAdd)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@CarID", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@ModelID", ToAdd.ModelID);
                cmd.Parameters.AddWithValue("@StyleID", ToAdd.StyleID);
                cmd.Parameters.AddWithValue("@Mileage", ToAdd.Mileage);
                cmd.Parameters.AddWithValue("@Salesprice", ToAdd.Saleprice);
                cmd.Parameters.AddWithValue("@Isnew", ToAdd.IsNew);
                cmd.Parameters.AddWithValue("@IsSold", ToAdd.IsSold);
                cmd.Parameters.AddWithValue("@IsFeatured", ToAdd.IsFeatured);
               
                cmd.Parameters.AddWithValue("@ModelYear", ToAdd.ModelYear);
                cmd.Parameters.AddWithValue("@ColorID", ToAdd.Color);
                cmd.Parameters.AddWithValue("@transmissionID", ToAdd.TransmissionID);
                cmd.Parameters.AddWithValue("@interiorID", ToAdd.Interior);
                cmd.Parameters.AddWithValue("@VIN", ToAdd.VIN);
                cmd.Parameters.AddWithValue("@MSRP", ToAdd.MSRP);
                if (string.IsNullOrWhiteSpace(ToAdd.CarDescription))
                {
                    ToAdd.CarDescription = null;
                }
                cmd.Parameters.AddWithValue("@Description", ToAdd.CarDescription);
                if (string.IsNullOrWhiteSpace(ToAdd.ImageFileName))
                {
                    ToAdd.ImageFileName = "Placeholder.jpg";
                }
                cmd.Parameters.AddWithValue("@ImageFileName", ToAdd.ImageFileName);
                cn.Open();
                cmd.ExecuteNonQuery();
                ToAdd.CarID = (int)Param.Value;
            }
        }

        public void AddMake(Makes ToAdd)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddMake", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MakeName", ToAdd.MakeName);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddModel(CarModels ToAdd)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddModel", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ModelName", ToAdd.ModelName);
                cmd.Parameters.AddWithValue("@MakeID", ToAdd.MakeID);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteCar(int CarID)
        {
            using(SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("CarID", CarID);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditCar(CarDetailed ToEdit)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("EditCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarID", ToEdit.CarID);
                cmd.Parameters.AddWithValue("@ModelID", ToEdit.ModelID);
                cmd.Parameters.AddWithValue("@StyleID", ToEdit.StyleID);
                cmd.Parameters.AddWithValue("@Mileage", ToEdit.Mileage);
                cmd.Parameters.AddWithValue("@Salesprice", ToEdit.Saleprice);
                cmd.Parameters.AddWithValue("@Isnew", ToEdit.IsNew);
              
                cmd.Parameters.AddWithValue("@isFeatured", ToEdit.IsFeatured);
                
                cmd.Parameters.AddWithValue("@ModelYear", ToEdit.ModelYear);
                cmd.Parameters.AddWithValue("@ColorID", ToEdit.Color);
                cmd.Parameters.AddWithValue("@transmissionID", ToEdit.TransmissionID);
                cmd.Parameters.AddWithValue("@interiorID", ToEdit.Interior);
                cmd.Parameters.AddWithValue("@VIN", ToEdit.VIN);
                cmd.Parameters.AddWithValue("@MSRP", ToEdit.MSRP);
                if (string.IsNullOrWhiteSpace(ToEdit.CarDescription))
                {
                    ToEdit.CarDescription = "  ";
                }
                cmd.Parameters.AddWithValue("@Description", ToEdit.CarDescription);
                if (string.IsNullOrWhiteSpace(ToEdit.ImageFileName))
                {
                    ToEdit.ImageFileName = "Placeholder.JPG";
                }
                cmd.Parameters.AddWithValue("@ImageFileName", ToEdit.ImageFileName);
                
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<CarBrief> GetAllCars()
        {
            List<CarBrief> Cars = new List<CarBrief>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetCarBrief", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarBrief current = new CarBrief();
                        current.ModelName = dr["ModelName"].ToString();
                        current.CarID = (int)dr["CarID"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        Cars.Add(current);
                    }
                }
            }
            return Cars;
        }
        public CarDetailed GetCarByID(int ID)
        {
            CarDetailed current = new CarDetailed();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                
                SqlCommand cmd = new SqlCommand("GetCarByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CarID", ID);
                cn.Open();
                
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.ModelID = (int)dr["ModelID"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.MakeID = (int)dr["MakeID"];
                        current.IsNew = (bool)dr["Isnew"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.TransmissionID = (int)dr["TransmissionID"];
                        current.TransmissionName = dr["TransmissionName"].ToString();
                        current.Color = (int)dr["color"];
                        current.ColorName = dr["Colorname"].ToString();
                        current.Interior = (int)dr["Interior"];
                        current.InteriorDescription = dr["InteriorDescription"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.VIN = dr["VIN"].ToString();
                        current.MSRP= (decimal)dr["MSRP"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.CarDescription = dr["CarDescription"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        current.AddedBy = dr["Addedby"].ToString();
                        string cdate= dr["CreatedDate"].ToString();
                        current.CreatedDate = DateTime.Parse(cdate);
                        current.IsSold = (bool)dr["IsSold"];
                        current.IsFeatured = (bool)dr["IsFeatured"];
                        current.StyleID = (int)dr["StyleID"];
                        current.BodyStyle = dr["StyleName"].ToString();
                    }
                }
            }
            return current;
        }

        public List<CarDetailed> GetCarsbyPrice(decimal min, decimal? max)
        {
            CarRepoADO repo = new CarRepoADO();
            if (max==null || max == 0)
            {
                max = repo.GetAllCars().Max(c => c.Saleprice) + 1000;
            }
            List<CarDetailed> Cars = new List<CarDetailed>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SearchByPrices", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@min", min);
                cmd.Parameters.AddWithValue("@max", max);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailed current = new CarDetailed();
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.ModelID = (int)dr["ModelID"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.MakeID = (int)dr["MakeID"];
                        current.IsNew = (bool)dr["Isnew"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.TransmissionID = (int)dr["TransmissionID"];
                        current.TransmissionName = dr["TransmissionName"].ToString();
                        current.Color = (int)dr["color"];
                        current.ColorName = dr["Colorname"].ToString();
                        current.Interior = (int)dr["Interior"];
                        current.InteriorDescription = dr["InteriorDescription"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.VIN = dr["VIN"].ToString();
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.CarDescription = dr["CarDescription"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        current.AddedBy = dr["Addedby"].ToString();
                        string cdate = dr["CreatedDate"].ToString();
                        current.CreatedDate = DateTime.Parse(cdate);
                        current.IsSold = (bool)dr["IsSold"];
                        current.IsFeatured = (bool)dr["IsFeatured"];
                        current.StyleID = (int)dr["StyleID"];
                        current.BodyStyle = dr["StyleName"].ToString();
                        Cars.Add(current);
                    }
                }
            }
            return Cars;
        }

        public List<CarDetailed> GetCarsByQuickSearch(string searchterm)//not by year will need split in controller
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetCarsByQuickSearch", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@searchstring", searchterm);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailed current = new CarDetailed();
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.ModelID = (int)dr["ModelID"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.MakeID = (int)dr["MakeID"];
                        current.IsNew = (bool)dr["Isnew"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.TransmissionID = (int)dr["TransmissionID"];
                        current.TransmissionName = dr["TransmissionName"].ToString();
                        current.Color = (int)dr["color"];
                        current.ColorName = dr["Colorname"].ToString();
                        current.Interior = (int)dr["Interior"];
                        current.InteriorDescription = dr["InteriorDescription"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.VIN = dr["VIN"].ToString();
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.CarDescription = dr["CarDescription"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        current.AddedBy = dr["Addedby"].ToString();
                        string cdate = dr["CreatedDate"].ToString();
                        current.CreatedDate = DateTime.Parse(cdate);
                        current.IsSold = (bool)dr["IsSold"];
                        current.IsFeatured = (bool)dr["IsFeatured"];
                        current.StyleID = (int)dr["StyleID"];
                        current.BodyStyle = dr["StyleName"].ToString();
                        Cars.Add(current);

                    }
                }
               
            }
            return Cars;
        }

        public List<CarDetailed> GetCarsByYear(int startyear, int? finishyear)
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetByYearRange", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Minyear", startyear);
                if (finishyear != null)
                {
                    cmd.Parameters.AddWithValue("@Maxyear", finishyear);
                }
                if (finishyear == null)
                {
                    int currentyear = int.Parse(DateTime.Now.Year.ToString());
                    cmd.Parameters.AddWithValue("@Maxyear", (currentyear+5));
                }
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailed current = new CarDetailed();
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.ModelID = (int)dr["ModelID"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.MakeID = (int)dr["MakeID"];
                        current.IsNew = (bool)dr["Isnew"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.TransmissionID = (int)dr["TransmissionID"];
                        current.TransmissionName = dr["TransmissionName"].ToString();
                        current.Color = (int)dr["color"];
                        current.ColorName = dr["Colorname"].ToString();
                        current.Interior = (int)dr["Interior"];
                        current.InteriorDescription = dr["InteriorDescription"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.VIN = dr["VIN"].ToString();
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.CarDescription = dr["CarDescription"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        current.AddedBy = dr["Addedby"].ToString();
                        string cdate = dr["CreatedDate"].ToString();
                        current.CreatedDate = DateTime.Parse(cdate);
                        current.IsSold = (bool)dr["IsSold"];
                        current.IsFeatured = (bool)dr["IsFeatured"];
                        current.StyleID = (int)dr["StyleID"];
                        current.BodyStyle = dr["StyleName"].ToString();
                        Cars.Add(current);
                    }
                }
            }
            return Cars;
        }

        public List<CarBrief> GetFeaturedCars()
        {
            List<CarBrief> Cars = new List<CarBrief>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetFeaturedCars", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarBrief current = new CarBrief();
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.MakeName = dr["MakeName"].ToString();
                        current.ModelYear = (int)dr["ModelYear"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        Cars.Add(current);
                    }
                }

            }
            return Cars;
        }

        public List<CarDetailed> GetInventory()
        {
            List<CarDetailed> Cars = new List<CarDetailed>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetInventory", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarDetailed current = new CarDetailed();
                        current.CarID = (int)dr["CarID"];
                        current.ModelName = dr["ModelName"].ToString();
                        current.ModelID = (int)dr["ModelID"];
                        current.MakeName = dr["MakeName"].ToString();
                        current.MakeID = (int)dr["MakeID"];
                        current.IsNew = (bool)dr["Isnew"];
                        current.ModelYear = (int)dr["ModelYear"];
                        current.TransmissionID = (int)dr["TransmissionID"];
                        current.TransmissionName = dr["TransmissionName"].ToString();
                        current.Color = (int)dr["color"];
                        current.ColorName = dr["Colorname"].ToString();
                        current.Interior = (int)dr["Interior"];
                        current.InteriorDescription = dr["InteriorDescription"].ToString();
                        current.Mileage = (int)dr["Mileage"];
                        current.VIN = dr["VIN"].ToString();
                        current.MSRP = (decimal)dr["MSRP"];
                        current.Saleprice = (decimal)dr["Saleprice"];
                        current.CarDescription = dr["CarDescription"].ToString();
                        current.ImageFileName = dr["ImageFileName"].ToString();
                        current.AddedBy = dr["Addedby"].ToString();
                        string cdate = dr["CreatedDate"].ToString();
                        current.CreatedDate = DateTime.Parse(cdate);
                        current.IsSold = (bool)dr["IsSold"];
                        current.IsFeatured = (bool)dr["IsFeatured"];
                        current.StyleID = (int)dr["StyleID"];
                        current.BodyStyle = dr["StyleName"].ToString();
                        Cars.Add(current);
                    }
                }
            }
            return Cars;
        }

        public void SellCar(SoldCar car, Customer customer, decimal FinalPrice, int PaymentTypeID, int PromoID, string SoldBy)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddCustomer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@CustomerID", SqlDbType.Int);//c
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Streetaddress", customer.StreetAddress);
                if (string.IsNullOrWhiteSpace(customer.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", customer.StreetAddress2);
                }
                cmd.Parameters.AddWithValue("@City", customer.City); 
                cmd.Parameters.AddWithValue("@StateID", customer.StateID); 
                cmd.Parameters.AddWithValue("@Zipcode", customer.Zipcode);
                cn.Open();
                cmd.ExecuteNonQuery();
                int x = (int)Param.Value;
                SqlCommand cmd1 = new SqlCommand("SellaCar", cn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@CarID", car.Car.CarID);
                cmd1.Parameters.AddWithValue("@CustomerID", x);
                cmd1.Parameters.AddWithValue("@SalesPrice", FinalPrice); 
                cmd1.Parameters.AddWithValue("@PurchaseTypeID", PaymentTypeID);
                cmd1.Parameters.AddWithValue("@PromotonID", PromoID);
                cmd1.Parameters.AddWithValue("@Soldby", SoldBy);
                cmd1.ExecuteNonQuery();
            }
        }
        public void SellCarNoPromo(SoldCar car, Customer customer, decimal FinalPrice, int PaymentTypeID, string SoldBy)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddCustomer", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@CustomerID", SqlDbType.Int);//c
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Streetaddress", customer.StreetAddress);
                if (string.IsNullOrWhiteSpace(customer.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", customer.StreetAddress2);
                }
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@StateID", customer.StateID);
                cmd.Parameters.AddWithValue("@Zipcode", customer.Zipcode);
                cn.Open();
                cmd.ExecuteNonQuery();
                int x = (int)Param.Value;
                SqlCommand cmd1 = new SqlCommand("SellaCar", cn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@CarID", car.Car.CarID);
                cmd1.Parameters.AddWithValue("@CustomerID", x);
                cmd1.Parameters.AddWithValue("@SalesPrice", FinalPrice);
                cmd1.Parameters.AddWithValue("@PurchaseTypeID", PaymentTypeID);
                cmd1.Parameters.AddWithValue("@PromotonID", DBNull.Value);
                cmd1.Parameters.AddWithValue("@Soldby", SoldBy);
                cmd1.ExecuteNonQuery();
            }
        }


        /*
        public void SellCar(SoldCar car, Customer customer, decimal FinalPrice, int PaymentTypeID, int PromoID, string SoldBy)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SellCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@SalesID", SqlDbType.Int);//c
                Param.Direction = ParameterDirection.Output;
                SqlParameter Param2 = new SqlParameter("@CustomerID", SqlDbType.Int);  //c
                Param2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.Add(Param2);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName); //c
                cmd.Parameters.AddWithValue("@Phone", customer.Phone); //c
                cmd.Parameters.AddWithValue("@Email", customer.Email);//c
                cmd.Parameters.AddWithValue("@Streetaddress", customer.StreetAddress);//c
                
                if (!string.IsNullOrWhiteSpace(customer.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", customer.StreetAddress2);//c
                }
              / else
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", DBNull.Value);//c
                }
                cmd.Parameters.AddWithValue("@City", customer.City); //c
                cmd.Parameters.AddWithValue("@StateID", customer.StateID); //c
                cmd.Parameters.AddWithValue("@Zipcode", customer.Zipcode);//c
                cmd.Parameters.AddWithValue("@CarID", car.Car.CarID); //c
                cmd.Parameters.AddWithValue("@SalesPrice", FinalPrice); //c
                cmd.Parameters.AddWithValue("@PurchaseTypeID", PaymentTypeID);//c
                cmd.Parameters.AddWithValue("@PromotonID", PromoID);//c
                cmd.Parameters.AddWithValue("@Soldby", SoldBy);//c
                cn.Open();
                cmd.ExecuteNonQuery();
            }               
        }
        
        public void SellCarNoPromo(SoldCar car, Customer customer, decimal FinalPrice, int PaymentTypeID, string SoldBy)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SellCar", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@SalesID", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                SqlParameter Param2 = new SqlParameter("@CustomerID", SqlDbType.Int);
                Param2.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.Add(Param2);
                cmd.Parameters.AddWithValue("@CustomerName", customer.CustomerName);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Email", customer.Email);
                cmd.Parameters.AddWithValue("@Streetaddress", customer.StreetAddress);
                if (string.IsNullOrWhiteSpace(customer.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", DBNull.Value);
                }
                if (!string.IsNullOrWhiteSpace(customer.StreetAddress2))
                {
                    cmd.Parameters.AddWithValue("@Streetaddress2", customer.StreetAddress2);
                }
                cmd.Parameters.AddWithValue("@City", customer.City);
                cmd.Parameters.AddWithValue("@StateID", customer.StateID);
                cmd.Parameters.AddWithValue("@Zipcode", customer.Zipcode);
                cmd.Parameters.AddWithValue("@CarID", car.Car.CarID);
                cmd.Parameters.AddWithValue("@SalesPrice", FinalPrice);
                cmd.Parameters.AddWithValue("@PurchaseTypeID", PaymentTypeID);
                cmd.Parameters.AddWithValue("@PromotonID", DBNull.Value);
                cmd.Parameters.AddWithValue("@Soldby", SoldBy);
                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }*/
    }
}
