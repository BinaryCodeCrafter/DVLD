using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
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
                                           ref int applicationStatus,
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
                    applicationStatus = (int)reader["ApplicationStatus"];
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


        public static int addNewApplication(int applicationPersonID , DateTime ApplicationDate,
                                             int applicationTypeID, int applicationStatus , DateTime lastStatusDate,
                                             int paidFees , int createdByUserID)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"insert into Applicatinos (ApplicationPersonID , ApplicationDate,
                            ApplicationTypeID , ApplicationStatus , LastStatusDate,
                            PaidFees, CreatedByUserID) valuse 
                            (@applicationPersonID , @ApplicationDate , @ApplicationTypeID,
                             @applicationStatus , @lastStatusDate , @paidFees ,
                             @createdByUserID);
                             select scope_identity();";

            SqlCommand command =new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@applicationPersonID" , applicationPersonID);
            command.Parameters.AddWithValue("@applicationDate" , ApplicationDate);
            command.Parameters.AddWithValue("@applicationTypeID" ,applicationTypeID);
            command.Parameters.AddWithValue("@applicationStatus" , applicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate" , applicationStatus);
            command.Parameters.AddWithValue("@paidFees" , createdByUserID);

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

            }
            finally
            {
                connection.Close();
            }

            return id;

        }


        public static bool updateApplication(int applicationID , int applicationPersonID,
                                             DateTime applicationDate , int applicationTypeID,
                                             int applicationStatus , DateTime lastStatusDate,
                                             int paidFees , int createdByUserID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update Appilcations set
                             ApplicationPersonID = @applicationPersonID ,
                             ApplicationDate = @applicationDate,
                             ApplicationTypeID = @applicationTypeID,
                             ApplicatinoStatus = @applicaitonStatus,
                             LastStatusDate = @lastStatusDate,
                             PaidFees = @paidFees ,CreatedByUserID = @createdByUserID
                            where ApplicationID = @applicationID";
            
            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@applicationPersonID" , applicationPersonID);
            command.Parameters.AddWithValue("@applicationDate" , applicationDate);
            command.Parameters.AddWithValue("@applicationTypeID" , applicationTypeID);
            command.Parameters.AddWithValue("@applicationStatus" , applicationStatus);
            command.Parameters.AddWithValue("@lastStatusDate" , lastStatusDate);
            command.Parameters.AddWithValue("@paidFees" , paidFees);
            command.Parameters.AddWithValue("@createdByUserID" , createdByUserID);
            command.Parameters.AddWithValue("@applicationID" , applicationID);


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
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


        public static bool deleteApplication(int id)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"delete Application where ApplicationID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id" ,id);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

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


        public static int getActiveApplicationID(int personID , int applicationTypeID)
        {
            int activeApplicationID = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select activeApplicationID=ApplicationID from Applications where
                             ApplicationPersonID = @applicationPersonID and
                             ApplicationTypeID = @applicationTypeID and
                             ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("applicationPersonID" , personID);
            command.Parameters.AddWithValue("applicationTypeID" , applicationTypeID);
            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int appID))
                {
                    activeApplicationID = appID;
                }
            }catch(Exception e)
            {
                activeApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }

            return activeApplicationID;

        }


        public static bool deosPersonHaveActiveApplicationID(int personID , int applicationTypeID)
        {
            return getActiveApplicationID(personID , applicationTypeID) != -1;
        }


        public static int getActiveApplicationIDForLiseceClass
            (int personId , int applicationTypeID , int lisenceClassID)
        {
            int activeApplicationID = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Applications inner join LocalDrivingLicenseApplications
                    on Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                where ApplicantPersonID = @applicationPersonID
                and ApplicationTypeID = @applicationTypeID
                and LocalDrivingLicenseApplications.LicenseClassID = @lisenceClassID
                and ApplicationStatus = 1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@applicationPersonID" , personId);
            command.Parameters.AddWithValue("@applicationTypeID" , applicationTypeID);
            command.Parameters.AddWithValue("@liseceClassID" , lisenceClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int id))
                {
                    activeApplicationID = id;
                }

            }catch(Exception e)
            {
                activeApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }

            return activeApplicationID;
        }

        public static bool updateStatus(int personID , int newStatus)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update Applications set
                             ApplicationStatus = @newStatus,
                             LastStatusDate = @lastStatusDate
                             where ApplicationPersonID = @personID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@newStatus" , newStatus);
            command.Parameters.AddWithValue("@personID" , personID);
            command.Parameters.AddWithValue("@lastStatusDate" , DateTime.Now);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
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

















