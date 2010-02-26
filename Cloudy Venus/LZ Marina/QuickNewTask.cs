using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class QuickNewTask : Form
    {
        private Form1 form;
        private TabControl tabControl;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();

        private const int SYSCommand = 0xf017;
        private const int WM_SysCommand = 0x0112;
        private const int SC_MOVE = 61456;
        private const int HTCAPTION = 2;

        public QuickNewTask()
        {
            InitializeComponent();
        }

        public QuickNewTask(Form1 form1, TabControl tabControl, ListView listView, ImageList imageList)
        {
            InitializeComponent();
            initialEvents();

            this.form = form1;
            this.tabControl = tabControl;
            this.listView1.LargeImageList = imageList;

            foreach (ListViewItem item in listView.Items)
            {
                ListViewItem listItem = (ListViewItem)item.Clone();
                listItem.ImageIndex = item.ImageIndex;
                this.listView1.Items.Add(listItem);
            }
        }

        protected void initialEvents()
        {
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
            this.MouseDown += new MouseEventHandler(QuickNewTask_MouseDown);
        }

        private void QuickNewTask_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_SysCommand, SC_MOVE | HTCAPTION, 0);
            }
        }

        protected void activateTask()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.form.activateAppInTab(this.tabControl, this.listView1, this.listView1.SelectedItems[0].Index);
                this.Dispose();
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.activateTask();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            this.activateTask();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.activateTask();
        }
    }
}
