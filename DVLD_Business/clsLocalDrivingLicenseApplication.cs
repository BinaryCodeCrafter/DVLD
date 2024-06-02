using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {

        public enum enMode { addNew = 0 , update = 1};
        enMode mode = enMode.addNew;

        public int localDrivingLicenseApplicationID {  get; set; }
        public int ApplicationID { get; set; }

        public int licenseClassID { get; set; }

        public clsLisenceClass lisenceClass { get; set; }

        public string fullName { get { return base.person.fullName;}}


        public clsLocalDrivingLicenseApplication()
        {
            this.localDrivingLicenseApplicationID = -1;
            this.licenseClassID = -1;
            mode = enMode.addNew;
        }


        private clsLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID ,
            int applicationID , int applicationPersonID , DateTime applicationDate , 
            int applicationTypeID , enApplicationStatus applicationStatus , DateTime lastStatusDate,
            int paidFees , int createdByUserID , int licenseClassID)
        {
            this.ApplicationID = applicationID;
            this.localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.applicatinoPersonID = applicationPersonID;
            this.applicationDate = applicationDate;
            this.applicatinoTypeID = applicationTypeID;
            this.applicationStatus = applicationStatus;
            this.lastStatusDate = lastStatusDate;
            this.paidFees = paidFees;
            this.createdByUserID = createdByUserID;
            this.licenseClassID = licenseClassID;
            this.mode = enMode.update;
            this.lisenceClass = clsLisenceClass.findLisenceClassByID(licenseClassID);
        }


        public bool addNewLocalDrivingLicenseApplication()
        {
            this.localDrivingLicenseApplicationID =  clsLocalDrivinglicenseApplicationData.addNewLocalDrivinglicenseApplication(
                base.applicationID , this.licenseClassID);
            return this.localDrivingLicenseApplicationID != -1;
        }

        public bool updateLocalDrivingLicenseApplication()
        {

            return clsLocalDrivinglicenseApplicationData.updateLocalDrivingLiceseApplication(
               this.localDrivingLicenseApplicationID ,base.applicationID ,licenseClassID );
        }


        public static clsLocalDrivingLicenseApplication findLocalDrivingLicneseApplicationByID(
            int loaclDrivingLicenseApplicationID)

        {
            int applicationID = -1 , licenseClassID = -1;

            bool isFound = clsLocalDrivinglicenseApplicationData.findLocalDrivingLicenseApplicationByItsID(
                loaclDrivingLicenseApplicationID, ref applicationID, ref licenseClassID);

            if (isFound)
            {
                clsApplication application = clsApplication.findApplicationByID(applicationID);

                return new clsLocalDrivingLicenseApplication(loaclDrivingLicenseApplicationID ,
                    application.applicationID ,
                  application.applicatinoPersonID , application.applicationDate,
                    application.applicatinoTypeID , application.applicationStatus,
                    application.lastStatusDate , application.paidFees,
                    application.createdByUserID , licenseClassID);
            }
            else
            {

                return null;
            }
        }

        public static clsLocalDrivingLicenseApplication
            findLocalDrivingLicenseApplicationByApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1, licenseClassID = -1;

            bool isFound = clsLocalDrivinglicenseApplicationData
                .findLocalDrivingLicenseApplicationByApplicationID(
                applicationID , ref localDrivingLicenseApplicationID , ref licenseClassID);

            if (isFound)
            {
                clsApplication application = clsApplication.findApplicationByID(applicationID);

                return new clsLocalDrivingLicenseApplication(localDrivingLicenseApplicationID,
                   applicationID, application.applicationID, application.applicationDate,
                   application.applicatinoTypeID, application.applicationStatus,
                   application.lastStatusDate, application.paidFees,
                   application.createdByUserID, licenseClassID);
            }
            else
            {
                return null;
            }


        }

        public bool save()
        {

            base.mode = (clsApplication.enMode)this.mode;
            if (!base.save())
            {
                return false;
            }

            switch (this.mode)
            {
                case enMode.addNew:
                    if (addNewLocalDrivingLicenseApplication())
                    {
                        this.mode = enMode.update;
                        base.mode = (clsApplication.enMode)this.mode;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.update:
                    return updateLocalDrivingLicenseApplication();
            }

            return false;


        }


        public static DataTable getAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivinglicenseApplicationData.getAllLocalDrivinglicenseApplications();
        }

        public bool delete()
        {
            bool isThisDeleted = false;
            bool isBaseDeleted = false;

            isThisDeleted = clsLocalDrivinglicenseApplicationData
                .deleteLocalDrivingLicenseApplication(this.localDrivingLicenseApplicationID);

            if (!isThisDeleted)
            {
                return false;
            }


            isBaseDeleted = clsApplication.dalete(ApplicationID);

            return isBaseDeleted;   
            
        }





    }

}























