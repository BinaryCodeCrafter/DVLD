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

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event EventHandler<int> PersonSelected;


        protected virtual void onPersonSelected(object sender ,int id)
        {
            PersonSelected?.Invoke(this, id);
        }

        private int _personID = -1;

        public int PersonID
        {
            get { return ctrlPersonCard1.personID; }
        }

        public clsPerson person
        {
            get { return ctrlPersonCard1.person; }
        }

        private bool _filterEnabed = true;

        public bool filterEnabled
        {
            get { return _filterEnabed; }
            set {

                _filterEnabed = value;
                groupBox1.Enabled = value;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }


        public void loadPersonInfo(int id)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Text = id.ToString();
            findNow();
        }

        private void findNow()
        {

            switch (comboBox1.Text) {

                case "Person ID":
                    ctrlPersonCard1.loadPerosnData(int.Parse(textBox1.Text));
                    break;


                case "Nationa No":
                    ctrlPersonCard1.loadPerosnData(textBox1.Text);
                    break;

            }

            if(PersonSelected != null && filterEnabled)
            {
                onPersonSelected(this, ctrlPersonCard1.personID);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            findNow();
        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();

            form.DataBackEventHandler += dataBack;

            form.ShowDialog();
        }

        private void dataBack(object sender, int id)
        {
            comboBox1.SelectedIndex = 1;
            textBox1.Text = _personID.ToString();
            ctrlPersonCard1.loadPerosnData(id);
        }
    }
}
