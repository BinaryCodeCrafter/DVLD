using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DVLD_DataAccess
{
    public class clsTestTypesData
    {

        public static DataTable getAllTestTypes()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"SELECT * FROM TestTypes order by TestTypeID";

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
            catch(Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return data;
        }


        public static bool findTestTypeByID(int id , ref string title , ref string desc,
                                            ref int fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from TestTypes where TestTypeID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    title = (string)reader["TestTypeTitle"];
                    desc = (string)reader["TestTypeDescription"];
                    fees = (int)((decimal)reader["TestTypeFees"]);

                    isFound = true;
                }

            }
            catch(Exception e)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }


        public static bool updateTestType(int id , string tite , string desc , int fees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update TestTypes
                                set TestTypeTitle = @title ,
                                    TestTypeDescription = @desc ,
                                    TestTypeFees = @fees 
                                    where TestTypeID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@title" , tite);
            command.Parameters.AddWithValue("@desc" , desc);
            command.Parameters.AddWithValue("@fees" , fees);
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
    }
}


















