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
        private int counter = 0;

        public Splash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ++counter;
            if (this.counter > 2)
            {
                this.label1.Visible = true;
                this.progressBarX1.Visible = false;
                if (this.counter > 3)
                {
                    this.Dispose();
                }
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
        }
    }
}
