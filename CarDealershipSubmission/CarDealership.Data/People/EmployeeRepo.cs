using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.People
{
    public class EmployeeRepo
    {
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using(SqlConnection cn =new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSystemUsers", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Employee current = new Employee();
                        current.EmployeeID = dr["Id"].ToString();
                        current.Email = dr["Email"].ToString();
                        current.UserName = dr["UserName"].ToString();
                        current.RoleName = dr["Name"].ToString();
                        current.Firstname = dr["FirstName"].ToString();
                        current.Lastname = dr["LastName"].ToString();
                        employees.Add(current);
                    }
                }
            }
            return employees;
        }
        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("select * from AspNetRoles", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Role r = new Role();
                        r.RoleID = dr["Id"].ToString();
                        r.RoleName = dr["Name"].ToString();
                        roles.Add(r);
                        
                    }
                }
            }
            return roles;
        }
        public Employee GetEmployeeByID(string EmployeeID)
        {
            Employee current = new Employee();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSystemUserByID", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", EmployeeID);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                       
                        current.EmployeeID = dr["Id"].ToString();
                        current.Email = dr["Email"].ToString();
                        current.UserName = dr["UserName"].ToString();
                        current.RoleName = dr["Name"].ToString();
                        current.Firstname = dr["FirstName"].ToString();
                        current.Lastname = dr["LastName"].ToString();
                        
                    }
                }
            }
            return current;

        }
        public void EditEmployee(Employee ToEdit)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateUser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", ToEdit.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", ToEdit.Firstname);
                cmd.Parameters.AddWithValue("@LastName", ToEdit.Lastname);
                cmd.Parameters.AddWithValue("@Email", ToEdit.Email);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DisableUser (string EmployeeID)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("disableuser", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", EmployeeID);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public Employee GetEmployeeByName(string UserName)
        {
            Employee current = new Employee();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSystemUserByName", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", UserName);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {

                        current.EmployeeID = dr["Id"].ToString();
                        current.Email = dr["Email"].ToString();
                        current.UserName = dr["UserName"].ToString();
                        current.RoleName = dr["Name"].ToString();
                        current.Firstname = dr["FirstName"].ToString();
                        current.Lastname = dr["LastName"].ToString();

                    }
                }
            }
            return current;

        }
        public SalesReport GetSalesReportByName(string username)
        {
            SalesReport report = new SalesReport();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetSalesReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        report.UserName = dr["UserName"].ToString();
                        report.TotalCarsSold = (int)dr["CarsSold"];
                        report.NetSales = (decimal)dr["TotalSales"];
                    }
                }
            }
                return report;
        }
        public List<Sales>GetListofSales(string username)
        {
            List<Sales> sales = new List<Sales>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetListOfSales", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Sales current = new Sales();
                        current.UserName = dr["UserName"].ToString();
                        string datetemp = dr["SoldDate"].ToString();
                        current.DateSold = DateTime.Parse(datetemp);
                        current.SalePrice = (decimal)dr["SalesPrice"];
                        sales.Add(current);
                    }
                }
            }
            return sales;
        }
        public List<InventoryReportItem> GetInventoryReport()
        {
            List<InventoryReportItem> reports = new List<InventoryReportItem>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetInventoryReport", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        InventoryReportItem current = new InventoryReportItem();
                        current.ModelName = dr["ModelName"].ToString();
                        current.MakeName = dr["MakeName"].ToString();
                        current.ModelYear = (int)dr["ModelYear"];
                        current.InStock = (int)dr["InStock"];
                        reports.Add(current);
                    }
                }
            }
            return reports;
        }
    }

}
