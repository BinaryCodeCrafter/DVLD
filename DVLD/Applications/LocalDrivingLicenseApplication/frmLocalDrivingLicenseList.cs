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
    public partial class frmLocalDrivingLicenseList : Form
    {

        DataTable data;
        public frmLocalDrivingLicenseList()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicenseList_Load(object sender, EventArgs e)
        {
            refreshData();
        }

        private void refreshData()
        {
            data = clsLocalDrivingLicenseApplication.getAllLocalDrivingLicenseApplications();
            dgvLocalDrivingLicenseApplications.DataSource = data;
            lblRecords.Text = dgvLocalDrivingLicenseApplications.Rows.Count.ToString();

            if (dgvLocalDrivingLicenseApplications.Rows.Count > 0)
            {


                dgvLocalDrivingLicenseApplications.Columns[0].HeaderText = "L.D.L.AppID";
                dgvLocalDrivingLicenseApplications.Columns[0].Width = 120;

                dgvLocalDrivingLicenseApplications.Columns[1].HeaderText = "Driving Class";
                dgvLocalDrivingLicenseApplications.Columns[1].Width = 300;

                dgvLocalDrivingLicenseApplications.Columns[2].HeaderText = "National No.";
                dgvLocalDrivingLicenseApplications.Columns[2].Width = 150;

                dgvLocalDrivingLicenseApplications.Columns[3].HeaderText = "Full Name";
                dgvLocalDrivingLicenseApplications.Columns[3].Width = 350;

                dgvLocalDrivingLicenseApplications.Columns[4].HeaderText = "Application Date";
                dgvLocalDrivingLicenseApplications.Columns[4].Width = 170;

                dgvLocalDrivingLicenseApplications.Columns[5].HeaderText = "Passed Tests";
                dgvLocalDrivingLicenseApplications.Columns[5].Width = 150;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
         frmAddUpdateLocalDrivingLicenseApplication form = new frmAddUpdateLocalDrivingLicenseApplication();
            form.onclose += refreshData;
            form.ShowDialog();
        }
    }
}
