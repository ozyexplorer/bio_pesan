using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PesanMakan.Dekstop.PesanMakanServices;
using PesanMakan.Desktop;

namespace PesanMakan.Dekstop
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            // Checks the value of the text.


            // Initializes the variables to pass to the MessageBox.Show method.

            string message = "Yakin untuk keluar program ?";
            string caption = "Perhatian !!!";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            // Displays the MessageBox.

            result = MessageBox.Show(message, caption, buttons);

            if (result == System.Windows.Forms.DialogResult.Yes)
            {

                // Closes the parent form.

                this.Close();

            }
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 3000; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            if (objService.startServiceDekstop()=="1")
            {
                string stat = objService.loginWithCardUIDDekstop();

                if (stat == "0")
                {
                    MessageBox.Show("Kartu Belum diregistrasi, Harap Registrasi di bagian SDM");
                    }
                else if (stat == "1")
                {
                    Dashboard objDashboard = new Dashboard();
                    this.Hide();
                    objDashboard.Show();
                    timer1.Stop();
                }
            }
        }

            
        
     
        private void login_Load(object sender, EventArgs e)
        {
            InitTimer();
        }

        


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
