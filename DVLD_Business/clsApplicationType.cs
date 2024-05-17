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

        public static bool uptateFees(int id , string title,  int fees)
        {
            return clsApplicationTypeData.updateApplication(id, title, fees);
        }




    }
}






