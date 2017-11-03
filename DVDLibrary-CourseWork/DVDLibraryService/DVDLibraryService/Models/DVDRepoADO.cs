using DVDLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace DVDLibraryService.Models
{
    public class DVDRepoADO : IRepository
    {
        public void AddDVD(DVD ToAdd)
        {
            using(SqlConnection cn =new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand  cmd = new SqlCommand("InsertDVD", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter Param = new SqlParameter("@DVDID", SqlDbType.Int);
                Param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(Param);
                cmd.Parameters.AddWithValue("@Title", ToAdd.Title);
                cmd.Parameters.AddWithValue("@Director", ToAdd.Director);
                cmd.Parameters.AddWithValue("@RatingID", ToAdd.RatingID);
                cmd.Parameters.AddWithValue("@RealeaseYear", ToAdd.realeaseYear);
                cn.Open();
                cmd.ExecuteNonQuery();
                ToAdd.DVDID=(int)Param.Value;

                if (ToAdd.Note != null)
                {
                    SqlCommand cmd2 = new SqlCommand("addnote", cn);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.Parameters.AddWithValue("@DVDID", ToAdd.DVDID);
                    cmd2.Parameters.AddWithValue("@note", ToAdd.Note);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

        public void DeleteDvD(int DVDID)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteDVD", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DVDID", DVDID);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EditDVD(DVD ToEdit)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateDVD", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DVDID", ToEdit.DVDID);
                cmd.Parameters.AddWithValue("@Title", ToEdit.Title);
                cmd.Parameters.AddWithValue("@Director", ToEdit.Director);
                cmd.Parameters.AddWithValue("@RatingID", ToEdit.RatingID);
                cmd.Parameters.AddWithValue("@RealeaseYear", ToEdit.realeaseYear);
                cmd.Parameters.AddWithValue("@note", ToEdit.Note);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
         }

        public IEnumerable<DVDDetailView> FindByDirector(string director)
        {
            List<DVDDetailView> results = new List<DVDDetailView>();
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetDVDsbyDirector", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Director", director);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVDDetailView current = new DVDDetailView();
                        current.DVDID = (int)dr["DVDID"];
                        current.Title = dr["Title"].ToString();
                        current.Director = dr["Director"].ToString();
                        current.RatingID = (int)dr["RatingID"];
                        current.RatingName = dr["RatingName"].ToString();
                        current.realeaseYear = (int)dr["realeaseYear"];
                        current.Note = dr["Note"].ToString();
                        results.Add(current);
                    }
                }

            }
            return results;
        }

        public IEnumerable<DVDDetailView> FindByTitle(string title)
        {
            List<DVDDetailView> results = new List<DVDDetailView>();
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetDVDsByTitle", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Title", title);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVDDetailView current = new DVDDetailView();
                        current.DVDID = (int)dr["DVDID"];
                        current.Title = dr["Title"].ToString();
                        current.Director = dr["Director"].ToString();
                        current.RatingID = (int)dr["RatingID"];
                        current.RatingName = dr["RatingName"].ToString();
                        current.realeaseYear = (int)dr["realeaseYear"];
                        current.Note = dr["Note"].ToString();
                        results.Add(current);
                    }
                }
            }
            return results;
        }
        public IEnumerable<DVDDetailView> FindbyYear(int year)
        {
            List<DVDDetailView> results = new List<DVDDetailView>();
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetDVDsByYear", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@realeaseYear", year);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVDDetailView current = new DVDDetailView();
                        current.DVDID = (int)dr["DVDID"];
                        current.Title = dr["Title"].ToString();
                        current.Director = dr["Director"].ToString();
                        current.RatingID = (int)dr["RatingID"];
                        current.RatingName = dr["RatingName"].ToString();
                        current.realeaseYear = (int)dr["realeaseYear"];
                        current.Note = dr["Note"].ToString();
                        results.Add(current);
                    }
                }
            }
            return results;
        }

        public IEnumerable<DVDListView> GetAll()
        {
            List<DVDListView> DVDs = new List<DVDListView>();
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectAllDVDS", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using(SqlDataReader dr= cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        DVDListView current = new DVDListView();
                        current.DVDID = (int)dr["DVDID"];
                        current.Title = dr["Title"].ToString();
                        current.Director = dr["Director"].ToString();
                        current.RatingID = (int)dr["RatingID"];
                        current.RatingName = dr["RatingName"].ToString();
                        current.realeaseYear = (int)dr["realeaseYear"];
                        DVDs.Add(current);
                    }
                }
            }
            return DVDs;
        }

        public DVDDetailView GetByID(int ID)
        {
            DVDDetailView Selected = null;
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("SelectByDVDID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DVDID", ID);
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Selected = new DVDDetailView();
                        Selected.DVDID = (int)dr["DVDID"];
                        Selected.Title = dr["Title"].ToString();
                        Selected.Director = dr["Director"].ToString();
                        Selected.RatingID = (int)dr["RatingID"];
                        Selected.RatingName = dr["RatingName"].ToString();
                        Selected.realeaseYear = (int)dr["realeaseYear"];
                        Selected.Note = dr["Note"].ToString();
                    }
                }

            }
            return Selected;
        }

    }
}