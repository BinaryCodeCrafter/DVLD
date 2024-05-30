using DVLD.Applications.LocalDrivingLicenseApplication;
using DVLD.ApplicationTypes;
using DVLD.GlobalClasses;
using DVLD.Login;
using DVLD.People;
using DVLD.TestTypes;
using DVLD.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmMain : Form
    {
        frmLogIn frmlogin;

        public event Action onClose;

      
        public frmMain(frmLogIn frmlogin)
        {
            InitializeComponent();
            this.frmlogin = frmlogin;
        }

       
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmListPeople peopleList = new frmListPeople();
            peopleList.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmListUsers form = new frmListUsers();

            form.ShowDialog();
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.currentUser = null;
            this.frmlogin.Show();
            this.Close();
        }


        private void onMainClose(object sender, FormClosingEventArgs e)
        {
            if (!(new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close")))
            {
                onClose?.Invoke();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.FormClosing += onMainClose;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmUserInfo form = new frmUserInfo(clsGlobal.currentUser.userID);
            form.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            frmChangePassword form = new frmChangePassword(clsGlobal.currentUser.userID);
            form.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmApplicatinoTypes form = new frmApplicatinoTypes();
            form.ShowDialog();
        }

        private void manageApplicationsTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTestTypesList form = new frmTestTypesList();
            form.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseList form = new frmLocalDrivingLicenseList();
            form.ShowDialog();
        }
    }
}
