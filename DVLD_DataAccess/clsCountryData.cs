using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsCountryData
    {

        public static DataTable getAllCountries()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Countries order by CountryName";

            SqlCommand command = new SqlCommand(query , connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    data.Load(reader);
                }

                reader.Close();
            }
            catch(Exception e)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }

            return data;
        }


        public static bool findCountryByID(int id ,ref string countryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Countries where CountryID = @countryID";

            SqlCommand command = new SqlCommand (query , connection);

            command.Parameters.AddWithValue("@countryID" , id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    countryName = (string)reader["CountryName"];
                }


            }
            catch(Exception e)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool findCountryByName(string name , ref int id)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Countries where CountryName = @name";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@name" , name);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    id = (int)reader["CountryID"];
                }

                reader.Close();
            }catch(Exception e)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
