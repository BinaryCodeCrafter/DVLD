using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLocalDrivinglicenseApplicationData
    {

        public static bool findLocalDrivingLicenseApplicationByItsID
            (int localDrivinglicenseApplicationID , ref int applicationID , ref int licenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LocalDrivingLicenseApplications where
                             LocalDrivingLicenseApplicationID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", localDrivinglicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    applicationID = (int)reader["ApplicationID"];
                    licenseClassID = (int)reader["LicenseClassID"];
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

        public static bool findLocalDrivingLicenseApplicationByApplicationID
            (int applicationID , ref int localDrivinglicenseApplicationID , ref int licenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LocalDrivinglicenseApplications where
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
                    localDrivinglicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    licenseClassID = (int)reader["LicenseClassID"];
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

        public static DataTable getAllLocalDrivinglicenseApplications()
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

        public static int addNewLocalDrivinglicenseApplication
            (int applicationID , int licenseClassID)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"insert into LocalDrivingLicenseApplications
                          (ApplicationID , LicenseClassID)
                            values (@applicationID , @licenseClassID);
                             select scope_identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@applicationID" , applicationID);
            command.Parameters.AddWithValue("@licenseClassID" , licenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int insertedID))
                {
                    id = insertedID;
                }
            }catch(Exception e)
            {
                id = -1;
            }
            finally
            {
                connection.Close();
            }

            return id;

        }


        public static bool updateLocalDrivingLiceseApplication
            (int localDrivingLicenseApplicationID , int applicationID , int licenseClassID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update LocalDrivingLicenseApplications set
                            ApplicationID = @applicationID ,
                            LicenseClassID = @licenseClassID 
                            where LocalDrivingLicenseApplicationID = @id";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@applicationID" , applicationID);
            command.Parameters.AddWithValue("@licenseClassID" , licenseClassID);
            command.Parameters.AddWithValue("@id" , localDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
                    
            }catch( Exception e )
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }


            return rowsAffected > 0;

        }

        public static bool deleteLocalDrivingLicenseApplication(int id)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"delete LocalDrivingLicenseApplications where
                             LocalDrivingLicnseApplicationID = @id";

            SqlCommand commmand = new SqlCommand(query , connection);


            commmand.Parameters.AddWithValue("@id" , id);

            try
            {
                connection.Open();

                rowsAffected = commmand.ExecuteNonQuery();

            }catch(Exception e)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }


            return rowsAffected > 0;
        
        }
    }
}

















