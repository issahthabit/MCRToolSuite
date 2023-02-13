using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ReportingServices.Diagnostics.Internal;
using System.Drawing.Printing;
using System.Net;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;
using QRCoder;
using System.Drawing.Imaging;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace MCRPrinting.Model
{
    public class PrinterModel
    {
        static string PrinterStatus = string.Empty;
        

        public static string CheckPrinterStatus()
        {
            
            PrintServer printServer = new PrintServer();
            //PrintQueue printQueue = printServer.GetPrintQueue("HP LaserJet M806 PCL 6"); //Microsoft Print to PDF
            PrintQueue printQueue = printServer.GetPrintQueue("Microsoft Print to PDF"); //Microsoft Print to PDF
            PrintQueueStatus status = printQueue.QueueStatus;
            switch (status)
            {
                case PrintQueueStatus.Paused:
                    PrinterStatus = "Printer is paused.";
                    break;
                case PrintQueueStatus.Error:
                    PrinterStatus = "Printer is in an error state.";
                    break;
                case PrintQueueStatus.PaperOut:
                    PrinterStatus = "Printer is out of paper.";
                    break;
                case PrintQueueStatus.Printing:
                    PrinterStatus = "Printer is printing";
                    break;
                case PrintQueueStatus.TonerLow:
                    PrinterStatus = "Printer is low on toner.";
                    break;
                case PrintQueueStatus.Offline:
                    PrinterStatus = "Printer is offline.";
                    break;
                case PrintQueueStatus.None:
                    PrinterStatus = "Printer not Connected";
                    break;
                default:
                    PrinterStatus = "Printer is ready.";
                    break;
            }
            return PrinterStatus;
        }
        
    }
    public class MCRModel
    {
        MCRConnection connection = new MCRConnection();
        public string GetAdmins()
        {
            string admin = "('TFWEXM5S', 'T71DWFS1', 'SABMGVBR', 'SMADZCGS', 'VCF363FX', 'V2WA3AKZ', 'SXM64P78', 'QJN4S81Z', 'RRF4E5TR', 'PQ6377WD', 'SAMRB7FS')";
            return admin;
        }
       
    }
    public class PrintCertificate
    {
        MCRConnection connection = new MCRConnection();
        PrintDocument printDocument = new PrintDocument();
        MCRQueries quries = new MCRQueries();

        public void PrintSingleRecord(string RegistrationId)
        {
            
        }
        public void  PrintRecords()
        {
            using (SqlConnection cons = new SqlConnection(connection.GetDBConnection()))
            {
                try
                {
                    cons.Open();
                    string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+ Othernames +' '+ Surname  as Name, " +
                        " DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+" +
                        " BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + " +
                        " MotherSurname as NameofMother, MotherNationality as NationalityofMother, " +
                        " FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, " +
                        " FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, " +
                        " Firstname +'~'+ Surname as QRData, InformantDistrict + ', ' + InformantTA + ', '+ " +
                        " InformantVillage as InformantAddress from ChildDetail where ben<>'' and brn<>'' and pin<>'' " +
                        " and  InformantTA ='" + BirthCertificateDetails.dtTA + "' " +
                        " and InformantVillage ='" + BirthCertificateDetails.dtVillage + "' " +
                        " and InformantDistrict ='" + BirthCertificateDetails.dtDistrict + "' and RecordLocked = 0" +
                        " ";
                    using (SqlCommand command = new SqlCommand(query, cons))
                    {
                        using (SqlDataReader rdr = command.ExecuteReader())
                        {

                            if (rdr.HasRows)
                            {
                                // TI: Locking selected records
                                quries.LockRecords(BirthCertificateDetails.dtDistrict, BirthCertificateDetails.dtTA, BirthCertificateDetails.dtVillage);
                                while (rdr.Read())
                                {
                                    //DateTime.Now.ToString("dddd, dd MMMM yyyy");
                                    BirthCertificateDetails.BEN = rdr[0].ToString();
                                    BirthCertificateDetails.NationalID = rdr[1].ToString();
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
                                    //GenerateQrCode();

                                    printDocument.PrintPage += PrintDocumentOnPrintPage;
                                    printDocument.PrintController = new StandardPrintController();
                                    printDocument.DefaultPageSettings.Landscape = true;
                                    
                                   
                                    printDocument.Print();
                                    quries.UpdateRecords(BirthCertificateDetails.BEN);
                                }
                            }
                        }
                    }
                    cons.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Failed to connect to Server!!! Please contact Administrator" +ex);
                    System.Windows.Forms.Application.Exit();
                }
                
            }
        }
        public void GenerateQrCode(string RegId)
        {
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            con.Open();
            //string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where  ben='" + RegId + "'";

            string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict as InformantAddress from ChildDetail where ben='" + RegId + "'";
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
                    BirthCertificateDetails.qrImage = bmp;
                    //pictureBox1.Image = bmp;
                    //pictureBox1.Width = 150;
                    //pictureBox1.Height = 150;
                }
                System.Drawing.Image data = BirthCertificateDetails.qrImage;
            }
            con.Close();
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            
            //authorizer.ima
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            Declaration dec = new Declaration();

            
            var fnt = new Font("Arial", 9, FontStyle.Regular);
            var fnt1 = new Font("Arial", 6, FontStyle.Regular);
            var fnt2 = new Font("Arial", 7, FontStyle.Regular);

            e.Graphics.DrawString("\t\t\t\t1.  " + BirthCertificateDetails.lblBEN(), fnt, Brushes.Black, 170, 275);
            e.Graphics.DrawString("\t\t\t\t2.  " + BirthCertificateDetails.lblNationalID(), fnt, Brushes.Black, 170, 300);
            e.Graphics.DrawString("\t\t\t\t3.  " + BirthCertificateDetails.lblFullname(), fnt, Brushes.Black, 170, 325);
            e.Graphics.DrawString("\t\t\t\t4.  " + BirthCertificateDetails.lblDateofBirth(), fnt, Brushes.Black, 170, 350);
            e.Graphics.DrawString("\t\t\t\t5.  " + BirthCertificateDetails.lblSex(), fnt, Brushes.Black, 170, 375);
            e.Graphics.DrawString("\t\t\t\t6.  " + BirthCertificateDetails.lblPlaceofBirth(), fnt, Brushes.Black, 170, 400);
            e.Graphics.DrawString("\t\t\t\t7.  " + BirthCertificateDetails.lblNameofMother(), fnt, Brushes.Black, 170, 425);
            e.Graphics.DrawString("\t\t\t\t8.  " + BirthCertificateDetails.lblMotherNationality(), fnt, Brushes.Black, 170, 450);
            e.Graphics.DrawString("\t\t\t\t9.  " + BirthCertificateDetails.lblNameOfFather(), fnt, Brushes.Black, 170, 475);
            e.Graphics.DrawString("\t\t\t\t10. " + BirthCertificateDetails.lblNationalityOfFather(), fnt, Brushes.Black, 170, 500);
            e.Graphics.DrawString("\t\t\t\t11. " + BirthCertificateDetails.lblDateofRegistration(), fnt, Brushes.Black, 170, 525);


            string name = BirthCertificateDetails.Fullname.ToLower();
            string fullnametitlecase = textInfo.ToTitleCase(name);


            string mothertitlecase = textInfo.ToTitleCase(BirthCertificateDetails.NameofMother.ToLower());

            string sextitlecase = textInfo.ToTitleCase(BirthCertificateDetails.Sex.ToLower());
            string placeofbirthtitlecase = textInfo.ToTitleCase(BirthCertificateDetails.PlaceofBirth.ToLower());
            string Fathertitlecase = textInfo.ToTitleCase(BirthCertificateDetails.NameOfFather.ToLower());

            // Right panel of the Certificate
            e.Graphics.DrawString(BirthCertificateDetails.BEN, fnt, Brushes.Black, 590, 275);
            e.Graphics.DrawString(BirthCertificateDetails.NationalID, fnt, Brushes.Black, 590, 300);
            e.Graphics.DrawString(fullnametitlecase, fnt, Brushes.Black, 590, 325);
            e.Graphics.DrawString(BirthCertificateDetails.DateofBirth.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 590, 350);
            e.Graphics.DrawString(sextitlecase, fnt, Brushes.Black, 590, 375);
            e.Graphics.DrawString(placeofbirthtitlecase, fnt, Brushes.Black, 590, 400);
            e.Graphics.DrawString(mothertitlecase, fnt, Brushes.Black, 590, 425);
            e.Graphics.DrawString("Malawian", fnt, Brushes.Black, 590, 450);
            e.Graphics.DrawString(Fathertitlecase, fnt, Brushes.Black, 590, 475);
            e.Graphics.DrawString("Malawian", fnt, Brushes.Black, 590, 500);

            e.Graphics.DrawString(BirthCertificateDetails.DateofRegistration.ToString("dd MMMM yyyy"), fnt, Brushes.Black, 590, 525);

            e.Graphics.DrawString("\t\t\t\t" + dec.Declaration1(), fnt, Brushes.Black, 160, 550);
            e.Graphics.DrawString("\t\t\t\t\t\t" + dec.Declaration2(), fnt, Brushes.Black, 150, 575);

            e.Graphics.DrawString("\t\t\t\t\t\t" + dec.DeclarationDate(), fnt, Brushes.Black, 150, 600);
            e.Graphics.DrawString( "Principal Secretary", fnt2, Brushes.Black, 650, 742);

            e.Graphics.DrawString(BirthCertificateDetails.PlaceOfRegistration.ToLower(), fnt1, Brushes.Black, 250, 790);
            //e.Graphics.DrawImage(myImage, e.MarginBounds);

            GenerateQrCode(BirthCertificateDetails.BEN);

            e.Graphics.DrawImage(BirthCertificateDetails.qrImage, 670, 30,110,110);


            System.Drawing.Image newImage = System.Drawing.Image.FromFile(@"C:\Users\ISSAH\source\repos\MCRPrinting\MCRPrinting\Resources\AuthoritySignature.gif");

            // Points, Top Left corner, Upper left corner, width, Height
            //e.Graphics.DrawImage(newImage,0,0,0,0);
            e.Graphics.DrawImage(newImage,640,693,90,40);
            using (Pen pen = new Pen(Color.Black))
            {
                e.Graphics.DrawLine(pen,770,755,610,755);
            }
            //UpdateRecords(BEN);
        }
    }
}
