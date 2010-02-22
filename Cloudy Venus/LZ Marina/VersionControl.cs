using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace LZ_Marina
{
    public partial class VersionControl : TabPage
    {
        private String currentSolution;
        private String currentStorage;
        private String currentTrack;
        private String currentBuffer;
        private int currentVersion;

        private TabControl tab;
        private Form1 form1;

        public VersionControl(TabControl tab, Form1 form1)
        {
            this.form1 = form1;
            this.tab = tab;
            this.currentSolution = this.currentStorage = this.currentTrack = "";
            this.currentBuffer = Application.StartupPath + @"\File System\Version Control\Buffer";
            this.currentVersion = 0;
            InitializeComponent();
            this.initialSolutions();
            this.listView3.DoubleClick += new EventHandler(listView3_DoubleClick);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.textBoxX5.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = @"Please choose your directory to be tracked with.";
            folder.ShowNewFolderButton = false;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBoxX2.Text = folder.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = @"Please choose a directory to be the storage.";
            folder.ShowNewFolderButton = true;
            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBoxX3.Text = folder.SelectedPath;
            }
        }

        protected void initialSolutions()
        {
            this.listView1.Items.Clear();
            FileInfo file = new FileInfo(Application.StartupPath + @"\File System\Version Control\Solutions");
            if (file.Exists)
            {
                try
                {
                    StreamReader reader = new StreamReader(Application.StartupPath + @"\File System\Version Control\Solutions");
                    String[] solutions = reader.ReadToEnd().Split('|');
                    reader.Close();
                    foreach (String str in solutions)
                    {
                        String[] solution = str.Split('`');
                        ListViewItem item = new ListViewItem(solution[0]);
                        item.SubItems.Add(solution[1]);
                        item.SubItems.Add(solution[2]);
                        this.listView1.Items.Add(item);
                    }
                }
                catch (ArgumentOutOfRangeException e)
                {
                    MessageBox.Show(e.ToString());
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
            else
            {
                file.Create();
            }
        }

        protected delegate void delegateNewSolution();

        protected void newSolution()
        {
            if (this.textBoxX1.Text.Length == 0 || this.textBoxX2.Text.Length == 0 || this.textBoxX3.Text.Length == 0)
            {
                MessageBox.Show("Please make sure that you have fill all the essential information correctly.",
                    "Information missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                StreamWriter writer = new FileInfo(Application.StartupPath + @"\File System\Version Control\Solutions").AppendText();
                String info = this.textBoxX1.Text + @"`" + this.textBoxX2.Text + @"`" + this.textBoxX3.Text + @"|";
                writer.Write(info);
                writer.Close();

                FileInfo file = new FileInfo(this.textBoxX3.Text + @"\VERCTRL.LUNA");
                file.Create();

                ICSharpCode.SharpZipLib.Zip.FastZip zip = new ICSharpCode.SharpZipLib.Zip.FastZip();
                zip.CreateZip(this.textBoxX3.Text + @"\" + this.textBoxX1.Text + @"_0.zip", this.textBoxX2.Text, true, "");

                writer = file.AppendText();
                info = DateTime.Now.ToLocalTime().ToString() + @"`Solution created.|";
                writer.Write(info);
                writer.Close();

                this.initialSolutions();
                this.textBoxX1.Text = this.textBoxX2.Text = this.textBoxX3.Text = "";
                MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This process may take several minutes.\r\nPess OK to start.", "Commit...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                new delegateNewSolution(this.newSolution).BeginInvoke(null, null);
            }
        }

        protected void removeSolution(String solutionInfo)
        {
            StreamReader reader = new StreamReader(Application.StartupPath + @"\File System\Version Control\Solutions");
            String str = reader.ReadToEnd();
            reader.Close();
            str = str.Replace(solutionInfo, @"");
            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\File System\Version Control\Solutions");
            writer.Flush();
            writer.Write(str);
            writer.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                if (MessageBox.Show("Are you sure you want to remove this solution?", "Remove a solution",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Do you want to keep all the milestones you have committed already?", "Completely remove?",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        this.clearBuffer(new DirectoryInfo(this.listView1.SelectedItems[0].SubItems[2].Text));
                    }

                    String info = this.listView1.SelectedItems[0].SubItems[0].Text + @"`" + this.listView1.SelectedItems[0].SubItems[1].Text +
                                        @"`" + this.listView1.SelectedItems[0].SubItems[2].Text + @"|";
                    this.removeSolution(info);
                    this.initialSolutions();
                    this.listView2.Items.Clear();
                    this.listView3.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please choose an existing solution from the list before doing this.", "Please try again.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected void loadList()
        {
            this.listView2.Items.Clear();
            StreamReader reader = new StreamReader(this.currentStorage + @"\VERCTRL.LUNA");
            String str = reader.ReadToEnd();
            reader.Close();
            int index = 0;

            if (str.Length != 0)
            {
                try
                {
                    String[] versions = str.Split('|');
                    foreach (String strVer in versions)
                    {
                        String[] ver = strVer.Split('`');
                        ListViewItem item = new ListViewItem(index++.ToString());
                        item.SubItems.Add(ver[0]);
                        item.SubItems.Add(ver[1]);
                        this.listView2.Items.Add(item);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                }
            }
            this.currentVersion = index;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.currentSolution = this.listView1.SelectedItems[0].SubItems[0].Text;
                this.currentTrack = this.listView1.SelectedItems[0].SubItems[1].Text;
                this.currentStorage = this.listView1.SelectedItems[0].SubItems[2].Text;

                this.loadList();

                this.listView3.Items.Clear();
            }
        }

        protected void clearBuffer(DirectoryInfo dirInfo)
        {
            DirectoryInfo dir = dirInfo;
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                this.clearBuffer(subDir);
                subDir.Delete();
            }
        }

        protected void extractZip(int version)
        {
                String path = Application.StartupPath + @"\File System\Version Control\Buffer";
                DirectoryInfo buffer = new DirectoryInfo(path);
                this.clearBuffer(buffer);
                try
                {
                    new ICSharpCode.SharpZipLib.Zip.FastZip().ExtractZip(this.currentStorage + @"\" + this.currentSolution + @"_" + version + @".zip", path, "");
                }
                catch (PathTooLongException)
                {
                }
        }

        protected void listView3_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView3.SelectedItems.Count != 0)
            {
                if (this.listView3.SelectedItems[0].SubItems[0].Text.Equals(@".."))
                {
                    this.currentBuffer = this.currentBuffer.Substring(0, this.currentBuffer.LastIndexOf('\\'));
                    new delegatePreviewBuffer(this.previewBuffer).BeginInvoke(this.currentBuffer, null, null);
                    //this.previewBuffer(this.currentBuffer);
                }
                else if (this.listView3.SelectedItems[0].SubItems[1].Text.Equals(@"<DIR>"))
                {
                    this.currentBuffer += @"\" + this.listView3.SelectedItems[0].SubItems[0].Text;
                    new delegatePreviewBuffer(this.previewBuffer).BeginInvoke(this.currentBuffer, null, null);
                    //this.previewBuffer(this.currentBuffer);
                }
                else
                {
                    new CloudyExplorer(this.form1.getImageList()).runItem(this.currentBuffer + @"\" + this.listView3.SelectedItems[0].SubItems[0].Text, this.form1.getTabControl());
                }
            }
        }

        protected delegate void delegatePreviewBuffer(String path);

        protected void previewBuffer(String path)
        {
            this.listView3.Items.Clear();
            this.extractZip(int.Parse(this.listView2.SelectedItems[0].SubItems[0].Text));
            DirectoryInfo buffer = new DirectoryInfo(path);
            if (!path.Equals(Application.StartupPath + @"\File System\Version Control\Buffer"))
            {
                ListViewItem item = new ListViewItem(@"..");
                item.SubItems.Add(@"<PARENT>");
                this.listView3.Items.Add(item);
            }
            
            foreach (DirectoryInfo dir in buffer.GetDirectories())
            {
                ListViewItem item = new ListViewItem(dir.Name);
                item.SubItems.Add(@"<DIR>");
                this.listView3.Items.Add(item);
            }

            foreach (FileInfo file in buffer.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.Extension);
                item.SubItems.Add(file.Length.ToString());
                this.listView3.Items.Add(item);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count != 0)
            {
                new delegatePreviewBuffer(this.previewBuffer).BeginInvoke(Application.StartupPath + @"\File System\Version Control\Buffer", null, null);
                //this.previewBuffer(Application.StartupPath + @"\File System\Version Control\Buffer");
                this.textBoxX4.Text = this.listView2.SelectedItems[0].SubItems[2].Text;
            }
        }

        protected delegate void delegateStartBackup();

        protected void startBackupThread()
        {
            String info = DateTime.Now.ToLocalTime().ToString() + @"`" + this.textBoxX5.Text + @"|";
            StreamWriter writer = new FileInfo(this.currentStorage + @"\VERCTRL.LUNA").AppendText();
            writer.Write(info);
            writer.Close();
            new ICSharpCode.SharpZipLib.Zip.FastZip().CreateZip(this.currentStorage + @"\" + this.currentSolution + @"_" + (this.currentVersion - 1).ToString() + @".zip",
                                                                                           this.currentTrack, true, "");
            this.textBoxX5.Text = "";
            this.loadList();
            MessageBox.Show("Process finished.", "Process finished...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This process may take several minutes.\r\nPess OK to start.", "Commit...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                new delegateStartBackup(this.startBackupThread).BeginInvoke(null, null);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (this.listView2.SelectedItems.Count != 0)
            {
                if (MessageBox.Show("Are you sure that you want to checkout with this version of your milestones?", "Checkout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.clearBuffer(new DirectoryInfo(this.currentTrack));
                    new ICSharpCode.SharpZipLib.Zip.FastZip().ExtractZip(this.currentStorage + @"\" + this.currentSolution + @"_" + this.listView2.SelectedItems[0].SubItems[0].Text + @".zip",
                                                                                                    this.currentTrack, "");
                    MessageBox.Show("Milestone has been checked out.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please choose a milestone before checking out.", "Checkout", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
