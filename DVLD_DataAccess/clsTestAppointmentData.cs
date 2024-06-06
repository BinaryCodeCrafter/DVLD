using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
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
    }



}














