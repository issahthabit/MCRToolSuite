using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.IO;
using QRCoder;
using System.Drawing.Imaging;

namespace MCRPrinting
{
    public partial class frmBatchPrint : Form
    {
        string Constring = @"Data Source=issah\issah;Initial Catalog=MassReg;Persist Security Info=True;User ID=sa;Password=lengan1";
        PrintDocument printDocument = new PrintDocument();

        string BEN = string.Empty;
        string Fullname = string.Empty;
        string regid = string.Empty;
        string NationalID = string.Empty;
        string DateofBirth = string.Empty;
        string Sex = string.Empty;
        string PlaceofBirth = string.Empty;
        string NameofMother = string.Empty;
        string MotherNationality = string.Empty;
        string NameOfFather = string.Empty;
        string NationalityOfFather = string.Empty;
        string DateofRegistration = string.Empty;
        string PlaceOfRegistration = string.Empty;

        public frmBatchPrint()
        {
            InitializeComponent();
            this.Height = 800;
            this.Width = 1000;
            this.CenterToScreen();

        }
        private void frmBatchPrint_Load(object sender, EventArgs e)
        {
            DistrictCombox.Items.Clear();
            DistrictCombox.Items.Insert(0, "SELECT DISTRICT");
            
            LoadDistrict();
        }
        private void DistrictCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TACombox.Items.Clear();
            //TACombox.Items.Insert(0, "Select TA");
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
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            GenerateQrCode(BEN);
             //System.Drawing.Image img = System.Drawing.Image.FromFile("D:\\Foto.jpg");
             var fnt = new Font("Times new Roman", 12, FontStyle.Regular);

            e.Graphics.DrawString("1.\t" + label1.Text, fnt, Brushes.Black, 130, 250);
            e.Graphics.DrawString("2.\t" + label2.Text, fnt, Brushes.Black, 130, 275);
            e.Graphics.DrawString("3.\t" + label3.Text, fnt, Brushes.Black, 130, 300);
            e.Graphics.DrawString("4.\t" + label4.Text, fnt, Brushes.Black, 10, 325);
            e.Graphics.DrawString("5.\t" + label5.Text, fnt, Brushes.Black, 10, 350);
            e.Graphics.DrawString("6.\t" + label6.Text, fnt, Brushes.Black, 10, 375);
            e.Graphics.DrawString("7.\t" + label7.Text, fnt, Brushes.Black, 10, 400);
            e.Graphics.DrawString("8.\t" + label8.Text, fnt, Brushes.Black, 10, 425);
            e.Graphics.DrawString("9.\t" + label9.Text, fnt, Brushes.Black, 10, 450);
            e.Graphics.DrawString("10.\t" + label10.Text, fnt, Brushes.Black, 10, 475);
            e.Graphics.DrawString("11.\t" + label11.Text, fnt, Brushes.Black, 10, 500);
            

            e.Graphics.DrawString(BEN, fnt, Brushes.Black, 300, 250);
            e.Graphics.DrawString(NationalID, fnt, Brushes.Black, 300, 275);
            e.Graphics.DrawString(Fullname, fnt, Brushes.Black, 300, 300);
            e.Graphics.DrawString(DateofBirth, fnt, Brushes.Black, 300, 325);
            e.Graphics.DrawString(Sex, fnt, Brushes.Black, 300, 350);
            e.Graphics.DrawString(PlaceofBirth, fnt, Brushes.Black, 300, 375);
            e.Graphics.DrawString(NameofMother, fnt, Brushes.Black, 300, 400);
            e.Graphics.DrawString(MotherNationality, fnt, Brushes.Black, 300, 425);
            e.Graphics.DrawString(NameOfFather, fnt, Brushes.Black, 300, 450);
            e.Graphics.DrawString(NationalityOfFather, fnt, Brushes.Black, 300, 475);
            e.Graphics.DrawString(DateofRegistration, fnt, Brushes.Black, 300, 500);
            e.Graphics.DrawString(PlaceOfRegistration.ToLower(), fnt, Brushes.Black, 150, 770);

            e.Graphics.DrawImage(pictureBox1.Image, 400, 75);
        }
        public void GenerateQrCode(string RegId)
        {
            //var Constring = ConfigurationManager.ConnectionStrings["MCRConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(Constring);
            con.Open();
            string query = "select RegistrationId as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where  RegistrationId='"+ RegId + "'";

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

        private void btnPrint_Click(object sender, EventArgs e)
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
                                string query = "select RegistrationId as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  RegistrationId='"+reader[0].ToString()+"'";
                                using (SqlCommand command = new SqlCommand(query, cons))
                                {
                                    using (SqlDataReader rdr = command.ExecuteReader())
                                    {
                                        while (rdr.Read())
                                        {
                                            BEN = rdr[0].ToString();
                                            Fullname = rdr[2].ToString();
                                            DateofBirth = rdr[3].ToString();
                                            Sex = rdr[4].ToString();
                                            PlaceofBirth = rdr[5].ToString();
                                            NameofMother = rdr[6].ToString();
                                            MotherNationality = rdr[7].ToString();
                                            NameOfFather = rdr[8].ToString();
                                            NationalityOfFather = rdr[9].ToString();
                                            DateofRegistration = rdr[10].ToString();
                                            PlaceOfRegistration = rdr[12].ToString();


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
        void LoadDistrict()
        {
            
            using (SqlConnection con = new SqlConnection(Constring))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("select InformantDistrict from ChildDetail where InformantDistrict<>'' and InformantDistrict<>'4' group by InformantDistrict ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
              
                DataTable dt = new DataTable();
                sda.Fill(dt);
                con.Close();
                
                DistrictCombox.ValueMember = "id";
                DistrictCombox.DisplayMember = "InformantDistrict";
                DistrictCombox.DataSource = dt;
                DistrictCombox.Text = "Select District";
            }
        }
        private void TACombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            TATota();
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
        void TATota()
        {
            using (SqlConnection con = new SqlConnection(Constring))
            {

                con.Open();
                string query = "select count(*) from ChildDetail where InformantTA='" + TACombox.Text + "' and InformantDistrict='"+ DistrictCombox.Text + "'";
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

        private void VillagecomBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            VillageTotal();
        }
    }
}
