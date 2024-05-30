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

namespace DVLD.Applications
{
    public partial class ctrlApplicationBasicInfo : UserControl
    {

        int applicationID;
        clsApplication application;

        public ctrlApplicationBasicInfo(int id)
        {
            InitializeComponent();
            this.applicationID = id;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlApplicationBasicInfo_Load(object sender, EventArgs e)
        {
            application = clsApplication.findApplicationByID(applicationID);
            if(application == null)
            {
                MessageBox.Show("Application Not Found!");
                return;
            }

            lblApplicant.Text = application.person.personID.ToString();
            lblApplicationID.Text = application.applicationID.ToString();
            lblCreatedByUser.Text = application.createdByUserID.ToString();
            lblDate.Text = application.applicationDate.ToString();
            lblFees.Text = application.paidFees.ToString();
            lblStatusDate.Text = application.lastStatusDate.ToString();


        }
    }
}
