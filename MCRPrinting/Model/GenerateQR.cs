using QRCoder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCRPrinting.Model;
using System.Data.Common;

namespace MCRPrinting.Model
{
    public class GenerateQR
    {
        MCRConnection connection = new MCRConnection();
        
        public Bitmap GenerateQrCode(string RegId)
        {
            Bitmap bmp = null;

            SqlConnection con = new SqlConnection(connection.GetDBConnection());
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
                bmp = qRCode.GetGraphic(1);
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Bmp);
                    //pictureBox1.Image = bmp;
                    //pictureBox1.Width = 150;
                    //pictureBox1.Height = 150;
                }
                
            }
            con.Close();

            return bmp;

        }
    }
}
