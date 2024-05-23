using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsApplication
    {

        enum enMode { addNew = 0, update = 1 }
        enMode mode = enMode.addNew;

        public enum enApplicationType
        {
            newDrivingLisence = 1, renewDrivingLisence = 2,
            replaceLostDrivingLisence = 3, replaceDamagedDrivingLisence = 4,
            releaseDetainedDrivingLisence = 5, newInternationalLisence = 6,
            reTakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Canacelled = 2, Completed = 3 };



        public enApplicationType applicationType { get; set; }
        public enApplicationStatus applicationStatus { get; set; }
        public int applicationID { get; set; }
        public int applicatinoPersonID { get; set; }
        public int applicatinoTypeID { get; set; }
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

        public clsPerson person;

        public clsApplication()
        {
            this.applicationID = -1;
            this.applicatinoPersonID = -1;
            this.applicationDate = DateTime.Now;
            this.lastStatusDate = DateTime.Now;
            this.applicatinoTypeID = -1;
            this.applicationStatus = enApplicationStatus.New;
            this.paidFees = 0;
            this.createdByUserID = -1;
            this.mode = enMode.addNew;
        }



        private clsApplication(int applicationID, int applicationPersonID, int applicationTypeID,
                        DateTime applicationDate, enApplicationStatus status,
                        DateTime lastStatusDate, int paidFees, int userID)
        {
            this.applicationID = applicationID;
            this.applicatinoPersonID = applicationPersonID;
            this.applicationDate = applicationDate;
            this.lastStatusDate = lastStatusDate;
            this.applicationStatus = status;
            this.createdByUserID = userID;
            this.mode = enMode.update;
            this.applicatinoTypeID = applicatinoTypeID;
            this.applicationTypeInfo = clsApplicationType.findApplicationTypeByID(applicationTypeID);
            this.person = clsPerson.find(applicationPersonID);
            this.user = clsUser.findUserByPersonID(userID);
            this.mode = enMode.update;
        }


        public static DataTable getAllApplications()
        {
            return clsApplicationsData.getAllApplications();
        }

        public static clsApplication findApplicationByID(int applicationID)
        {
            int applicationTypeID = -1, applicationPersonID = -1;
            DateTime applicationDate = DateTime.Now, lastStatusDate = DateTime.Now;
            int applicationStatus = 1, paidFees = 0, createdByUserID = -1;

            if (clsApplicationsData.findApplicationByID(applicationID, ref applicationPersonID,
                ref applicationDate, ref applicationTypeID, ref applicationStatus,
                ref lastStatusDate, ref paidFees, ref createdByUserID))
            {
                return new clsApplication(applicationID, applicationPersonID, applicationTypeID,
                    applicationDate, (enApplicationStatus)applicationStatus, lastStatusDate, paidFees, createdByUserID);
            }
            else
            {
                return null;
            }
        }

        public bool cancel()
        {
            return clsApplicationsData.updateStatus(applicationID, 2);
        }

        public bool setComplete()
        {
            return clsApplicationsData.updateStatus(applicationID, 3);
        }
        public static bool isApplicationExistByApplicationID(int id)

        {
            return clsApplicationsData.isApplicationExistByApplicationID(id);
        }

        private bool addNewApplication()
        {
            this.applicationID = clsApplicationsData.addNewApplication(applicatinoPersonID, applicationDate,
                 applicatinoTypeID, (int)applicationStatus, lastStatusDate, paidFees,
                 createdByUserID);

            return this.applicationID != -1;
        }

        private bool updateApplication()
        {
            return clsApplicationsData.updateApplication(applicationID, applicatinoPersonID,
                applicationDate, applicatinoTypeID, (int)applicationStatus, lastStatusDate,
                paidFees, createdByUserID);
        }


        public bool save()
        {
            if (this.mode == enMode.addNew)
            {
                if (addNewApplication())
                {
                    this.mode = enMode.update;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return this.updateApplication();
            }
        }

        public static bool dalete(int id)
        {
            return clsApplicationsData.deleteApplication(id);
        }

        public static clsApplication getActiveApplication(int personID, int applicationTypeID)
        {
            int activeAppID = clsApplicationsData.getActiveApplicationID(personID, applicationTypeID);
            clsApplication activeApp = clsApplication.findApplicationByID(activeAppID);
            if (activeApp != null)
            {
                return activeApp;
            }
            else
            {
                return null;
            }

        }

        public static int getActiveApplicationID(int personID, int applicationTypeID)
        {
            return clsApplicationsData.getActiveApplicationID(personID, applicationTypeID);
        }


        public static bool doesPersonHaveActiveApplicationID(int personID, int applicationTypeID)
        {
            return clsApplicationsData.deosPersonHaveActiveApplicationID(personID , applicationTypeID);
        }

        public static int getActiveApplicationIDForLisenceClass
            (int personID , int applicationTypeID ,int lisenceClass)
        {
            return clsApplicationsData.getActiveApplicationIDForLiseceClass(personID , applicationTypeID , lisenceClass);   
        }
        
        public bool updateStatus(int status) {
            return clsApplicationsData.updateStatus(this.applicationID , status);
        }

    }
}











