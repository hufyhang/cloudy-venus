using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class SetAlarm : Form
    {
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();

        private const int SYSCommand = 0xf017;
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 61456;
        private const int HTCAPTION = 2;

        public SetAlarm()
        {
            InitializeComponent();
            this.initialEvents();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        protected void initialEvents()
        {
            this.MouseDown += new MouseEventHandler(SetAlarm_MouseDown);
        }

        private void SetAlarm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SysCommand, SC_MOVE | HTCAPTION, 0);
            }
        }

        public String getTime()
        {
            return this.dateTimePicker1.Text;
        }
    }
}
