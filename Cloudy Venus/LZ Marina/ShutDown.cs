using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class ShutDown : Form
    {
        private TabControl tab;
        private Form1 form;

        public ShutDown(Form1 form, TabControl tab)
        {
            this.form = form;
            this.tab = tab;
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reload Cloudy Venus now?\r\nAll your unsaved works will lose.", "Cloudy Venus reloading...",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log off from Cloudy Venus?", "Log off...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {                
                Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tab.TabPages.Add(new Console(form));
            tab.SelectedIndex = tab.TabPages.Count - 1;
            this.Dispose();
        }
    }
}
