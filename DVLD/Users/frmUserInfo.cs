using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmUserInfo : Form
    {
        private int userID;
        public frmUserInfo(int userID)
        {
            InitializeComponent();
            this.userID = userID;
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            ctrlUserInfo1.loadUserInfo(this.userID);
        }
    }
}
