using DVLD_Business;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GlobalClasses
{
    internal static class clsGlobal
    {
        public static clsUser currentUser;

        public static bool remmemberCredential(string userName , string password)
        {
            try
            {
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDirectory + "\\data.txt";

                if(userName == "" && File.Exists(filePath)){
                    File.Delete(filePath);
                    return true;
                }

                string dataToSave = userName + "#//#" + password;

                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(dataToSave);
                    return true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("An Error Occurred : " + ex.Message);
                return false;
            }


        }
 
        public static bool getStoredCredential(ref string userName , ref string password) {
            try
            {
                string currentDiroctory = System.IO.Directory.GetCurrentDirectory();

                string filePath = currentDiroctory + "\\data.txt";

                if (File.Exists(filePath))
                {
                    using(StreamReader reader = new StreamReader(filePath))
                    {
                        string line;
                        while((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                            userName = result[0];
                            password = result[1];
                        }
                        return true;
                    }

                }

                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                MessageBox.Show("An Error Occurred : " + ex.Message);
                return false;
            }
        }

    }
}
