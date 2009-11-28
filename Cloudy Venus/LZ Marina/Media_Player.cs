using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using AxWMPLib;

namespace LZ_Marina
{
    public partial class Media_Player : TabPage
    {
        public Media_Player()
        {
            InitializeComponent();
            this.Text = "Media Player";
            this.axWindowsMediaPlayer1.settings.setMode("loop", true);
            this.axWindowsMediaPlayer1.currentPlaylist = this.axWindowsMediaPlayer1.newPlaylist("Default playlist", "");
            InitialEvents();
        }

        protected void InitialEvents()
        {
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
        }

        protected void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.axWindowsMediaPlayer1.Ctlcontrols.playItem(this.axWindowsMediaPlayer1.currentPlaylist.get_Item(this.listView1.SelectedItems[0].Index));
                this.Text = this.axWindowsMediaPlayer1.currentMedia.name + @" - Media Player";
            }
        }

        protected void media_play(String URL)
        {
            this.axWindowsMediaPlayer1.URL = URL;
            this.axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog files = new OpenFileDialog())
            {
                files.Multiselect = true;
                files.Title = @"Import...";
                if (files.ShowDialog() == DialogResult.OK)
                {
                    String[] items = files.FileNames;
                    foreach (String str in items)
                    {
                        this.axWindowsMediaPlayer1.currentPlaylist.appendItem(this.axWindowsMediaPlayer1.newMedia(str));
                        FileInfo info = new FileInfo(str);
                        this.listView1.Items.Add(new ListViewItem(info.Name));
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                this.axWindowsMediaPlayer1.currentPlaylist.removeItem(this.axWindowsMediaPlayer1.currentPlaylist.get_Item(this.listView1.SelectedItems[0].Index));
                this.listView1.Items.RemoveAt(this.listView1.SelectedItems[0].Index);
            }
            else
            {
                MessageBox.Show("Please select an item before removing.", "Unknown item...", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
