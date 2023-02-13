using MCRPrinting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCRPrinting
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();

            

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string scannedText = txtScanCard.Text;
            string[] cardpart = scannedText.Split('~');

            string Fullname = cardpart[4].ToString();

            BirthCertificateDetails.username= Fullname;

            SplashScreen splashScreen = new SplashScreen();
            splashScreen.Show();
            this.Hide();
        }
    }
}
