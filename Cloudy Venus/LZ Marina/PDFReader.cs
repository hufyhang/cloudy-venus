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
    public partial class PDFReader : TabPage
    {
        private ArrayList list = new ArrayList();

        public PDFReader()
        {
            InitializeComponent();
            this.Text = "PDF Reader";
            this.listView1.SelectedIndexChanged += new EventHandler(listView1_SelectedIndexChanged);
            this.axAcroPDF1.src = "";
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
//                MessageBox.Show(this.list[this.listView1.SelectedItems[0].Index].ToString());
                String src = this.list[this.listView1.SelectedItems[0].Index].ToString();
                this.axAcroPDF1.src = src;
                this.Text = new FileInfo(this.list[this.listView1.SelectedItems[0].Index].ToString()).Name + @" - PDF Reader";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Multiselect = true;
                file.Filter = "PDF Files (*.PDF) | *.PDF";
                file.Title = @"Import...";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    String[] files = file.FileNames;
                    foreach (String str in files)
                    {
                        FileInfo info = new FileInfo(str);
                        this.list.Add(info.FullName);
                        ListViewItem item = new ListViewItem(info.Name);
                        item.SubItems.Add(info.LastWriteTime.ToString());
                        this.listView1.Items.Add(item);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.list.RemoveAt(this.listView1.SelectedItems[0].Index);
                this.listView1.Items.RemoveAt(this.listView1.SelectedItems[0].Index);
            }
            else
            {
                MessageBox.Show("Please select an item before removing.", "Unknown item...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
