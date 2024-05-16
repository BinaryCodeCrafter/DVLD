using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Pipes;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsUserData
    {

        public static DataTable getAllUsers()
        {
            DataTable users = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select Users.UserID , Users.PersonID ,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                            Users.UserName, Users.IsActive
                                From Users inner join People on
                                Users.PersonID = People.PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    users.Load(reader);
                }

                reader.Close();
            }

            catch (Exception ex)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }

            return users;

        }


        public static bool getUserInfoByPersonID(int personID , ref int userID , ref string userName,
                                                ref string password , ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Users where Users.PersonID = @personID";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue("@personID" , personID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    userID = (int)reader["UserID"];
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }





        public static bool getUserInfoByUserID(int userID , ref int personID , ref string userName,
                                                ref string password , ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Users where Users.UserID = @userID";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue("@UserID" , userID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personID = (int)reader["PersonID"];
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    isActive = (bool)reader["IsActive"];
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }




        public static bool getUserInfoByUserNameAndPassword(string userName,string password ,
           ref int userID , ref int personID  , ref bool isActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from Users where Users.UserName = @userName AND 
                                Users.Password = @password";

            SqlCommand command = new SqlCommand (query, connection);

            command.Parameters.AddWithValue("@userName" , userName);
            command.Parameters.AddWithValue("@password" , password);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    personID = (int)reader["PersonID"];
                    userID = (int)reader["UserID"];
                    isActive = (bool)reader["IsActive"];
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                }

                reader.Close();
            }
            catch(Exception ex)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }



        public static int addNewUser(int personID , string userName , string password , bool isActive)
        {

            int userID = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"insert into table Users (PersonID , UserName , Password ,IsActive)
                            values (@personID , @userName , @password ,@ isActive);
                             select scope_identity();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@personID", personID);
            command.Parameters.AddWithValue("@userName", userName);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@isActive", isActive);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if(result != null && int.TryParse(result.ToString() , out int insertedID))
                {
                    userID = insertedID;
                }

            }
            catch(Exception e)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }


            return userID;

        }


        public static bool updateUser(int userID , int personID , string userName , string password,
            bool isActive)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update Users 
                             set PersonID = @personID ,
                                 UserName = @userName,
                                 Password = @password,
                                 IsActive = @isActive
                                 where UserID = @userID";


            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@personID" , personID);
            command.Parameters.AddWithValue("@userName" , userName);
            command.Parameters.AddWithValue("@password" , password);
            command.Parameters.AddWithValue("@isActive" , isActive);
            command.Parameters.AddWithValue("@userID" , userID);


            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }catch(Exception e)
            {
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected > 0;
        }


        public static bool deleteUser(int userID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"delete Users where UserID = @userId";

            SqlCommand command = new SqlCommand (query , connection);

            command.Parameters.AddWithValue("userID", userID);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }catch( Exception e) {
                connection.Close();
                return false;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool isUserExist(string userName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select found=1 from Users where UserName = @userName";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@userName" , userName);

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


        public static bool isUserExist(int userID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select found=1 from Users where UserID = @userID";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@userID" , userID);

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


        public static bool isUserExistForPersonID(int personID)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select found=1 from Users where PersonIe = @personID";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@personID" , personID);

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

        public static bool changePassword(int userID , string password)
        {
            int rowsAffected = 0;

            SqlConnection connoction = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update Users
                             set Password = @password
                             where UserID = @userID";

            SqlCommand command = new SqlCommand(query, connoction);

            command.Parameters.AddWithValue("@password" , password);
            command.Parameters.AddWithValue("@userID" , userID);

            try
            {
                connoction.Open();

                rowsAffected = command.ExecuteNonQuery();
            }catch(Exception e)
            {
                rowsAffected = 0;
            }
            finally
            {
                connoction.Close();
            }

            return rowsAffected > 0;
        }

    }
}
