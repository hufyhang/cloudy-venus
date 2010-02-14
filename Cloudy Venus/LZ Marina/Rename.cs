using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LZ_Marina
{
    public partial class Rename : Form
    {
        private String former;
        private String formerName;
        private int flag;

        public Rename()
        {
            InitializeComponent();
        }

        public Rename(String name, String full, int flag)
        {
            InitializeComponent();

            this.textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);

            this.formerName = this.textBox1.Text = name;
            this.former = full;
            this.flag = flag;
        }

        protected void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                process();
                this.DialogResult = DialogResult.OK;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            process();
            this.DialogResult = DialogResult.OK;
        }

        protected void process()
        {
            StreamReader reader;
            if (this.flag == 1)
            {
                reader = new StreamReader(Application.StartupPath + @"\apps");
            }
            else
            {
                reader = new StreamReader(Application.StartupPath + @"\localApps");
            }

            String text = reader.ReadToEnd();
            reader.Close();
            text = text.Replace(this.formerName + @"`" + this.former, this.textBox1.Text + @"`" + this.former);
            StreamWriter writer;
            if (this.flag == 1)
            {
                writer = new StreamWriter(Application.StartupPath + @"\apps");
            }
            else
            {
                writer = new StreamWriter(Application.StartupPath + @"\localApps");
            }
            writer.Flush();
            writer.Write(text);
            writer.Close();
        }
    }
}
