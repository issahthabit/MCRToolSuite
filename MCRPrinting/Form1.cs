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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MCRPrinting
{
    public partial class Form1 : Form
    {
        //MCRConnection connection = new MCRConnection();
        string Constring = @"Data Source=issah\issah;Initial Catalog=MassReg;Persist Security Info=True;User ID=sa;Password=lengan1";
       //string Constring = @"Data Source=10.45.80.51\mcr;Initial Catalog=MassReg;User ID=sa;Password=Password1";
        PrintDocument printDocument = new PrintDocument();
        

        MCRQueries queries= new MCRQueries();
        
        Declaration dec =  new Declaration();

        TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

        string dtTA = string.Empty;
        string dtVillage = string.Empty;
        string dtDistrict = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.Height = 700;
            this.Width = 1200;
            this.CenterToScreen();

            if (PrinterModel.CheckPrinterStatus() != "Printer is ready")
            {
                lblPrintStatus.ForeColor = Color.Red;
                lblPrintStatus.Text = PrinterModel.CheckPrinterStatus();
            }
            else if (PrinterModel.CheckPrinterStatus() == "Printer not Connected")
            {
                lblPrintStatus.ForeColor = Color.Red;
                lblPrintStatus.Text = PrinterModel.CheckPrinterStatus();
            }


            else
            {
                lblPrintStatus.ForeColor = Color.Green;
                lblPrintStatus.Text = PrinterModel.CheckPrinterStatus();
            }
            

            tabControl1.TabPages.Remove(tabAdjudication);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //printDocument.PrinterSettings.PrinterName = "";

            if(printDocument.PrinterSettings.PrinterName != "Evolis Primacy")
            {
                MessageBox.Show("Printer not connected");
            }

            printDocument.PrintPage += PrintDocumentOnPrintPage;
            printDocument.PrintController = new StandardPrintController();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.EndPrint += new PrintEventHandler(EndPrint);
            printDocument.Print();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DistrictCombox.Items.Clear();
            DistrictCombox.Items.Insert(0, "SELECT DISTRICT");

            DistrictCombox.ValueMember = "id";
            DistrictCombox.DisplayMember = "InformantDistrict";
            DistrictCombox.DataSource = queries.LoadDistrict();

            //LoadRecordsCount();
        }

        private void DistrictCombox_SelectedValueChanged(object sender, EventArgs e)
        {
            //LoadDistrictTotal();
            TACombox.Enabled = true;
            TACombox.ValueMember = "id";
            TACombox.DisplayMember = "InformantTA";
            TACombox.DataSource = queries.LoadTA(DistrictCombox.Text);

            dataGridView2.DataSource=queries.LoadrecordsCountByDistrict(DistrictCombox.Text);
        }
        private void TACombox_SelectedValueChanged(object sender, EventArgs e)
        {
            queries.LoadTATotal(DistrictCombox.Text,TACombox.Text);
            VillagecomBox.Enabled = true;

            VillagecomBox.Text = "Select Village";
            VillagecomBox.ValueMember = "id";
            VillagecomBox.DisplayMember = "InformantVillage";
            VillagecomBox.DataSource = queries.LoadVillages(DistrictCombox.Text, TACombox.Text);
            dataGridView2.DataSource = queries.LoadrecordsCountByTA(DistrictCombox.Text, TACombox.Text);
        }

        private void VillagecomBox_SelectedValueChanged(object sender, EventArgs e)
        {
            queries.LoadVillageTotal(DistrictCombox.Text, TACombox.Text, VillagecomBox.Text);
            queries.LoadRecords(TACombox.Text, VillagecomBox.Text);
            dataGridView2.DataSource = queries.LoadrecordstByVillage(DistrictCombox.Text, TACombox.Text,VillagecomBox.Text);
        }
        
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            GenerateQrCode(BirthCertificateDetails.BEN);
            var fnt = new Font("Arial", 9, FontStyle.Regular);


            e.Graphics.DrawString("\t\t\t\t1.  " + label13.Text, fnt, Brushes.Black, 170, 275);
            e.Graphics.DrawString("\t\t\t\t2.  " + label12.Text, fnt, Brushes.Black, 170, 300);
            e.Graphics.DrawString("\t\t\t\t3.  " + label3.Text, fnt, Brushes.Black, 170, 325);
            e.Graphics.DrawString("\t\t\t\t4.  " + label4.Text, fnt, Brushes.Black, 170, 350);
            e.Graphics.DrawString("\t\t\t\t5.  " + label5.Text, fnt, Brushes.Black, 170, 375);
            e.Graphics.DrawString("\t\t\t\t6.  " + label6.Text, fnt, Brushes.Black, 170, 400);
            e.Graphics.DrawString("\t\t\t\t7.  " + label7.Text, fnt, Brushes.Black, 170, 425);
            e.Graphics.DrawString("\t\t\t\t8.  " + label8.Text, fnt, Brushes.Black, 170, 450);
            e.Graphics.DrawString("\t\t\t\t9.  " + label9.Text, fnt, Brushes.Black, 170, 475);
            e.Graphics.DrawString("\t\t\t\t10. " + label10.Text, fnt, Brushes.Black, 170, 500);
            e.Graphics.DrawString("\t\t\t\t11. " + label11.Text, fnt, Brushes.Black, 170, 525);


            string name = BirthCertificateDetails.Fullname.ToLower();
            string fullnametitlecase = textInfo.ToTitleCase(name);


            string mothertitlecase = textInfo.ToTitleCase(BirthCertificateDetails.NameofMother.ToLower());

            string sextitlecase = textInfo.ToTitleCase(BirthCertificateDetails.Sex.ToLower());
            string placeofbirthtitlecase = textInfo.ToTitleCase(BirthCertificateDetails.PlaceofBirth.ToLower());
            string Fathertitlecase = textInfo.ToTitleCase(BirthCertificateDetails.NameOfFather.ToLower());

            // Right panel of the Certificate
            e.Graphics.DrawString(BirthCertificateDetails.BEN, fnt, Brushes.Black, 550, 275);
            e.Graphics.DrawString(BirthCertificateDetails.NationalID, fnt, Brushes.Black, 550, 300);
            e.Graphics.DrawString(fullnametitlecase, fnt, Brushes.Black, 550, 325);
            e.Graphics.DrawString(BirthCertificateDetails.DateofBirth.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 550, 350);
            e.Graphics.DrawString(sextitlecase, fnt, Brushes.Black, 550, 375);
            e.Graphics.DrawString(placeofbirthtitlecase, fnt, Brushes.Black, 550, 400);
            e.Graphics.DrawString(mothertitlecase, fnt, Brushes.Black, 550, 425);
            e.Graphics.DrawString(BirthCertificateDetails.MotherNationality, fnt, Brushes.Black, 550, 450);
            e.Graphics.DrawString(Fathertitlecase, fnt, Brushes.Black, 550, 475);
            e.Graphics.DrawString(BirthCertificateDetails.NationalityOfFather, fnt, Brushes.Black, 550, 500);
            
            e.Graphics.DrawString(BirthCertificateDetails.DateofRegistration.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 550, 525);

            e.Graphics.DrawString("\t\t\t\t" + dec.Declaration1(), fnt, Brushes.Black, 160, 550);
            e.Graphics.DrawString("\t\t\t\t\t\t" + dec.Declaration2(), fnt, Brushes.Black, 150, 575);

            e.Graphics.DrawString("\t\t\t\t" + dec.DeclarationDate(), fnt, Brushes.Black, 150, 600);

            e.Graphics.DrawString(BirthCertificateDetails.PlaceOfRegistration.ToLower(), fnt, Brushes.Black, 550, 825);

            

            e.Graphics.DrawImage(pictureBox1.Image, 600, 75);
            //UpdateRecords(BEN);
        }
        public void GenerateQrCode(string RegId)
        {
            SqlConnection con = new SqlConnection(Constring);
            con.Open();
            //string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where  ben='" + RegId + "'";

            string query = "select '' as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where RegistrationId='" + txtSearchEntry.Text + "'";
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
                string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  InformantTA='"+dtTA+"' and InformantVillage='"+dtVillage+"' and InformantDistrict='"+dtDistrict+"'";
                using (SqlCommand command = new SqlCommand(query, cons))
                {
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        if(rdr.HasRows)
                        {

                        }
                        while (rdr.Read())
                        {
                            
                            //DateTime.Now.ToString("dddd, dd MMMM yyyy");
                            BirthCertificateDetails.BEN = rdr[0].ToString();
                            BirthCertificateDetails.Fullname = rdr[2].ToString();
                            BirthCertificateDetails.DateofBirth = DateTime.Parse(rdr[3].ToString());
                            BirthCertificateDetails.Sex = rdr[4].ToString();
                            BirthCertificateDetails.PlaceofBirth = rdr[5].ToString();
                            BirthCertificateDetails.NameofMother = rdr[6].ToString();
                            BirthCertificateDetails.MotherNationality = rdr[7].ToString();
                            BirthCertificateDetails.NameOfFather = rdr[8].ToString();
                            BirthCertificateDetails.NationalityOfFather = rdr[9].ToString();
                            BirthCertificateDetails.DateofRegistration = DateTime.Parse(rdr[10].ToString());
                            BirthCertificateDetails.PlaceOfRegistration = rdr[12].ToString();

                            //printDocument.PrintPage += PrintDocumentOnPrintPage;
                            //printDocument.Print();
                            

                            printDocument.PrintPage += PrintDocumentOnPrintPage;
                            printDocument.PrintController = new StandardPrintController();
                            printDocument.DefaultPageSettings.Landscape = true;
                            printDocument.EndPrint += new PrintEventHandler(EndPrint);
                           
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
                                            BirthCertificateDetails.BEN = rdr[0].ToString();
                                            BirthCertificateDetails.Fullname = rdr[2].ToString();
                                            BirthCertificateDetails.DateofBirth = DateTime.Parse( rdr[3].ToString());
                                            BirthCertificateDetails.Sex = rdr[4].ToString();
                                            BirthCertificateDetails.PlaceofBirth = rdr[5].ToString();
                                            BirthCertificateDetails.NameofMother = rdr[6].ToString();
                                            BirthCertificateDetails.MotherNationality = rdr[7].ToString();
                                            BirthCertificateDetails.NameOfFather = rdr[8].ToString();
                                            BirthCertificateDetails.NationalityOfFather = rdr[9].ToString();
                                            BirthCertificateDetails.DateofRegistration = DateTime.Parse(rdr[10].ToString());
                                            BirthCertificateDetails.PlaceOfRegistration = rdr[12].ToString();


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
        private void EndPrint(object sender, PrintEventArgs e)
        {
            if (e.PrintAction == PrintAction.PrintToPrinter)
            {
                //Console.WriteLine("Print job completed successfully");
                //MessageBox.Show("Print job completed successfully");
            }
            else
            {
                //Console.WriteLine("Print job failed");
                //MessageBox.Show("Print job failed");
            }
        }

    }
}
