using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class Form1 : Form
    {
        private ArrayList userAppUrl = new ArrayList();
        private ArrayList plugins = new ArrayList();
        private int sysComponents = 0;
        private int pluginsNumber = 0;

        private String homePage = "";
        private String picRoot = "";
        private String textRoot = "";

        [DllImport("kernel32.dll")]
        private static extern int SetProcessWorkingSetSize(IntPtr hProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        public static int SetProcessMemoryToMin()
        {
            return SetProcessMemoryToMin(Process.GetCurrentProcess().Handle);
        }

        public static int SetProcessMemoryToMin(IntPtr SetProcess)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                return SetProcessWorkingSetSize(SetProcess, -1, -1);
            }
            return -1;
        }

        public Form1()
        {
            new Splash().ShowDialog();

            InitializeComponent();
            screenMode();

            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.tabControl1.DoubleClick += new EventHandler(tabControl1_DoubleClick);
            this.tabControl1.KeyDown += new KeyEventHandler(tabControl1_KeyDown);
            this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            this.DoubleClick += new EventHandler(Form1_DoubleClick);

            loginUser();
            sysComponents = this.listView1.Items.Count;
            pluginsInitial();
            userApps();
        }

        protected void screenMode()
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.ShowIcon = true;
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Normal;
                this.WindowState = FormWindowState.Maximized;
            }
        }

        protected void Form1_DoubleClick(object sender, EventArgs e)
        {
            screenMode();
            /*
            this.tabControl1.Controls.Add(new Browser());
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
            */
        }

        protected void initialPanel()
        {
            this.userAppUrl = new ArrayList();
            this.listView1.Items.Clear();

            System.Windows.Forms.ListViewItem listViewItem0 = new System.Windows.Forms.ListViewItem("Cloud Explorer", 8);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Web Browser", 2);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("2Dooo Special", 1);
//            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Hotmail", 0);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Picture Viewer", 9);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Notepad", 11);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Media Player", 12);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("PDF Reader", 13);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Process Pool", 10);

            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                listViewItem0,
                listViewItem1,
                listViewItem2,
//                listViewItem3,
                listViewItem6,
                listViewItem7,
                listViewItem4,
                listViewItem5,
                listViewItem8
                });

            this.sysComponents = this.listView1.Items.Count;

            pluginsInitial();
            userApps();
        }

        protected void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                initialPanel();
            }
        }

        protected void pluginsInitial()
        {
            DirectoryInfo root = new DirectoryInfo(Application.StartupPath + @"\plugins");
            DirectoryInfo[] dirs = root.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                String name = dir.Name;
                StreamReader reader = new StreamReader(dir.FullName + @"\start_up.cvPlug");
                String path = reader.ReadLine();
                String plug = "";
                if (!path.Equals(@"[URL]"))
                {
                    reader.Close();
                    plug = dir.FullName + @"\" + path;
                }
                else
                {
                    plug = reader.ReadLine();
                    reader.Close();
                }
                this.plugins.Add(plug);
                ListViewItem item = new ListViewItem(name, 7);
                this.listView1.Items.Add(item);
            }

            pluginsNumber = this.listView1.Items.Count - this.sysComponents;
        }

        protected void userApps()
        {
            this.userAppUrl.Clear();
            FileInfo file = new FileInfo(Application.StartupPath + @"\apps");
            if (file.Exists)
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\apps");
                String temp = "";
                while ((temp = reader.ReadLine()) != null)
                {
                    if (temp.Length > 0)
                    {
                        String[] info = temp.Split('`');
                        ListViewItem item = new ListViewItem(info[0], 5);
                        this.userAppUrl.Add(info[1]);
                        this.listView1.Items.Add(item);
                    }
                }
                reader.Close();
            }
            else
            {
                StreamWriter writer = new StreamWriter(Application.StartupPath + @"\apps");
                String userInfo = @"";
                writer.Write(userInfo);
                writer.Close();
            }
        }

        protected void loginUser()
        {
            String fullscreen = "";
            FileInfo file = new FileInfo(Application.StartupPath + @"\user");
            if (file.Exists)
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\user");
                this.label1.Text = @"Welcome, " + reader.ReadLine() + @"!";
                this.homePage = reader.ReadLine();
                this.pictureBox1.Image = new Bitmap(reader.ReadLine());
                fullscreen = reader.ReadLine();
                this.picRoot = reader.ReadLine() + @"\";
                this.textRoot = reader.ReadLine() + @"\";
                reader.Close();

                if (picRoot.Equals(@"[default]\"))
                {
                    this.picRoot = Application.StartupPath + @"\File System\Pictures";
                }

                if (textRoot.Equals(@"[default]\"))
                {
                    this.textRoot = Application.StartupPath + @"\File System\Common Files";
                }

                if (!fullscreen.Equals(@"fullScreen=1"))
                {
                    this.screenMode();
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(Application.StartupPath + @"\user");
                String userInfo = @"develop_user";
                userInfo += "\r\nhttp://www.google.com";
                userInfo += "\r\nuserPic.jpg";
                userInfo += "\r\nfullScreen=1";
                writer.Write(userInfo);
                writer.Close();
                loginUser();
            }
        }

        protected void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.W && this.tabControl1.SelectedIndex != 0)
            {
                this.tabControl1.SelectedTab.Dispose();
            }
        }

        protected void tabControl1_DoubleClick(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 0)
            {
                int index = this.tabControl1.SelectedIndex;
                this.tabControl1.SelectedTab.Dispose();
                this.tabControl1.SelectedIndex = index - 1;
                GC.Collect();
            }
        }

        protected void listView1_DoubleClick(object sender, EventArgs e)
        {
            Thread thread = new Thread(activateAppThread);
            thread.Start();
        }

        protected void activateAppThread()
        {
            MethodInvoker mi = new MethodInvoker(activateApp);
            this.BeginInvoke(mi);
        }

        protected void activateApp()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                switch (this.listView1.SelectedIndices[0])
                {
                    case 0:
                        this.tabControl1.Controls.Add(new CloudyExplorer(Application.StartupPath + @"\File System\", this.tabControl1));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 1:
                        this.tabControl1.Controls.Add(new Browser(this.homePage));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 2:
                        this.tabControl1.Controls.Add(new _2Dooo());
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 3:
                        this.tabControl1.Controls.Add(new Picture_Viewer(this.picRoot));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 4:
                        this.tabControl1.Controls.Add(new Editor(this.textRoot));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 5:
                        this.tabControl1.Controls.Add(new Media_Player());
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 6:
                        this.tabControl1.Controls.Add(new PDFReader());
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    case 7:
                        this.tabControl1.Controls.Add(new ProcsPool(this.tabControl1));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                    default:
                        String url = "";
                        if (this.listView1.SelectedItems[0].Index >= sysComponents + pluginsNumber)
                        {
                            url = this.userAppUrl[this.listView1.SelectedItems[0].Index - sysComponents - pluginsNumber].ToString();
                        }
                        else
                        {
                            url = this.plugins[this.listView1.SelectedItems[0].Index - sysComponents].ToString();
                        }
                        this.tabControl1.Controls.Add(new AppBrowser(url, this.listView1.SelectedItems[0].Text));
                        this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
                        break;
                }
            }
        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                activateApp();
            }
        }

        private void removeFromCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.listView1.SelectedItems[0].Index < sysComponents + pluginsNumber)
                {
                    MessageBox.Show("Cannot remove this system component from list.", "System component", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to remove this favourite item?", "Remove a favourite item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int index = this.listView1.SelectedItems[0].Index - sysComponents - pluginsNumber;
                        String temp = this.listView1.SelectedItems[0].Text + "`" + this.userAppUrl[index].ToString();
                        StreamReader reader = new StreamReader(Application.StartupPath + @"\apps");
                        String former = reader.ReadToEnd();
                        reader.Close();
                        former = former.Replace(temp, "");
                        StreamWriter writer = new StreamWriter(Application.StartupPath + @"\apps");
                        writer.Flush();
                        writer.Write(former);
                        writer.Close();

                        this.listView1.SelectedItems[0].Remove();
                        initialPanel();
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labelX1.Text = DateTime.Now.ToString() + @" | " + (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + @"% bettery remaining.";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to log off from Cloudy Venus?", "Log off...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            screenMode();
        }

        private void clostTabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 0)
            {
                this.tabControl1.SelectedTab.Dispose();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.tabControl1.Controls.Add(new UserScreen());
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reload Cloudy Venus now?\r\nAll your unsaved works will lose.", "Cloudy Venus reloading...", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }
    }
}
