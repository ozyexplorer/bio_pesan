using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PesanMakan.Desktop;

namespace PesanMakan.Desktop
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);


        }
        private const int cGrip = 16;
        private const int cCaption = 32;

        protected override void WndProc(ref Message m)
        {
            if(m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if(pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                } 
                if(pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
                
            }
            base.WndProc(ref m);
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            int w = Screen.PrimaryScreen.Bounds.Width;
            int h = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Size = new Size(w, h);﻿

            Show();
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            PesanMakan.Dekstop.RegCard objRegCard = new PesanMakan.Dekstop.RegCard();
            this.Hide();
            objRegCard.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            RegisterMember objRegMember = new RegisterMember();
            this.Hide();
            objRegMember.Show();            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            PesanMakan.Dekstop.login objLogin = new PesanMakan.Dekstop.login();
            this.Hide();
            objLogin.Show();
        }
    }
}
