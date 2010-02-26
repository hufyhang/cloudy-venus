using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

namespace LZ_Marina
{
    public partial class CloudyExplorer : TabPage
    {
        private TabControl tabControl;

        private ListView sortRootList = new ListView();
        private ListView sortSubList = new ListView();
        private ListView sortItemList = new ListView();
        private ListView.ListViewItemCollection backupRoot;
        private ListView.ListViewItemCollection backupSub;
        private ListView.ListViewItemCollection backupItem;

        private String root = "";
        private String sub = "";
        private String thd = "";
        private String tail = "";
        private String tempSub = "";

        private ImageList imageList;

        public CloudyExplorer(ImageList imageList)
        {
            InitializeComponent();
            initialEvents();
            this.imageList = imageList;
        }

        public CloudyExplorer(String root, TabControl tabControl, ImageList imageList)
        {
            this.backupRoot = new ListView.ListViewItemCollection(this.sortRootList);
            this.backupSub = new ListView.ListViewItemCollection(this.sortSubList);
            this.backupItem = new ListView.ListViewItemCollection(this.sortItemList);
            this.root = root;
            this.tabControl = tabControl;
            this.imageList = imageList;
            InitializeComponent();
            initialEvents();
            loadRoot();
        }

        protected void initialEvents()
        {
            this.Text = @"Cloudy Explorer";
            this.textBoxX1.KeyDown += new KeyEventHandler(textBoxX1_KeyDown);
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            this.listView2.SelectedIndexChanged += new EventHandler(listView2_SelectedIndexChanged);
            this.listView3.DoubleClick += new EventHandler(listView3_DoubleClick);
        }

        public struct ShellExecuteInfo
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            public string lpVerb;
            public string lpFile;
            public string lpParameters;
            public string lpDirectory;
            public int nShow;
            public int hInstApp;
            public int lpIDList;
            public string lpClass;
            public int hkeyClass;
            public uint dwHotKey;
            public int hIcon;
            public int hProcess;
        }

        public const int SW_SHOW = 5;
        public const uint SEE_MASK_INVOKEIDLIST = 12;

        [DllImport("shell32.dll")]
        public static extern bool ShellExecuteEx(ref ShellExecuteInfo lpExecInfo);

