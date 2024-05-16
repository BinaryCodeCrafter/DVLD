using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmAddUpdataUser : Form
    {

        enum enMode { addNew = 0 , update = 1}
        enMode mode = enMode.addNew;

        private int userID = -1;
        private clsUser user;


        public frmAddUpdataUser()
        {
            InitializeComponent();
            this.mode = enMode.addNew;
        }
        
        public frmAddUpdataUser(int userID)
        {
            InitializeComponent();
            this.mode = enMode.update;
            this.userID = userID;
        }

        private void frmAddUpdataUser_Load(object sender, EventArgs e)
        {
            setUpDefualtData();
            if(mode == enMode.update)
            {
                loadData();
            }
        }

        private void setUpDefualtData()
        {
            if(mode == enMode.addNew)
            {
                lblLabel.Text = "Add A New User";
                this.Text = "Add A New User";
                user = new clsUser();
                tpLoginInfo.Enabled = false;
                ctrlPersonCardWithFilter1.Focus();

            }
            else
            {
                lblLabel.Text = "Update User";
                this.Text = "Update User";
                loadData();
            }
        }

        private void loadData()
        {
            user = clsUser.findByUserID(userID);
            ctrlPersonCardWithFilter1.filterEnabled = false;

            lblUserID.Text = user.userID.ToString();
            txtUserName.Text = user.userName;
            txtPassword.Text = user.password;
            txtConfirmPassword.Text = user.password;
            cbIsActive.Checked = user.isActive;
            ctrlPersonCardWithFilter1.loadPersonInfo(user.personID);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password fields must match!");
                return;
            }
            user.userName = txtUserName.Text;
            user.password = txtPassword.Text;
            user.isActive = cbIsActive.Checked;

            if (user.save())
            {
                lblLabel.Text = "Update User";
                this.Text = "Update User";
                this.mode = enMode.update;
                lblUserID.Text = user.userID.ToString();
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
