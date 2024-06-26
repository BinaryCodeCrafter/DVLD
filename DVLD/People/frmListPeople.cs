﻿using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        private static DataTable allPepleData = clsPerson.getAllPeople();

        private DataTable peopleData =
            allPepleData.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        private void refreshPeople()
        {
            allPepleData = clsPerson.getAllPeople();

            peopleData = allPepleData.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            this.dataGridView1.DataSource = peopleData;
            this.comboBox1.SelectedIndex = 0;
            this.txtRecods.Text = dataGridView1.Rows.Count.ToString();
        }

        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = peopleData;

            txtRecods.Text = dataGridView1.Rows.Count.ToString();

            if(dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Columns[0].HeaderText = "Person ID";
                dataGridView1.Columns[0].Width = 110;

                dataGridView1.Columns[1].HeaderText = "National No.";
                dataGridView1.Columns[1].Width = 120;


                dataGridView1.Columns[2].HeaderText = "First Name";
                dataGridView1.Columns[2].Width = 120;

                dataGridView1.Columns[3].HeaderText = "Second Name";
                dataGridView1.Columns[3].Width = 140;


                dataGridView1.Columns[4].HeaderText = "Third Name";
                dataGridView1.Columns[4].Width = 120;

                dataGridView1.Columns[5].HeaderText = "Last Name";
                dataGridView1.Columns[5].Width = 120;

                dataGridView1.Columns[6].HeaderText = "Gendor";
                dataGridView1.Columns[6].Width = 120;

                dataGridView1.Columns[7].HeaderText = "Date Of Birth";
                dataGridView1.Columns[7].Width = 140;

                dataGridView1.Columns[8].HeaderText = "Nationality";
                dataGridView1.Columns[8].Width = 120;


                dataGridView1.Columns[9].HeaderText = "Phone";
                dataGridView1.Columns[9].Width = 120;


                dataGridView1.Columns[10].HeaderText = "Email";
                dataGridView1.Columns[10].Width = 170;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (comboBox1.Text != "None");

            if (comboBox1.Visible)
            {
                txtFilter.Text = "";
                txtFilter.Focus();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch(comboBox1.Text) {

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if(txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                peopleData.DefaultView.RowFilter = "";
                txtRecods.Text = peopleData.Rows.Count.ToString();
                return;
            }

            if(FilterColumn == "PersonID")
            {
                peopleData.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            }
            else
            {
                peopleData.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());
            }

            txtRecods.Text = dataGridView1.Rows.Count.ToString();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            frmShowPersonInfo form = new frmShowPersonInfo(id);

            form.ShowDialog();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmAddUpdatePerson form = new frmAddUpdatePerson();

            form.ShowDialog();

            refreshPeople();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            frmAddUpdatePerson form = new frmAddUpdatePerson();

            form.ShowDialog();

            refreshPeople();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;
            frmAddUpdatePerson form = new frmAddUpdatePerson(id);

            form.ShowDialog();
            refreshPeople();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells[0].Value;

            if(MessageBox.Show($"Are you sure you want to delete person {id}" , "Confirm Delete" , MessageBoxButtons.OKCancel , MessageBoxIcon.Question) == DialogResult.OK)
            {

                if (clsPerson.deletePerson(id))
                {
                    MessageBox.Show("Person Deleted Successfully" , "Success" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                    refreshPeople();
                }
                else
                {
                    MessageBox.Show("Person was not deleted due to the fact that there is data linked to it" , "Error" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                }

            }
        }
    }
}
