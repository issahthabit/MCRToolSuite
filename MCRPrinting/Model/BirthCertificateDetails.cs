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
        public static string MotherNationality { get; set; } = string.Empty;
        public static string NameOfFather { get; set; } = string.Empty;
        public static string NationalityOfFather { get; set; } = string.Empty;
        public static DateTime DateofRegistration { get; set; } = DateTime.Now;
        public static string PlaceOfRegistration { get; set; } = string.Empty;
        public static string Placeofreg { get; set; } = string.Empty;
        public static string dtTA { get; set; } = string.Empty;
        public static string dtVillage { get; set; } = string.Empty;
        public static string dtDistrict { get; set; } = string.Empty;
        public static Image qrImage { get; set; } = null;



        // Labels
        public static string lblBEN ()
        {
            string bens = "Birth Entry Number";
            return bens;
        }
        public static string lblFullname ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblregid ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblNationalID ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblDateofBirth ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblSex ()
        {
            string name = "Registration ID";
            return name;
        }

        public static string lblPlaceofBirth ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblNameofMother ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblMotherNationality ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblNameOfFather ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblNationalityOfFather ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblDateofRegistration ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblPlaceOfRegistration ()
        {
            string name = "Registration ID";
            return name;
        }
        public static string lblPlaceofreg ()
        {
            string name = "Registration ID";
            return name;
        }
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
