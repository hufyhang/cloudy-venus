using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class Splash : Form
    {

        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            this.label4.Location = new Point(this.Width, this.label4.Location.Y);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.label4.Location.X >= 0 - this.label4.Width)
            {
                this.label4.Location = new Point(this.label4.Location.X - 5, this.label4.Location.Y);
            }
            else
            {
                this.label4.Location = new Point(this.Width, this.label4.Location.Y);
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            this.Opacity += 0.05;
        }
    }
}
