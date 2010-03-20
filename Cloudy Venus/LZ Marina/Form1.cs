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
using System.Media;
using System.Net;
using System.Reflection;
using System.Web;
using System.Net.Sockets;

namespace LZ_Marina
{
    public partial class Form1 : Form
    {
        private ArrayList userAppUrl = new ArrayList();
        private ArrayList plugins = new ArrayList();
        private ArrayList localAppUrl = new ArrayList();
        private int sysComponents = 0;
        private int pluginsNumber = 0;

        private String username = "";
        private String homePage = "";
        private String picRoot = "";
        private String textRoot = "";

        private Boolean alarmClock;
        private String alarmTime;
        private SoundPlayer alarm = new SoundPlayer(Application.StartupPath + @"\File System\Media\alarm.wav");
        private String searchToolbar;

        private ArrayList vitual1 = new ArrayList();
        private ArrayList vitual2 = new ArrayList();

        private int virtualIndex;
        private int currentIndex1;
        private int currentIndex2;
        private Boolean inCloseingTab;

        private LanChat lanChat;

//        private const int INTERNET_CONNECTION_MODEM = 1;
//        private const int INTERNET_CONNECTION_LAN = 2;
        
        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);   

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

        public Form1(Boolean flag)
        {
            loginUser();
        }

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            this.tabControl1.ItemSize = new Size(this.Width * 3 / 4, 20);

            screenMode();

            this.initialEvents();

            loginUser();

            this.virtualIndex = 1;
            this.button5.Text = "";
            this.alarmClock = false;
            this.alarmTime = "";
            this.searchToolbar = "";
            sysComponents = this.listView1.Items.Count;
            
            pluginsInitial();
            userApps();
            localApps();

            new Splash().ShowDialog();

            new SoundPlayer(Application.StartupPath + @"\File System\Media\Startup.wav").Play();
            this.vitual1.Add(this.tabControl1.TabPages[0]);
            this.vitual2.Add(this.tabControl1.TabPages[0]);
            this.currentIndex1 = this.currentIndex2 = 0;
            this.inCloseingTab = false;

            this.label4.Location = new Point(this.label1.Location.X + this.label1.Size.Width + 5, this.label4.Location.Y);
            //this.label4.Location.X = this.label1.Location.X + 10;

            this.lanChat = new LanChat(this.username);

