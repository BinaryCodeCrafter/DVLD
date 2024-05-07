using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;
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



        public static int addNewPerson(string firstName , string secondName ,
            string thirdName , string lastName , string nationalNo ,
            DateTime dateOfBirth , short gendor , string address , string phone , string email,
            int NationalityCountyID , string imagePath)
        {
            int personID = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"insert into People values 
                          VALUES
           (@NationalNo,
           @FirstName, 
           @SecondName,
           @ThirdName,
           @LastName, 
           @DateOfBirth,
           @Gendor,
           @Address,
           @Phone, 
           @Email,
           @NationalityCountryID,
           @ImagePath);
            SELECT SCOPE_IDENTITY();";
                                  

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo" , nationalNo);
            command.Parameters.AddWithValue("@FirstName" , firstName);
            command.Parameters.AddWithValue("@SecondName" , secondName);
            command.Parameters.AddWithValue("@LastName" , lastName);
            command.Parameters.AddWithValue("@DateOfBirth" , dateOfBirth);
            command.Parameters.AddWithValue("@Gendor" , gendor);
            command.Parameters.AddWithValue("@Addredd" , address);
            command.Parameters.AddWithValue("@Phone" , phone);

            if(thirdName != "" && thirdName != null)
            {
                command.Parameters.AddWithValue("@ThirdName", thirdName);
            }

            if(imagePath != "" &&  imagePath != null)
            {
                command.Parameters.AddWithValue("@ImagePaht", imagePath);
            }

             if(email != "" &&  email != null)
            {
                command.Parameters.AddWithValue("@Email", email);
            }


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    personID = insertedID;
                }

            }catch(Exception e)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }

            return personID;

        }


        public static bool updatePerson(int personID ,string firstName , string secondName ,
            string thirdName , string lastName , string nationalNo ,
            DateTime dateOfBirth , short gendor , string address , string phone , string email,
            int NationalityCountyID , string imagePath)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update People
                            set FirstName = @FirstName,
                                SecondName = @SecondName,
                                ThirdName = @ThirdName,
                                LastName = @LastName, 
                                NationalNo = @NationalNo,
                                DateOfBirth = @DateOfBirth,
                                Gendor=@Gendor,
                                Address = @Address,  
                                Phone = @Phone,
                                Email = @Email, 
                                NationalityCountryID = @NationalityCountryID,
                                ImagePath =@ImagePath
                                where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@FirstName" , firstName);
            command.Parameters.AddWithValue("@SecondName" , secondName);
            command.Parameters.AddWithValue("@LastName" , lastName);
            command.Parameters.AddWithValue("@Address" , address);
            command.Parameters.AddWithValue("@Phone" , phone);
            command.Parameters.AddWithValue("@NationalNo" , nationalNo);
            command.Parameters.AddWithValue("@DateOfBirth" , dateOfBirth);
            command.Parameters.AddWithValue("@Gendor" , gendor);
            command.Parameters.AddWithValue("@NationalityCountryID" , NationalityCountyID);

            if(thirdName != "" && thirdName != null)
            {
                command.Parameters.AddWithValue("ThirdName", thirdName);
            }

            if(email != "" &&  email != null)
            {
                command.Parameters.AddWithValue("Email",email);
            }

            if(imagePath != "" &&  imagePath != null)
            {
                command.Parameters.AddWithValue("ImagePath", imagePath);
            }


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }catch(Exception ex)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }


            return rowsAffected > 0;
        }


        public static DataTable getAllPeople()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"SELECT People.PersonID, People.NationalNo,
              People.FirstName, People.SecondName, People.ThirdName, People.LastName,
			  People.DateOfBirth, People.Gendor,  
				  CASE
                  WHEN People.Gendor = 0 THEN 'Male'

                  ELSE 'Female'

                  END as GendorCaption ,
			  People.Address, People.Phone, People.Email, 
              People.NationalityCountryID, Countries.CountryName, People.ImagePath
              FROM            People INNER JOIN
                         Countries ON People.NationalityCountryID = Countries.CountryID
                ORDER BY People.FirstName";

            SqlCommand command = new SqlCommand(query, connection);

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


        public static bool deletePerson(int personID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"delete People where PersonID = @personID";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("personID" , personID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }


            return rowsAffected > 0;

        }
        public static bool isPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select Found = 1 from People where NationalNo = @nationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@nationalNo", NationalNo);
            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }




        public static bool isPersonExist(int personID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select Found = 1 from People where PersonID = @personID";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@personID", personID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }





    }
}
