using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.ApplicationTypes
{
    public partial class frmUpdateApplicationType : Form
    {





        private int id;
        private clsApplicationType applicationType;

        public frmUpdateApplicationType(int id)
        {
            InitializeComponent();
            this.id = id;
            
        }

        private void frmUpdateApplicationType_Load(object sender, EventArgs e)
        {
            applicationType = clsApplicationType.findApplicationTypeByID(id);

            if (applicationType == null )
            {
                MessageBox.Show($"application type with id {id} is not found");
                this.Close();
                return;
            }

            txtTitle.Text = applicationType.applicationTypeTitle;
            txtFees.Text = applicationType.applicationFees.ToString();
                

        }

        private void button1_Click(object sender, EventArgs e)
        {

            applicationType.applicationTypeTitle = txtTitle.Text;
            applicationType.applicationFees = int.Parse(txtFees.Text);

            if (applicationType.uptateFees())
            {
                MessageBox.Show("Done");
            }
            else
            {
                MessageBox.Show("something went wrong");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}














