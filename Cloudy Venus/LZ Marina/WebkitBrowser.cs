using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class WebkitBrowser : Form
    {
        public WebkitBrowser()
        {
            InitializeComponent();
        }

        protected void initialEvents()
        {
            this.webKitBrowser1.NewWindowRequest += new WebKit.NewWindowRequestEventHandler(webKitBrowser1_NewWindowRequest);
        }

        void webKitBrowser1_NewWindowRequest(object sender, WebKit.NewWindowRequestEventArgs args)
        {
            this.webKitBrowser1.Navigate(args.Url);
            throw new NotImplementedException();
        }
    }
}
