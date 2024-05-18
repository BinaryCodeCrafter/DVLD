using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {

        public static DataTable getAllApplicationTypes()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = "select * from ApplicationTypes";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    data.Load(reader);
                }

                reader.Close();

            }catch(Exception ex)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }

            return data;
        }




        public static bool updateApplication(int id , string title , int fee)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update ApplicationTypes set 
                                ApplicationTypeTitle = @title ,
                                ApplicationFees = @fee 
                                 where ApplicationTypeID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@fee" , fee);
            command.Parameters.AddWithValue("@title" , title);
            command.Parameters.AddWithValue("@id" , id);


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


        public static bool findApplicationTypeByID(int id , ref string title,
                                                    ref int fees )
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from ApplicationTypes 
                             where ApplicationTypeID = @id";

            SqlCommand command = new SqlCommand(query , connection);

            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    title = (string)reader["ApplicationTypeTitle"];
                    fees = (int)((decimal)reader["ApplicationFees"]);

                    isFound = true;
                }
                reader.Close();
            }
            catch( Exception e )
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










