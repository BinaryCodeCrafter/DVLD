using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        
        public int testAppointmentID {  get; set; }
        public int testTypeID {  get; set; }
        public int localDrivingLicenseApplicationID { get; set; }
        public DateTime appointmentDate { get; set; }
        public int paidFees {  get; set; }
        public int createdByUserID { get; set; }
        public bool isLocked {  get; set; }
        public int retakeTestApplicationID { get; set; }

        public clsApplication retakeTestApplicationInfo;

        public int testID
        {
            get { return getTestID(); }
        }



        public enum enMode { addNew = 0 , update= 1}

        public enMode mode = enMode.addNew;


        public clsTestAppointment()
        {
            testAppointmentID = -1;
            testTypeID = -1;
            localDrivingLicenseApplicationID = -1;
            appointmentDate = DateTime.Now;
            paidFees = 0;
            createdByUserID = -1;
            isLocked = false;
            retakeTestApplicationID = -1;
            mode = enMode.addNew;
        }

        private clsTestAppointment(int testAppointmentID, int testTypeID, int localDrivingLicenseApplicationID, DateTime appointmentDate, int paidFees, int createdByUserID, bool isLocked, int retakeTestApplicationID)
        {
            this.testAppointmentID = testAppointmentID;
            this.testTypeID = testTypeID;
            this.localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            this.appointmentDate = appointmentDate;
            this.paidFees = paidFees;
            this.createdByUserID = createdByUserID;
            this.isLocked = isLocked;
            this.retakeTestApplicationID = retakeTestApplicationID;
            this.mode = enMode.update;
            this.retakeTestApplicationInfo = clsApplication.findApplicationByID(retakeTestApplicationID);
        }




        public static clsTestAppointment findTestAppointmentByID(int testAppointmentID)
        {
            int testTypeID = -1 , localDrivingLicnseApplicationID = -1 , paidFees = 0 , createdByUserID = -1 ,
                retakeTestApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            bool isLocked = false;

            if(clsTestAppointmentData.findTestAppointmentByID(testAppointmentID , ref testTypeID ,
                ref localDrivingLicnseApplicationID , ref appointmentDate ,ref paidFees , ref createdByUserID,
                ref isLocked , ref retakeTestApplicationID))
            {
                return new clsTestAppointment(testAppointmentID , testTypeID , localDrivingLicnseApplicationID ,
                    appointmentDate ,paidFees , createdByUserID , isLocked , retakeTestApplicationID);
            }
            else
            {
                return null;
            }

        }

        public static clsTestAppointment getLastAppointmentTest(int localDrivingLicenseApplicationID ,
            int testTypeID)
        {

            int testAppointmentID = -1 , paidFees = 0 , createdByUserID = -1 ,
                retakeTestApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            bool isLocked = false;

            if(clsTestAppointmentData.getLastAppointmentTest(localDrivingLicenseApplicationID , testTypeID ,
                ref testAppointmentID , ref appointmentDate , ref paidFees , ref createdByUserID ,
                ref isLocked ,ref retakeTestApplicationID))
            {
                return new clsTestAppointment(testAppointmentID , testTypeID , localDrivingLicenseApplicationID ,
                    appointmentDate , paidFees , createdByUserID ,isLocked , retakeTestApplicationID);
            }
            else
            {
                return null;
            }

        }



        public static DataTable getAllAppointments()
        {
            return clsTestAppointmentData.getAllAppointments();
        }


        public static DataTable getApplicationTestAppointmentsPerTest(int localDrivingLicenseApplicationID , 
            int testTypeID)
        {

            return clsTestAppointmentData.getApplicationTestAppointmentsPerTestTest(localDrivingLicenseApplicationID,
                testTypeID);
        }


        private bool addNew()
        {
            testAppointmentID = clsTestAppointmentData.addNewTestAppointment(testTypeID , localDrivingLicenseApplicationID,
                appointmentDate , paidFees , createdByUserID , isLocked , retakeTestApplicationID);

            return this.testAppointmentID != -1;
        }

        private bool update()
        {
            return clsTestAppointmentData.updateTestAppointment( testAppointmentID ,testTypeID , localDrivingLicenseApplicationID,
                appointmentDate , paidFees , createdByUserID , isLocked , retakeTestApplicationID);
        }


        

        public bool save()
        {
            if(this.mode == enMode.addNew)
            {
                if (addNew())
                {
                    this.mode = enMode.update;
                    return true;
                }
                return false;
            }
            else
            {
                return update();
            }
        }

        public int getTestID()
        {
            return clsTestAppointmentData.GetTestID(testAppointmentID);
        }
    }
}












