using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LZ_Marina
{
    public partial class Picture_Viewer : TabPage
    {
        private String path = "";
        private ListView searchListView = new ListView();
        private ListView.ListViewItemCollection searchList;
        private int currentItemIndex;

        public Picture_Viewer()
        {
            InitializeComponent();
            this.Text = "Picture Viewer";
            initialEvents();
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose your picture folder.";
                folder.ShowNewFolderButton = false;
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.path = folder.SelectedPath;
                    initialPath();
                }
            }
        }

        public Picture_Viewer(String path)
        {
            InitializeComponent();
            this.Text = "Picture Viewer";
            initialEvents();
            this.path = path;
            initialPath();
        }

        public Picture_Viewer(String path, Boolean flag)
        {
            InitializeComponent();
            this.Text = "Picture Viewer";
            initialEvents();
            this.path = path;
            initialFile();
        }

        protected void initialEvents()
        {
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            this.textBoxX1.TextChanged += new EventHandler(textBoxX1_TextChanged);

            searchList = new ListView.ListViewItemCollection(this.searchListView);
        }

        protected void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.listView1.Items.Clear();
                if (this.textBoxX1.Text.Length != 0)
                {

                    foreach (ListViewItem item in this.searchList)
                    {
                        if (item.SubItems[0].Text.Contains(this.textBoxX1.Text))
                        {
                            this.listView1.Items.Add((ListViewItem)item.Clone());
                        }
                    }
                }
                else
                {
                    foreach (ListViewItem item in this.searchList)
                    {
                        this.listView1.Items.Add((ListViewItem)item.Clone());
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void initialFile()
        {
            FileInfo file = new FileInfo(this.path);
            ListViewItem item = new ListViewItem(file.Name);
            item.SubItems.Add((file.Length / 1024).ToString());
            item.SubItems.Add(file.LastWriteTime.ToString());
            this.listView1.Items.Add(item);
            this.pictureBox1.Image = new Bitmap(this.path);
            this.label1.Text = file.Name;
            this.listView1.Items[0].Selected = true;

            foreach (ListViewItem tempItem in this.listView1.Items)
            {
                this.searchList.Add((ListViewItem)tempItem.Clone());
            }
        }

        protected void initialPath()
        {
            DirectoryInfo dir = new DirectoryInfo(this.path);
            foreach (FileInfo file in dir.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add((file.Length / 1024).ToString());
                item.SubItems.Add(file.LastWriteTime.ToString());
                this.listView1.Items.Add(item);
            }

            foreach (ListViewItem item in this.listView1.Items)
            {
                this.searchList.Add((ListViewItem)item.Clone());
            }
        }

        protected void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                String pic = this.listView1.SelectedItems[0].SubItems[0].Text;
                try
                {
                    this.pictureBox1.Image = new Bitmap(this.path + @"\" + pic);
                    this.label1.Text = pic;
                }
                catch (Exception)
                {
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.listView1.SelectedItems[0].Index > 0)
                {
                    this.listView1.Items[this.listView1.SelectedItems[0].Index - 1].Selected = true;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.listView1.SelectedItems[0].Index < this.listView1.Items.Count -1)
                {
                    this.listView1.Items[this.listView1.SelectedItems[0].Index + 1].Selected = true;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose your picture folder.";
                folder.ShowNewFolderButton = false;
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.listView1.Items.Clear();
                    this.pictureBox1.Image = null;
                    this.label1.Text = "";

                    this.path = folder.SelectedPath;
                    initialPath();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                if (this.timer1.Enabled)
                {
                    this.timer1.Enabled = false;
                    this.progressBarX1.Visible = false;
                }
                else
                {
                    currentItemIndex = this.listView1.SelectedItems[0].Index;
                    this.timer1.Enabled = true;
                    this.progressBarX1.Visible = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.currentItemIndex < this.listView1.Items.Count)
            {
                String pic = this.listView1.Items[currentItemIndex++].SubItems[0].Text;
                try
                {
                    this.pictureBox1.Image = new Bitmap(this.path + @"\" + pic);
                    this.label1.Text = pic;
                    this.listView1.Items[currentItemIndex].Selected = true;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                this.currentItemIndex = this.listView1.Items[0].Index;
                String pic = this.listView1.Items[currentItemIndex].SubItems[0].Text;
                try
                {
                    this.pictureBox1.Image = new Bitmap(this.path + @"\" + pic);
                    this.label1.Text = pic;
                    this.listView1.Items[currentItemIndex].Selected = true;
                }
                catch (Exception)
                {
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.textBoxX1.Text = "";
        }

    }
}
