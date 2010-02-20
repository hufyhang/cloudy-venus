using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace LZ_Marina
{
    public partial class ProcsPool : TabPage
    {
        private TabControl tabControl = null;
        private ArrayList tabList = new ArrayList();

        public ProcsPool()
        {
            InitializeComponent();
            this.Text = "Process Pool";

            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);

            refresh();
        }

        public ProcsPool(TabControl tab)
        {
            InitializeComponent();
            this.tabControl = tab;
            this.Text = "Process Pool";

            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.listView2.DoubleClick += new EventHandler(listView2_DoubleClick);

            refresh();
        }

        protected void refresh()
        {
            tabList = new ArrayList();
            this.listView1.Items.Clear();

            foreach (Process proc in Process.GetProcesses())
            {
                int size = proc.PrivateMemorySize / 1024;
                ListViewItem item = new ListViewItem(proc.Id.ToString());
                item.SubItems.Add(proc.ProcessName);
                item.SubItems.Add(size.ToString());
                item.SubItems.Add(proc.MainWindowTitle);
                this.listView1.Items.Add(item);
            }
            this.label2.Text = @"Total processes: " + this.listView1.Items.Count;

            this.listView2.Items.Clear();
            foreach (TabPage page in tabControl.TabPages)
            {
                this.tabList.Add(page);
                String url = "[Offline App]";
                ListViewItem tabItem;
                if (page.Text == @"" && page.ImageIndex == 0)
                {
                    continue;
                    //tabItem = new ListViewItem(@"Luna Home Screen");
                }
                else
                {
                    tabItem = new ListViewItem(page.Text);
                }
                try
                {
                    foreach (Control ctrl in page.Controls)
                    {
                        if (ctrl is WebBrowser)
                        {
                            WebBrowser web = (WebBrowser)ctrl;
                            url = web.Document.Url.ToString();
                        }
                    }
                }
                catch (Exception)
                {
                }
                tabItem.SubItems.Add(url);
                this.listView2.Items.Add(tabItem);
            }
            this.label3.Text = @"Total Apps: " + this.listView2.Items.Count;
        }

        protected void listView2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                this.tabControl.SelectedTab = (TabPage)this.tabList[this.listView2.SelectedItems[0].Index];
            }
        }

        protected void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                Process[] procs = Process.GetProcesses();
                foreach (Process proc in procs)
                {
                    if (proc.Id == int.Parse(this.listView1.SelectedItems[0].SubItems[0].Text))
                    {
                        if (MessageBox.Show("Are you sure you want to kill the process of " + this.listView1.SelectedItems[0].SubItems[1].Text + " ?", "Kill a process...",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            proc.Kill();
                        }
                    }
                }
            }
            refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Are you sure you want to close " + this.listView2.SelectedItems[0].SubItems[0].Text + " ?", "Close app...",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.tabControl.SelectedTab = (TabPage)this.tabList[this.listView2.SelectedItems[0].Index];
                    if (this.tabControl.SelectedIndex != 0)
                    {
                        this.tabControl.SelectedTab.Dispose();
                        this.refresh();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.refresh();
        }
    }
}
