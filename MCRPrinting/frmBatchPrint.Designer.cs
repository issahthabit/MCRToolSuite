namespace MCRPrinting
{
    partial class frmBatchPrint
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.LblTotals = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label29 = new System.Windows.Forms.Label();
            this.VillagecomBox = new System.Windows.Forms.ComboBox();
            this.TACombox = new System.Windows.Forms.ComboBox();
            this.DistrictCombox = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblPlaceofRegistration = new System.Windows.Forms.Label();
            this.lblChildDateofRegistration = new System.Windows.Forms.Label();
            this.lblNationalityofFather = new System.Windows.Forms.Label();
            this.lblNameofFather = new System.Windows.Forms.Label();
            this.lblNationalityofMother = new System.Windows.Forms.Label();
            this.lblChildNameofMother = new System.Windows.Forms.Label();
            this.lblChildPlaceofBirth = new System.Windows.Forms.Label();
            this.lblChildSex = new System.Windows.Forms.Label();
            this.lblChidDOB = new System.Windows.Forms.Label();
            this.lblChildName = new System.Windows.Forms.Label();
            this.lblNationalId = new System.Windows.Forms.Label();
            this.lblBithEntryNumber = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.LblTotals);
            this.panel1.Controls.Add(this.label27);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 173);
            this.panel1.TabIndex = 0;
            // 
            // LblTotals
            // 
            this.LblTotals.AutoSize = true;
            this.LblTotals.Location = new System.Drawing.Point(407, 48);
            this.LblTotals.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.LblTotals.Name = "LblTotals";
            this.LblTotals.Size = new System.Drawing.Size(19, 13);
            this.LblTotals.TabIndex = 15;
            this.LblTotals.Text = "00";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(359, 21);
            this.label27.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(132, 13);
            this.label27.TabIndex = 14;
            this.label27.Text = "Number of Active Records";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label29);
            this.panel2.Controls.Add(this.VillagecomBox);
            this.panel2.Controls.Add(this.TACombox);
            this.panel2.Controls.Add(this.DistrictCombox);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Controls.Add(this.label25);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(270, 169);
            this.panel2.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(60, 19);
            this.label29.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(137, 13);
            this.label29.TabIndex = 21;
            this.label29.Text = "Informant Resident Address";
            // 
            // VillagecomBox
            // 
            this.VillagecomBox.Enabled = false;
            this.VillagecomBox.FormattingEnabled = true;
            this.VillagecomBox.Location = new System.Drawing.Point(69, 106);
            this.VillagecomBox.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.VillagecomBox.Name = "VillagecomBox";
            this.VillagecomBox.Size = new System.Drawing.Size(175, 21);
            this.VillagecomBox.TabIndex = 20;
            this.VillagecomBox.SelectedIndexChanged += new System.EventHandler(this.VillagecomBox_SelectedIndexChanged);
            // 
            // TACombox
            // 
            this.TACombox.Enabled = false;
            this.TACombox.FormattingEnabled = true;
            this.TACombox.Location = new System.Drawing.Point(69, 73);
            this.TACombox.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.TACombox.Name = "TACombox";
            this.TACombox.Size = new System.Drawing.Size(175, 21);
            this.TACombox.TabIndex = 19;
            this.TACombox.SelectedIndexChanged += new System.EventHandler(this.TACombox_SelectedIndexChanged);
            // 
            // DistrictCombox
            // 
            this.DistrictCombox.FormattingEnabled = true;
            this.DistrictCombox.Location = new System.Drawing.Point(69, 44);
            this.DistrictCombox.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.DistrictCombox.Name = "DistrictCombox";
            this.DistrictCombox.Size = new System.Drawing.Size(175, 21);
            this.DistrictCombox.TabIndex = 18;
            this.DistrictCombox.SelectedIndexChanged += new System.EventHandler(this.DistrictCombox_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(16, 109);
            this.label24.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(38, 13);
            this.label24.TabIndex = 17;
            this.label24.Text = "Village";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(16, 76);
            this.label25.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(21, 13);
            this.label25.TabIndex = 16;
            this.label25.Text = "TA";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(16, 44);
            this.label26.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(39, 13);
            this.label26.TabIndex = 15;
            this.label26.Text = "District";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.lblPlaceofRegistration);
            this.panel3.Controls.Add(this.lblChildDateofRegistration);
            this.panel3.Controls.Add(this.lblNationalityofFather);
            this.panel3.Controls.Add(this.lblNameofFather);
            this.panel3.Controls.Add(this.lblNationalityofMother);
            this.panel3.Controls.Add(this.lblChildNameofMother);
            this.panel3.Controls.Add(this.lblChildPlaceofBirth);
            this.panel3.Controls.Add(this.lblChildSex);
            this.panel3.Controls.Add(this.lblChidDOB);
            this.panel3.Controls.Add(this.lblChildName);
            this.panel3.Controls.Add(this.lblNationalId);
            this.panel3.Controls.Add(this.lblBithEntryNumber);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 173);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(828, 514);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(620, 6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 126);
            this.pictureBox1.Size = new System.Drawing.Size(86, 101);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(681, 472);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(111, 40);
            this.btnPrint.TabIndex = 46;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblPlaceofRegistration
            // 
            this.lblPlaceofRegistration.AutoSize = true;
            this.lblPlaceofRegistration.Location = new System.Drawing.Point(214, 459);
            this.lblPlaceofRegistration.Name = "lblPlaceofRegistration";
            this.lblPlaceofRegistration.Size = new System.Drawing.Size(94, 13);
            this.lblPlaceofRegistration.TabIndex = 45;
            this.lblPlaceofRegistration.Text = "PlaceofResidence";
            // 
            // lblChildDateofRegistration
            // 
            this.lblChildDateofRegistration.AutoSize = true;
            this.lblChildDateofRegistration.Location = new System.Drawing.Point(315, 413);
            this.lblChildDateofRegistration.Name = "lblChildDateofRegistration";
            this.lblChildDateofRegistration.Size = new System.Drawing.Size(95, 13);
            this.lblChildDateofRegistration.TabIndex = 44;
            this.lblChildDateofRegistration.Text = "DateofRegistration";
            // 
            // lblNationalityofFather
            // 
            this.lblNationalityofFather.AutoSize = true;
            this.lblNationalityofFather.Location = new System.Drawing.Point(315, 372);
            this.lblNationalityofFather.Name = "lblNationalityofFather";
            this.lblNationalityofFather.Size = new System.Drawing.Size(95, 13);
            this.lblNationalityofFather.TabIndex = 43;
            this.lblNationalityofFather.Text = "NationalityofFather";
            // 
            // lblNameofFather
            // 
            this.lblNameofFather.AutoSize = true;
            this.lblNameofFather.Location = new System.Drawing.Point(315, 333);
            this.lblNameofFather.Name = "lblNameofFather";
            this.lblNameofFather.Size = new System.Drawing.Size(74, 13);
            this.lblNameofFather.TabIndex = 42;
            this.lblNameofFather.Text = "NameofFather";
            // 
            // lblNationalityofMother
            // 
            this.lblNationalityofMother.AutoSize = true;
            this.lblNationalityofMother.Location = new System.Drawing.Point(315, 291);
            this.lblNationalityofMother.Name = "lblNationalityofMother";
            this.lblNationalityofMother.Size = new System.Drawing.Size(98, 13);
            this.lblNationalityofMother.TabIndex = 41;
            this.lblNationalityofMother.Text = "NationalityofMother";
            // 
            // lblChildNameofMother
            // 
            this.lblChildNameofMother.AutoSize = true;
            this.lblChildNameofMother.Location = new System.Drawing.Point(315, 247);
            this.lblChildNameofMother.Name = "lblChildNameofMother";
            this.lblChildNameofMother.Size = new System.Drawing.Size(77, 13);
            this.lblChildNameofMother.TabIndex = 40;
            this.lblChildNameofMother.Text = "NameofMother";
            // 
            // lblChildPlaceofBirth
            // 
            this.lblChildPlaceofBirth.AutoSize = true;
            this.lblChildPlaceofBirth.Location = new System.Drawing.Point(315, 210);
            this.lblChildPlaceofBirth.Name = "lblChildPlaceofBirth";
            this.lblChildPlaceofBirth.Size = new System.Drawing.Size(65, 13);
            this.lblChildPlaceofBirth.TabIndex = 39;
            this.lblChildPlaceofBirth.Text = "PlaceofBIrth";
            // 
            // lblChildSex
            // 
            this.lblChildSex.AutoSize = true;
            this.lblChildSex.Location = new System.Drawing.Point(315, 176);
            this.lblChildSex.Name = "lblChildSex";
            this.lblChildSex.Size = new System.Drawing.Size(25, 13);
            this.lblChildSex.TabIndex = 38;
            this.lblChildSex.Text = "Sex";
            // 
            // lblChidDOB
            // 
            this.lblChidDOB.AutoSize = true;
            this.lblChidDOB.Location = new System.Drawing.Point(315, 145);
            this.lblChidDOB.Name = "lblChidDOB";
            this.lblChidDOB.Size = new System.Drawing.Size(66, 13);
            this.lblChidDOB.TabIndex = 37;
            this.lblChidDOB.Text = "Date of Birth";
            // 
            // lblChildName
            // 
            this.lblChildName.AutoSize = true;
            this.lblChildName.Location = new System.Drawing.Point(315, 106);
            this.lblChildName.Name = "lblChildName";
            this.lblChildName.Size = new System.Drawing.Size(40, 13);
            this.lblChildName.TabIndex = 36;
            this.lblChildName.Text = "Names";
            // 
            // lblNationalId
            // 
            this.lblNationalId.AutoSize = true;
            this.lblNationalId.Location = new System.Drawing.Point(315, 68);
            this.lblNationalId.Name = "lblNationalId";
            this.lblNationalId.Size = new System.Drawing.Size(55, 13);
            this.lblNationalId.TabIndex = 35;
            this.lblNationalId.Text = "NationalId";
            // 
            // lblBithEntryNumber
            // 
            this.lblBithEntryNumber.AutoSize = true;
            this.lblBithEntryNumber.Location = new System.Drawing.Point(315, 23);
            this.lblBithEntryNumber.Name = "lblBithEntryNumber";
            this.lblBithEntryNumber.Size = new System.Drawing.Size(86, 13);
            this.lblBithEntryNumber.TabIndex = 34;
            this.lblBithEntryNumber.Text = "BithEntryNumber";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(105, 413);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "Date of Registration";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(105, 372);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "Nationality of Father";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(104, 333);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "Name of Father";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 291);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "Nationality of Mother";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 247);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Name of Mother";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(104, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Place of Birth";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Sex";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(105, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Date of Birth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(104, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "National ID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Birth Entry Number";
            // 
            // frmBatchPrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 687);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "frmBatchPrint";
            this.Text = "frmBatchPrint";
            this.Load += new System.EventHandler(this.frmBatchPrint_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox VillagecomBox;
        private System.Windows.Forms.ComboBox TACombox;
        private System.Windows.Forms.ComboBox DistrictCombox;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPlaceofRegistration;
        private System.Windows.Forms.Label lblChildDateofRegistration;
        private System.Windows.Forms.Label lblNationalityofFather;
        private System.Windows.Forms.Label lblNameofFather;
        private System.Windows.Forms.Label lblNationalityofMother;
        private System.Windows.Forms.Label lblChildNameofMother;
        private System.Windows.Forms.Label lblChildPlaceofBirth;
        private System.Windows.Forms.Label lblChildSex;
        private System.Windows.Forms.Label lblChidDOB;
        private System.Windows.Forms.Label lblChildName;
        private System.Windows.Forms.Label lblNationalId;
        private System.Windows.Forms.Label lblBithEntryNumber;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label LblTotals;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}