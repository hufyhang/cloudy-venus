using System;
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

        public Browser()
        {
            InitializeComponent();

            this.backButton.Enabled = false;
            this.forwardButton.Enabled = false;

            initialEvents();

            this.webBrowser1.Navigate(@"http://www.google.com");
        }

        public Browser(String URL)
        {
            InitializeComponent();

            this.backButton.Enabled = false;
            this.forwardButton.Enabled = false;

            initialEvents();

            this.webBrowser1.Navigate(URL);
            //this.webBrowser1.AllowWebBrowserDrop = true;
        }

        protected void initialEvents()
        {
            currentBrowser = new ExtendedWebBrowser.ExtendedWebBrowser();
            this.currentBrowser = this.webBrowser1;
            currentBrowser.BeforeNewWindow += new EventHandler<WebBrowserExtendedNavigatingEventArgs>(currentBrowser_BeforeNewWindow); 

            this.addressBox.KeyDown += new KeyEventHandler(addressBox_KeyDown);
            this.currentBrowser.Navigating += new WebBrowserNavigatingEventHandler(currentBrowser_Navigating);
            this.currentBrowser.Navigated += new WebBrowserNavigatedEventHandler(currentBrowser_Navigated);
            this.currentBrowser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(currentBrowser_DocumentCompleted);
            this.backButton.Click += new EventHandler(backButton_Click);
            this.forwardButton.Click += new EventHandler(forwardButton_Click);
            this.reloadButton.Click += new EventHandler(reloadButton_Click);
        }

        protected void currentBrowser_BeforeNewWindow(object sender, WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            ((ExtendedWebBrowser.ExtendedWebBrowser)sender).Navigate(e.Url);
        }

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

        protected void currentBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.addressBox.Text = this.currentBrowser.Url.ToString();
            this.reloadButton.Text = "R";
            String tabTitle = this.currentBrowser.Document.Title.ToString();
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
        }

        protected void addressBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.currentBrowser.Navigate(this.addressBox.Text);
                if (!this.addressBox.Items.Contains(this.addressBox.Text))
                {
                    this.addressBox.Items.Add(this.addressBox.Text);
                }
            }

            if (e.Control && e.KeyCode == Keys.Enter)
            {
                String url = @"http://" + this.addressBox.Text + @".com";
                this.currentBrowser.Navigate(url);
                if (!this.addressBox.Items.Contains(url))
                {
                    this.addressBox.Items.Add(url);
                }
            }
            e.Handled = true;
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

    }
}
