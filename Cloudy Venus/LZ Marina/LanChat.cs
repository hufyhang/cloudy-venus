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
using System.Collections;

namespace LZ_Marina
{
    public partial class LanChat : Form
    {
        private int PORT = 1012;
        private String username;
        private TcpListener tcpl;
        private ArrayList ipList;
        private String ipAddress;

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

            this.ipList = new ArrayList();
            this.username = username;
            this.ipAddress = this.getIPAddress();

            new delegateActivateListner(this.ActivateListner).BeginInvoke(null, null);
            new delegateUpdateUserlist(this.updateUserlist).BeginInvoke(null, null);
            new delegateBroadcast(this.broadcast).BeginInvoke(null, null);

            this.richTextBox1.Text = "> LAN Messenger <";
        }

        protected void initialEvents()
        {
            this.Disposed += new EventHandler(LanChat_Disposed);
            this.textBoxX1.KeyDown += new KeyEventHandler(textBoxX1_KeyDown);
            this.MouseDown += new MouseEventHandler(LanChat_MouseDown);
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.ipAddress = (String)this.ipList[this.listView1.SelectedItems[0].Index];
            }
        }

        private void LanChat_MouseDown(object sender, MouseEventArgs e)
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
                if (this.textBoxX1.Text.Length != 0)
                {
                    new delegateSender(this.Sender).BeginInvoke(this.ipAddress, this.textBoxX1.Text, null, null);
                }
            }
        }

        protected String getIPAddress()
        {
            IPHostEntry host = new IPHostEntry();
            host = Dns.Resolve(Dns.GetHostName());
            if (host.AddressList.Length == 1)
            {
                return host.AddressList[0].ToString();
            }
            else
            {
                return host.AddressList[1].ToString();
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
                this.richTextBox1.Text += ">ERROR< \r\n " + e.ToString() + "\r\n";
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
                this.richTextBox1.Text += ">ERROR< \r\n " + e.ToString() + "\r\n";
            }
        }

        protected delegate void delegateBroadcast();
        protected void broadcast()
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("192.168.109.255"), 1012);

            string computerInfo = ":USER" + ":" + this.username + ":" + Dns.Resolve(Dns.GetHostName()).AddressList[0];

            byte[] buff = Encoding.Default.GetBytes(computerInfo);
            while (true)
            {
                udpClient.Send(buff, buff.Length, ep);
                Thread.Sleep(2000);
            }
        }

        protected delegate void delegateUpdateUserlist();
        protected void updateUserlist()
        {
            UdpClient server = new UdpClient(1012);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                byte[] buff = server.Receive(ref ep);
                String user = Encoding.Default.GetString(buff);
                String cmd = user.Substring(0, 6);
                String user1 = user.Substring(6);
                if (cmd == ":USER:")
                {
                    try
                    {
                        String[] s = user1.Split(':');
                        ListViewItem lviUserName = new ListViewItem();
                        lviUserName.Text = s[0];
                        String lvsiIP = s[1];
                        this.ipList.Add(lvsiIP);

                        bool flag = true;
                        for (int i = 0; i < this.listView1.Items.Count; i++)
                        {
                            if (lvsiIP == (String)this.ipList[i])
                            {
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            this.listView1.Items.Add(lviUserName);
                        }
                    }
                    catch
                    {
                        this.richTextBox1.Text += "> Buddy offline < \r\n";
                    }
                }
            }
        }
 

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (this.textBoxX1.Text.Length != 0)
            {
                new delegateSender(this.Sender).BeginInvoke(this.ipAddress, this.textBoxX1.Text, null, null);
            }
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
