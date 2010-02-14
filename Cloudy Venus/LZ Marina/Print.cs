using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class Print : Form
    {
        private WebBrowser web;

        public Print()
        {
            InitializeComponent();
        }

        public Print(WebBrowser web)
        {
            this.web = web;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.web.ShowPrintDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.web.ShowPrintPreviewDialog();
        }


    }
}
