using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsUser
    {
        public enum enMode { addNew = 0, upadte = 1 }

        public enMode mode = enMode.addNew;

        public int userID { get; set; }

        public string userName { get; set; }

        public string password { get; set; }
        public bool isActive { get; set; }
        public int personID { get; set; }

        public clsPerson person;

        public clsUser()
        {
            this.userID = -1;
            this.userName = "";
            this.password = "";
            this.mode = enMode.addNew;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            this.userID = userID;
            this.personID = personID;
            this.password = password;
            this.userName = userName;
            this.mode = enMode.upadte;
            this.isActive = isActive;
            this.person = clsPerson.find(personID);
        }

        private bool addNewUser()
        {
            this.userID = clsUserData.addNewUser(this.personID, this.userName, this.userName,
                isActive);

            return this.userID != -1;
        }

        private bool updateUser()
        {
            return clsUserData.updateUser(this.userID, this.personID, this.userName, this.password,
                this.isActive);
        }


        public bool save()
        {
            switch (this.mode)
            {
                case enMode.addNew:
                    if (addNewUser())
                    {
                        this.mode = enMode.upadte;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.upadte:
                    return updateUser();
            }

            return false;
        }


        public static clsUser findByUserID(int userID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;

            bool isFound = clsUserData.getUserInfoByUserID(userID, ref personID, ref userName, ref password,
                                                ref isActive);

            if (isFound)
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }
        }

        public static clsUser findUserByPersonID(int personID)
        {
            int userID = -1;
            string userName = "", password = "";
            bool isActive = false;

            bool isFound = clsUserData.getUserInfoByPersonID(personID, ref userID,
                                        ref userName, ref password, ref isActive);

            if (isFound)
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }

        }

        public static clsUser findByUserNameAndPassword(string userName, string password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;

            bool isFound = clsUserData.getUserInfoByUserNameAndPassword(userName, password,
                                    ref userID, ref personID, ref isActive);
            if (isFound)
            {
                return new clsUser(userID, personID, userName, password, isActive);
            }
            else
            {
                return null;
            }

        }


        public static DataTable getAllUsers()
        {
            return clsUserData.getAllUsers();
        }

        public static bool deleteUser(int userID)
        {
            return clsUserData.deleteUser(userID);
        }

        public static bool isUserExist(int userID)
        {
            return clsUserData.isUserExist(userID);
        }

        public static bool isUserExist(string userName)
        {
            return clsUserData.isUserExist(userName);
        }

        public static bool isUserExistForUPersonID(int personID)
        {
            return clsUserData.isUserExistForPersonID(personID);
        }


    }
}
