using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestType
    {

        private int id { get; set; }
        private string title { get; set; }
        private string description { get; set; }
        private int fees { get; set; }

        private clsTestType(int id, string title, string description, int fees)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.fees = fees;
        }

        public static DataTable getAllTestTypes()
        {
            return clsTestTypesData.getAllTestTypes();
        }

        public static clsTestType findTestTypeByID(int id)
        {
            string desc = "", title = "";
            int fees = 0;

            if (clsTestTypesData.findTestTypeByID(id, ref title, ref desc, ref fees))
            {
                return new clsTestType(id, title, desc, fees);
            }
            else
            {
                return null;
            }

        }


        public bool updateTestType()
        {
            return clsTestTypesData.updateTestType(id, title, description, fees);
        }

    }
}












