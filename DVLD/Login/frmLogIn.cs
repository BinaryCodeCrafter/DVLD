using DVLD.GlobalClasses;
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

namespace DVLD.Login
{
    public partial class frmLogIn : Form
    {
        public frmLogIn()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            clsUser user = clsUser.findByUserNameAndPassword(userName, password);

            if (user != null)
            {


                if (chkRememberMe.Checked)
                {
                    clsGlobal.remmemberCredential(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    clsGlobal.remmemberCredential("", "");
                }


                if (!user.isActive)
                {
                    MessageBox.Show("Error: User Is Not Active", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                clsGlobal.currentUser = user;
                frmMain form = new frmMain(this);
                form.onClose += onClose;
                this.Hide();
                form.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error: Incorrect Username Or Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void onClose()
        {
            this.Close();
        }

        private void frmLogIn_Load(object sender, EventArgs e)
        {
            string userName = "", password = "";

            if(clsGlobal.getStoredCredential(ref userName ,ref password))
            {
                this.txtUserName.Text = userName;
                this.txtPassword.Text = password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = true;
            }

        }
    }
}