        protected void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    this.tail += this.listView3.SelectedItems[0].SubItems[0].Text + @"\";
                    this.loadThird(this.root + this.sub + this.thd + this.tail);
                }
                else if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<PARENT>"))
                {
                    while (this.tail.Length != 0 && this.tail[0].Equals('\\'))
                    {
                        if (this.tail.Length == 1)
                        {
                            this.tail = "";
                            break;
                        }
                        else
                        {
                            this.tail = this.tail.Substring(1);
                        }
                    }

                    if (this.tail.Length != 0)
                    {
                        String tempTail = @"\";
                        tempTail += this.tail;
                        this.tail = tempTail;
                        if (!this.tail[this.tail.Length - 1].Equals('\\'))
                        {
                            this.tail = this.tail.Substring(0, this.tail.LastIndexOf('\\') + 1);
                            this.loadThird(this.root + this.sub + this.thd + this.tail);
                            this.textBoxX1.Text = this.root + this.sub + this.thd + this.tail;
                        }
                        else
                        {
                            do
                            {
                                this.tail = this.tail.Substring(0, this.tail.Length - 1);
                            } while (this.tail[this.tail.Length - 1].Equals('\\'));
                            this.tail = this.tail.Substring(0, this.tail.LastIndexOf('\\') + 1);
                            this.loadThird(this.root + this.sub + this.thd + this.tail);
                            this.textBoxX1.Text = this.root + this.sub + this.thd + this.tail;
                        }
                    }
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
                    runItem(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text, this.tabControl);
                    //System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
                this.textBoxX1.Text = this.root + this.sub + this.thd + this.tail;
            }
        }

        public void runItem(String filename, TabControl tabctrl)
        {
            try
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
                    Picture_Viewer pic = new Picture_Viewer(filename, true);
                    pic.ImageIndex = 3;
                    tabctrl.Controls.Add(pic);
                }
                else if (media.Contains(extension))
                {
                    Media_Player mediaPlayer = new Media_Player(filename);
                    mediaPlayer.ImageIndex = 5;
                    tabctrl.Controls.Add(mediaPlayer);
                }
                else if (extension.Equals(@".pdf"))
                {
                    PDFReader pdf = new PDFReader(filename);
                    pdf.ImageIndex = 6;
                    tabctrl.Controls.Add(pdf);
                }
                else if (extension.Equals(@".exe"))
                {
                    System.Diagnostics.Process.Start(filename);
                }
                else
                {
                    Editor editor = new Editor(filename, true);
                    editor.ImageIndex = 4;
                    tabctrl.Controls.Add(editor);
                }
                tabctrl.SelectedIndex = tabctrl.TabCount - 1;
                Application.DoEvents();
            }
            catch (Exception)
            {
            }
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
                this.tail = "";
                this.textBoxX1.Text = this.root + this.sub + this.thd;
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
                    this.textBoxX1.Text = this.root + this.sub;
                    this.tempSub = this.sub;
                }
                else
                {
                    this.textBoxX1.Text = this.root;
                    this.loadRoot();
                }
            }
        }

        protected void sortUpdate()
        {
            this.backupItem.Clear();
            foreach (ListViewItem item in this.listView3.Items)
            {
                this.backupItem.Add((ListViewItem)item.Clone());
            }

            this.backupSub.Clear();
            foreach (ListViewItem item in this.listView2.Items)
            {
                this.backupSub.Add((ListViewItem)item.Clone());
            }

            this.backupRoot.Clear();
            foreach (ListViewItem item in this.listView1.Items)
            {
                this.backupRoot.Add((ListViewItem)item.Clone());
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

                ListViewItem itmParent = new ListViewItem(@"..");
                itmParent.SubItems.Add(@"<PARENT>");
                this.listView3.Items.Add(itmParent);

                foreach (DirectoryInfo dir in thirdDir.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(dir.Name);
                    item.SubItems.Add(@"<DIR>");
                    this.listView3.Items.Add(item);
                }
                foreach (FileInfo file in thirdDir.GetFiles())
                {
                    if (!file.Extension.ToLower().Equals(@".lnk"))
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(file.Extension);
                        item.SubItems.Add((file.Length / 1024).ToString());
                        item.SubItems.Add(file.LastWriteTime.ToString());
                        this.listView3.Items.Add(item);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
            }

            this.sortUpdate();
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

            this.sortUpdate();
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
                    if (!file.Extension.ToLower().Equals(@".lnk"))
                    {
                        ListViewItem item = new ListViewItem(file.Name);
                        item.SubItems.Add(file.Extension);
                        item.SubItems.Add((file.Length / 1024).ToString());
                        item.SubItems.Add(file.LastWriteTime.ToString());
                        this.listView3.Items.Add(item);
                    }
                }
                this.textBoxX1.Text = this.root;
            }
            catch (Exception)
            {
                this.root = "";
                MessageBox.Show("Please make sure you typed a valid address.", "Unknown path...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.sortUpdate();
        }

        protected void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.textBoxX1.Text.Length != 0)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (!this.textBoxX1.Text[this.textBoxX1.Text.Length - 1].Equals('\\'))
                {
                    this.root = this.textBoxX1.Text + @"\";
                    this.textBoxX1.Text = this.root;
                }
                else
                {
                    this.root = this.textBoxX1.Text;
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
                    System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
                this.textBoxX1.Text = this.root + this.sub + this.thd + this.tail;
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                Editor edit = new Editor(this.root + @"\" + this.listView1.SelectedItems[0].SubItems[0].Text);
                edit.ImageIndex = 4;
                this.tabControl.Controls.Add(edit);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                PDFReader pdfReader = new PDFReader(this.root + @"\" + this.listView1.SelectedItems[0].SubItems[0].Text, true);
                pdfReader.ImageIndex = 6;
                this.tabControl.Controls.Add(pdfReader);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void addListIntoNotepadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                Editor edit = new Editor(this.root + this.sub + this.thd);
                edit.ImageIndex = 4;
                this.tabControl.Controls.Add(edit);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void addListIntoPDFReaderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                PDFReader pdfReader = new PDFReader(this.root + this.sub + this.thd, true);
                pdfReader.ImageIndex = 6;
                this.tabControl.Controls.Add(pdfReader);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void addListIntoNotepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    Editor edit = new Editor(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                    edit.ImageIndex = 4;
                    this.tabControl.Controls.Add(edit);
                    this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
                }
            }
        }

        private void addListIntoPDFReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    PDFReader pdfReader = new PDFReader(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text, true);
                    pdfReader.ImageIndex = 6;
                    this.tabControl.Controls.Add(pdfReader);
                    this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
                }
            }
        }

        private void openDirectoryWithPictureViewerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                Picture_Viewer pic = new Picture_Viewer(this.root + @"\" + this.listView1.SelectedItems[0].SubItems[0].Text);
                pic.ImageIndex = 3;
                this.tabControl.Controls.Add(pic);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void openDirectoryWithPictureViewerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                Picture_Viewer pic = new Picture_Viewer(this.root + this.sub + this.thd);
                pic.ImageIndex = 3;
                this.tabControl.Controls.Add(pic);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
            }
        }

        private void openDirectoryWithPictureViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    Picture_Viewer pic = new Picture_Viewer(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                    pic.ImageIndex = 3;
                    this.tabControl.Controls.Add(pic);
                    this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
                }
            }
        }

        protected delegate void delegateSendTo(String destination);

        protected void sendTo(String destination)
        {
            String[] file = new String[this.listView3.SelectedItems.Count];

            foreach (ListViewItem item in this.listView3.SelectedItems)
            {
                if (!item.SubItems[1].Text.Equals(@"<DIR>") && !item.SubItems[1].Text.Equals(@"<ROOT>"))
                {
                    String filename = item.SubItems[0].Text;
                    FileInfo info = new FileInfo(destination + @"\" + filename);
                    if (info.Exists)
                    {
                        if (MessageBox.Show(filename + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "File exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            info.Delete();
                            new FileInfo(this.root + this.sub + this.thd + this.tail + @"\" + filename).CopyTo(destination + @"\" + filename);
                        }
                    }
                    else
                    {
                        new FileInfo(this.root + this.sub + this.thd + this.tail + @"\" + filename).CopyTo(destination + @"\" + filename);
                    }
                }

                else if (item.SubItems[1].Text.Equals(@"<DIR>"))
                {
                    try
                    {
                        String dirName = item.SubItems[0].Text;
                        DirectoryInfo dir = new DirectoryInfo(destination + @"\" + dirName);
                        if (dir.Exists)
                        {
                            if (MessageBox.Show(dirName + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "Directory exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                dir.Delete(true);
                            }
                        }
                        new DirectoryInfo(destination + @"\" + dirName).Create();
                        this.sendDir(this.root + this.sub + this.thd + this.tail + dirName, destination + @"\" + dirName);
                    }
                    catch (IOException exp)
                    {
                        MessageBox.Show(exp.ToString(), "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sendToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose a destination.";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    new delegateSendTo(this.sendTo).BeginInvoke(folder.SelectedPath, null, null);
                    /*
                                        destination = folder.SelectedPath;

                                        String[] file = new String[this.listView3.SelectedItems.Count];

                                        foreach (ListViewItem item in this.listView3.SelectedItems)
                                        {
                                            if (!item.SubItems[1].Text.Equals(@"<DIR>") && !item.SubItems[1].Text.Equals(@"<ROOT>"))
                                            {
                                                String filename = item.SubItems[0].Text;
                                                FileInfo info = new FileInfo(destination + @"\" + filename);
                                                if (info.Exists)
                                                {
                                                    if (MessageBox.Show(filename + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "File exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                    {
                                                        info.Delete();
                                                        new FileInfo(this.root + this.sub + this.thd + this.tail + @"\" + filename).CopyTo(destination + @"\" + filename);
                                                    }
                                                }
                                                else
                                                {
                                                    new FileInfo(this.root + this.sub + this.thd + this.tail + @"\" + filename).CopyTo(destination + @"\" + filename);
                                                }
                                            }

                                            else if (item.SubItems[1].Text.Equals(@"<DIR>"))
                                            {
                                                try
                                                {
                                                    String dirName = item.SubItems[0].Text;
                                                    DirectoryInfo dir = new DirectoryInfo(destination + @"\" + dirName);
                                                    if (dir.Exists)
                                                    {
                                                        if (MessageBox.Show(dirName + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "Directory exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                                        {
                                                            dir.Delete(true);
                                                        }
                                                    }
                                                    new DirectoryInfo(destination + @"\" + dirName).Create();
                                                    this.sendDir(this.root + this.sub + this.thd + this.tail + dirName, destination + @"\" + dirName);
                                                }
                                                catch (IOException exp)
                                                {
                                                    MessageBox.Show(exp.ToString(), "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                }
                                            }

                                        }
                                        MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     */
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView3.SelectedItems)
            {
                String filename = item.SubItems[0].Text;
                FileInfo info = new FileInfo(this.root + this.sub + this.thd + this.tail + @"\" + filename);
                if (info.Exists)
                {
                    if (MessageBox.Show("Are you sure you want to remove " + filename + " from your file system?", "Remove file...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        info.Delete();
                    }
                }
            }
        }

        protected delegate void delegateSendTo1(String destination);

        protected void sendTo1(String destination)
        {
            try
            {
                String dirName = this.listView2.SelectedItems[0].SubItems[0].Text;
                DirectoryInfo dir = new DirectoryInfo(destination + @"\" + dirName);
                if (dir.Exists)
                {
                    if (MessageBox.Show(dirName + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "Directory exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dir.Delete(true);
                    }
                }
                new DirectoryInfo(destination + @"\" + dirName).Create();
                //                            this.sendDir(this.root + this.sub + this.thd + this.listView2.SelectedItems[0].SubItems[0].Text, destination + dirName);
                this.sendDir(this.root + this.sub + this.thd, destination + @"\" + dirName);
                MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException exp)
            {
                MessageBox.Show(exp.ToString(), "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendToToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                using (FolderBrowserDialog folder = new FolderBrowserDialog())
                {
                    folder.Description = @"Please choose a destination.";
                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        new delegateSendTo1(this.sendTo1).BeginInvoke(folder.SelectedPath, null, null);
                    }
                }
            }
        }

        protected void sendDir(String source, String destination)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(source);
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.CopyTo(destination + @"\" + file.Name);
            }

            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                new DirectoryInfo(destination + @"\" + dir.Name).Create();
                sendDir(dir.FullName, destination + @"\" + dir.Name);
            }
        }

        protected delegate void delegateSendTo2(String destination);

        protected void sendTo2(String destination)
        {
            try
            {
                String dirName = this.listView1.SelectedItems[0].SubItems[0].Text;
                DirectoryInfo dir = new DirectoryInfo(destination + dirName);
                if (dir.Exists)
                {
                    if (MessageBox.Show(dirName + " is existing in the destination already.\r\nAre you sure you want to overwrite it now?", "Directory exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        dir.Delete(true);
                    }
                }

                new DirectoryInfo(destination + @"\" + dirName).Create();
                this.sendDir(this.root + this.listView1.SelectedItems[0].SubItems[0].Text, destination + @"\" + dirName);

                MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (IOException exp)
            {
                MessageBox.Show(exp.ToString(), "Unknown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void sendToToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                using (FolderBrowserDialog folder = new FolderBrowserDialog())
                {
                    folder.Description = @"Please choose a destination.";
                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        new delegateSendTo2(this.sendTo2).BeginInvoke(folder.SelectedPath, null, null);
                    }
                }
            }
        }

        private void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxX2.Text.Length == 0)
            {
                this.listView1.Items.Clear();
                foreach (ListViewItem item in this.backupRoot)
                {
                    this.listView1.Items.Add((ListViewItem)item.Clone());
                }

                this.listView2.Items.Clear();
                foreach (ListViewItem item in this.backupSub)
                {
                    this.listView2.Items.Add((ListViewItem)item.Clone());
                }

                this.listView3.Items.Clear();
                foreach (ListViewItem item in this.backupItem)
                {
                    this.listView3.Items.Add((ListViewItem)item.Clone());
                }
            }
            else
            {
                this.listView1.Items.Clear();
                foreach (ListViewItem rootItem in this.backupRoot)
                {
                    if (rootItem.SubItems[0].Text.ToUpper().Contains(this.textBoxX2.Text.ToUpper()))
                    {
                        this.listView1.Items.Add((ListViewItem)rootItem.Clone());
                    }
                }

                this.listView2.Items.Clear();
                foreach (ListViewItem subItem in this.backupSub)
                {
                    if (subItem.SubItems[0].Text.ToUpper().Contains(this.textBoxX2.Text.ToUpper()))
                    {
                        this.listView2.Items.Add((ListViewItem)subItem.Clone());
                    }
                }

                this.listView3.Items.Clear();
                foreach (ListViewItem thdItem in this.backupItem)
                {
                    if (thdItem.SubItems[0].Text.ToUpper().Contains(this.textBoxX2.Text.ToUpper()))
                    {
                        this.listView3.Items.Add((ListViewItem)thdItem.Clone());
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Text = "";

            this.listView1.Items.Clear();
            foreach (ListViewItem item in this.backupRoot)
            {
                this.listView1.Items.Add((ListViewItem)item.Clone());
            }

            this.listView2.Items.Clear();
            foreach (ListViewItem item in this.backupSub)
            {
                this.listView2.Items.Add((ListViewItem)item.Clone());
            }

            this.listView3.Items.Clear();
            foreach (ListViewItem item in this.backupItem)
            {
                this.listView3.Items.Add((ListViewItem)item.Clone());
            }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count > 0)
            {
                if (!this.listView3.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                    ShellExecuteInfo vShellExecuteInfo = new ShellExecuteInfo();

                    vShellExecuteInfo.cbSize = Marshal.SizeOf(vShellExecuteInfo);
                    vShellExecuteInfo.lpVerb = "properties";
                    vShellExecuteInfo.lpFile = this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text;
                    vShellExecuteInfo.nShow = SW_SHOW;
                    vShellExecuteInfo.fMask = SEE_MASK_INVOKEIDLIST;
                    ShellExecuteEx(ref vShellExecuteInfo);
                    //                  System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
            }
        }

        private void propertiesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count > 0)
            {
                if (!this.listView2.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                    ShellExecuteInfo vShellExecuteInfo = new ShellExecuteInfo();

                    vShellExecuteInfo.cbSize = Marshal.SizeOf(vShellExecuteInfo);
                    vShellExecuteInfo.lpVerb = "properties";
                    vShellExecuteInfo.lpFile = this.root + this.sub + @"\" + this.listView2.SelectedItems[0].SubItems[0].Text;
                    vShellExecuteInfo.nShow = SW_SHOW;
                    vShellExecuteInfo.fMask = SEE_MASK_INVOKEIDLIST;
                    ShellExecuteEx(ref vShellExecuteInfo);
                    //                  System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
            }
        }

        private void propertiesToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (!this.listView1.SelectedItems[0].SubItems[0].Text.Equals(@"<ROOT>"))
                {
                    ShellExecuteInfo vShellExecuteInfo = new ShellExecuteInfo();

                    vShellExecuteInfo.cbSize = Marshal.SizeOf(vShellExecuteInfo);
                    vShellExecuteInfo.lpVerb = "properties";
                    vShellExecuteInfo.lpFile = this.root + this.listView1.SelectedItems[0].SubItems[0].Text;
                    vShellExecuteInfo.nShow = SW_SHOW;
                    vShellExecuteInfo.fMask = SEE_MASK_INVOKEIDLIST;
                    ShellExecuteEx(ref vShellExecuteInfo);
                    //                  System.Diagnostics.Process.Start(this.root + this.sub + this.thd + this.tail + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text);
                }
            }
        }
    }
}
