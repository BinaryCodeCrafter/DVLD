using DVLD.GlobalClasses;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {

        public delegate void onClose();

        public onClose onclose;


        public enum enMode { addNew = 0, update = 1 }
        private enMode mode;


        public int localDrivingLicenseApplicationID = -1;
        public int selectedPersonID = -1;
        public clsLocalDrivingLicenseApplication localDrivingLicenseApplication;




        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();
            mode = enMode.addNew;
        }

        public frmAddUpdateLocalDrivingLicenseApplication(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            mode = enMode.update;
            this.localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
        }

        private void fillComboBoxWithLicenseClasses()
        {
            DataTable classes = clsLisenceClass.getAllLiseceClasses();

            foreach (DataRow row in classes.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }

        private void refreshDefaultValues()
        {
            fillComboBoxWithLicenseClasses();

            if (mode == enMode.addNew)
            {
                label1.Text = "Add New Local Driving License Application";
                this.Text = "Add New Local Driving License Application";
                localDrivingLicenseApplication = new clsLocalDrivingLicenseApplication();
                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationType.findApplicationTypeByID((int)clsApplication.enApplicationType.newDrivingLisence).applicationFees.ToString();
                lblApplicationDate.Text = DateTime.Now.ToString();
                lblCreatedByUser.Text = GlobalClasses.clsGlobal.currentUser.userName;
            }
            else
            {
                label1.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
            }

        }

        private void LoadData()
        {
            localDrivingLicenseApplication = clsLocalDrivingLicenseApplication
                .findLocalDrivingLicenseApplicationByApplicationID(
                localDrivingLicenseApplicationID);

            if (localDrivingLicenseApplication == null)
            {
                MessageBox.Show("No Application with ID = " + localDrivingLicenseApplication, "Application Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }


            ctrlPersonCardWithFilter1.loadPersonInfo(localDrivingLicenseApplication.applicatinoPersonID);
            lblLocalDrivingLicebseApplicationID.Text = localDrivingLicenseApplication.localDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = localDrivingLicenseApplication.applicationDate.ToString();
            cbLicenseClass.SelectedText = cbLicenseClass.FindString(clsLisenceClass.findLisenceClassByID(localDrivingLicenseApplication.licenseClassID)._className).ToString();
            lblFees.Text = localDrivingLicenseApplication.paidFees.ToString();
            lblCreatedByUser.Text = clsUser.findByUserID(localDrivingLicenseApplication.createdByUserID).userName;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void handleDataBack(object sender, int personID)
        {
            selectedPersonID = personID;
            ctrlPersonCardWithFilter1.loadPersonInfo(personID);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show("Please Select A Person First");
                return;
            }

            tabControl1.SelectedTab = tabControl1.TabPages["Application Info"];

        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            refreshDefaultValues();

            if(mode == enMode.update)
            {
                LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            int licenseClassID  = clsLisenceClass.findLicenseByName(cbLicenseClass.Text)._id;

            int activeApplicationID = clsApplication.getActiveApplicationIDForLisenceClass(selectedPersonID, (int)clsApplication.enApplicationType.newDrivingLisence, licenseClassID);

            if(activeApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + activeApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }


            // todo check if user already have issued license of the same driving  class.

            localDrivingLicenseApplication.applicatinoPersonID = ctrlPersonCardWithFilter1.PersonID;
            localDrivingLicenseApplication.applicationDate = DateTime.Now;
            localDrivingLicenseApplication.lastStatusDate = DateTime.Now;
            localDrivingLicenseApplication.applicatinoTypeID = 1;
            localDrivingLicenseApplication.applicationStatus = clsApplication.enApplicationStatus.New;
            localDrivingLicenseApplication.paidFees = int.Parse(lblFees.Text.ToString());
            localDrivingLicenseApplication.createdByUserID = clsGlobal.currentUser.userID;
            localDrivingLicenseApplication.licenseClassID = licenseClassID;

            if (localDrivingLicenseApplication.save())
            {
                lblLocalDrivingLicebseApplicationID.Text = localDrivingLicenseApplication.localDrivingLicenseApplicationID.ToString();

                mode = enMode.update;
                label1.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void ctrlPersonCardWithFilter1_PersonSelected(object sender, int e)
        {
            selectedPersonID = e;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Enter(object sender, EventArgs e)
        {

        }

        private void frmAddUpdateLocalDrivingLicenseApplication_FormClosing(object sender, FormClosingEventArgs e)
        {
            onclose?.Invoke();
        }
    }
}
