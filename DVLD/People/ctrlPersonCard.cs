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

namespace DVLD.People
{
    public partial class ctrlPersonCard : UserControl
    {


        private clsPerson person;

        private int personID;

        public ctrlPersonCard(int personID)
        {
            InitializeComponent();
            this.personID = personID;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
