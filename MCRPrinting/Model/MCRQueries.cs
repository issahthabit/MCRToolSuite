using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCRPrinting.Model;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Net;
using System.Globalization;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace MCRPrinting.Model
{
    public class MCRQueries
    {
        MCRConnection connection = new MCRConnection();
        MCRModel model = new MCRModel();
        PrintDocument printDocument = new PrintDocument();

        public DataTable LoadRecordsByTAandVillage(string TA,string village)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            SqlCommand cmd = new SqlCommand("select ben,Firstname,Othernames,Surname,DateOfBirth,MotherFirstname,MotherOthernames,MotherSurname,FatherFirstname,FatherOthernames,FatherSurname from ChildDetail where InformantTA='" + TA + "' and InformantVillage='" + village + "' and ben<>'' and RecStatus=4 and brn<>'' and Edituser not in "+model.GetAdmins()+"  order by Surname,Firstname asc",con);
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            sda.Fill(dt);
                 
            return dt;
        }
        public void GetRecordsByPersonid(string personid)
        {
            string qry = "select ben as BirthEntryNumber, pin as NationalID, Firstname + ' '+Othernames +' '+Surname  as Name, DateOfBirth as DateOfBirth, ChildSex as Sex, BirthVillage +',' + BirthTA +','+BirthDistrict as PlaceofBirth, MotherFirstname + ' '+MotherOthernames +' ' + MotherSurname as NameofMother, MotherNationality as NationalityofMother, FatherFirstname + ' '+ FatherOthernames +' '+ FatherSurname as NameofFather, FatherNationality as NationalityofFather, DateOfRegistration as DateOfRegistration, Firstname+'~'+Surname as QRData, InformantDistrict + ', ' +InformantTA + ', '+InformantVillage as InformantAddress from ChildDetail where  RegistrationId='" + personid + "'";

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
        public void UpdateRecords(string personid)
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
        public DataTable LoadrecordsCountByDistrict(string District)
        {
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict='" + District + "' and ben<>'' and brn<>'' and PlaceOfRegistrationId<>'' and Edituser not in "+model.GetAdmins()+" GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataTable LoadrecordsCountByTA(string District,string TA)
        {
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict='" + District + "' AND InformantTA='"+TA+ "' and ben<>'' and brn<>'' and PlaceOfRegistrationId<>'' GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataTable LoadrecordstByVillage(string District,string TA, string Village)
        {
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict='" + District + "' AND  PlaceOfRegistrationId<>'' and InformantTA='" + TA+ "' AND InformantVillage='"+Village+"' and ben<>'' and brn<>''  and PlaceOfRegistrationId<>'' GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage");
            SqlDataAdapter sda = new SqlDataAdapter();
            cmd.Connection = con;
            sda.SelectCommand = cmd;
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return dt;
        }
        public DataTable LoadDistrict()
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select InformantDistrict from ChildDetail where InformantDistrict<>'' and InformantDistrict<>'4' and brn<>'' and ben<>'' and PlaceOfRegistrationId<>'' group by InformantDistrict ", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                
                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
        void LoadRecordsCount()
        {
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT InformantDistrict,InformantTA,InformantVillage,COUNT(*) AS RECORDS FROM ChildDetail where InformantDistrict<>'4' and InformantDistrict<>'' and PlaceOfRegistrationId<>'' GROUP BY InformantDistrict,InformantTA,InformantVillage order by InformantDistrict,InformantTA,InformantVillage"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                        }
                    }
                }
            }
        }
        public string LoadDistrictTotal(string district)
        {
            string districts;
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            con.Open();
            string query = "select count(*) from ChildDetail where InformantDistrict='" + district + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                districts  = reader[0].ToString();
            }
            con.Close();
            return district;
        }
        public string LoadTATotal(string district, string TA)
        {
            string districts;
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            con.Open();
            string query = "select count(*) from ChildDetail where InformantDistrict = '" + district + "' and InformantTA = '"+TA+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                districts = reader[0].ToString();
            }
            con.Close();
            return district;
        }
        public string LoadVillageTotal(string district, string TA, string Village)
        {
            string districts;
            SqlConnection con = new SqlConnection(connection.GetDBConnection());
            con.Open();
            string query = "select count(*) from ChildDetail where InformantDistrict = '" + district + "' and InformantTA = '" + TA + "' and InformantVillage = '" + Village + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                districts = reader[0].ToString();
            }
            con.Close();
            return district;
        }
        public DataTable LoadTA(string District)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct InformantTA from ChildDetail where InformantDistrict='" + District + "' and InformantTA <> '' and brn<>'' and ben <> ''", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public DataTable LoadVillages(string District, string TA)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct InformantVillage from ChildDetail where InformantDistrict='" + District + "' and  order by InformantVillage asc", con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                sda.Fill(dt);
                con.Close();
            }
            return dt;
        }
        public DataTable LoadRecords(string TA, string village)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(connection.GetDBConnection()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select ben,Firstname,Othernames,Surname,DateOfBirth,MotherFirstname,MotherOthernames,MotherSurname,FatherFirstname,FatherOthernames,FatherSurname from ChildDetail where InformantTA='" + TA + "' and InformantVillage='" + village + "' and ben<>'' and RecStatus=4 and brn<>''  order by Surname,Firstname asc"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                con.Close();
            }
            return dt;
        }
        
       
    }
}
