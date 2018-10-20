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

namespace PesanMakan.Dekstop
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
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



        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            string username = txtUsername.Text;
            string password = txtPassword.Text;

            if(username != "" && password != ""){
                
                string status = objService.loginWithUserNameDekstop(username, password);

                if(status == "0"){
                    MessageBox.Show("User Name belum diregistrasi, Harap registrasi di bagian SDM");
                }else if(status == "1"){
                    RegCard objRegCard = new RegCard();
                    this.Hide();
                    objRegCard.Show();
                }

                
            }else{
                MessageBox.Show("Username atau Nama harus diisi");
            }
        }

    }
}
