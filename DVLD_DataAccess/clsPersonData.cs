using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        
        public static bool getPersonById(int id , ref string firstName , ref string secondName ,
            ref string thirdName , ref string lastName , ref string nationalNo ,
            ref DateTime dateOfBirth , ref short gendor , ref string address ,
            ref string phone , ref string email , ref int nationalityCountryID , 
            ref string imagePath)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from People where PersonID = @personID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                }

                firstName = (string)reader["FirstName"];
                secondName = (string)reader["SecondName"];

                if (reader["ThirdName"] != DBNull.Value)
                {
                    thirdName = (string)reader["ThirdName"];
                }
                else
                {
                    firstName = "";
                }

                nationalNo = (string)reader["NationalNo"];
                lastName = (string)reader["LastName"];
                address = (string)reader["Address"];
                phone = (string)reader["Phone"];
                gendor = (byte)reader["Gendor"];
                nationalityCountryID = (int)reader["NationalityCountryID"];
                dateOfBirth = (DateTime)reader["DateOfBirth"];

                if (reader["Email"] != DBNull.Value)
                {
                    email = (string)reader["Email"];
                }
                else
                {
                    email = "";
                }

                if (reader["ImagePath"] != DBNull.Value)
                {
                    imagePath = (string)reader["ImagePath"];
                }

                reader.Close();
            }
            catch(Exception e){
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }





      public static bool getPersonByNationalNo(string nationalNo , ref string firstName , ref string secondName ,
            ref string thirdName , ref string lastName , ref int personID ,
            ref DateTime dateOfBirth , ref short gendor , ref string address ,
            ref string phone , ref string email , ref int nationalityCountryID , 
            ref string imagePath)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from People where NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", nationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                }

                firstName = (string)reader["FirstName"];
                secondName = (string)reader["SecondName"];

                if (reader["ThirdName"] != DBNull.Value)
                {
                    thirdName = (string)reader["ThirdName"];
                }
                else
                {
                    firstName = "";
                }

                lastName = (string)reader["LastName"];
                address = (string)reader["Address"];
                phone = (string)reader["Phone"];
                gendor = (byte)reader["Gendor"];
                nationalityCountryID = (int)reader["NationalityCountryID"];
                dateOfBirth = (DateTime)reader["DateOfBirth"];
                personID = (int)reader["PersonID"]

                if (reader["Email"] != DBNull.Value)
                {
                    email = (string)reader["Email"];
                }
                else
                {
                    email = "";
                }

                if (reader["ImagePath"] != DBNull.Value)
                {
                    imagePath = (string)reader["ImagePath"];
                }

                reader.Close();
            }
            catch(Exception e){
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
