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

namespace PesanMakan.Desktop
{
    public partial class RegisterMember : Form
    {
        public RegisterMember()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Show()
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            DataTable dsReturn = new DataTable();
            dsReturn = objService.GetNikNamaActDesktop();

            dgMember.DataSource = dsReturn;
            dgMember.Columns[2].Visible = false;
        }

        private void RegisterMember_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);﻿

            Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (dgMember.CurrentRow.Index != -1)
            {
                txtNik.Text = dgMember.CurrentRow.Cells[0].Value.ToString();
                txtNama.Text = dgMember.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            Dashboard objDashboard = new Dashboard();
            this.Hide();
            objDashboard.Show();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
         
            GetSetServiceSoapClient objService1 = new GetSetServiceSoapClient();
            
            objService1.RegMember(txtNik.Text, txtNama.Text, txtCardId.Text); 
            MessageBox.Show("Data Berhasil Dimasukan");
            Show();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetSetServiceSoapClient objService = new GetSetServiceSoapClient();

            DataTable dsReturn = new DataTable();

            if (txtSearch.Text != "")
            {
                dsReturn = objService.GetSearchDataDekstop(txtSearch.Text);
                dgMember.DataSource = dsReturn;
                dgMember.Columns[2].Visible = false;
            }
            else
            {
                Show();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
