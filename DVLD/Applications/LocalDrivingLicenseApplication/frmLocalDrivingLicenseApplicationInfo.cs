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
    public partial class frmLocalDrivingLicenseApplicationInfo : Form
    {

        int id;
        clsLocalDrivingLicenseApplication LDLapplication;
        public frmLocalDrivingLicenseApplicationInfo(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            LDLapplication = clsLocalDrivingLicenseApplication.findLocalDrivingLicneseApplicationByID(id);
            ctrlLicenseApplicationInfo1.loadData(id);
        }


    }
}
