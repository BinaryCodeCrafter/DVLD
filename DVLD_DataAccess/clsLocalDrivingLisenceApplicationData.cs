using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLisenceApplicationData
    {

        public static bool findLocalDrivingLisenceApplicationByItsID
            (int localDrivingLisenceApplicationID , ref int applicationID , ref int LisenceClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LocalDrivingLisenceApplications where
                             LocalDrivingLisenceApplicationID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", localDrivingLisenceApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    applicationID = (int)reader["ApplicationID"];
                    LisenceClassID = (int)reader["LisenceClassID"];
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

        public static bool findLocalDrivingLisenceApplicationByApplicationID
            (int applicationID , ref int localDrivingLisenceApplicationID , ref int LisenceClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LocalDrivingLisenceApplications where
                             applicationID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", applicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    localDrivingLisenceApplicationID = (int)reader["LocalDrivingLisenceApplicationID"];
                    LisenceClassID = (int)reader["LisenceClassID"];
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

        public static DataTable getAllLocalDrivingLisenceApplications()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications_View";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    data.Load(reader);
                }
            }catch(Exception e)
            {

            }
            finally
            {
                connection.Close();
            }

            return data;
        }
    }
}

















