using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace LZ_Marina
{
    public partial class ConsoleApps : TabPage
    {
        private Process consoleProcess;
        private String defaultAppsPath;

        public ConsoleApps()
        {
            InitializeComponent();
            this.initialEvents();

            this.defaultAppsPath = Application.StartupPath + @"\File System\ConsoleApps\";
            this.loadApps();
            this.initialConsole();

            this.welcome();
            //this.redirectTest();
        }

        protected void redirectTest()
        {
            this.richTextBox1.Text += "\r\n\r\n===========================\r\n\r\n";
            this.consoleProcess.Start();
            StreamReader reader = this.consoleProcess.StandardOutput;
            for (int index = 0; index != 3; ++index)
            {
                this.richTextBox1.Text += reader.ReadLine() + "\r\n";
            }
        }

        protected void welcome()
        {
            if (new FileInfo(Application.StartupPath + @"\File System\ConsoleApps\Introduction.txt").Exists)
            {
                StreamReader reader = new StreamReader(Application.StartupPath + @"\File System\ConsoleApps\Introduction.txt");
                this.richTextBox1.Text = reader.ReadToEnd();
                this.richTextBox1.Text += "\r\n";
                reader.Close();
            }
        }

        protected void initialEvents()
        {
            this.Text = @"Console Apps Platform";
            this.listView1.DoubleClick += new EventHandler(listView1_DoubleClick);
            this.textBoxX2.TextChanged += new EventHandler(textBoxX2_TextChanged);
//            this.richTextBox1.KeyDown += new KeyEventHandler(richTextBox1_KeyDown);
        }

        protected void loadApps(Boolean flag)
        {
            this.listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(this.defaultAppsPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToUpper() == @".EXE" && file.Name.ToUpper().Contains(this.textBoxX2.Text.ToUpper()))
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    /* Reserved space for "Description" */
                    this.listView1.Items.Add(item);
                }
            }
        }

        protected void loadApps()
        {
            this.listView1.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(this.defaultAppsPath);
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Extension.ToUpper() == @".EXE")
                {
                    ListViewItem item = new ListViewItem(file.Name);
                    /* Reserved space for "Description" */
                    this.listView1.Items.Add(item);
                }
            }
        }

        protected void initialConsole()
        {
            this.consoleProcess = new Process();
            this.consoleProcess.StartInfo.FileName = @"cmd.exe";
            this.consoleProcess.StartInfo.CreateNoWindow = true;
            this.consoleProcess.StartInfo.RedirectStandardError = true;
            this.consoleProcess.StartInfo.RedirectStandardInput = true;
            this.consoleProcess.StartInfo.RedirectStandardOutput = true;
            this.consoleProcess.StartInfo.UseShellExecute = false;
            this.consoleProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        }

        protected String executeCommand(String cmd)
        {
            String ans = "";
            try
            {
                this.consoleProcess.Start();
                this.consoleProcess.StandardInput.WriteLine(cmd);
                this.consoleProcess.StandardInput.WriteLine(@"exit");
                ans = this.consoleProcess.StandardOutput.ReadToEnd();
                ans += "\r\n" + this.consoleProcess.StandardError.ReadToEnd();
                this.consoleProcess.WaitForExit();
                this.consoleProcess.Close();

                String[] temp = ans.Split('\n');
                String header = temp[0] + "\n" + temp[1];
                String statement = temp[3];
                //String statement = temp[3].Substring(0, temp[3].Length - 1);
                String tail = statement.Replace(cmd, "exit");

                ans = ans.Replace(header, "");
                ans = ans.Replace(statement, "");
                ans = ans.Replace(tail, "");

                /*Use to remove the 5(five) "newline"/"return" at the beginning of answer string after formatting. */
                ans = ans.Substring(4);
            }
            catch (Exception)
            {
            }
            return ans;
        }

        protected delegate void delegateExecuteApp(String app, String arg);

        protected void executeApp(String app, String arg)
        {
            this.consoleProcess.Start();
            this.consoleProcess.StandardInput.WriteLine("\"" + this.defaultAppsPath + app + "\" " + arg);
            this.consoleProcess.StandardInput.WriteLine(@"exit");
            String ans = this.consoleProcess.StandardOutput.ReadToEnd();
            ans += "\r\n" + this.consoleProcess.StandardError.ReadToEnd();
            this.consoleProcess.WaitForExit();
            this.consoleProcess.Close();

            String[] temp = ans.Split('\n');
            String header = temp[0] + "\n" + temp[1];
            String statement = temp[3];
            //String statement = temp[3].Substring(0, temp[3].Length - 1);
            String tail = statement.Replace("\"" + this.defaultAppsPath + app + "\"", "exit");

            ans = ans.Replace(header, "");
            ans = ans.Replace(statement, "");
            ans = ans.Replace(tail, "");

            //Use to remove the 5(five) "newline"/"return" at the beginning of answer string after formatting.
            ans = ans.Substring(4);

            this.richTextBox1.Text = ans;
        }

        protected void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                String app = this.listView1.SelectedItems[0].SubItems[0].Text;
                this.Text = app + @" - Console Apps Platform";
                if (this.textBoxX1.Text.Length != 0)
                {
                    new delegateExecuteApp(this.executeApp).BeginInvoke(app, " \"" + this.textBoxX1.Text + "\"", null, null);
                }
                else
                {
                    new delegateExecuteApp(this.executeApp).BeginInvoke(app, "", null, null);
                }
            }
        }

        protected void textBoxX2_TextChanged(object sender, EventArgs e)
        {
            if (this.textBoxX2.Text.Length != 0)
            {
                this.loadApps(true);
            }
            else
            {
                this.loadApps();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Text = "";
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.richTextBox1.Text.Contains('!'))
            {
                String cmd = this.richTextBox1.Text.Substring(this.richTextBox1.Text.LastIndexOf("!"));
                cmd = cmd.Remove(0, 1);
                this.richTextBox1.Text = this.executeCommand(cmd);
            }
        }
    }
}
