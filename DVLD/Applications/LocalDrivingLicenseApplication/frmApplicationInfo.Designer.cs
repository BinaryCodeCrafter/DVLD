﻿namespace DVLD.Applications.LocalDrivingLicenseApplication
{
    partial class frmApplicationInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlApplicationBasicInfo1 = new DVLD.Applications.ctrlApplicationBasicInfo();
            this.SuspendLayout();
            // 
            // ctrlApplicationBasicInfo1
            // 
            this.ctrlApplicationBasicInfo1.Location = new System.Drawing.Point(12, 9);
            this.ctrlApplicationBasicInfo1.Name = "ctrlApplicationBasicInfo1";
            this.ctrlApplicationBasicInfo1.Size = new System.Drawing.Size(937, 276);
            this.ctrlApplicationBasicInfo1.TabIndex = 0;
            // 
            // frmApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 283);
            this.Controls.Add(this.ctrlApplicationBasicInfo1);
            this.Name = "frmApplicationInfo";
            this.Text = "frmApplicationInfo";
            this.Load += new System.EventHandler(this.frmApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlApplicationBasicInfo ctrlApplicationBasicInfo1;
    }
}