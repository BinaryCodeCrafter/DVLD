using DVLD.Properties;
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

namespace DVLD.Test.Controls
{
    public partial class ctrlScheduleTest : UserControl
    {
        public ctrlScheduleTest()
        {
            InitializeComponent();
        }


        private clsTestType.enTestType _testTypeID = clsTestType.enTestType.StreetTest;

        public clsTestType.enTestType testTypeID
        {
            get { return _testTypeID; }
            set {

                _testTypeID = value;

                switch (_testTypeID) {
                    case clsTestType.enTestType.VisionTest:
                        {
                            gbTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            gbTestType.Text = "Streen Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;
                        }
                    case clsTestType.enTestType.WrittenTest:
                        {
                            gbTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                }


            }
        }
    }
}
