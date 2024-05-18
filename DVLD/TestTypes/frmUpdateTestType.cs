using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.TestTypes
{
    public partial class frmUpdateTestType : Form
    {

        public delegate void onCloseDelegate();
        onCloseDelegate onClose;

        private int testTypeID;
        private clsTestType testType;
        public frmUpdateTestType(int id , onCloseDelegate onclose)
        {
            InitializeComponent();
            testTypeID = id;
            this.onClose = onclose;
        }

        private void frmUpdateTestType_Load(object sender, EventArgs e)
        {
            testType = clsTestType.findTestTypeByID(testTypeID);

            txtDescription.Text = testType.description;
            txtFees.Text = testType.fees.ToString();
            txtTitle.Text= testType.title;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            testType.description = txtDescription.Text;
            testType.title = txtTitle.Text;
            testType.fees =int.Parse(txtFees.Text);

            if (testType.updateTestType())
            {
                MessageBox.Show("Done");
                onClose?.Invoke();
            }
            else
            {
                MessageBox.Show("something went wrong");
            }
        }
    }
}
