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

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class ctrlLicenseApplicationInfo : UserControl
    {

        int id;
        clsLocalDrivingLicenseApplication LDLapplication;
        public ctrlLicenseApplicationInfo()
        {
            InitializeComponent();
        }

        private void ctrlApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void loadData(int id)
        {
            this.id = id;
            LDLapplication = clsLocalDrivingLicenseApplication.findLocalDrivingLicneseApplicationByID(id);
            if(LDLapplication == null)
            {
                MessageBox.Show("application not found!");
                return;
            }

            fillData();
        }

        private void fillData()
        {

            lblLocalDrivingLicenseApplicationID.Text = LDLapplication.localDrivingLicenseApplicationID.ToString();
            lblAppliedFor.Text = LDLapplication.lisenceClass._className;
            ctrlApplicationBasicInfo1.loadApplicationInfo(LDLapplication.ApplicationID);
        }


    }
}
