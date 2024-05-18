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

namespace DVLD.TestTypes
{
    public partial class frmTestTypesList : Form
    {

        private DataTable data;

        public frmTestTypesList()
        {
            InitializeComponent();
        }

        private void frmTestTypesList_Load(object sender, EventArgs e)
        {
            data = clsTestType.getAllTestTypes();
            dataGridView1.DataSource = data;
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();

            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[0].Width = 120;

            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[1].Width = 200;

            dataGridView1.Columns[2].HeaderText = "Description";
            dataGridView1.Columns[2].Width = 400;

            dataGridView1.Columns[3].HeaderText = "Fees";
            dataGridView1.Columns[3].Width = 100;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmUpdateTestType form = new frmUpdateTestType(id , refresh);
            form.ShowDialog();
        }

        private void refresh()
        {
            data = clsTestType.getAllTestTypes();
            dataGridView1.DataSource = data;
            lblRecordsCount.Text = dataGridView1.Rows.Count.ToString();
        }
    }
}
