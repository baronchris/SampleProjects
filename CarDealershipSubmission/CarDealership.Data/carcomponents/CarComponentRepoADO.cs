using CarDealership.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CarDealership.Data.carcomponents
{
    public class CarComponentRepoADO : IComponentRepo
    {
        public List<BodyStyles> GetBodies()
        {
            List<BodyStyles> Styles = new List<BodyStyles>();
            using(SqlConnection cn =new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from BodyStyle", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        BodyStyles s = new BodyStyles();
                        s.StyleID = (int)dr["StyleID"];
                        s.BodyStyle = dr["StyleName"].ToString();
                        Styles.Add(s);
                    }
                }
            }


            return Styles;
        }

        public List<Colors> GetColors()
        {
            List<Colors> colors = new List<Colors>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from Color", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Colors s = new Colors();
                        s.ColorID = (int)dr["ColorID"];
                        s.ColorName = dr["Colorname"].ToString();
                        colors.Add(s);
                    }
                }
            }


            return colors;
        }

        public List<Interiors> GetInteriors()
        {
            List<Interiors> interiors = new List<Interiors>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from Interiors", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Interiors s = new Interiors();
                        s.InteriorID = (int)dr["InteriorId"];
                        s.InteriorName = dr["InteriorDescription"].ToString();
                        interiors.Add(s);
                    }
                }
            }


            return interiors;
        }

        public List<Makes> GetMakes()
        {
            List<Makes> MakeChoices = new List<Makes>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from Make", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Makes s = new Makes();
                        s.MakeID = (int)dr["MakeID"];
                        s.MakeName = dr["MakeName"].ToString();
                        s.AddedBy = dr["Addedby"].ToString();
                        s.DateAdded = DateTime.Parse(dr["DateAdded"].ToString()).Date;
                        MakeChoices.Add(s);
                    }
                }
            }


            return MakeChoices;
        }

        public List<CarModels> GetModels()
        {
            List<CarModels> ModelChoices = new List<CarModels>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetModelsforDisplay", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        CarModels s = new CarModels();
                        s.ModelID = (int)dr["ModelID"];
                        s.MakeName = dr["MakeName"].ToString();
                        s.MakeID = (int)dr["MakeID"];
                        s.ModelName = dr["ModelName"].ToString();
                        s.AddedBy = dr["Addedby"].ToString();
                        s.DateAdded = DateTime.Parse(dr["DateAdded"].ToString()).Date;
                        ModelChoices.Add(s);
                    }
                }
            }


            return ModelChoices;
        }

        public List<Transmission> GetTrans()
        {
            List<Transmission> trans = new List<Transmission>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from Transmission", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Transmission s = new Transmission();
                        s.TransmissionID = (int)dr["TransmissionID"];
                        s.TransmissionName = dr["TransmissionName"].ToString();
                        trans.Add(s);
                    }
                }
            }
            return trans;
        }
    }
}
