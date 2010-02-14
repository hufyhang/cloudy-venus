﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class AppBrowser : TabPage
    {
        private String URL = "";

        public AppBrowser()
        {
            InitializeComponent();
        }

        public AppBrowser(String URL, String title)
        {
            InitializeComponent();
            this.URL = URL;
            this.labelX1.Text = title;
            //this.webBrowser1.NewWindow += new CancelEventHandler(webBrowser1_NewWindow);
            this.webBrowser1.BeforeNewWindow += new EventHandler<ExtendedWebBrowser.WebBrowserExtendedNavigatingEventArgs>(webBrowser1_BeforeNewWindow);
            this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
            this.webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            this.webBrowser1.Navigate(URL);
            this.Text = title;

            this.button2.Enabled = this.button3.Enabled = false;
        }

        protected void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.progressBarX1.Visible = true;
        }

        protected void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (this.webBrowser1.CanGoBack)
            {
                this.button2.Enabled = true;
            }
            else
            {
                this.button2.Enabled = false;
            }

            if (this.webBrowser1.CanGoForward)
            {
                this.button3.Enabled = true;
            }
            else
            {
                this.button3.Enabled = false;
            }

            this.progressBarX1.Visible = false;
        }

        protected void webBrowser1_BeforeNewWindow(object sender, ExtendedWebBrowser.WebBrowserExtendedNavigatingEventArgs e)
        {
            e.Cancel = true;
            try
            {
                String url = this.webBrowser1.Document.ActiveElement.GetAttribute("href");
                ((ExtendedWebBrowser.ExtendedWebBrowser)sender).Navigate(e.Url);
            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.webBrowser1.GoForward();
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(URL);
        }

        private void progressBarX1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Stop();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.webBrowser1.Url.AbsoluteUri);
            new fullscreenBrowser(this.webBrowser1).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new Print(this.webBrowser1).ShowDialog();
        }
    }
}
