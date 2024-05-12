using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class ctrlUserInfo : UserControl
    {

        public int userID = -1;
        private clsUser user;


        public ctrlUserInfo()
        {
            InitializeComponent();
        }

        private void ctrlUserInfo_Load(object sender, EventArgs e)
        {
        }

        public void loadUserInfo(int userID)
        {
            user = clsUser.findByUserID(userID);

            if(user == null)
            {
                resetInfo();
                MessageBox.Show("No User with UserID = " + userID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            fillInfo();

        }

        private void fillInfo()
        {
            ctrlPersonCard1.loadPerosnData(user.personID);

            lblUserID.Text = user.userID.ToString();
            lblUserName.Text = user.userName;

            if(user.isActive) {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }

        private void resetInfo()
        {
            ctrlPersonCard1.resetPersonInfo();
            this.lblIsActive.Text = "";
            this.lblUserID.Text = "";
            this.lblIsActive.Text = "";
        }
    }
}
