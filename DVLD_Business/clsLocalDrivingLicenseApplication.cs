using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        public enum enMode { addNew = 0 , update = 1};
        enMode mode = enMode.addNew;

        public int localDrivingLicenseApplicationID {  get; set; }
        public int applicationID { get; set; }

        public int licenseClassID { get; set; }

        public clsLisenceClass lisenceClass { get; set; }

        public string fullName { get { return base.person.fullName;}}


        public clsLocalDrivingLicenseApplication()
        {
            this.localDrivingLicenseApplicationID = -1;
            this.licenseClassID = -1;
            mode = enMode.addNew;
        }


        public clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID ,
            int applicationID , int applicationPersonID , DateTime applicationDate , 
            int applicationTypeID , enApplicationStatus applicationStatus , DateTime lastStatusDate,
            int paidFees , int createdByUserID , int licenseClassID)
        {
            this.applicationID = applicationID;
            this.localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.applicatinoPersonID = applicatinoPersonID;
            this.applicationDate = applicationDate;
            this.applicatinoTypeID = applicatinoTypeID;
            this.applicationStatus = applicationStatus;
            this.lastStatusDate = lastStatusDate;
            this.paidFees = paidFees;
            this.createdByUserID = createdByUserID;
            this.licenseClassID = licenseClassID;
            this.mode = enMode.update;
            this.lisenceClass = clsLisenceClass.findLisenceClassByID(licenseClassID);
        }

    }
}























