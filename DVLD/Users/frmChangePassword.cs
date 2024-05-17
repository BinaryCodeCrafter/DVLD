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
using System.Windows.Forms.VisualStyles;

namespace DVLD.Users
{
    public partial class frmChangePassword : Form
    {
        private int userID;
        private clsUser user;
        public frmChangePassword(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(txtCurrentPassword.Text != user.password)
            {
                MessageBox.Show("Error: Current Password Is Not Correct", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Error: Password and Cofirm Passrod fields must match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            user.password = txtNewPassword.Text;
            if (user.save())
            {
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show("Error: Data is not saved", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.loadUserInfo(userID);
            this.user = clsUser.findByUserID(userID);
        }
    }
}
