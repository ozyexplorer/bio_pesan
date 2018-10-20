using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using Limilabs.Barcode;
using System.Drawing.Imaging;
using PesanMakan.Ticketing.PesanMakanService;

namespace PesanMakan.Ticketing
{
    public partial class ticketing : Form
    {
        public ticketing()
        {
            InitializeComponent();
        }


        private String myField; //"private" means access to this is restricted

        public String getMyField()
        {
            //include validation, logic, logging or whatever you like here
            return this.myField;
        }
        public void setMyField(String value)
        {
            //include more logic
            this.myField = value;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
                string stat = getMyField();
                e.Graphics.DrawString(stat, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new PointF(100, 100));
            
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

            if (objService.startServiceDekstop() == "1")
            {
                string stat = objService.startServiceTicketing();

                if (stat == "Tidak Booking"){
                    MessageBox.Show("Tidak Booking");
                }
                else if (stat == "Belum Terdaftar")
                {
                    MessageBox.Show("Anda Belum Terdaftar Dalam Database Pesan Makan Silahkan Daftar Di Bagian SDM");
                }
                else if (stat == "Jam Makan Tutup")
                {
                    MessageBox.Show("Jam Makan Tutup, Silahkan Hadir Tepat Waktu");
                }
                else if (stat == "Print")
                {
                    MessageBox.Show("Tiket Anda Sudah Terprint");
                }
                else 
                {
                    setMyField(stat);
                    if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                        printDocument1.Print();
                    //MessageBox.Show(stat);
                }
            }
        }

        private void ticketing_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);﻿

            InitTimer();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
    }
}
