using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsPerson
    {

        public enum enMode { addNew = 0, update = 1 };
        public enMode mode = enMode.addNew;

        public int personID { get; set; }

        public string firstName { get; set; }
        public string secondName { get; set; }
        public string thirdName { get; set; }
        public string lastName { get; set; }
        public string fullName { get
            { return firstName + " " + secondName + " " + thirdName + " " + lastName; }
        }

        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int nationanlityCountryID { get; set; }
        public string nationalNo { get; set; }
        public short gendor { get; set; }

        private string _imagePath;

        public string imagePath
        {
            get { return _imagePath; }
            set { _imagePath = value; }
        }

        clsCountry coutnry { get; set; }


        public clsPerson()
        {
            this.personID = -1;
            this.firstName = "";
            this.secondName = "";
            this.thirdName = "";
            this.lastName = "";
            this.imagePath = "";
            this.address = "";
            this.email = "";
            this.phone = "";
            this.dateOfBirth = DateTime.Now;
            this.gendor = 1;
            this.nationalNo = "";
            this.nationanlityCountryID = -1;

            this.mode = enMode.addNew;
        }


        private clsPerson(int PersonID, string FirstName, string SecondName, string ThirdName,
            string LastName, string NationalNo, DateTime DateOfBirth, short Gendor,
             string Address, string Phone, string Email,
            int NationalityCountryID, string ImagePath)
        {
            this.personID = PersonID;
            this.firstName = FirstName;
            this.secondName = SecondName;
            this.thirdName = ThirdName;
            this.lastName = LastName;
            this.nationalNo = NationalNo;
            this.dateOfBirth = dateOfBirth;
            this.gendor = gendor;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.nationanlityCountryID = nationanlityCountryID;
            this.imagePath = imagePath;
            this.coutnry = clsCountry.find(this.nationanlityCountryID);
            this.mode = enMode.update;
        }


        public static DataTable getAllPeople()
        {
            return clsPersonData.getAllPeople();
        }

        public static bool deletePerson(int id)
        {
            return clsPerson.deletePerson(id);
        }

        public static bool isPersonExist(int id)
        {
            return isPersonExist(id);
        }

        public static bool isPersonExist(string name)
        {
            return clsPersonData.isPersonExist(name);
        }



        private bool _addNewPerson()
        {
           this.personID = clsPersonData.addNewPerson(firstName , secondName , thirdName ,lastName ,
                nationalNo , dateOfBirth , gendor , address , phone , email , 
                nationanlityCountryID , imagePath);

            return this.personID != -1;
        }

        private bool _updatePerson()
        {
            return clsPersonData.updatePerson(personID , firstName , secondName ,
                thirdName , lastName , nationalNo , dateOfBirth , gendor , address,
                phone , email , nationanlityCountryID , imagePath);
        }



        public bool save()
        {
            if(this.mode == enMode.addNew)
            {
                if (_addNewPerson())
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
                return _updatePerson();
            }
        }

        public static clsPerson find(string nationalNo)
        {
            int personID = -1;
            string firstName = "";
            string secondNmae = "";
            string thirdName = "";
            string lastName = "";
            string address = "";
            string phone = "";
            string email = "";
            string imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int nationalityCountyID = -1;
            short gendor = 0;

            if (clsPersonData.getPersonByNationalNo(nationalNo, ref firstName, ref secondNmae,
                ref thirdName, ref lastName, ref personID ,  ref dateOfBirth,
                ref gendor, ref address, ref phone, ref email, ref nationalityCountyID,
                ref imagePath))
            {
                return new clsPerson(personID, firstName, secondNmae, thirdName,
                    lastName, nationalNo, dateOfBirth, gendor, address,
                    phone, email, nationalityCountyID, imagePath);
            }
            else
            {
                return null;
            }
           
          
            }


        public static clsPerson find(int personID)
        {
            string firstName = "";
            string secondNmae = "";
            string thirdName = "";
            string lastName = "";
            string address = "";
            string phone = "";
            string email = "";
            string nationalNo = "";
            string imagePath = "";
            DateTime dateOfBirth = DateTime.Now;
            int nationalityCountyID = -1;
            short gendor = 1;

            if (clsPersonData.getPersonById(personID, ref firstName, ref secondNmae,
                ref thirdName, ref lastName, ref nationalNo, ref dateOfBirth,
                ref gendor, ref address, ref phone, ref email, ref nationalityCountyID,
                ref imagePath))
            {
                return new clsPerson(personID, firstName, secondNmae, thirdName,
                    lastName, nationalNo, dateOfBirth, gendor, address,
                    phone, email, nationalityCountyID, imagePath);
            }
            else
            {
                return null;
            }
           
          
            }


    }



 }

