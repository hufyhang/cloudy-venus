﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ExtendedWebBrowser;

namespace LZ_Marina
{
    public partial class Browser : TabPage
    {
        private ExtendedWebBrowser.ExtendedWebBrowser currentBrowser;
        private TabControl tabControl;
        private String firstUrl = "";
        private ImageList imageList;
        private Form1 form1;

        public Browser(TabControl tab, Form1 form1)
        {
            InitializeComponent();

            this.tabControl = tab;
            this.backButton.Enabled = false;
            this.forwardButton.Enabled = false;

            initialEvents();

            this.webBrowser1.Navigate(@"http://www.google.com");
            this.form1 = form1;
            this.imageList = this.form1.getImageList();
        }

        public Browser(String URL, TabControl tab, Form1 form1)
        {
            InitializeComponent();

            this.tabControl = tab;
            this.backButton.Enabled = false;
            this.forwardButton.Enabled = false;

            initialEvents();

            this.webBrowser1.Navigate(URL);
            this.form1 = form1;
            this.imageList = this.form1.getImageList();
            //this.webBrowser1.AllowWebBrowserDrop = true;
        }

        protected void initialEvents()
        {
            currentBrowser = new ExtendedWebBrowser.ExtendedWebBrowser();
            this.currentBrowser = this.webBrowser1;
            this.currentBrowser.IsWebBrowserContextMenuEnabled = true;
            this.currentBrowser.BeforeNewWindow += new EventHandler<WebBrowserExtendedNavigatingEventArgs>(currentBrowser_BeforeNewWindow); 
            
            this.addressBox.GotFocus += new EventHandler(addressBox_GotFocus);
            this.addressBox.KeyDown += new KeyEventHandler(addressBox_KeyDown);
            this.currentBrowser.Navigating += new WebBrowserNavigatingEventHandler(currentBrowser_Navigating);
            this.currentBrowser.Navigated += new WebBrowserNavigatedEventHandler(currentBrowser_Navigated);
//            this.currentBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(currentBrowser_DocumentCompleted);
            this.backButton.Click += new EventHandler(backButton_Click);
            this.forwardButton.Click += new EventHandler(forwardButton_Click);
            this.reloadButton.Click += new EventHandler(reloadButton_Click);
        }

        protected void addressBox_GotFocus(object sender, EventArgs e)
        {
            this.addressBox.SelectAll();
        }

        protected void currentBrowser_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                //this.tabControl.Controls.Add(new Browser(this.currentBrowser.Document.ActiveElement.GetAttribute("href"), this.tabControl));
                Browser browser = new Browser(e.Url, this.tabControl, this.form1);
                browser.ImageIndex = 2;
                this.tabControl.Controls.Add(browser);
                this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
                //this.tabControl.SelectedIndex = this.tabControl.TabCount - 1;
                //((ExtendedWebBrowser.ExtendedWebBrowser)sender).Navigate(e.Url);
            }
            catch (Exception)
            {
            }
        }
/*
        protected void currentBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            foreach (HtmlElement archor in this.currentBrowser.Document.Links)
            {
                archor.SetAttribute("target", "_self");
            }

            foreach (HtmlElement form in this.currentBrowser.Document.Forms)
            {
                form.SetAttribute("target", "_self");
            }

        }
*/
        protected void currentBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.addressBox.Text = this.currentBrowser.Url.ToString();
            this.webBrowser1.Focus();
//            this.addressBox.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.reloadButton.Text = "R";
            String tabTitle = this.currentBrowser.Document.Title.ToString();

            this.ImageIndex = 2;
            this.Text = tabTitle;

            if (this.webBrowser1.CanGoBack)
            {
                this.backButton.Enabled = true;
            }
            else
            {
                this.backButton.Enabled = false;
            }

            if (this.webBrowser1.CanGoForward)
            {
                this.forwardButton.Enabled = true;
            }
            else
            {
                this.forwardButton.Enabled = false;
            }
/*
            if (this.Text != @"Loading...")
            {
                Size tempSize = this.tabControl.ItemSize;
                this.tabControl.ItemSize = new Size(0, 0);
                this.tabControl.ItemSize = tempSize;
            }
 */
        }

        protected void addressBox_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                this.addressBox.Text.Remove(this.addressBox.Text.Length - 1);
                if (this.firstUrl.Equals(""))
                {
                    this.firstUrl = this.addressBox.Text;
                }
                this.currentBrowser.Navigate(this.addressBox.Text);
            }

            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                this.addressBox.Text.Remove(this.addressBox.Text.Length - 1);
                String url = @"http://" + this.addressBox.Text + @".com";
                this.currentBrowser.Navigate(url);
            }

            if (e.Control && e.KeyCode == Keys.A)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                this.addressBox.SelectAll();
            }

            //e.Handled = true;
//            this.addressBox.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        protected void forwardButton_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }

        protected void backButton_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        protected void reloadButton_Click(object sender, EventArgs e)
        {
            if (this.reloadButton.Text.Equals("S"))
            {
                this.webBrowser1.Stop();
                this.reloadButton.Text = "R";
                this.Text = this.webBrowser1.Document.Title.ToString();
            }
            else
            {
                this.webBrowser1.Refresh();
            }
        }

        protected void currentBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            try
            {
                String url = this.currentBrowser.Document.ActiveElement.GetAttribute("href");
                this.currentBrowser.Url = new Uri(url);
            }
            catch
            {
            }
        }

        protected void currentBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.reloadButton.Text = "S";
            this.Text = "Loading...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FileInfo file = new FileInfo(Application.StartupPath + @"\apps");
            StreamWriter writer = file.AppendText();
            String info = this.currentBrowser.Document.Title.ToString() + @"`" + this.currentBrowser.Url.ToString() + "\r\n";
            writer.Write(info);
            writer.Close();
            MessageBox.Show("Website favourited.", "Favourite", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.firstUrl.Length != 0)
            {
                this.webBrowser1.Navigate(this.firstUrl);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new fullscreenBrowser(this.webBrowser1).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Print(this.webBrowser1).ShowDialog();
        }

    }
}
