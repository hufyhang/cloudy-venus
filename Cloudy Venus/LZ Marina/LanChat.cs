using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Reflection;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class LanChat : Form
    {
        private int PORT = 1012;
        private String username;
        private TcpListener tcpl;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();

        private const int SYSCommand = 0xf017;
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 61456;
        private const int HTCAPTION = 2;

        public LanChat(String username)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.initialEvents();

            this.username = username;

            new delegateActivateListner(this.ActivateListner).BeginInvoke(null, null);

            this.richTextBox1.Text = "> LAN Messenger <";
        }

        protected void initialEvents()
        {
            this.Disposed += new EventHandler(LanChat_Disposed);
            this.textBoxX1.KeyDown += new KeyEventHandler(textBoxX1_KeyDown);
            this.panelEx1.MouseDown += new MouseEventHandler(panelEx1_MouseDown);
        }

        private void panelEx1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SysCommand, SC_MOVE | HTCAPTION, 0);
            }
        }

        private void LanChat_Disposed(object sender, EventArgs e)
        {
            this.tcpl.Stop();
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                new delegateSender(this.Sender).BeginInvoke(this.textBoxX2.Text, this.textBoxX1.Text, null, null);
            }
        }

        protected delegate void delegateActivateListner();
        protected void ActivateListner()
        {
            while (true)
            {
                this.Receiver();
            }
        }

        protected delegate void delegateSender(String IP, String msg);
        protected void Sender(String IP, String msg)
        {
            String sendMsg = this.username + @" > " + msg;
            this.richTextBox1.Text += "\r\n" + sendMsg + "\r\n";
            this.textBoxX1.Text = "";
            this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
            this.richTextBox1.ScrollToCaret();
            try
            {
                TcpClient tcpc = new TcpClient(IP, this.PORT);
                NetworkStream tcpStream = tcpc.GetStream();
                StreamWriter reqStreamW = new StreamWriter(tcpStream);
                reqStreamW.Write(sendMsg);
                reqStreamW.Flush();
                tcpStream.Close();
                tcpc.Close();
            }
            catch (Exception e)
            {
                this.richTextBox1.Text += @">ERROR< \r\n " + e.ToString() + "\r\n";
            }
        }

        protected delegate void delegateReceiver();
        protected void Receiver()
        {
            try
            {
                this.tcpl = new TcpListener(this.PORT);
                this.tcpl.Start();
                Socket s = this.tcpl.AcceptSocket();
                string remote = s.RemoteEndPoint.ToString();
                Byte[] stream = new Byte[102400];
                int i = s.Receive(stream);
                this.richTextBox1.Text += System.Text.Encoding.UTF8.GetString(stream) + "\r\n";
                this.richTextBox1.Select(this.richTextBox1.TextLength, 0);
                this.richTextBox1.ScrollToCaret();
                this.tcpl.Stop();
            }
            catch (Exception e)
            {
                this.richTextBox1.Text += @">ERROR< \r\n " + e.ToString() + "\r\n";
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            new delegateSender(this.Sender).BeginInvoke(this.textBoxX2.Text, this.textBoxX1.Text, null, null);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text = "";
        }
    }
}