            this.updateTodoInfo();
        }

        protected void initialEvents()
        {
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.tabControl1.DoubleClick += new EventHandler(tabControl1_DoubleClick);
            this.tabControl1.KeyDown += new KeyEventHandler(tabControl1_KeyDown);
            this.tabControl1.SelectedIndexChanged += new EventHandler(tabControl1_SelectedIndexChanged);
            this.DoubleClick += new EventHandler(Form1_DoubleClick);
            this.textBoxX1.KeyDown += new KeyEventHandler(textBoxX1_KeyDown);
//            this.textBoxX1.KeyPress += new KeyPressEventHandler(textBoxX1_KeyPress);
            this.expandablePanel1.ExpandedChanged += new DevComponents.DotNetBar.ExpandChangeEventHandler(expandablePanel1_ExpandedChanged);
            this.listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
            this.tabControl1.ControlAdded += new ControlEventHandler(dynamicTabSize);
            this.tabControl1.ControlRemoved += new ControlEventHandler(dynamicTabSize);
            this.tabControl1.ControlRemoved += new ControlEventHandler(tabControl1_ControlRemoved);
        }

        protected void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            this.inCloseingTab = true;
        }

        protected void updateTodoInfo()
        {
            String temp = this.textBoxX2.Text.ToUpper();
            temp = temp.Replace(@"[TODO]", @"◎");
            String[] str = temp.Split('◎');
            //this.expandablePanel1.TitleText = @"My Tips + " + (str.Length - 1).ToString() + @" TODOs";
            this.expandablePanel1.TitleText = @"My Tips + TODOs";

            if ((str.Length - 1) != 0)
            {
                this.label4.Text = (str.Length - 1).ToString() + @" to-dos remaining.";
            }
            else
            {
                this.label4.Text = "";
            }
        }

        public void dynamicTabSize()
        {
            if (this.tabControl1.TabCount != 0)
            {
                if (this.tabControl1.TabCount == 1)
                {
                    this.tabControl1.ItemSize = new Size(this.Width / 2, 20);
                }
                else
                {
                    this.tabControl1.ItemSize = new Size((this.Width - 20) / this.tabControl1.TabCount, 20);
                }
                /*
                if (this.tabControl1.TabCount * 260 < this.Width)
                {
                    this.tabControl1.ItemSize = new Size(260, 20);
                }
                else
                {
                    this.tabControl1.ItemSize = new Size((this.Width - 20) / this.tabControl1.TabCount, 20);
                }
                 */
            }
        }

        protected void dynamicTabSize(object sender, ControlEventArgs e)
        {
            this.dynamicTabSize();
        }

        protected void updateVirtual()
        {
            switch (this.virtualIndex)
            {
                case 1:
                    this.vitual1.Clear();
                    for (int index = 0; index != this.tabControl1.TabPages.Count; ++index )
                    {
                        this.vitual1.Add(this.tabControl1.TabPages[index]);
                    }
                    break;

                case 2:
                    this.vitual2.Clear();
                    for (int index = 0; index != this.tabControl1.TabPages.Count; ++index)
                    {
                        this.vitual2.Add(this.tabControl1.TabPages[index]);
                    }
                    break;
            }
        }

        public TabControl getTabControl()
        {
            return this.tabControl1;
        }

        protected void expandablePanel1_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\todo");
            writer.Flush();
            writer.Write(this.textBoxX2.Text);
            writer.Close();

            this.updateTodoInfo();
        }

        protected void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                String[] search = this.textBoxX1.Text.Split(' ');
                String searchURL = @"http://www.google.com/search?q=";
                foreach (String str in search)
                {
                    searchURL += str + @"+";
                }
                Browser browser = new Browser(searchURL, this.tabControl1, this);
                browser.ImageIndex = 2;
                this.tabControl1.TabPages.Add(browser);
                //this.tabControl1.TabPages.Add(new AppBrowser(searchURL, this.textBoxX1.Text + @" - Google"));
                this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.textBoxX1.Text = "";
            }
        }

        public void screenMode()
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
            //screenMode();
            /*
            this.tabControl1.Controls.Add(new Browser());
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
            */
        }

        protected void initialPanel()
        {
            this.userAppUrl = new ArrayList();
            this.localAppUrl = new ArrayList();
            this.listView1.Items.Clear();

            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Cloud Explorer", 8);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Web Browser", 2);
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Picture Viewer", 9);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Notepad", 11);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Media Player", 12);
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("PDF Reader", 13);
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Console Apps", 10);
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Version Control", 1);
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Local Search", 3);

            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
                listViewItem1,
                listViewItem2,
                listViewItem3,
                listViewItem4,
                listViewItem5,
                listViewItem6,
                listViewItem7,
                listViewItem8,
                listViewItem9
                });

            this.sysComponents = this.listView1.Items.Count;

            pluginsInitial();
            userApps();
            localApps();
        }

        protected void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.inCloseingTab)
            {
                switch (this.virtualIndex)
                {
                    case 1:
                        if (this.currentIndex1 >= this.tabControl1.TabCount - 1)
                        {
                            this.tabControl1.SelectedIndex = this.tabControl1.TabCount - 1;
                        }
                        else
                        {
                            this.tabControl1.SelectedIndex = this.currentIndex1;
                        }
                        break;

                    case 2:
                        if (this.currentIndex2 >= this.tabControl1.TabCount - 1)
                        {
                            this.tabControl1.SelectedIndex = this.tabControl1.TabCount - 1;
                        }
                        else
                        {
                            this.tabControl1.SelectedIndex = this.currentIndex2;
                        }
                        break;
                }
                this.inCloseingTab = false;
            }

            else if (this.tabControl1.SelectedIndex != 0)
            {
                switch (this.virtualIndex)
                {
                    case 1:
                        this.currentIndex1 = this.tabControl1.SelectedIndex;
                        break;

                    case 2:
                        this.currentIndex2 = this.tabControl1.SelectedIndex;
                        break;
                }
            }

            try
            {
                if (this.tabControl1.SelectedIndex == 0)
                {
                    this.label2.Text = @"Cloudy Venus - Luna, Evaluation Copy, 1.0.3";
                    initialPanel();
                }
                else
                {
                    this.label2.Text = this.tabControl1.SelectedTab.Text;
                }
            }
            catch (NullReferenceException)
            {
            }

            this.dynamicTabSize();
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

        protected void localApps()
        {
            this.localAppUrl.Clear();
            FileInfo file = new FileInfo(Application.StartupPath + @"\localApps");
            if (file.Exists)
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\localApps");
                String temp = "";
                while ((temp = reader.ReadLine()) != null)
                {
                    if (temp.Length > 0)
                    {
                        String[] info = temp.Split('`');
                        ListViewItem item = new ListViewItem(info[0], 14);
                        this.localAppUrl.Add(info[1]);
                        this.listView1.Items.Add(item);
                    }
                }
            }
            else
            {
                StreamWriter writer = new StreamWriter(Application.StartupPath + @"\localApps");
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
                this.username = reader.ReadLine();
                this.label1.Text = @"Welcome, " + this.username + @"!";
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

            StreamReader rdr = new StreamReader(Application.StartupPath + @"\todo");
            this.textBoxX2.Text = rdr.ReadToEnd();
            rdr.Close();
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
                if(this.tabControl1.SelectedTab.ImageIndex == 12)
                {
                    this.tabControl1.SelectedTab.Hide();
                }
                else
                {
                int index = this.tabControl1.SelectedIndex;
                this.tabControl1.SelectedTab.Dispose();
                //this.tabControl1.SelectedIndex = index - 1;
                GC.Collect();
                }
            }
        }

        protected void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                Thread thread = new Thread(activateAppThread);
                thread.Start();
                //new delegateActivateApp(this.activateApp).BeginInvoke(null, null);
            }
        }

        protected void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Thread thread = new Thread(activateAppThread);
                thread.Start();
            }
            else if(e.KeyCode == Keys.F2)
            {
                this.renameItem();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                this.removeItem();
            }
        }

        public void activateAppInTab(TabControl tabControl, ListView listView, int index)
        {
            switch (index)
            {
                case 0:
                    CloudyExplorer explorer = new CloudyExplorer(Application.StartupPath + @"\File System\", tabControl, this.imageList1);
                    explorer.ImageIndex = 1;
                    tabControl.Controls.Add(explorer);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 1:
                    Browser browser = new Browser(this.homePage, tabControl, this);
                    browser.ImageIndex = 2;
                    tabControl.Controls.Add(browser);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 2:
                    Picture_Viewer pic = new Picture_Viewer(this.picRoot);
                    pic.ImageIndex = 3;
                    tabControl.Controls.Add(pic);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 3:
                    Editor editor = new Editor(this.textRoot, false);
                    editor.ImageIndex = 4;
                    tabControl.Controls.Add(editor);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 4:
                    Media_Player media = new Media_Player();
                    media.ImageIndex = 5;
                    tabControl.Controls.Add(media);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 5:
                    PDFReader pdf = new PDFReader();
                    pdf.ImageIndex = 6;
                    tabControl.Controls.Add(pdf);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 6:
                    ConsoleApps consoleApps = new ConsoleApps();
                    consoleApps.ImageIndex = 7;
                    tabControl.Controls.Add(consoleApps);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 7:
                    VersionControl verctrl = new VersionControl(tabControl, this);
                    verctrl.ImageIndex = 8;
                    tabControl.Controls.Add(verctrl);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                case 8:
                    Search_Local localSearch = new Search_Local(this);
                    localSearch.ImageIndex = 13;
                    tabControl.Controls.Add(localSearch);
                    tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    break;
                default:
                    String url = "";
                    if (listView.SelectedItems[0].Index >= sysComponents + pluginsNumber)
                    {
                        if (listView.SelectedItems[0].ImageIndex == 5)
                        {
                            url = this.userAppUrl[listView.SelectedItems[0].Index - sysComponents - pluginsNumber].ToString();
                        }
                        else
                        {
                            url = @"<NULL>";
                            new CloudyExplorer(this.imageList1).runItem(this.localAppUrl[listView.SelectedItems[0].Index - sysComponents - pluginsNumber - this.userAppUrl.Count].ToString(), tabControl);
                        }
                    }
                    else
                    {
                        url = this.plugins[listView.SelectedItems[0].Index - sysComponents].ToString();
                    }
                    if (url != @"<NULL>")
                    {
                        AppBrowser app = new AppBrowser(url, listView.SelectedItems[0].Text, this.tabControl1, this);
                        app.ImageIndex = 9;
                        tabControl.Controls.Add(app);
                        tabControl.SelectedIndex = tabControl.TabPages.Count - 1;
                    }
                    break;
            }

            Application.DoEvents();
        }

        protected void activateAppThread()
        {
            MethodInvoker mi = new MethodInvoker(activateApp);
            this.BeginInvoke(mi);
        }

        protected void activateApp()
        {
            this.activateAppInTab(this.tabControl1, this.listView1, this.listView1.SelectedIndices[0]);
        }

        public ImageList getImageList()
        {
            return this.imageList1;
        }

        private void activateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                activateApp();
            }
        }

        protected void removeItem()
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.listView1.SelectedItems[0].Index < sysComponents + pluginsNumber)
                {
                    MessageBox.Show("Cannot remove this system component from list.", "System component", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (this.listView1.SelectedItems[0].ImageIndex == 5)
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
                else
                {
                    if (MessageBox.Show("Are you sure you want to remove this favourite item?", "Remove a favourite item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        int index = this.listView1.SelectedItems[0].Index - sysComponents - pluginsNumber - this.userAppUrl.Count;
                        String temp = this.listView1.SelectedItems[0].Text + "`" + this.localAppUrl[index].ToString();
                        StreamReader reader = new StreamReader(Application.StartupPath + @"\localApps");
                        String former = reader.ReadToEnd();
                        reader.Close();
                        former = former.Replace(temp, "");
                        StreamWriter writer = new StreamWriter(Application.StartupPath + @"\localApps");
                        writer.Flush();
                        writer.Write(former);
                        writer.Close();

                        this.listView1.SelectedItems[0].Remove();
                        initialPanel();
                    }
                }
            }
        }

        private void removeFromCloudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.removeItem();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.label3.Text = DateTime.Now.ToString() + @" | " + (SystemInformation.PowerStatus.BatteryLifePercent * 100).ToString() + @"% bettery remaining";
/*
            System.Int32 dwFlag = new int();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                this.progressBarX4.ColorTable = DevComponents.DotNetBar.eProgressBarItemColor.Error;
            }
            else if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
            {
                this.progressBarX4.ColorTable = DevComponents.DotNetBar.eProgressBarItemColor.Paused;
            }
            else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
            {
                this.progressBarX4.ColorTable = DevComponents.DotNetBar.eProgressBarItemColor.Normal;
            }
 */ 

            if (this.alarmClock)
            {
                if (DateTime.Now.ToLongTimeString() == this.alarmTime)
                {
                    this.alarm.Play();
                    this.alarm.PlayLooping();
                }
            }
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

        private void button1_Click(object sender, EventArgs e)
        {
            new ShutDown(this, this.tabControl1).ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            String[] search = this.textBoxX1.Text.Split(' ');
            String searchURL = @"http://www.google.com/search?q=";
            foreach (String str in search)
            {
                searchURL += str + @"+";
            }
            Browser browser = new Browser(searchURL, this.tabControl1, this);
            browser.ImageIndex = 2;
            this.tabControl1.TabPages.Add(browser);
            //this.tabControl1.TabPages.Add(new AppBrowser(searchURL, this.textBoxX1.Text + @" - Google"));
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.alarmClock)
            {
                this.alarm.Stop();
                this.alarmClock = false;
                this.label3.ForeColor = Color.White;
            }
            else
            {
                SetAlarm setAlarm = new SetAlarm();
                if (setAlarm.ShowDialog() == DialogResult.OK)
                {
                    this.alarmTime = setAlarm.getTime();
                    this.alarmClock = true;
                    this.label3.ForeColor = Color.Yellow;
                }
                setAlarm.Dispose();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.updateVirtual();
            this.virtualIndex = 1;
            this.tabControl1.TabPages.Clear();
            for (int index = 0; index != this.vitual1.Count; ++index)
            {
                this.tabControl1.TabPages.Add((TabPage)(this.vitual1[index]));
            }
            this.dynamicTabSize();
            this.button4.Text = "2";
            this.button5.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.updateVirtual();
            this.virtualIndex = 2;
            this.tabControl1.TabPages.Clear();
            for (int index = 0; index != this.vitual2.Count; ++index )
            {
                this.tabControl1.TabPages.Add((TabPage)(this.vitual2[index]));
            }
            this.dynamicTabSize();
            this.button4.Text = "";
            this.button5.Text = "1";
        }

        private void createALocalShortcutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.localShortcut();
            this.initialPanel();
        }

        protected void localShortcut()
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                open.Title = @"Shortcut...";
                if (open.ShowDialog() == DialogResult.OK)
                {
                    FileInfo file = new FileInfo(Application.StartupPath + @"\localApps");
                    StreamWriter writer = file.AppendText();
                    String info = System.IO.Path.GetFileName(open.FileName) + @"`" + System.IO.Path.GetFullPath(open.FileName) + "\r\n";
                    writer.Write(info);
                    writer.Close();
                }
            }
        }

        protected void renameItem()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                if (this.listView1.SelectedItems[0].Index < this.sysComponents + this.pluginsNumber)
                {
                    MessageBox.Show("Cannot rename a system component.", "Rename...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    String name = this.listView1.SelectedItems[0].Text;
                    String full;
                    int flag;
                    if (this.listView1.SelectedItems[0].ImageIndex == 5)
                    {
                        flag = 1;
                        full = this.userAppUrl[this.listView1.SelectedItems[0].Index - this.sysComponents - this.pluginsNumber].ToString();
                    }
                    else
                    {
                        flag = 2;
                        full = this.localAppUrl[this.listView1.SelectedItems[0].Index - this.sysComponents - this.pluginsNumber - this.userAppUrl.Count].ToString();
                    }
                    if (new Rename(name, full, flag).ShowDialog() == DialogResult.OK)
                    {
                        this.initialPanel();
                    }
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.renameItem();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex != 0)
            {
                this.label2.Text = this.tabControl1.SelectedTab.Text;
                if (this.label2.Text == @"")
                {
                    this.label2.Text = "> Untitled <";
                }
            }

            foreach (TabPage tab in this.tabControl1.TabPages)
            {
                tab.ToolTipText = tab.Text;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
/*
            if (this.monthCalendar1.Visible == true)
            {
                this.monthCalendar1.Visible = false;
            }
            else
            {
                this.monthCalendar1.Visible = true;
            }
 */ 
        }

        protected void newWebBrowser()
        {
            Browser browser = new Browser(this.homePage, this.tabControl1, this);
            browser.ImageIndex = 2;
            this.tabControl1.Controls.Add(browser);
            this.tabControl1.SelectedIndex = this.tabControl1.TabPages.Count - 1;
        }

        protected void activateNewBrowser()
        {
            MethodInvoker mi = new MethodInvoker(this.newWebBrowser);
            this.BeginInvoke(mi);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //new Thread(this.activateNewBrowser).Start();

            new QuickNewTask(this, this.tabControl1, this.listView1, this.ApplicationsImg).ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.lanChat.Show();
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
