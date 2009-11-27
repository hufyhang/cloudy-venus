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
        private int currentItemIndex;

        public Picture_Viewer()
        {
            InitializeComponent();
            this.Text = "Picture Viewer";
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
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

        protected void initialPath()
        {
            DirectoryInfo dir = new DirectoryInfo(this.path);
            foreach (FileInfo file in dir.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add((file.Length / 1024).ToString());
                this.listView1.Items.Add(item);
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
        }

    }
}
