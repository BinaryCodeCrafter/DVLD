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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmAddUpdataUser form = new frmAddUpdataUser(id);
            form.ShowDialog();
            refreshUsers();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddUpdataUser form = new frmAddUpdataUser();
            form.ShowDialog();
            refreshUsers();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmChangePassword form = new frmChangePassword(id);
            form.ShowDialog();
            refreshUsers();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            if(cbFilter.Text == "Is Active") {
                cbFilter2.Visible = true;
                txtFilter.Visible = false;
            }
            else
            {
            txtFilter.Visible = cbFilter.Text != "None";

            txtFilter.Text = "";
 
            }


        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                allUsers.DefaultView.RowFilter = "";
                txtRecords.Text =dataGridView1.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "UserName")
                //in this case we deal with numbers not string.
                allUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                allUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            txtRecords.Text = allUsers.Rows.Count.ToString();
        }

        private void cbFilter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbFilter2.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                allUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                allUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

                txtFilter.Text = allUsers.Rows.Count.ToString();
        }
    }
}
