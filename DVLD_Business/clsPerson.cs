using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsPerson
    {

        public enum enMode { addNew = 0 , update = 1};
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
            set {_imagePath = value; } 
        }




    }
}
