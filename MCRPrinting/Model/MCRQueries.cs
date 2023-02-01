using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCRPrinting.Model;
using System.Drawing;

namespace MCRPrinting.Model
{
    public class MCRQueries
    {
        MCRConnection connection = new MCRConnection();

        public DataTable LoadRecordsByTAandVillage(string TA,string village)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            SqlCommand cmd = new SqlCommand("select ben,Firstname,Othernames,Surname,DateOfBirth,MotherFirstname,MotherOthernames,MotherSurname,FatherFirstname,FatherOthernames,FatherSurname from ChildDetail where InformantTA='" + TA + "' and InformantVillage='" + village + "' and ben<>'' and RecStatus=4 and brn<>''  order by Surname,Firstname asc",con);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
                 
            return dt;
        }
        public void GetRecordsByPersonid(string personid)
        {
            

            string qry = "select ben as BirthEntryNumber, '' as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  RegistrationId='" + personid + "'";

            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
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

                        }
                    }
                }
                con.Close();
            }
        }
        void UpdateRecords(string personid)
        {
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                string query = "update ChildDetail set RecStatus=5 where ben='" + personid + "'";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }
        }
    }
}
