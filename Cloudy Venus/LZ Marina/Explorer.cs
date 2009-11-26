using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Controls;

namespace LZ_Marina
{
    public partial class Explorer : TabPage
    {
        public Explorer()
        {
            InitializeComponent();
            this.comboBox1.KeyDown += new KeyEventHandler(comboBox1_KeyDown);
            this.explorerBrowser1.NavigationComplete += new ExplorerBrowserNavigationCompleteEventHandler(explorerBrowser1_NavigationComplete);
            this.explorerBrowser1.Navigate((ShellObject)KnownFolders.Desktop);
        }

        protected void explorerBrowser1_NavigationComplete(object sender, NavigationCompleteEventArgs e)
        {
            this.Text = this.explorerBrowser1.NavigationLog.CurrentLocation.Name;
            this.comboBox1.Text = this.explorerBrowser1.NavigationLog.CurrentLocation.ParsingName;
        }

        protected void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.explorerBrowser1.Navigate(ShellFolder.FromParsingName(this.comboBox1.Text));
                if (!this.comboBox1.Items.Contains(this.comboBox1.Text))
                {
                    this.comboBox1.Items.Add(this.comboBox1.Text);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.explorerBrowser1.NavigateLogLocation(this.explorerBrowser1.NavigationLog.CurrentLocationIndex - 1);
        }
    }
}
