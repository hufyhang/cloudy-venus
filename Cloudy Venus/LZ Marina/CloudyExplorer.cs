using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace LZ_Marina
{
    public partial class CloudyExplorer : TabPage
    {
        private TabControl tabControl;
        private String root = "";
        private String sub = "";
        private String thd = "";
        private String tail = "";
        private String tempSub = "";

        public CloudyExplorer()
        {
            InitializeComponent();
            initialEvents();
        }

        public CloudyExplorer(String root, TabControl tabControl)
        {
            this.root = root;
            this.tabControl = tabControl;
            InitializeComponent();
            initialEvents();
            loadRoot();
        }

        protected void initialEvents()
        {
            this.Text = @"Cloudy Explorer";
            this.textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            this.listView2.SelectedIndexChanged += new EventHandler(listView2_SelectedIndexChanged);
            this.listView3.DoubleClick += new EventHandler(listView3_DoubleClick);
        }

        protected void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    this.tail += this.listView3.SelectedItems[0].SubItems[0].Text + @"\";
                    this.loadThird(this.root + this.sub + this.thd + this.tail);
                }
                else if (this.listView3.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                    if (this.tail.Length != 0)
                    {
                        int index = this.tail.LastIndexOf('\\');
                        this.tail = this.tail.Substring(0, this.tail.Length - 1 - index);
                    }
                    this.loadThird(this.root + this.sub + this.thd + this.tail);
                }
                else
                {
                    runItem(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                    //System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
                this.textBox1.Text = this.root + this.sub + this.thd + this.tail;
            }
        }

        protected void runItem(String filename)
        {
            FileInfo file = new FileInfo(filename);
            ArrayList img = new ArrayList();
            img.Add(@".jpg");
            img.Add(@".png");
            img.Add(@".bmp");
            img.Add(@".gif");
            img.Add(@".ico");
            img.Add(@".tif");

            ArrayList media = new ArrayList();
            media.Add(@".wma");
            media.Add(@".wmv");
            media.Add(@".wav");
            media.Add(@".rm");
            media.Add(@".rmvb");
            media.Add(@".avi");
            media.Add(@".wmv");
            media.Add(@".mp3");
            media.Add(@".3gp");
            media.Add(@".mpeg");
            media.Add(@".mpg");
            media.Add(@".mov");
            media.Add(@".flv");

            String extension = file.Extension.ToLower();

            if (img.Contains(extension))
            {
                this.tabControl.Controls.Add(new Picture_Viewer(filename, true));
            }
            else if (media.Contains(extension))
            {
                this.tabControl.Controls.Add(new Media_Player(filename));
            }
            else if (extension.Equals(@".pdf"))
            {
                this.tabControl.Controls.Add(new PDFReader(filename));
            }
            else if (extension.Equals(@".exe"))
            {
                System.Diagnostics.Process.Start(filename);
            }
            else
            {
                this.tabControl.Controls.Add(new Editor(filename, true));
            }
            this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
        }

        protected void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                if (!this.listView2.SelectedItems[0].SubItems[0].Text.EndsWith(@"<ROOT>"))
                {
                    this.thd = this.listView2.SelectedItems[0].SubItems[0].Text + @"\";
                    this.loadThird(this.root + this.sub + this.thd);
                }
                else
                {
                    this.sub = this.tempSub;
                    this.thd = "";
                    this.loadSub(this.root + this.sub);
                }
                this.tail =  "";
                this.textBox1.Text = this.root + this.sub + this.thd;
            }
        }

        protected void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (!this.listView1.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                    this.sub = this.listView1.SelectedItems[0].SubItems[0].Text + @"\";
                    loadSub(this.root + this.sub);
                    this.thd = this.tail = "";
                    this.textBox1.Text = this.root + this.sub;
                    this.tempSub = this.sub;
                }
                else
                {
                    this.textBox1.Text = this.root;
                    this.loadRoot();
                }
            }
        }

        protected void loadThird(String path)
        {
            try
            {
                this.listView3.Items.Clear();
                DirectoryInfo thirdDir = new DirectoryInfo(path);
                ListViewItem itm = new ListViewItem(@"<ROOT>");
                itm.SubItems.Add(@"<ROOT>");
                this.listView3.Items.Add(itm);
                foreach (DirectoryInfo dir in thirdDir.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.SubItems.Add(@"<DIR>");
                    this.listView3.Items.Add(item);
                }
                foreach (FileInfo file in thirdDir.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add((file.Length / 1024).ToString());
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    this.listView3.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        protected void loadSub(String path)
        {
            try
            {
                this.listView2.Items.Clear();
                this.listView3.Items.Clear();
                DirectoryInfo subDir = new DirectoryInfo(path);
                this.listView2.Items.Add(new ListViewItem(@"<ROOT>"));
                foreach (DirectoryInfo dir in subDir.GetDirectories())
                {
                    this.listView2.Items.Add(new ListViewItem(dir.Name));
                }
                foreach (FileInfo file in subDir.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add((file.Length / 1024).ToString());
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    this.listView3.Items.Add(item);
                }
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        protected void loadRoot()
        {
            this.sub = this.thd = this.tail = "";
            this.listView1.Items.Clear();
            this.listView2.Items.Clear();
            this.listView3.Items.Clear();
            this.listView1.Items.Add(new ListViewItem(@"<ROOT>"));
            try
            {
                DirectoryInfo rootDir = new DirectoryInfo(this.root);
                foreach (DirectoryInfo dir in rootDir.GetDirectories())
                {
                    this.listView1.Items.Add(new ListViewItem(dir.Name));
                }

                foreach (FileInfo file in rootDir.GetFiles())
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.Extension);
                    item.SubItems.Add((file.Length / 1024).ToString());
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    this.listView3.Items.Add(item);
                }
                this.textBox1.Text = this.root;
            }
            catch (Exception)
            {
                this.root = "";
                MessageBox.Show("Please make sure you typed a valid address.", "Unknown path...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!this.textBox1.Text[this.textBox1.Text.Length - 1].Equals('\\'))
                {
                    this.root = this.textBox1.Text + @"\";
                    this.textBox1.Text = this.root;
                }
                else
                {
                    this.root = this.textBox1.Text;
                }
                this.loadRoot();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.loadRoot();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose the root path.";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.root = folder.SelectedPath + @"\";
                    this.loadRoot();
                }
            }
        }

        private void runWithDefaultApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                }
                else if (this.listView3.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                }
                else
                {
                    //MessageBox.Show(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                    System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
                this.textBox1.Text = this.root + this.sub + this.thd + this.tail;
            }
        }
    }
}
