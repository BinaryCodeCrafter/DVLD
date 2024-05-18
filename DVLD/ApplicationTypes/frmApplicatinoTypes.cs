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

namespace DVLD.ApplicationTypes
{
    public partial class frmApplicatinoTypes : Form
    {

        DataTable data;
        public frmApplicatinoTypes()
        {
            InitializeComponent();
            data = clsApplicationType.getAllApplicationTypes();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmApplicatinoTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = data;

            dataGridView1.Columns[0].HeaderText = "Application Type ID";
            dataGridView1.Columns[0].Width = 200;
 
            dataGridView1.Columns[1].HeaderText = "Application Type Title";
            dataGridView1.Columns[1].Width = 300;
            
 
            dataGridView1.Columns[2].HeaderText = "Application Type ID";
            dataGridView1.Columns[2].Width = 200;

            lblRecords.Text = data.Rows.Count.ToString();
            
            
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmUpdateApplicationType form = new frmUpdateApplicationType(id);

            form.ShowDialog();

            dataGridView1.DataSource = clsApplicationType.getAllApplicationTypes();
        }
    }
}









