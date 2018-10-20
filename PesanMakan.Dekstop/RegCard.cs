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
    public partial class RegCard : Form
    {
        public RegCard()
        {
            InitializeComponent();
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
            txtCardID.Text = objService.initCardUIDesktop();
        }

        private void Show()
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            DataTable dsReturn = new DataTable();
            dsReturn = objService.GetUserDataDekstop();

            dgRegCard.DataSource = dsReturn;
            dgRegCard.Columns[4].Visible = false;

            cbRole.Items.Add("Admin");
            cbRole.Items.Add("User");
            cbRole.Items.Add("Vendor");
            cbRole.SelectedItem = "Admin";

            DataTable dtReturn = new DataTable();
            dtReturn = objService.GetNikNamaDekstop();

            for (int i = 0; i < dtReturn.Rows.Count; i++ )
            {
                string hasil = dtReturn.Rows[i]["NIK"] + " - " + dtReturn.Rows[i]["NAMA"];
                cbNik.Items.Add(hasil);
                cbNik.SelectedItem = dtReturn.Rows[0]["NIK"] + " - " + dtReturn.Rows[0]["NAMA"];
            }
        }

        private void frm_menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void RegCard_Load_1(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);﻿
            Show();
            InitTimer();
        }

        private void btnEdit_Click_1(object sender, EventArgs e)
        {
            if (dgRegCard.CurrentRow.Index != -1)
            {
                cbNik.Text = dgRegCard.CurrentRow.Cells[0].Value.ToString();
                txtCardID.Text = dgRegCard.CurrentRow.Cells[2].Value.ToString();
                cbRole.Text = dgRegCard.CurrentRow.Cells[3].Value.ToString();
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgRegCard.CurrentRow.Index != -1)
            {
                string cbNik = dgRegCard.CurrentRow.Cells[0].Value.ToString();
                string nama = dgRegCard.CurrentRow.Cells[1].Value.ToString();
                string txtCardID = dgRegCard.CurrentRow.Cells[2].Value.ToString();
                string cbRole = dgRegCard.CurrentRow.Cells[3].Value.ToString();

                GetSetServiceSoapClient objService2 = new GetSetServiceSoapClient();
                objService2.DelData(cbNik, nama, txtCardID, cbRole);
                MessageBox.Show("Data Berhasil Dihapus");
                Show();
            }
        }

        private void btnSimpan_Click_1(object sender, EventArgs e)
        {
            string cbNikselectedItem = cbNik.Items[cbNik.SelectedIndex].ToString();
            string cbRoleselectedItem = cbRole.Items[cbRole.SelectedIndex].ToString();

            string[] cbNikSplit = cbNikselectedItem.Split('-');
            string nik = cbNikSplit[0].Trim();
            string nama = cbNikSplit[1].Trim();

            GetSetServiceSoapClient objService1 = new GetSetServiceSoapClient();
            int stat = objService1.RegCardDekstop(nik, nama, txtCardID.Text, cbRoleselectedItem);

            if (stat == 0)
            {
                MessageBox.Show("Data Berhasil Dimasukan");
            }
            else if (stat == 1)
            {
                MessageBox.Show("Gagal Didaftarkan Karna ID Sudah Terpakai");
            }

            Show();
        }

        private void bunifuImageButton1_Click_1(object sender, EventArgs e)
        {
            Dashboard objDashboard = new Dashboard();
            this.Hide();
            objDashboard.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            DataTable dsReturn = new DataTable();

            if (txtSearch.Text!="")
            {
                dsReturn = objService.GetSearchDataDekstop(txtSearch.Text);
                dgRegCard.DataSource = dsReturn;
                dgRegCard.Columns[4].Visible = false;
            }else{
                Show();
            }
            
        }

    }
}
