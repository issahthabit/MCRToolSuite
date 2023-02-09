using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCRPrinting.Model
{
    public class MCRConnection
    {
        
        public string GetDBConnection()
        {
            //string Constring = @"Data Source=issah\issah;Initial Catalog=MassReg;Persist Security Info=True;User ID=sa;Password=lengan1";
            string Constring = @"Data Source=10.45.80.51\mcr;Initial Catalog=MassReg;User ID=sa;Password=Password1";
            return Constring;
        }
        
    }
}
