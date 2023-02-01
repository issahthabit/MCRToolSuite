using MCRPrinting.Model;
using QRCoder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MCRPrinting
{
    public partial class Form1 : Form
    {
        string Constring = @"Data Source=issah\issah;Initial Catalog=MassReg;Persist Security Info=True;User ID=sa;Password=lengan1";
        //string Constring = @"Data Source=10.45.80.51\mcr;Initial Catalog=MassReg;User ID=sa;Password=Password1";
        PrintDocument printDocument = new PrintDocument();

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        //string BEN = string.Empty;
        string Fullname = string.Empty;
        string regid = string.Empty;
        string NationalID = string.Empty;
        DateTime DateofBirth = DateTime.Now;
        string Sex = string.Empty;
        string PlaceofBirth = string.Empty;
        string NameofMother = string.Empty;
        string MotherNationality = string.Empty;
        string NameOfFather = string.Empty;
        string NationalityOfFather = string.Empty;
        DateTime DateofRegistration = DateTime.Now;
        string PlaceOfRegistration = string.Empty;
        string Placeofreg = string .Empty;
        string dtTA = string.Empty;
        string dtVillage = string.Empty;
        string dtDistrict = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.Height = 700;
            this.Width = 1200;
            this.CenterToScreen();

            tabControl1.TabPages.Remove(tabAdjudication);
        }
        void LoadrecordsCountByDistrict()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict='"+DistrictCombox.Text+"' and ben<>'' and brn<>'' GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                            dataGridView2.AutoGenerateColumns = false;
                        }
                    }
                }
            }
        }
        void LoadRecordsCount()
        {
             using (SqlConnection con = new SqlConnection(Constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict<>'4' and InformantDistrict<>'' GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }
        }
        void LoadDistrict()
        {

            DistrictCombox.SelectedValue = "Select District";

            using (SqlConnection con = new SqlConnection(Constring))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("select InformantDistrict from ChildDetail where InformantDistrict<>'' and InformantDistrict<>'4' group by InformantDistrict ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                sda.Fill(dt);

               
                DistrictCombox.SelectedValue = "Select District";

                DistrictCombox.ValueMember = "id";
                DistrictCombox.DisplayMember = "InformantDistrict";
                DistrictCombox.DataSource = dt;

                con.Close();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //printDocument.BeginPrint(sender,e);
            printDocument.PrintPage += PrintDocumentOnPrintPage;
            printDocument.DefaultPageSettings.Landscape = false;
            printDocument.Print();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DistrictCombox.Items.Clear();
            DistrictCombox.Items.Insert(0, "SELECT DISTRICT");
            LoadDistrict();
            LoadRecordsCount();
        }

        private void DistrictCombox_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadrecordsCountByDistrict();
            LoadDistrictTotal();
            TACombox.Enabled = true;
            using (SqlConnection con = new SqlConnection(Constring))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct InformantTA from ChildDetail where InformantDistrict='" + DistrictCombox.Text + "' and InformantTA<>''", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                TACombox.ValueMember = "id";
                TACombox.DisplayMember = "InformantTA";
                TACombox.DataSource = dt;

            }
        }
        private void TACombox_SelectedValueChanged(object sender, EventArgs e)
        {
            TATotal();
            VillagecomBox.Enabled = true;
            using (SqlConnection con = new SqlConnection(Constring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct InformantVillage from ChildDetail where InformantTA='" + TACombox.Text + "' order by InformantVillage asc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                VillagecomBox.Text = "Select Village";
                VillagecomBox.ValueMember = "id";
                VillagecomBox.DisplayMember = "InformantVillage";
                VillagecomBox.DataSource = dt;
            }
        }

        private void VillagecomBox_SelectedValueChanged(object sender, EventArgs e)
        {
            VillageTotal();
            LoadRecords();
        }
        public void LoadRecords()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {
                using (SqlCommand cmd = new SqlCommand("select ben,Firstname,Othernames,Surname,DateOfBirth,MotherFirstname,MotherOthernames,MotherSurname,FatherFirstname,FatherOthernames,FatherSurname from ChildDetail where InformantTA='" + TACombox.Text+"' and InformantVillage='" + VillagecomBox.Text + "' and ben<>'' and RecStatus=4 and brn<>''  order by Surname,Firstname asc"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            resultdataGridView.DataSource = dt;
                        }
                    }
                }
            }
        }
        void LoadDistrictTotal()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {

                con.Open();
                string query = "select count(*) from ChildDetail where InformantDistrict='" + DistrictCombox.Text + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LblTotals.Text = reader[0].ToString();
                    }
                }
                con.Close();
            }
        }
        void TATotal()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {
                con.Open();
                string query = "select count(*) from ChildDetail where InformantTA='" + TACombox.Text + "' and InformantDistrict='" + DistrictCombox.Text + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LblTotals.Text = reader[0].ToString();
                    }
                }
                con.Close();
            }
        }
        void VillageTotal()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {
                con.Open();
                string query = "select count(*) from ChildDetail where InformantVillage='" + VillagecomBox.Text + "' and InformantTA='" + TACombox.Text + "' and InformantDistrict='" + DistrictCombox.Text + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        LblTotals.Text = reader[0].ToString();
                    }
                }
                con.Close();
            }
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {


            //GenerateQrCode(BEN);
            var fnt = new Font("Arial", 9, FontStyle.Regular);
            //var dec = new Font("Arial", 9, FontStyle.Regular,StringAlignment.Center.ToString());
            // 

            e.Graphics.DrawString("\t\t1.  " + label13.Text, fnt, Brushes.Black, 130, 250);
            e.Graphics.DrawString("\t\t2.  " + label12.Text, fnt, Brushes.Black, 130, 275);
            e.Graphics.DrawString("\t\t3.  " + label3.Text, fnt, Brushes.Black, 130, 300);
            e.Graphics.DrawString("\t\t4.  " + label4.Text, fnt, Brushes.Black, 130, 325);
            e.Graphics.DrawString("\t\t5.  " + label5.Text, fnt, Brushes.Black, 130, 350);
            e.Graphics.DrawString("\t\t6.  " + label6.Text, fnt, Brushes.Black, 130, 375);
            e.Graphics.DrawString("\t\t7.  " + label7.Text, fnt, Brushes.Black, 130, 400);
            e.Graphics.DrawString("\t\t8.  " + label8.Text, fnt, Brushes.Black, 130, 425);
            e.Graphics.DrawString("\t\t9.  " + label9.Text, fnt, Brushes.Black, 130, 450);
            e.Graphics.DrawString("\t\t10. " + label10.Text, fnt, Brushes.Black, 130, 475);
            e.Graphics.DrawString("\t\t11. " + label11.Text, fnt, Brushes.Black, 130, 500);



            //string s = "hello world";

            //TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            //string titleCase = textInfo.ToTitleCase(s);

            //Console.WriteLine(titleCase);


            string name = Fullname.ToLower();
            string fullnametitlecase = textInfo.ToTitleCase(name);

            string mothertitlecase = textInfo.ToTitleCase(NameofMother.ToLower());
            string sextitlecase = textInfo.ToTitleCase(Sex.ToLower());
            string placeofbirthtitlecase = textInfo.ToTitleCase(PlaceofBirth.ToLower());
            string Fathertitlecase = textInfo.ToTitleCase(NameOfFather.ToLower());
            string declaration = "I hereby certify the above to be a true and correct extract from the";
            string declaration2 = "Birth Regigister kept at NRB.";
            //string mothertitlecase = textInfo.ToTitleCase(NameofMother.ToLower());
            string dateprintedtext = "Dated this ";
            string dateprintedtext2 = dateprintedtext + DateTime.Now.ToString("dd MMMM yyyy");
            //char.ToUpper(BEN[0]);
            //e.Graphics.DrawString(BEN, fnt, Brushes.Black, 450, 250);
            e.Graphics.DrawString(NationalID, fnt, Brushes.Black, 450, 275);
            e.Graphics.DrawString(fullnametitlecase, fnt, Brushes.Black, 450, 300);
            e.Graphics.DrawString(DateofBirth.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 450, 325);
            e.Graphics.DrawString(sextitlecase, fnt, Brushes.Black, 450, 350);
            e.Graphics.DrawString(placeofbirthtitlecase, fnt, Brushes.Black, 450, 375);
            e.Graphics.DrawString(mothertitlecase, fnt, Brushes.Black, 450, 400);
            e.Graphics.DrawString(MotherNationality, fnt, Brushes.Black, 450, 425);
            e.Graphics.DrawString(Fathertitlecase, fnt, Brushes.Black, 450, 450);
            e.Graphics.DrawString(NationalityOfFather, fnt, Brushes.Black, 450, 475);
            
            e.Graphics.DrawString(DateofRegistration.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 450, 500);

            e.Graphics.DrawString("\t\t" + declaration, fnt, Brushes.Black, 150, 525);
            e.Graphics.DrawString("\t\t\t\t" + declaration2, fnt, Brushes.Black, 140, 550);

            e.Graphics.DrawString("\t\t\t\t" + dateprintedtext2, fnt, Brushes.Black, 140, 575);

            e.Graphics.DrawString(PlaceOfRegistration.ToLower(), fnt, Brushes.Black, 150, 770);

            

            e.Graphics.DrawImage(pictureBox1.Image, 500, 75);
            //UpdateRecords(BEN);
        }
        public void GenerateQrCode(string RegId)
        {
            SqlConnection con = new SqlConnection(Constring);
            con.Open();
            string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where  ben='" + RegId + "'";

            //string query = "select '' as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where RegistrationId='" + txtSearchEntry.Text + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string qrData = "04" + "~" + reader["BirthEntryNumber"].ToString() + "~" + reader["Name"].ToString() + "~" + reader["NationalID"].ToString() + "~" + reader["NameofMother"].ToString() + "~" + reader["NationalityofMother"].ToString() + "~" + reader["NameofFather"].ToString() + "~" + reader["NationalityofFather"].ToString() + "~" + reader["DateOfRegistration"].ToString();

                QRCoder.QRCodeGenerator qRCodeGenerator = new QRCoder.QRCodeGenerator();
                QRCoder.QRCodeData qRCodeData = qRCodeGenerator.CreateQrCode(qrData, QRCoder.QRCodeGenerator.ECCLevel.Q);
                QRCoder.QRCode qRCode = new QRCode(qRCodeData);
                Bitmap bmp = qRCode.GetGraphic(1);
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    pictureBox1.Image = bmp;
                    pictureBox1.Width = 150;
                    pictureBox1.Height = 150;
                }
            }
            con.Close();
        }
        void PrintRecords()
        {
            using (SqlConnection cons = new SqlConnection(Constring))
            {
                cons.Open();
                string query = "select ben as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  InformantTA='"+dtTA+"' and InformantVillage='"+dtVillage+"' and InformantDistrict='"+dtDistrict+"'";
                using (SqlCommand command = new SqlCommand(query, cons))
                {
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //DateTime.Now.ToString("dddd, dd MMMM yyyy");
                            //BEN = rdr[0].ToString();
                            Fullname = rdr[2].ToString();
                            DateofBirth = DateTime.Parse(rdr[3].ToString());
                            Sex = rdr[4].ToString();
                            PlaceofBirth = rdr[5].ToString();
                            NameofMother = rdr[6].ToString();
                            MotherNationality = rdr[7].ToString();
                            NameOfFather = rdr[8].ToString();
                            NationalityOfFather = rdr[9].ToString();
                            DateofRegistration = DateTime.Parse(rdr[10].ToString());
                            PlaceOfRegistration = rdr[12].ToString();

                            char.ToUpper(Fullname[0]);

                            printDocument.PrintPage += PrintDocumentOnPrintPage;
                            printDocument.Print();

                        }
                    }
                }
                cons.Close();
            }
                        
                    
        }

        private void btnBatchPrint_Click(object sender, EventArgs e)
        {
            string str = "select RegistrationId  from ChildDetail where InformantTA='" + TACombox.Text + "' and InformantVillage='" + VillagecomBox.Text + "'";
            using (SqlConnection con = new SqlConnection(Constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(str, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            using (SqlConnection cons = new SqlConnection(Constring))
                            {
                                cons.Open();
                                string query = "select RegistrationId as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  RegistrationId='" + reader[0].ToString() + "'";
                                using (SqlCommand command = new SqlCommand(query, cons))
                                {
                                    using (SqlDataReader rdr = command.ExecuteReader())
                                    {
                                        while (rdr.Read())
                                        {
                                            //DateTime.Now.ToString("dddd, dd MMMM yyyy");
                                            //BEN = rdr[0].ToString();
                                            Fullname = rdr[2].ToString();
                                            DateofBirth = DateTime.Parse( rdr[3].ToString());
                                            Sex = rdr[4].ToString();
                                            PlaceofBirth = rdr[5].ToString();
                                            NameofMother = rdr[6].ToString();
                                            MotherNationality = rdr[7].ToString();
                                            NameOfFather = rdr[8].ToString();
                                            NationalityOfFather = rdr[9].ToString();
                                            DateofRegistration = DateTime.Parse(rdr[10].ToString());
                                            PlaceOfRegistration = rdr[12].ToString();

                                            char.ToUpper(Fullname[0]);

                                            printDocument.PrintPage += PrintDocumentOnPrintPage;
                                            printDocument.Print();
                                        }
                                    }
                                }
                                cons.Close();
                            }
                        }
                    }
                }
                con.Close();
            }
        }
        private void btnLoadRecords_Click(object sender, EventArgs e)
        {
            MCRQueries mcr = new MCRQueries();
            mcr.GetRecordsByPersonid(txtSearchEntry.Text);

            lblBithEntryNumber.Text = BirthCertificateDetails.BEN;
            lblChildDateofRegistration.Text = BirthCertificateDetails.DateofRegistration.ToString();
            lblChildName.Text = textInfo.ToTitleCase(BirthCertificateDetails.Fullname.ToString());
            lblNationalId.Text = BirthCertificateDetails.NationalID;
            lblChidDOB.Text = BirthCertificateDetails.DateofBirth.ToString("dddd, dd MMMM yyyy");
            lblChildSex.Text = textInfo.ToTitleCase(BirthCertificateDetails.Sex);
            lblChildPlaceofBirth.Text = textInfo.ToTitleCase(BirthCertificateDetails.PlaceofBirth);
            lblChildNameofMother.Text = textInfo.ToTitleCase(BirthCertificateDetails.NameofMother);
            lblNationalityofMother.Text = BirthCertificateDetails.MotherNationality;
            lblNameofFather.Text = textInfo.ToTitleCase(BirthCertificateDetails.NameOfFather);
            lblNationalityofFather.Text = BirthCertificateDetails.NationalityOfFather;
            lblChildDateofRegistration.Text = BirthCertificateDetails.DateofRegistration.ToString("dddd, dd MMMM yyyy");
            
        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];
                //MessageBox.Show(dataGridView2.RowContextMenuStripChanged)
                //MessageBox.Show(dataGridView2.SelectedCells[0].Value.ToString() + "," + dataGridView2.SelectedCells[1].Value.ToString());
                //Placeofreg = row.Cells[3].Value.ToString();

                dtDistrict = dataGridView2.SelectedCells[0].Value.ToString();
                dtVillage = dataGridView2.SelectedCells[2].Value.ToString();
                dtTA = dataGridView2.SelectedCells[1].Value.ToString(); 

            }
        }

        private void btnStartPrint_Click(object sender, EventArgs e)
        {
            PrintRecords();
        }

        private void txtSearchresults_TextChanged(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where BEN<>'' AND BRN<>'' AND  InformantVillage like '%"+txtSearchresults.Text+ "%' AND RecStatus=4 GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            dataGridView2.DataSource = dt;
                        }
                    }
                }
            }
        }
    }
}
