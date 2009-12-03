using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class fullscreenBrowser : Form
    {
        private String root = "";

        public fullscreenBrowser()
        {
            InitializeComponent();
            initialEvents();
        }

        public fullscreenBrowser(WebBrowser web)
        {
            InitializeComponent();
            initialEvents();
            this.root = web.Url.AbsoluteUri;
            this.webBrowser1.Navigate(this.root);
            this.Text = this.labelX1.Text = web.Document.Title;
        }

        protected void initialEvents()
        {
            this.webBrowser1.Navigating += new WebBrowserNavigatingEventHandler(webBrowser1_Navigating);
            this.webBrowser1.Navigated += new WebBrowserNavigatedEventHandler(webBrowser1_Navigated);
        }

        protected void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            this.progressBarX1.Visible = false;
        }

        protected void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            this.progressBarX1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void labelX1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate(this.root);
        }

        private void progressBarX1_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Stop();
        }
    }
}
