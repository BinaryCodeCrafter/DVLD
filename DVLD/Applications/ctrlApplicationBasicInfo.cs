using DVLD.People;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {

        int applicationID;
        clsApplication application;

        public ctrlApplicationBasicInfo()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {
       }

        public void loadApplicationInfo(int id)
        {
            this.applicationID = id;
            application = clsApplication.findApplicationByID(applicationID);
            if(application == null)
            {
                MessageBox.Show("Application Not Found!");
                return;
            }

            fillApplicationInfo();

        }


        private void fillApplicationInfo()
        {

            lblApplicant.Text = application.person.personID.ToString();
            lblApplicationID.Text = application.applicationID.ToString();
            lblCreatedByUser.Text = application.createdByUserID.ToString();
            lblDate.Text = application.applicationDate.ToString();
            lblFees.Text = application.paidFees.ToString();
            lblStatus.Text = application.applicationStatus.ToString();
            lblStatusDate.Text = application.lastStatusDate.ToString();
            lblType.Text = application.applicationTypeInfo.applicationTypeTitle;
 

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowPersonInfo form = new frmShowPersonInfo(application.applicatinoPersonID);
            form.ShowDialog();
            loadApplicationInfo(applicationID);
        }
    }
}
