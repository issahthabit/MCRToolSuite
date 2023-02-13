using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRPrinting.Model
{
    public static class BirthCertificateDetails
    {
        public static string BEN { get; set; } = string.Empty;
        public static string Fullname { get; set; } = string.Empty;
        public static string regid { get; set; }
        public static string NationalID { get; set; } = string.Empty;
        public static DateTime DateofBirth { get; set; } = DateTime.Now;
        public static string Sex { get; set; } = string.Empty;
        public static string PlaceofBirth { get; set; } = string.Empty;
        public static string NameofMother { get; set; } = string.Empty;
        public static string MotherNationality { get; set; } = "Malawian";
        public static string NameOfFather { get; set; } = string.Empty;
        public static string NationalityOfFather { get; set; } = string.Empty;
        public static DateTime DateofRegistration { get; set; } = DateTime.Now;
        public static string PlaceOfRegistration { get; set; } = string.Empty;
        public static string Placeofreg { get; set; } = string.Empty;
        public static string dtTA { get; set; } = string.Empty;
        public static string dtVillage { get; set; } = string.Empty;
        public static string dtDistrict { get; set; } = string.Empty;
        public static Image qrImage { get; set; } = null;

        public static string username { get; set; } = string.Empty;
        


        // Labels
        public static string lblBEN ()
        {
            string bens = "Birth Entry Number";
            return bens;
        }
        public static string lblFullname ()
        {
            string name = "Name";
            return name;
        }
        public static string lblregid ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblNationalID ()
        {
            string name = "National ID";
            return name;
        }
        public static string lblDateofBirth ()
        {
            string name = "Date of Birth";
            return name;
        }
        public static string lblSex ()
        {
            string name = "Sex";
            return name;
        }

        public static string lblPlaceofBirth ()
        {
            string name = "Place of Birth";
            return name;
        }
        public static string lblNameofMother ()
        {
            string name = "Name of Mother";
            return name;
        }
        public static string lblMotherNationality ()
        {
            string name = "Nationality of Mother";
            return name;
        }
        public static string lblNameOfFather ()
        {
            string name = "Name of Father";
            return name;
        }
        public static string lblNationalityOfFather ()
        {
            string name = "Nationality of Father";
            return name;
        }
        public static string lblDateofRegistration ()
        {
            string name = "Date of Registration";
            return name;
        }
        public static string lblPlaceOfRegistration ()
        {
            string name = "Place of Registration";
            return name;
        }
        //public static string lblPlaceofreg ()
        //{
        //    string name = "Registration ID";
        //    return name;
        //}
        //public static string lbldtTA ()
        //{
        //    string name = "Registration ID";
        //    return name;
        //}
        //public static string lbldtVillage ()
        //{
        //    string name = "Registration ID";
        //    return name;
        //}
        //public static string lbldtDistrict ()
        //{
        //    string name = "Registration ID";
        //    return name;
        

    }

}
