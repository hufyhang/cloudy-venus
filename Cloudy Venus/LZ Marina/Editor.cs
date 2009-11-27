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
    public partial class Editor : TabPage
    {
        private String path = "";
        private ArrayList storage = new ArrayList();
        private Boolean wordWrap = true;

        public Editor()
        {
            InitializeComponent();
            this.Text = "Notepad";
            this.path = Application.StartupPath + @"\File System\Text Files\";
            loadFiles();
        }

        protected void loadFiles()
        {
            this.Text = "Notepad";
            this.listView1.Items.Clear();
            this.storage.Clear();

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles())
            {
                ListViewItem item = new ListViewItem(file.Name);
                item.SubItems.Add(file.LastWriteTime.ToString());
                this.listView1.Items.Add(item);

                this.storage.Add(file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Text = "Notepad";
            this.textBoxX1.Text = "";
            this.richTextBox1.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.textBoxX1.Text.Length != 0)
            {
                String title = this.textBoxX1.Text;
                FileInfo file = new FileInfo(this.path + this.textBoxX1.Text);
                StreamWriter writer = new StreamWriter(this.path + this.textBoxX1.Text);
                writer.Write(this.richTextBox1.Text);
                writer.Close();
                this.loadFiles();
                this.Text = title;
            }
            else
            {
                MessageBox.Show("Please enter a file name before saving.", "Untitled file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.richTextBox1.Text = this.textBoxX1.Text = "";
                FileInfo file = (FileInfo)this.storage[this.listView1.SelectedItems[0].Index];
                this.textBoxX1.Text = file.Name;
                StreamReader reader = new StreamReader(file.FullName);
                this.richTextBox1.Text = reader.ReadToEnd();
                reader.Close();
                this.Text = this.textBoxX1.Text;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                FileInfo file = (FileInfo)this.storage[this.listView1.SelectedItems[0].Index];
                if (MessageBox.Show("Are you sure you want to remove " + file.Name + "?", "Remove item...",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.richTextBox1.Text = this.textBoxX1.Text = "";
                    file.Delete();
                    this.loadFiles();
                }
            }
            else
            {
                MessageBox.Show("Please choose an item to be removed with.", "Unknown item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.wordWrap)
            {
                this.wordWrap = false;
            }
            else
            {
                this.wordWrap = true;
            }
            this.richTextBox1.WordWrap = this.wordWrap;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.SelectAll();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Paste();
        }

    }
}
