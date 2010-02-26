using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class Rename : Form
    {
        private String former;
        private String formerName;
        private int flag;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();

        private const int SYSCommand = 0xf017;
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 61456;
        private const int HTCAPTION = 2;

        public Rename()
        {
            InitializeComponent();
            this.initialEvents();
        }

        public Rename(String name, String full, int flag)
        {
            InitializeComponent();
            this.initialEvents();

            this.textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);

            this.formerName = this.textBox1.Text = name;
            this.former = full;
            this.flag = flag;
        }

        protected void initialEvents()
        {
            this.MouseDown += new MouseEventHandler(Rename_MouseDown);
        }

        private void Rename_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SysCommand, SC_MOVE | HTCAPTION, 0);
            }
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
