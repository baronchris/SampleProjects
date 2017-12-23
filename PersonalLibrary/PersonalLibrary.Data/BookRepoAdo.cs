using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using PersonalLibrary.Data.Models;
using System.Data.SqlClient;

namespace PersonalLibrary.Data
{
    public class BookRepoAdo
    {
        public string cs = DataSetting.GetConnectionString();
        public void PopulateDatabase(List<Book> Books)
        {

             foreach (Book b in Books)
             {
                if (!string.IsNullOrWhiteSpace(b.Title) && !string.IsNullOrWhiteSpace(b.ISBN))
                {
                    AddBookfromCSV(b);
                }
             }
            
        }

        public void AddBookfromCSV(Book book)
        {
            int x = 0;
            int y = 0;
            using(SqlConnection cn =new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("AddFromCSV",cn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter param = new SqlParameter("@BookID", SqlDbType.Int);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@ISBN", book.ISBN);
                cn.Open();
                cmd.ExecuteNonQuery();
                x = (int)param.Value;
                List<Author> Authors = GetAuthorList();
                foreach (string author in book.AuthorsToAdd)
                {
                    if (Authors.Where(a => a.AuthorName == author).FirstOrDefault() != null)
                    {
                        y = Authors.Where(a => a.AuthorName.ToLower() == author.ToLower()).FirstOrDefault().AuthorID;
                        SqlCommand cmd2 = new SqlCommand("ReuseAuthor", cn);
                        cmd2.CommandType = CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@BookID", x);
                        cmd2.Parameters.AddWithValue("@AuthorID", y);
                        cmd2.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd3 = new SqlCommand("AddNewAuthor", cn);
                        cmd3.CommandType = CommandType.StoredProcedure;
                        SqlParameter param2 = new SqlParameter("AuthorID", SqlDbType.Int);
                        param2.Direction = ParameterDirection.Output;
                        cmd3.Parameters.Add(param2);
                        cmd3.Parameters.AddWithValue("@BookID", x);
                        cmd3.Parameters.AddWithValue("@AuthorName", author);
                        cmd3.ExecuteNonQuery();
                    }
                    //need to get list of authors, check DB for repeats 
                    //sproc will either add to author and bridge or just to bridge
                    // ("@BookID",x)
                }
            }
        }

        public List<Author> GetAuthorList()
        {
            List<Author> Authors = new List<Author>();
            using (SqlConnection cn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("Select * from Author", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Author current = new Author();
                        current.AuthorID = (int)dr["AuthorID"];
                        current.AuthorName = dr["AuthorName"].ToString();
                        Authors.Add(current);
                    }
                }
            }
            return Authors;
        }
    }
}
