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
        private Boolean singleFile;

        private ListView searchListView = new ListView();
        private ListView.ListViewItemCollection searchList;

        public Editor()
        {
            InitializeComponent();
            this.Text = "Notepad";
            this.path = Application.StartupPath + @"\File System\Text Files\";
            loadFiles();
        }

        public Editor(String path, Boolean SingleFileFlag)
        {
            InitializeComponent();

            this.richTextBox1.KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
            this.textBoxX1.KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            this.textBoxX2.TextChanged += new EventHandler(textBoxX2_TextChanged);

            searchList = new ListView.ListViewItemCollection(this.searchListView);

            this.Text = "Notepad";
            this.path = path;
            this.singleFile = SingleFileFlag;
            if (this.singleFile)
            {
                loadFile();
            }
            else
            {
                loadFiles();
            }
        }

        protected void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            //this.listView1.Items.Clear();
            try
            {
                if (!this.singleFile)
                {
                    if (this.textBoxX2.Text.Length != 0)
                    {
                        loadFiles(true);
                    }
                    else
                    {
                        loadFiles();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        protected void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)
            {
                saveItem();
            }
        }

        protected void loadFile()
        {
            this.Text = "Notepad";
            this.listView1.Items.Clear();
            this.storage.Clear();
            this.textBoxX1.Text = this.richTextBox1.Text = "";

            FileInfo file = new FileInfo(path);
            ListViewItem item = new ListViewItem(file.Name);
            item.SubItems.Add(file.LastWriteTime.ToString());
            this.listView1.Items.Add(item);
            this.storage.Add(file);
            this.listView1.Items[0].Selected = true;
            this.textBoxX1.Text = file.Name;
        }

        protected void loadFiles(Boolean flag)
        {
            this.Text = "Notepad";
            this.listView1.Items.Clear();
            this.storage.Clear();
            this.textBoxX1.Text = this.richTextBox1.Text = "";

            DirectoryInfo dir = new DirectoryInfo(path);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.Contains(this.textBoxX2.Text))
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    item.SubItems.Add(file.LastWriteTime.ToString());
                    this.listView1.Items.Add(item);

                    this.storage.Add(file);
                }
            }
        }

        protected void loadFiles()
        {
            this.Text = "Notepad";
            this.listView1.Items.Clear();
            this.storage.Clear();
//            this.textBoxX1.Text = this.richTextBox1.Text = "";

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

        protected void saveItem()
        {
            if (this.textBoxX1.Text.Length != 0)
            {
                if (this.singleFile)
                {
                    this.richTextBox1.Text = this.richTextBox1.Text.Replace("\n", "\r\n");
                    if (!new FileInfo(this.path).Exists)
                    {
                        String title = this.textBoxX1.Text;
                        String content = this.richTextBox1.Text;
                        FileInfo file = new FileInfo(this.path);
                        this.richTextBox1.SaveFile(this.path, RichTextBoxStreamType.PlainText);
                        this.Text = this.textBoxX1.Text = title;
                        this.richTextBox1.Text = content;
                    }
                    else
                    {
                        if (MessageBox.Show(this.textBoxX1.Text + " exists already.\r\nAre you sure you want to overwrite it?",
                            "Item exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            String title = this.textBoxX1.Text;
                            String content = this.richTextBox1.Text;
                            FileInfo file = new FileInfo(this.path);
                            this.richTextBox1.SaveFile(this.path, RichTextBoxStreamType.PlainText);
                            this.Text = this.textBoxX1.Text = title;
                            this.richTextBox1.Text = content;
                        }
                    }
                }
                else
                {
                    this.richTextBox1.Text = this.richTextBox1.Text.Replace("\n", "\r\n");
                    if (!new FileInfo(this.path + this.textBoxX1.Text).Exists)
                    {
                        String title = this.textBoxX1.Text;
                        String content = this.richTextBox1.Text;
                        FileInfo file = new FileInfo(this.path + this.textBoxX1.Text);
                        //                    StreamWriter writer = new StreamWriter(this.path + this.textBoxX1.Text, false, System.Text.Encoding.Default);
                        this.richTextBox1.SaveFile(this.path + this.textBoxX1.Text, RichTextBoxStreamType.PlainText);
                        //                    writer.Write(this.richTextBox1.Text);
                        //                    writer.Close();
                        this.loadFiles();
                        this.Text = this.textBoxX1.Text = title;
                        this.richTextBox1.Text = content;
                    }
                    else
                    {
                        if (MessageBox.Show(this.textBoxX1.Text + " exists already.\r\nAre you sure you want to overwrite it?",
                            "Item exists...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            String title = this.textBoxX1.Text;
                            String content = this.richTextBox1.Text;
                            FileInfo file = new FileInfo(this.path + this.textBoxX1.Text);
                            //                        StreamWriter writer = new StreamWriter(this.path + this.textBoxX1.Text, false, System.Text.Encoding.Default);
                            this.richTextBox1.SaveFile(this.path + this.textBoxX1.Text, RichTextBoxStreamType.PlainText);
                            //                        writer.Write(this.richTextBox1.Text);
                            //                        writer.Close();
                            this.loadFiles();
                            this.Text = this.textBoxX1.Text = title;
                            this.richTextBox1.Text = content;
                        }
                    }
                    loadFiles();
                }
            }
            else
            {
                MessageBox.Show("Please enter a file name before saving.", "Untitled file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            saveItem();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                if (this.singleFile)
                {
                    StreamReader reader = new StreamReader(this.path, System.Text.Encoding.Default);
                    this.richTextBox1.Text = reader.ReadToEnd();
                    reader.Close();
                }
                else
                {
                    this.richTextBox1.Text = this.textBoxX1.Text = "";
                    FileInfo file = (FileInfo)this.storage[this.listView1.SelectedItems[0].Index];
                    this.textBoxX1.Text = file.Name;
                    StreamReader reader = new StreamReader(file.FullName, System.Text.Encoding.Default);
                    this.richTextBox1.Text = reader.ReadToEnd();
                    reader.Close();
                    this.Text = this.textBoxX1.Text;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                if (this.singleFile)
                {
                    FileInfo file = new FileInfo(this.path);
                    if (MessageBox.Show("Are you sure you want to remove " + this.textBoxX1.Text + "?", "Remove item...",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.richTextBox1.Text = this.textBoxX1.Text = "";
                        file.Delete();
                        this.listView1.Items.Clear();
                    }
                }
                else
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

        private void button5_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose your item folder.";
                folder.ShowNewFolderButton = false;
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.path = folder.SelectedPath + @"\";
                    this.loadFiles();
                }
            }
        }

        protected void export()
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                using (FolderBrowserDialog folder = new FolderBrowserDialog())
                {
                    folder.Description = "Please choose a destination for " + this.listView1.SelectedItems[0].SubItems[0].Text;
                    if (folder.ShowDialog() == DialogResult.OK)
                    {
                        if (this.singleFile)
                        {
                            FileInfo info = new FileInfo(this.path);
                            String to = folder.SelectedPath + @"\" + info.Name;
                            FileInfo file = new FileInfo(to);
                            FileInfo target = new FileInfo(this.path);
                            if (file.Exists)
                            {
                                if (MessageBox.Show("Are you sure you want to overwrite " + info.Name + @"?",
                                    "Item existing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    file.Delete();
                                    target.CopyTo(file.FullName);
                                }
                            }
                            else
                            {
                                target.CopyTo(file.FullName);
                            }
                        }
                        else
                        {
                            String to = folder.SelectedPath + @"\" + this.listView1.SelectedItems[0].SubItems[0].Text;
                            FileInfo file = new FileInfo(to);
                            FileInfo target = new FileInfo(this.path + this.listView1.SelectedItems[0].SubItems[0].Text);
                            if (file.Exists)
                            {
                                if (MessageBox.Show("Are you sure you want to overwrite " + this.listView1.SelectedItems[0].SubItems[0].Text + @"?",
                                    "Item existing...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    file.Delete();
                                    target.CopyTo(file.FullName);
                                }
                            }
                            else
                            {
                                target.CopyTo(file.FullName);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Please choose an existing item before exporting.", "Unknown item selection...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.export();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Text = "";
        }
    }
}
