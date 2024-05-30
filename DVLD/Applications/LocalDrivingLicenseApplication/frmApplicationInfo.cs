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
    public partial class frmApplicationInfo : Form
    {

        int applicationID;
        clsApplication application;
        public frmApplicationInfo(int id)
        {
            InitializeComponent();
            applicationID = id;
        }

        private void frmApplicationInfo_Load(object sender, EventArgs e)
        {
            application = clsApplication.findApplicationByID(applicationID);
            if(application == null)
            {
                MessageBox.Show("Application Not Found");
                this.Close();
                return;
            }

            ctrlApplicationBasicInfo1.loadApplicationInfo(applicationID);
        }
    }
}
