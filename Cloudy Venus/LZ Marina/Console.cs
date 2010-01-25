using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LZ_Marina
{
    public partial class Console : TabPage
    {
        Form1 form;
        public Console(Form1 form)
        {
            this.form = form;
            InitializeComponent();
            initialEvents();
        }

        protected void initialEvents()
        {
            this.richTextBox1.Focus();
            String welcome = "====== Cloudy Venus - Luna, Console ======\r\n?";
            this.richTextBox1.AppendText(welcome);
            this.richTextBox1.KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
        }

        protected void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.consoleCommands(this.richTextBox1.Text.Substring(this.richTextBox1.Text.LastIndexOf(@"?")));
            }
        }

        protected void consoleCommands(String cmd)
        {
            String ans = "";
            if (cmd.ToUpper().Equals("?VER"))
            {
                ans = "\r\n" + @"Product Name: " + Application.ProductName + "\r\n" + @"Current Version: "
                    + Application.ProductVersion + "\r\n" + @"Exe. path: " + Application.ExecutablePath + "\r\n";
            }

            else if (cmd.ToUpper().Equals("?SCRMODE"))
            {
                this.form.screenMode();
                ans = "\r\n" + @"Screen mode has been changed." + "\r\n";
            }

            else if (cmd.ToUpper().Equals("?DATE"))
            {
                ans = "\r\n" + @"System Date & Time: " + DateTime.Now.ToLocalTime().ToString() + "\r\n";
            }
/*
            else if (cmd.ToUpper().Equals("?VERCTRL"))
            {
                TabControl tab = this.form.getTabControl();
                tab.TabPages.Add(new VersionControl());
                tab.SelectedIndex = tab.TabPages.Count - 1;
                ans = "\r\n" + @"Version Control has been activited." + "\r\n";
            }
 */ 
            else if (cmd.ToUpper().Equals("?LOGOFF"))
            {
                Application.Exit();
                ans = "\r\n" + @"Bye." + "\r\n";
            }

            else if (cmd.ToUpper().Equals("?REBOOT"))
            {
                Application.Restart();
                ans = "\r\n" + @"Luna is reloading now. Please wait..." + "\r\n";
            }

            this.richTextBox1.AppendText(ans);
        }
    }
}
