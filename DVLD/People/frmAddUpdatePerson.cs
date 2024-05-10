using DVLD.Properties;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {

        public event Action<object, int> DataBackEventHandler;

        public enum enMode { addNew = 0 , update = 1};

        public enum enGendor { male = 0 , female = 1};

        private enMode mode;
        private int _personID;
        private clsPerson _person;

        public frmAddUpdatePerson(int id)
        {
            InitializeComponent();
            this.mode = enMode.update;
            this._personID = id;
        }

        public frmAddUpdatePerson()
        {
            InitializeComponent();
            this.mode = enMode.addNew;
        }


        private void resultValues()
        {
            fillComboBoxWithCoutries();

            if(mode == enMode.update)
            {
                lblLabel.Text = "Update Person";
            }
            else
            {
                lblLabel.Text = "Add New Person";
            }

            if (this.radioButton1.Checked)
            {
                this.pbProfile.Image = Resources.Male_512;
            }
            else
            {
                this.pbProfile.Image = Resources.Female_512;
            }


            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            dateTimePicker1.Value = dateTimePicker1.MaxDate;


            dateTimePicker1.MinDate = DateTime.Now.AddYears(-100);

        }


        private void fillComboBoxWithCoutries()
        {
            DataTable countris = clsCountry.getAllCounties();

            foreach(DataRow row in countris.Rows)
            {
                this.countryConboBox.Items.Add(row["CountryName"]);
            }

        }


        private void loadData()
        {
            _person = clsPerson.find(_personID);

            if(_person == null )
            {
                MessageBox.Show("No Person with ID = " + _personID, "Person Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            txtFirstName.Text = _person.firstName;
            txtSecondName.Text = _person.secondName;
            txtThirdName.Text = _person.thirdName;
            txtLastName.Text = _person.lastName;
            txtEmail.Text = _person.email;
            txtAddress.Text = _person.address;
            txtPhone.Text = _person.phone;
            txtNationalNo.Text = _person.nationalNo;
            txtID.Text = _person.personID.ToString();
            //dateTimePicker1.Value = _person.dateOfBirth;

            if(_person.gendor == 1)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            string country = clsCountry.find(_person.coutnry.countryName).countryName;

            countryConboBox.SelectedText = country;


        }


        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {
            resultValues();

            if(mode == enMode.update)
            {
                loadData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(_person == null)
            {
                _person = new clsPerson();

            }

            _person.firstName = txtFirstName.Text;
            _person.secondName = txtSecondName.Text;
            _person.thirdName= txtThirdName.Text;
            _person.lastName = txtLastName.Text;
            _person.address = txtAddress.Text;
            _person.email = txtEmail.Text;
            _person.phone = txtPhone.Text;
            _person.nationalNo = txtNationalNo.Text;
            _person.nationanlityCountryID = countryConboBox.SelectedIndex;
            _person.dateOfBirth = dateTimePicker1.Value;
            if (_person.save())
            {
                DataBackEventHandler?.Invoke(this, _person.personID);
                MessageBox.Show("done");
            }
            else{
                MessageBox.Show("some went wrong");
            }

            this.lblLabel.Text = "Update Person";
            this.mode = enMode.update;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
