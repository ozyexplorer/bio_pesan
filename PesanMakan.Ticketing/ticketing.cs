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
using BarcodeLib.Barcode.WinForms;
using System.Drawing.Imaging;
using PesanMakan.Ticketing.PesanMakanService;
using BarcodeLib.Barcode;

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

        public String getNama()
        {
            //include validation, logic, logging or whatever you like here
            return this.myNama;
        }
        public void setNama(String value)
        {
            //include more logic
            this.myNama = value;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
                string stat = getMyField();
                string nama = getNama();
                e.Graphics.DrawString(stat, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, new PointF(100, 100));
                
                BarcodeLib.Barcode.Linear code128 = new BarcodeLib.Barcode.Linear();
                 code128.Type = BarcodeType.CODE128;
                 code128.Data = stat;

                 code128.AddCheckSum = true;
                 code128.UOM = UnitOfMeasure.PIXEL;

                 // Set Code 128 barcode image size and quiet zones.
                 code128.BarWidth = 1;
                 code128.BarHeight = 75;
                 code128.BottomMargin = 10;
                 code128.TopMargin = 10;
                 code128.ImageFormat = System.Drawing.Imaging.ImageFormat.Jpeg;
   
                 // More Code 128 barcode settings here, like image color, font, human-readable text font etc.

                 // save barcode image into your system
                 code128.drawBarcode("hiks1.jpg");
    
              
                    Image newImage = Image.FromFile("hiks1.jpg");

                    float x = 90.0F;
                    float y = 30.0F;
                    // Draw image to screen.
                    e.Graphics.DrawImage(newImage, x, y);
                    e.Graphics.DrawString(nama, new Font("Times New Roman", 13, FontStyle.Bold), Brushes.Black, new PointF(60, 10));

                    string tanggal = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    e.Graphics.DrawString(tanggal, new Font("Times New Roman", 12), Brushes.Black, new PointF(75, 150));
                    
        }

        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            if (objService.startServiceDekstop() == "1")
            {
                string token = objService.startServiceTicketing();

                string[] tokensplit = token.Split('-');
                string stat = tokensplit[0];
                string nama = tokensplit[1];


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
                    setNama(nama);
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

        public string myNama { get; set; }
    }
}
