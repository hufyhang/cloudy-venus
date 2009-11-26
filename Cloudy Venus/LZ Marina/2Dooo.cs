using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace LZ_Marina
{
    public partial class _2Dooo : TabPage
    {
        private static string planPath = null;
        private static string[][] elements;
        private static int SelectedIndex = -1;

        public _2Dooo()
        {
            InitializeComponent();
            planPath = this.getPath(Application.StartupPath + "\\2DoooPath.2Dooo");
            getPlans(planPath);
            this.Text = "2Dooo Special";
        }

        protected string getPath(string path)
        {
            string ans = null;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                StreamReader reader = new StreamReader(path);
                ans = reader.ReadToEnd();
                reader.Close();
                return ans;
            }
            else
            {
                StreamWriter writer = new StreamWriter(path);
                writer.Write(Application.StartupPath + "\\2DoooDefaultPlans.2Dooo");
                writer.Close();
                return getPath(path);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void clear()
        {
            this.textBoxX1.Text = this.textBoxX2.Text = this.dateTimeInput1.Text = "";
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            string str = this.textBoxX1.Text + "`" + this.dateTimeInput1.Text + "`" + this.textBoxX2.Text + "|";
            if (new FileInfo(planPath).Exists)
            {
                writer = File.AppendText(planPath);
            }
            else
            {
                writer = new StreamWriter(planPath);
            }
            writer.Write(str);
            writer.Close();
            clear();
            MessageBox.Show("New 2Dooo planned. =)", "Mission completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getPlans(planPath);
        }

        protected void getPlans(string path)
        {
            try
            {
                this.listViewEx1.Items.Clear();
                string[] str = null;
                int index = 0;
                if (new FileInfo(path).Exists)
                {
                    StreamReader reader = new StreamReader(path);
                    string tmp = reader.ReadToEnd();
                    if (tmp.Substring(0, 1).Equals("|"))
                    {
                        tmp = tmp.Substring(1);
                    }
                    str = tmp.Split('|');
                    reader.Close();
                    elements = new string[str.Length][];
                    foreach (string temp in str)
                    {
                        elements[index] = temp.Split('`');
                        ++index;
                    }

                    for (int i = 0; i != index - 1; ++i)
                    {
                        ListViewItem item = new ListViewItem((i + 1).ToString());
                        item.SubItems.Add(elements[i][0]);
                        item.SubItems.Add(elements[i][1]);
                        this.listViewEx1.Items.Add(item);
                    }
                }
                else
                {
                    StreamWriter writer = new StreamWriter(path);
                    writer.Write("");
                    writer.Close();
                }

                this.buttonItem1.Text = "2Dooo List -- " + this.listViewEx1.Items.Count + " tips remaining.";
            }
            catch (Exception)
            {
            }
        }

        private void listViewEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.listViewEx1.SelectedItems.Count != 0)
                {
                    this.textBoxX4.Text = elements[listViewEx1.SelectedItems[0].Index][0];
                    this.dateTimeInput2.Text = elements[listViewEx1.SelectedItems[0].Index][1];
                    this.textBoxX6.Text = elements[listViewEx1.SelectedItems[0].Index][2];
                    SelectedIndex = listViewEx1.SelectedItems[0].Index;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            try
            {
                this.dateTimeInput2.Text = elements[listViewEx1.SelectedItems[0].Index][1];
                this.textBoxX6.Text = elements[listViewEx1.SelectedItems[0].Index][2];
            }
            catch (ArgumentOutOfRangeException exp)
            {
            }
        }

        protected void finishSave()
        {
            try
            {
                elements[listViewEx1.SelectedItems[0].Index][0] = this.textBoxX4.Text;
                elements[listViewEx1.SelectedItems[0].Index][1] = this.dateTimeInput2.Text;
                elements[listViewEx1.SelectedItems[0].Index][2] = this.textBoxX6.Text;
                string str = "";
                for (int index = 0; index != elements.Length - 1; ++index)
                {
                    str += elements[index][0] + "`" + elements[index][1] + "`" + elements[index][2] + "|";
                }
                StreamWriter writer = new StreamWriter(planPath);
                writer.Flush();
                writer.Write(str);
                writer.Close();
            }
            catch (ArgumentOutOfRangeException exp)
            {
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            try
            {
                elements[SelectedIndex][1] = this.dateTimeInput2.Text;
                elements[SelectedIndex][2] = this.textBoxX6.Text;
                string str = "";
                for (int index = 0; index != elements.Length - 1; ++index)
                {
                    str += elements[index][0] + "`" + elements[index][1] + "`" + elements[index][2] + "|";
                }
                StreamWriter writer = new StreamWriter(planPath);
                writer.Flush();
                writer.Write(str);
                writer.Close();
                MessageBox.Show("2Dooo element is up to date now. =)", "Mission completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                getPlans(planPath);
            }
            catch (ArgumentOutOfRangeException exp)
            {
            }
        }

        protected void finishDirect()
        {
            this.textBoxX4.Text = "<###FINISHING IN PROGRESS###>";
            this.textBoxX6.Text = "<###FINISHING IN PROGRESS###>";
            finishSave();
            string str = "";
            if (this.listViewEx1.SelectedItems.Count != 0)
            {
                str += this.textBoxX4.Text + "`" + this.dateTimeInput2.Text
                    + "`" + this.textBoxX6.Text + "|" + "";
            }
            StreamReader reader = new StreamReader(planPath);
            string[] income = Regex.Split(reader.ReadToEnd(), str);
            reader.Close();
            string outcome = "";
            foreach (string s in income)
            {
                outcome += s;
            }

            outcome = outcome.Replace("||", "|");

            StreamWriter writer = new StreamWriter(planPath);
            writer.Flush();
            writer.Write(outcome);
            writer.Close();

            this.textBoxX4.Text = this.textBoxX6.Text = this.dateTimeInput2.Text = "";
            getPlans(planPath);
        }

        protected void finish()
        {
            this.textBoxX4.Text = "<###FINISHING IN PROGRESS###>";
            this.textBoxX6.Text = "<###FINISHING IN PROGRESS###>";
            finishSave();
            string str = "";
            if (this.listViewEx1.SelectedItems.Count != 0)
            {
                str += elements[listViewEx1.SelectedItems[0].Index][0] + "`" + elements[listViewEx1.SelectedItems[0].Index][1]
                    + "`" + elements[listViewEx1.SelectedItems[0].Index][2] + "|" + "";
            }
            StreamReader reader = new StreamReader(planPath);
            string[] income = Regex.Split(reader.ReadToEnd(), str);
            reader.Close();
            string outcome = "";
            foreach (string s in income)
            {
                outcome += s;
            }

            outcome = outcome.Replace("||", "|");

            StreamWriter writer = new StreamWriter(planPath);
            writer.Flush();
            writer.Write(outcome);
            writer.Close();

            this.textBoxX4.Text = this.textBoxX6.Text = this.dateTimeInput2.Text = "";
            getPlans(planPath);
        }

        private void finishedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to finish this tip?", "Finish tip", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                finish();
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to finish this tip?", "Finish tip", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                finishDirect();
            }
        }

        private void buttonItem8_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            StreamWriter writer;
            string str = this.textBoxX1.Text + "`" + this.dateTimeInput1.Text + "`" + this.textBoxX2.Text + "|";
            if (new FileInfo(planPath).Exists)
            {
                writer = File.AppendText(planPath);
            }
            else
            {
                writer = new StreamWriter(planPath);
            }
            writer.Write(str);
            writer.Close();
            clear();
            MessageBox.Show("New 2Dooo planned. =)", "Mission completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            getPlans(planPath);
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog open = new OpenFileDialog())
            {
                if (open.ShowDialog() == DialogResult.OK)
                {
                    string file = open.FileName;
                    StreamReader reader = new StreamReader(file);
                    this.textBoxX2.Text += reader.ReadToEnd();
                    reader.Close();
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxX6.Copy();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxX6.Cut();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxX6.Paste();
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.textBoxX2.SelectAll();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Copy();
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Cut();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.textBoxX2.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.textBoxX6.SelectAll();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            try
            {
                SpeechLib.SpeechVoiceSpeakFlags spFlags = SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync;
                SpeechLib.SpVoice voice = new SpeechLib.SpVoice();
                voice.Speak("Message's subject: " + this.textBoxX4.Text + " . The message's contents are as follows. " + this.textBoxX6.Text, spFlags);
            }
            catch (Exception exp)
            {
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog file = new OpenFileDialog())
            {
                file.Filter = "2Dooo Files(*.2Dooo)|*.2Dooo";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = File.AppendText(planPath);
                    writer.Write(new StreamReader(file.FileName).ReadToEnd());
                    writer.Close();
                }
            }
            getPlans(planPath);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog file = new SaveFileDialog())
            {
                file.Filter = "2Dooo Files(*.2Dooo)|*.2Dooo";
                if (file.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(file.FileName);
                    writer.Write(new StreamReader(planPath).ReadToEnd());
                    writer.Close();
                    MessageBox.Show("2Dooo List has been exported. =)", "Mission completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }      
        }

        private void buttonItem10_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
