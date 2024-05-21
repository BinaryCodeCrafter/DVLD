using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationsData
    {


        public static DataTable getAllApplications()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Applications";

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


        public static bool findApplicationByID(int id , ref int applicationPersonID , 
                                           ref DateTime applicationDate ,
                                           ref int applicationTypeID,
                                           ref DateTime lastStatusDate,
                                           ref int paidFees, ref int createdByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Applications where ApplicationID = @id";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@id" , id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    applicationPersonID = (int)reader["ApplicationPersonID"];
                    applicationDate = (DateTime)reader["ApplicationDate"];
                    applicationTypeID = (int)reader["ApplicationTypeID"];
                    lastStatusDate = (DateTime)reader["LastStatusDate"];
                    paidFees = (int)reader["PaidFees"];
                    createdByUserID = (int)reader["CreatedByUserID"];
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
       


        public static bool isApplicationExistByApplicationID(int id)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Applications where ApplicationID = @id";
            
            SqlCommand command = new SqlCommand (query , connection);

            command.Parameters.AddWithValue("@id" , id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
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










