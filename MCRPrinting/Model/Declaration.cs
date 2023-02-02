using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRPrinting.Model
{
    public class Declaration
    {
        public string Declaration1()
        {
            string dec = "I hereby certify the above to be a true and correct extract from the";
            return dec;
        }
        public string Declaration2()
        {
            string dec = "Birth Register kept at NRB.";
            return dec;
        }
        public string DeclarationDate()
        {
            string dec = "Dated this " + DateTime.Now.ToString("dd MMMM yyyy"); 
            return dec;
        }
    }
}
