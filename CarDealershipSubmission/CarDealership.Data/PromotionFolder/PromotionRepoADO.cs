using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CarDealership.Data
{
    public class PromotionRepoADO : IPromotionRepo
    {
        public void AddPromotion(Promotion promo)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddPromotion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PromotionName", promo.PromotionName);
                cmd.Parameters.AddWithValue("@PromotionDescription", promo.Description);
                if (promo.StartDate != null)
                {
                    cmd.Parameters.Add("@PromotionStartDate", SqlDbType.DateTime2).Value = promo.StartDate;
                }
                if((promo.StartDate == null))
                {
                    cmd.Parameters.AddWithValue("@PromotionStartDate", DBNull.Value);
                }
                if (promo.EndDate != null)
                {
                    cmd.Parameters.Add("@PromotionEndDate", SqlDbType.DateTime2).Value = promo.EndDate;
                }
                if ((promo.EndDate == null))
                {
                    cmd.Parameters.AddWithValue("@PromotionEndDate", DBNull.Value);
                }
               
                cmd.Parameters.AddWithValue("@IsForUsed", promo.IsForUsed);
                cmd.Parameters.AddWithValue("@IsForNew", promo.IsForNew);
                cmd.Parameters.AddWithValue("@FlatDiscount", promo.FlatDiscount);
                cmd.Parameters.AddWithValue("@PercentDiscount", promo.PercentDiscount);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeletePromotion(int ID)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeletePromotion", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PromotionID", ID);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Promotion> GetALLpromotions()
        {
            List<Promotion> promos = new List<Promotion>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from promotion", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Promotion p = new Promotion();
                        p.PromotionID = (int)dr["PromotionID"];
                        p.PromotionName = dr["PromotionName"].ToString();
                        p.Description = dr["PromotionDescription"].ToString();
                        string sdate = dr["PromotionStartDate"].ToString();
                        if (!string.IsNullOrEmpty(sdate))
                        {
                            p.StartDate = DateTime.Parse(sdate);
                        }
                        string edate = dr["PromotionEndDate"].ToString();
                        if (!string.IsNullOrEmpty(edate))
                        {
                            p.EndDate = DateTime.Parse(edate);
                        }
                        p.FlatDiscount = (decimal)dr["FlatDiscount"];
                        p.PercentDiscount = (int)dr["PercentDiscount"];
                        p.IsCanceled = (bool)dr["IsCanceled"];
                        p.IsForNew = (bool)dr["IsForNew"];
                        p.IsForUsed = (bool)dr["IsForUsed"];
                        promos.Add(p);
                    }

                }
               

            }
            return promos;
        }

        public Promotion GetPromotionByDate(DateTime date)
        {
            Promotion current = new Promotion();
            using(SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetPromotionByDate", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        current.PromotionID = (int)dr["PromotionID"];
                        current.PromotionName = dr["PromotionName"].ToString();
                        current.Description = dr["PromotionDescription"].ToString();
                      
                        current.StartDate = (DateTime)dr["PromotionStartDate"];

                        current.EndDate = (DateTime)dr["PromotionEndDate"];
                        current.IsForNew = (bool)dr["IsForNew"];
                        current.IsForUsed = (bool)dr["IsForUsed"];
                        current.FlatDiscount = (decimal)dr["FlatDiscount"];
                        current.PercentDiscount = (int)dr["PercentDiscount"];
                        current.IsCanceled = (bool)dr["IsCanceled"];
                    }
                   else
                    {
                        current.PromotionID = 0;
                        current.PromotionName = "none";
                        current.Description = "none";
                        current.StartDate = DateTime.Now;
                        current.EndDate = DateTime.Now;
                        current.IsForNew = true;
                        current.IsForUsed = true;
                        current.FlatDiscount = 0;
                        current.PercentDiscount = 0;
                        current.IsCanceled = true;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(current.PromotionName) )
            {
                current.PromotionID = 0;
                current.PromotionName = "none";
                current.Description = "none";
                current.StartDate = DateTime.Now;
                current.EndDate = DateTime.Now;
                current.IsForNew = true;
                current.IsForUsed = true;
                current.FlatDiscount = 0;
                current.PercentDiscount = 0;
            }
            return current;
        }

        public Promotion GetPromotionByID(int ID)
        {
            throw new NotImplementedException();
        }

        public List<Promotion> GetValidPromotions()
        {
            List<Promotion> promotions = GetALLpromotions();
            List<Promotion> promos = (from p in promotions
                                      where p.StartDate == null && p.EndDate == null && p.IsCanceled == false
                                      select p).ToList();
            promos.Add(GetPromotionByDate(DateTime.Now));
            return promos;

        }
    }
}
