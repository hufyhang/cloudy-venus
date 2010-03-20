using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LZ_Marina
{
    public partial class Search_Local : TabPage
    {
        private String target;
        private String directory;
        private Form1 form;

        private delegateBeginToSearch begin;
        private IAsyncResult result = null;

        public Search_Local(Form1 form)
        {
            InitializeComponent();
            this.initailEvents();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = @"Local Search";
            this.form = form;
        }

        public Search_Local(Form1 form, String target, String directory)
        {
            InitializeComponent();
            this.initailEvents();
            Control.CheckForIllegalCrossThreadCalls = false;

            this.Text = @"Local Search";
            this.form = form;
            this.textBoxX1.Text = this.target = target;
            this.textBoxX2.Text = this.directory = directory;
        }

        protected void initailEvents()
        {
            this.textBoxX1.KeyDown += new KeyEventHandler(textBox_KeyDown);
            this.textBoxX2.KeyDown += new KeyEventHandler(textBox_KeyDown);
            this.textBoxX3.KeyDown += new KeyEventHandler(textBox_KeyDown);
            this.listView1.KeyDown += new KeyEventHandler(listView1_KeyDown);
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.begin = new delegateBeginToSearch(this.BeginToSearch);
                this.begin.BeginInvoke(null, null);
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                if (this.listView1.SelectedItems.Count != 0)
                {
                    this.activateFromList();
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                this.activateFromList();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.Description = @"Please identify the arrangement you want to search with...";
            folder.ShowNewFolderButton = false;

            if (folder.ShowDialog() == DialogResult.OK)
            {
                this.textBoxX2.Text = this.directory = folder.SelectedPath;
            }
        }

        protected void activateFromList()
        {
            new CloudyExplorer(this.form.getImageList()).runItem(this.listView1.SelectedItems[0].SubItems[1].Text.ToString() + @"\" +
                                            this.listView1.SelectedItems[0].SubItems[0].Text.ToString(), this.form.getTabControl());
        }

        protected delegate void delegateBeginToSearch();
        protected void BeginToSearch()
        {
            if (this.textBoxX2.Text.Length == 0)
            {
                MessageBox.Show("Please identify information before searching.", "Unknow Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
/*
                if (this.buttonX2.Text == @"Search")
                {
                    this.buttonX2.Text = @"Stop";
                }
 */ 
                this.listView1.Items.Clear();
                this.target = this.textBoxX1.Text;
                this.directory = this.textBoxX2.Text;
                this.Text = this.target + @" - Local Search";
                this.doSearch(this.directory);
                this.buttonX2.Text = @"Search";
            }
        }

        protected void doSearch(String path)
        {
            try
            {
                DirectoryInfo directory = new DirectoryInfo(path);
                {
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        this.toolStripStatusLabel1.Text = file.FullName;
                        if (file.Name.ToUpper().Contains(this.target.ToUpper()))
                        {
                            if (this.textBoxX3.Text.Length == 0 || this.textBoxX3.Text == @"*")
                            {
                                ListViewItem item = new ListViewItem(file.Name);
                                item.SubItems.Add(file.Directory.FullName);
                                item.SubItems.Add(file.Extension);
                                item.SubItems.Add(file.Length.ToString());
                                item.SubItems.Add(file.LastWriteTime.ToString());
                                this.listView1.Items.Add(item);
                            }
                            else
                            {
                                if (file.Extension.ToUpper() == @"." + this.textBoxX3.Text.ToUpper())
                                {
                                    ListViewItem item = new ListViewItem(file.Name);
                                    item.SubItems.Add(file.Directory.FullName);
                                    item.SubItems.Add(file.Extension);
                                    item.SubItems.Add(file.Length.ToString());
                                    item.SubItems.Add(file.LastWriteTime.ToString());
                                    this.listView1.Items.Add(item);
                                }
                            }
                        }
                    }

                    if (this.checkBoxX1.Checked)
                    {
                        foreach (DirectoryInfo dir in directory.GetDirectories())
                        {
                            this.doSearch(dir.FullName);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            this.toolStripStatusLabel1.Text = "Ready";
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.begin = new delegateBeginToSearch(this.BeginToSearch);
            this.result = this.begin.BeginInvoke(null, null);
        }

        private void runAsDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                System.Diagnostics.Process.Start(this.listView1.SelectedItems[0].SubItems[1].Text.ToString() + @"\" + this.listView1.SelectedItems[0].SubItems[0].Text.ToString());
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
