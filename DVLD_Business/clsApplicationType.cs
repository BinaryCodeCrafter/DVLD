using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DVLD_Business
{
    public class clsApplicationType
    {

        public int applicationTypeID { get; set; }
        public string applicationTypeTitle { get; set; }
        public int applicationFees {  get; set; }

        private clsApplicationType(int id , string title , int fee)
        {
            applicationTypeID = id;
            applicationTypeTitle = title;
            applicationFees = fee;
        }


        public static DataTable getAllApplicationTypes()
        {
            return clsApplicationTypeData.getAllApplicationTypes();
        }

        public bool uptateFees()
        {
            return clsApplicationTypeData.updateApplication(applicationTypeID,
                        applicationTypeTitle, applicationFees);
        }

        public static clsApplicationType findApplicationTypeByID(int id)
        {
            string title = "";
            int fees = 0;

            if (clsApplicationTypeData.findApplicationTypeByID(id, ref title,
                                        ref fees))
            {
                return new clsApplicationType(id, title, fees);
            }
            else
            {
                return null;
            }
        }



    }
}






