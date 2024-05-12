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

namespace DVLD.Users
{
    public partial class frmListUsers : Form
    {

        private static DataTable allUsers;


        public frmListUsers()
        {
            InitializeComponent();
        }


        private void refreshUsers()
        {
            allUsers = clsUser.getAllUsers();
            dataGridView1.DataSource = allUsers;
 
        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            allUsers = clsUser.getAllUsers();
            dataGridView1.DataSource = allUsers;
            cbFilter.SelectedIndex = 1;
            cbFilter2.SelectedIndex = 1;
            txtRecords.Text = allUsers.Rows.Count.ToString();


            dataGridView1.Columns[0].HeaderText = "User ID";
            dataGridView1.Columns[0].Width = 110;

            dataGridView1.Columns[1].HeaderText = "Person ID";
            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[2].HeaderText = "Full Name";
            dataGridView1.Columns[2].Width = 350;

            dataGridView1.Columns[3].HeaderText = "UserName";
            dataGridView1.Columns[3].Width = 120;

            dataGridView1.Columns[4].HeaderText = "Is Active";
            dataGridView1.Columns[4].Width = 120;




        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUserInfo form = new frmUserInfo(id);
            form.ShowDialog();
            refreshUsers();
        }
    }
}
