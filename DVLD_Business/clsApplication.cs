using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {

        enum enMode  {addNew = 0 , update = 1}
        enMode mode = enMode.addNew;

        public enum enApplicationType { newDrivingLisence = 1 , renewDrivingLisence = 2 ,
                                 replaceLostDrivingLisence = 3 , replaceDamagedDrivingLisence = 4,
                                 releaseDetainedDrivingLisence = 5 , newInternationalLisence =6,
                                    reTakeTest = 7};

        public enum enApplicationStatus { New = 1 , Canacelled = 2 , Completed = 3};



        public enApplicationType applicationType { get; set; }
        public enApplicationStatus applicationStatus {  get; set; }
        public int applicationID { get; set; }
        public int applicatinoPersonID { get; set; }
        public DateTime applicationDate { get; set; }
        public DateTime lastStatusDate { get; set; }
        public int paidFees { get; set; }
        public string ApplicationPersonFullName
        {
            get
            {
                return clsPerson.find(this.applicatinoPersonID).fullName;
            }
        }
        public int createdByUserID { get; set; }

        public clsUser user;

        public clsApplicationType applicationTypeInfo;
 





                

    }

}










