using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRPrinting.Model
{
    public static class BirthCertificateDetails
    {
        public static string BEN { get; set; }
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
    }
}
