using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        public static bool findTestAppointmentByID(int testAppointmentID , ref int testTypeID ,
            ref int localDrivingLicenseApplicationID , ref DateTime appointmentDate , ref int paidFees,
            ref int createdByUser ,ref bool isLocked, ref int retakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "select * from TestAppointments where TestAppointmentID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id" , testAppointmentID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testTypeID = (int)reader["TestTypeID"];
                    localDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = (int)reader["PaidFees"];
                    createdByUser = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestApplicationID = (int)reader["RetakeTestApplicationID"];
                }
            }
            catch (Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool getLastAppointmentTest(int localDrivingLicenseApplicationID , 
            int testTypeID , ref int testAppointmentID , ref DateTime appointmentDate , ref int paidFees,
            ref int createdByUser , ref bool isLocked , ref int retakeTestAppliactionID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "select top 1 from TestAppointments where" +
                "            LocalDrivingLicenseApplicationID = @localDivingLicenseApplicationID" +
                "        and TestTypeID = @testTypeID" +
                "           order by TestAppointmentID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID" , localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@testTypeID" , testTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    testAppointmentID = (int)reader["TestAppointmentID"];
                    appointmentDate = (DateTime)reader["AppointmentDate"];
                    paidFees = (int)reader["PaidFees"];
                    createdByUser = (int)reader["CreatedByUserID"];
                    isLocked = (bool)reader["IsLocked"];
                    retakeTestAppliactionID = (int)reader["RetakeTestApplicationID"];

                }
            }catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable getAllAppointments()
        {
            DataTable datat = new DataTable();

            SqlConnection connetion = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "select * from TestAppointments_View order by AppointmentDate DESC";

            SqlCommand command = new SqlCommand(query, connetion);

            try
            {
                connetion.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    datat.Load(reader);
                }
            }catch(Exception e)
            {
                // do something
            }
            finally
            {
                connetion.Close();
            }

            return datat;

        }

        public static DataTable getApplicationTestAppointmentsPerTestTest(int localDrivingLicenseApplicationID,
            int testTypeID)
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "select * from TestAppointments where" +
                "           LocalDrivingLicenseApplicationID = @localDrivingLicenseApplicationID" +
                "       and TestTypeID = @testTypeID " +
                "       order by TestAppointmentID desc";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID" , localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@testTypeID" , testTypeID);

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
                // do something
            }
            finally
            {
                connection.Close();
            }

            return data;
        }


        public static int addNewTestAppointment(int testTypeID , int localDrivingLicenseApplicationID ,
            DateTime appointmentDate , int paidFees , int createdByUserID , bool isLocked ,
            int retakeTestApplicationID)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "insert into TestAppointments values " +
                "(@testTypeID ,@localDrivingLicenseApplicationID , @appointmentDate , @paidFees ," +
                "@createdByUserID , @isLocked , @retakeTestApplicationID);" +
                "select scope_identity();";


            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@testTypeID" , testTypeID);
            command.Parameters.AddWithValue("@localDrivingLicenseApplicationID" , localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@appointmentDate" , appointmentDate);
            command.Parameters.AddWithValue("@paidFees" , paidFees);
            command.Parameters.AddWithValue("@createdByUserID" , createdByUserID);
            command.Parameters.AddWithValue("@isLocked" , isLocked);
            command.Parameters.AddWithValue("@retakeTestApplicationID" , retakeTestApplicationID);

            try
            {
                connection.Open();

                id = command.ExecuteNonQuery();
            }
            catch
            {
                id = -1;
            }
            finally{
                connection.Open();
            }

            return id;

        }

        public static bool updateTestAppointment(int testAppointmetnID ,int testTypeID , int localDrivingLicenseApplicationID ,
            DateTime appointmentDate , int paidFees , int createdByUserID , bool isLocked ,
            int retakeTestApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update TestAppointments set
                             TestTypeID = @testTypeID ,
                             LocalDrivingLicenseApplicationID = @lcoalDrivingLicenseApplicationID,
                             AppointmentDate = @appointmentDate ,
                             PaidFees = @paidFees , 
                             CreatedByUserID = @createdByUserID ,
                             IsLocked = @isLocked , 
                             RetakeTestApplicationID = @retakeTestApplicationID , 
                             where TestAppointmentID = @testAppointmentID";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@testTypeID" , testTypeID);
            command.Parameters.AddWithValue("@lcoalDrivingLicenseApplicationID" , localDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@appointmentDate" , appointmentDate);
            command.Parameters.AddWithValue("@paidFees" , paidFees);
            command.Parameters.AddWithValue("@createdByUserID" ,createdByUserID);
            command.Parameters.AddWithValue("@isLocekd" ,isLocked);
            command.Parameters.AddWithValue("@retakeTestApplicationID" ,retakeTestApplicationID);
            command.Parameters.AddWithValue("@testAppointmentID" ,testAppointmetnID);


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

        public static int GetTestID(int testAppointmentID)
        {
            int testID = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select TestID from Tests where TestAppointmentID = @testAppointmentID";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@testAppointmentID" , testAppointmentID);

            try
            {
                connection.Open();

                testID = (int)command.ExecuteScalar();
            }catch(Exception e)
            {
                testID = -1;
            }
            finally
            {
                connection.Close();
            }

            return testID;
        }
    }





}














