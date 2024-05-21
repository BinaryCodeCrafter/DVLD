using DVLD_DataAccess;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsLisenceClass
    {



        public enum enMode { addNew = 0, update = 1 };

        public enMode _mode = enMode.addNew;

        public int _id { get; set; }
        public string _className { get; set; }
        public string _classDescription { get; set; }
        public int _minimumAllowedAge { get; set; }
        public int _defualtValidityLenght { get; set; }
        public int _classFees { get; set; }



        public clsLisenceClass()
        {
            this._classFees = 0;
            this._minimumAllowedAge = 0;
            this._className = "";
            this._classDescription = "";
            this._id = -1;
            this._mode = enMode.addNew;
        }

        private clsLisenceClass(int id, string className, string classDescription,
                                int minimumAllowedAge, int defaultValidityLenght,
                                int classFees)
        {
            this._id = id;
            this._className = className;
            this._classDescription = classDescription;
            this._minimumAllowedAge = minimumAllowedAge;
            this._defualtValidityLenght = defaultValidityLenght;
            this._classFees = classFees;
            this._mode = enMode.update;

        }



        public static clsLisenceClass findLisenceClassByID(int id)
        {
            string className = "", classDescription = "";
            int minimumAllowedAge = 0, defaultValidityLenght = 0, classFees = 0;

            if (clsLicenseClassesData.findLicenseClassByID(id, ref className, ref classDescription,
                ref minimumAllowedAge, ref defaultValidityLenght, ref classFees))
            {
                return new clsLisenceClass(id, className, classDescription, minimumAllowedAge,
                    defaultValidityLenght, classFees);
            }
            else
            {
                return null;
            }
           

        }
        

        public bool updateLisenceClass(int id , string className , string classDescription,
                                        int minimumAllowedAge , int defaultValidityLenght ,
                                        int classFees)
        {
            return clsLicenseClassesData.updateLicenseClass(id, className, classDescription,
                                        minimumAllowedAge, defaultValidityLenght, classFees);
        }

        public DataTable getAllLiseceClasses()
        {
            return clsLicenseClassesData.getAllLicenseClasses();
        }


        public static clsLisenceClass findLicenseByName(string className)
        {
            int id = -1, minimumAllowedAge = 0, defaultValidityLenght = 0 , classFees = 0;
            string classDescription = "";

            if(clsLicenseClassesData.findLicenseClassByName(ref id , className , ref classDescription,
                ref minimumAllowedAge, ref defaultValidityLenght , ref classFees))
            {
                return new clsLisenceClass(id , className , classDescription , minimumAllowedAge,
                            defaultValidityLenght , classFees);
            }
            else
            {
                return null;
            }
        }


    }
}








