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
        
        void PrintRecords()
        {
            using (SqlConnection cons = new SqlConnection(connection.GetDBConnection()))
            {
                cons.Open();
                string query = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  InformantTA='" + BirthCertificateDetails.dtTA + "' and InformantVillage='" + BirthCertificateDetails.dtVillage + "' and InformantDistrict='" + BirthCertificateDetails.dtDistrict + "'";
                using (SqlCommand command = new SqlCommand(query, cons))
                {
                    using (SqlDataReader rdr = command.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {


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
                                //printDocument.EndPrint += new PrintEventHandler(EndPrint);

                                printDocument.Print();

                            }
                        }
                    }
                }
                cons.Close();
            }
        }
        private void PrintDocumentOnPrintPage(object sender, PrintPageEventArgs e)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            Declaration dec = new Declaration();

            //GenerateQrCode(BirthCertificateDetails.BEN);
            var fnt = new Font("Arial", 9, FontStyle.Regular);

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


            e.Graphics.DrawImage(BirthCertificateDetails.qrImage, 600, 75);
            //UpdateRecords(BEN);
        }
    }
}
