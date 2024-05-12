using DVLD.Properties;
using DVLD_Business;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class ctrlPersonCard : UserControl
    {


        public clsPerson person;

        public int personID = -1;


        public ctrlPersonCard()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void loadPerosnData(int personID)
        {
            person = clsPerson.find(personID);

            if(person == null)
            {

                resetPersonInfo();
                MessageBox.Show("No Person with PersonID = " + personID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fillPersonInfo();
        }

        public void loadPerosnData(string nationalNo)
        {
            person = clsPerson.find(nationalNo);

            if(person == null)
            {

                resetPersonInfo();
                MessageBox.Show("No Person with nationalNo = " + nationalNo, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fillPersonInfo();
        }


      

        public void resetPersonInfo()
        {
            lblName.Text ="";
            lblEmail.Text = "";
            lblNationalNo.Text = "";
            lblPersonID.Text = "";
            lblPhone.Text = "";
            lblAddress.Text = "";
        }

        private void fillPersonInfo()
        {
            personID = person.personID;
            lblName.Text = person.fullName;
            lblEmail.Text = person.email;
            lblNationalNo.Text = person.nationalNo;
            lblPersonID.Text = person.personID.ToString();
            lblPhone.Text = person.phone;
            lblAddress.Text = person.address;
            lblGendor.Text = person.gendor.ToString();
            lblCountry.Text = person.coutnry.countryName;
            lblDataOfBirth.Text = person.dateOfBirth.ToString();
            loadPicture();

        }

        private void loadPicture()
        {
            if(person.gendor == 0)
            {
                pictureBox1.Image = Resources.Female_512;
            }
            else
            {
                pictureBox1.Image = Resources.Male_512;
            }

            string imagePath = "";
            if(person.imagePath != "")
            {

                if (File.Exists(imagePath))
                {
                    pictureBox1.ImageLocation = imagePath;
                }
                else
                {
                  //  MessageBox.Show("Could not find this image: = " + imagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson(personID);

            form.ShowDialog();

            loadPerosnData(personID);
        }
    }
}
