using OrionTek.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OrionTek.Helpper
{
    public class DataAccess
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

        public List<Clients> GetClients()
        {
            string query = string.Empty;

            con.Open();

            SqlCommand com = new SqlCommand("SELECT * FROM CLIENTS", con);
            List<Clients> clients = new List<Clients>();



            using (var Reader = com.ExecuteReader())
            {

                if (Reader.HasRows)
                {

                    while (Reader.Read())
                    {



                        clients.Add(new Clients()
                        {
                            ID = Convert.ToInt32(Reader["ID"]),
                            Names = (Reader["Names"]).ToString(),
                            LastNames = (Reader["LastNames"]).ToString(),
                            Email = (Reader["Email"]).ToString(),
                            Phone = (Reader["Phone"]).ToString(),
                            Birthday = Convert.ToDateTime(Reader["Birthday"].ToString()),
                            Description = (Reader["Description"]).ToString()
                        });

                    }
                }
            }


            con.Close();
            return clients  ;

        }

        public List<Clients> GetRecentClients()
        {
            string query = string.Empty;

            con.Open();

            SqlCommand com = new SqlCommand("SELECT TOP(5)* FROM CLIENTS", con);
            List<Clients> clients = new List<Clients>();



            using (var Reader = com.ExecuteReader())
            {

                if (Reader.HasRows)
                {

                    while (Reader.Read())
                    {



                        clients.Add(new Clients()
                        {
                            ID = Convert.ToInt32(Reader["ID"]),
                            Names = (Reader["Names"]).ToString(),
                            LastNames = (Reader["LastNames"]).ToString(),
                            Email = (Reader["Email"]).ToString(),
                            Phone = (Reader["Phone"]).ToString(),
                            Birthday = Convert.ToDateTime(Reader["Birthday"].ToString()),
                            Description = (Reader["Description"]).ToString()
                        });

                    }
                }
            }


            con.Close();
            return clients;

        }

        public bool CreateClient(Clients client)
        {
            bool result = true;
            con.Open();

            string query = "INSERT INTO Clients (NAMES, LASTNAMES, EMAIL, PHONE, BIRTHDAY, DESCRIPTION) " +
                "VALUES (@NAMES ,@LASTNAMES ,@EMAIL, @PHONE, @BIRTHDAY, @DESCRIPTION ) ";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add("@NAMES", client.Names);
            command.Parameters.Add("@LASTNAMES", client.LastNames);
            command.Parameters.Add("@EMAIL", client.Email);
            command.Parameters.Add("@PHONE", client.Phone);
            command.Parameters.Add("@BIRTHDAY", client.Birthday);
            command.Parameters.Add("@DESCRIPTION", client.Description);

            command.ExecuteNonQuery();

            con.Close();

            return result;
        }


        public bool CreateClientAddress(Address address)
        {
            bool result = true;
            con.Open();

            string query = "INSERT INTO Addresses (ClientID, Address1, Address2, City, Zip, Country) " +
                "VALUES (@ClientID ,@Address1 ,@Address2, @City, @Zip, @Country ) ";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add("@ClientID", address.ClientID) ;            
            command.Parameters.Add("@Address1", address.Address1);           
            command.Parameters.Add("@Address2", address.Address2);
            command.Parameters.Add("@City", address.City);
            command.Parameters.Add("@Zip", address.Zip);
            command.Parameters.Add("@Country", address.Country);


            command.ExecuteNonQuery();

            con.Close();

            return result;
        }


        public List<Address> GetAddresses(int IDClient)
        {
            string query = string.Empty;

            con.Open();

            SqlCommand com = new SqlCommand("SELECT * FROM ADDRESSES WHERE CLIENTID = @IDCLIENT", con);
            com.Parameters.Add("@IDCLIENT", SqlDbType.Int);
            com.Parameters["@IDCLIENT"].Value = IDClient;

            List<Address> addresses = new List<Address>();



            using (var Reader = com.ExecuteReader())
            {

                if (Reader.HasRows)
                {

                    while (Reader.Read())
                    {



                        addresses.Add(new Address()
                        {
                            ID = Convert.ToInt32(Reader["ID"]),
                            Address1 = (Reader["Address1"]).ToString(),
                            Address2 = (Reader["Address2"]).ToString(),
                            City = (Reader["City"]).ToString(),
                            Zip = (Reader["Zip"]).ToString(),
                            Country = (Reader["Country"].ToString())
                           
                        });

                    }
                }
            }


            con.Close();
            return addresses;

        }

        public Clients GetClient(int IDClient)
        {
            string query = string.Empty;

            con.Open();

            SqlCommand com = new SqlCommand("SELECT * FROM CLIENTS WHERE ID = @ID ", con);
            com.Parameters.Add("@ID", SqlDbType.Int);
            com.Parameters["@ID"].Value = IDClient;

            Clients client = new Clients();


            using (var Reader = com.ExecuteReader())
            {

                if (Reader.HasRows)
                {

                    while (Reader.Read())
                    {

                       client.ID = (Convert.ToInt32(Reader["ID"]));
                       client.Names = (Reader["Names"]).ToString();
                       client.LastNames = (Reader["LastNames"]).ToString();
                       client.Email = (Reader["Email"]).ToString();
                       client.Phone = (Reader["Phone"]).ToString();
                       
                       client.Birthday = Convert.ToDateTime(Reader["Birthday"].ToString());
                       client.Description = (Reader["Description"]).ToString();
                            
                    }
                }
            }


            con.Close();
            return client;

        }

        public bool DeleteAddress(Address address)
        {
            bool result = true;
            con.Open();

            string query = "DELETE FROM ADDRESSES WHERE ID = @ID";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add("@ID", address.ID);


            command.ExecuteNonQuery();

            con.Close();

            return result;
        }


        public bool DeleteClient(Clients client)
        {
            bool result = true;
            
            con.Open();

            string query = "DELETE FROM ADDRESSES WHERE CLIENTID = @ID";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add("@ID", client.ID);


            command.ExecuteNonQuery();

           query = "DELETE FROM CLIENTS WHERE ID = @ID";

            command = new SqlCommand(query, con);
            command.Parameters.Add("@ID", client.ID);


            command.ExecuteNonQuery();

            con.Close();


            return result;
        }

        public bool UpdateClient(Clients client)
        {
            bool result = true;

            con.Open();

            string query = "UPDATE CLIENTS SET NAMES = ISNULL(@NAMES, NAMES), " +
                                                     " LASTNAMES = ISNULL(@LastNames, LastNames), " +
                                                     " EMAIL = ISNULL(@Email, Email), " +
                                                     " PHONE = ISNULL(@Phone, Phone), " +
                                                     " DESCRIPTION = ISNULL(@Description, Description) " +
                                                     " WHERE ID = @ID";

            SqlCommand command = new SqlCommand(query, con);
            command.Parameters.Add("@Names", (object)client.Names ?? DBNull.Value);
            command.Parameters.Add("@LastNames", (object)client.LastNames ?? DBNull.Value);
            command.Parameters.Add("@Email", (object)client.Email ?? DBNull.Value);
            command.Parameters.Add("@Phone", (object)client.Phone ?? DBNull.Value);
            command.Parameters.Add("@Description", (object)client.Description ?? DBNull.Value);
            command.Parameters.Add("@ID", client.ID);


            command.ExecuteNonQuery();
            con.Close();


            return result;
        }


    }
}