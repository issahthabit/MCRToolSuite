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
    public partial class SplashScreen : Form
    {
        MCRConnection connection = new MCRConnection();

        Serverconnection serverconnection = new Serverconnection();
        public SplashScreen()
        {
            InitializeComponent();

            //bool server = serverconnection.IsServerConnected(connection.GetDBConnection());
            //if (serverconnection.IsServerConnected(connection.GetDBConnection()))
            //    MessageBox.Show("Server not available");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Increment(2);
            if(progressBar1.Value ==100 )
            {
                timer1.Enabled=false;
                Form1 form = new Form1();
                form.Show();
                this.Hide();
            }
        }
    }
}
