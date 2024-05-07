using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsCountry
    {

        public string countryName {  get; set; }

        public int countryID {  get; set; }

        public clsCountry()
        {
            this.countryID = -1;
            this.countryName = "";
        }

        private clsCountry(int id , string name)
        {
            this.countryID = id;
            this.countryName = name;
        }

        public clsCountry find(int id)
        {
            string name = "";

            if (clsCountryData.findCountryByID(id ,ref name))
            {
                return new clsCountry(id , name);
            }
            else
            {
                return null;
            }

        }

        public clsCountry find(string name)
        {
            int id = -1;

            if(clsCountryData.findCountryByName(name , ref id))
            {
                return new clsCountry(id , name);
            }
            else
            {
                return null;
            }
        }

        public static DataTable getAllCounties()
        {
            return clsCountryData.getAllCountries();
        }


    }
}
