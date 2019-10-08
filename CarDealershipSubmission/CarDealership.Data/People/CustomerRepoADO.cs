using CarDealership.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CarDealership.Data.People
{
    public class CustomerRepoADO : ICustomerRepo
    {
        public List<PaymentType> GetPaymentTypes()
        {
            List<PaymentType> payments = new List<PaymentType>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                
                SqlCommand cmd = new SqlCommand("Select * from PurchaseType", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using(SqlDataReader dr= cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        PaymentType p = new PaymentType();
                        p.PaymentTypeID = (int)dr["PurchaseTypeID"];
                        p.PaymentTypeName = dr["PurchaseTypeName"].ToString();
                        payments.Add(p);
                    }
                }
            }
            return payments;
        }

        public List<State> GetStates()
        {
            List<State> states = new List<State>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {

                SqlCommand cmd = new SqlCommand("Select * from States", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        State s = new State();
                        s.StateID= dr["StateID"].ToString();
                        s.StateName= dr["StateName"].ToString();
                        states.Add(s);
                    }
                }
            }
            return states;
        }
        public void AddContact(Contact contact)
        {
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("AddContact", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ContactName", contact.ContactName);
                if (!string.IsNullOrWhiteSpace(contact.Phone))
                {
                    cmd.Parameters.AddWithValue("@Contactphone", contact.Phone);
                }
                if (string.IsNullOrWhiteSpace(contact.Phone))
                {
                    cmd.Parameters.AddWithValue("@Contactphone", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@ContactEmail", contact.Email);
                cmd.Parameters.AddWithValue("@ContactMessage", contact.ContactMessage);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<Contact> GetAllContacts()
        {
            List<Contact> contacts = new List<Contact>();
            using (SqlConnection cn = new SqlConnection(DataSettings.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("Select * from Contacts", cn);
                cmd.CommandType = CommandType.Text;
                cn.Open();
                using(SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Contact current = new Contact();
                        current.ContactID = (int)dr["ContactID"];
                        current.ContactName = dr["ContactName"].ToString();
                        try
                        {
                            current.Phone = dr["ContactPhone"].ToString();
                        }
                        catch
                        {
                            current.Phone = "Unknown";

                        }
                        current.Email = dr["ContactEmail"].ToString();
                        current.ContactMessage = dr["ContactMessage"].ToString();
                        string contactdate = dr["ContactDate"].ToString();
                        current.ContactDate = DateTime.Parse(contactdate);
                        current.Responded = (bool)dr["Responded"];
                        contacts.Add(current);

                    }
                }
            }
            return contacts;

        }
    }
}
