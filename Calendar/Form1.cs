using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calendar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int i;
            
            int j;
            for (j = 0; j < 5; j++)
            {
                for (i = 0; i < 7; i++)
                {
                    var picture = new PictureBox
                    {
                        Name = "pictureBox",
                        Size = new Size(100, 100),
                        Location = new Point((100 * i) + (2 * i) + i +50, (100 * j) + (2 * j) + j+50),
                        Image = Image.FromFile("C:/Users/USER/Downloads/hai.jpg"),
                        BackgroundImageLayout = ImageLayout.Zoom,
                    };
                    this.Controls.Add(picture);

                }
            }
        }


    }
}
