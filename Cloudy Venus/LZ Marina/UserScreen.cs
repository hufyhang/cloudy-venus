using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LZ_Marina
{
    public partial class UserScreen : TabPage
    {
        private string name = "";
        private string home = "";
        private string pic = "";
        private string screen = "";

        private String picRoot = "";
        private String textRoot = "";

        public UserScreen()
        {
            InitializeComponent();
            this.Text = "User Account";

            StreamReader reader = new StreamReader(Application.StartupPath + @"\user");
            name = reader.ReadLine();
            home = reader.ReadLine();
            pic = reader.ReadLine();
            screen = reader.ReadLine();
            picRoot = reader.ReadLine();
            textRoot = reader.ReadLine();
            reader.Close();

            if (picRoot.Equals(@"[default]"))
            {
                picRoot = Application.StartupPath + @"\File System\Pictures\";
            }

            if (textRoot.Equals(@"[default]"))
            {
                textRoot = Application.StartupPath + @"\File System\Common Files\";
            }

            this.textBoxX1.Text = this.name;
            this.textBoxX2.Text = this.home;
            this.textBoxX3.Text = this.picRoot;
            this.textBoxX4.Text = this.textRoot;
            this.pictureBox1.Image = new Bitmap(pic);

            if (this.screen.Equals(@"fullScreen=1"))
            {
                this.checkBoxX1.Checked = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog file = new OpenFileDialog())
                {
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        this.pictureBox1.Image = new Bitmap(file.FileName);
                        this.pic = file.FileName;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please make sure you choose a valid image.", "User logo...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void commandLink2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void commandLink1_Click(object sender, EventArgs e)
        {
            this.name = this.textBoxX1.Text;
            this.home = this.textBoxX2.Text;
            this.picRoot = this.textBoxX3.Text;
            this.textRoot = this.textBoxX4.Text;
            String screen = "";

            if (this.checkBoxX1.Checked)
            {
                screen = @"fullScreen=1";
            }
            else
            {
                screen = @"fullScreen=0";
            }

            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\user");
            writer.Flush();
            String str = name + "\r\n" + home + "\r\n" + pic + "\r\n" + screen + "\r\n" + picRoot + "\r\n" + textRoot;
            writer.Write(str);
            writer.Close();

            MessageBox.Show("Your user account changes will be updated after your next-time login.", "User Account", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose your default Picture Folder";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxX3.Text = folder.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                folder.Description = @"Please choose your default Common Files folder";
                if (folder.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxX4.Text = folder.SelectedPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.name = this.textBoxX1.Text;
            this.home = this.textBoxX2.Text;
            this.picRoot = this.textBoxX3.Text;
            this.textRoot = this.textBoxX4.Text;
            String screen = "";

            if (this.checkBoxX1.Checked)
            {
                screen = @"fullScreen=1";
            }
            else
            {
                screen = @"fullScreen=0";
            }

            StreamWriter writer = new StreamWriter(Application.StartupPath + @"\user");
            writer.Flush();
            String str = name + "\r\n" + home + "\r\n" + pic + "\r\n" + screen + "\r\n" + picRoot + "\r\n" + textRoot;
            writer.Write(str);
            writer.Close();

            MessageBox.Show("Your user account changes will be updated after your next-time login.", "User Account", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
