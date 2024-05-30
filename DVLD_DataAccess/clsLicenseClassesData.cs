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
    public class clsLicenseClassesData
    {

        public static DataTable getAllLicenseClasses()
        {
            DataTable data = new DataTable();

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LicenseClasses";

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
            } catch (Exception e)
            {
                // log it
            }
            finally
            {
                connection.Close();
            }

            return data;
        }

        public static bool findLicenseClassByName(ref int id, string className,
                                                ref string classDescription,
                                                ref int minimalAllowedAge,
                                                ref int DefaultValidityLength, ref int classFees)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LicenseClasses where ClassName = @name";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@name", className);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    id = (int)reader["LicenseClassID"];
                    classDescription = (string)reader["ClassDescription"];
                    minimalAllowedAge =(byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    classFees =  (int)(Convert.ToSingle(reader["ClassFees"]));

                }
            } catch (Exception e)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }


        public static bool findLicenseClassByID(int id, ref string className,
                                                ref string classDescription,
                                                ref int minimalAllowedAge,
                                                ref int DefaultValidityLength, ref int classFees)
        {

            bool isFound = false;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"select * from LicenseClasses where LicenseClassID = @id";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@id", id);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    className = (string)reader["ClassName"];
                    classDescription = (string)reader["ClassDescription"];
                    minimalAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    classFees = (int)Convert.ToSingle( reader["ClassFees"]);

                }
            } catch (Exception e)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }


        public static bool updateLicenseClass(int id, string className,
                                                 string classDescription,
                                                 int minimalAllowedAge,
                                                 int DefaultValidityLength, int classFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"update LicenseClasses
                            set ClassName = @className , 
                                ClassDescription = @classDesciption ,
                                ClassFees = @classFees ,
                                minimumAllowedAge = @minimumAllowedAge,
                                DefaultValidityLengh = @defaultValidityLengh 
                                where LicenseClassID = @id";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@className", className);
            command.Parameters.AddWithValue("@classDescription", classDescription);
            command.Parameters.AddWithValue("@minimumAllowedAge", minimalAllowedAge);
            command.Parameters.AddWithValue("@defaultValidityLenght", DefaultValidityLength);
            command.Parameters.AddWithValue("@classFees", classFees);
            command.Parameters.AddWithValue("@id", id);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                rowsAffected = 0;

            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }


        public static int addNewLiceseClass(string className, string classDescription,
                                             int minimumAllowedAge, int defaultValidiyLength,
                                             int classFees)
        {

            int id = -1;

            SqlConnection connection = new SqlConnection(SettingsDataAccess.connectionString);

            string query = @"insert into LisenceClasses (ClassName , ClassDescription , 
                                                         MinimumAllowedAge , DefaultValidityLengh,
                                                         ClassFees) 
                            values(@className , @classDescription , @minimumAllowedAge ,
                                    @defaultValidityLength , @classFees);
                            select SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@className" , className);
            command.Parameters.AddWithValue("@classDescription" , classDescription);
            command.Parameters.AddWithValue("@minimumAllowedAge" , minimumAllowedAge);
            command.Parameters.AddWithValue("@defaultValidityLenght" , defaultValidiyLength);
            command.Parameters.AddWithValue("@classFees" , classFees);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString() , out int insertedID)) {
                    id = insertedID;
                }
            }catch (Exception e)
            {
                id = -1;
            }
            finally
            {
                connection.Close();
            }

            return id;
        }
    }

    
}







