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
        
        public UserScreen()
        {
            InitializeComponent();
            this.Text = "User Account";

            StreamReader reader = new StreamReader(Application.StartupPath + @"\user");
            name = reader.ReadLine();
            home = reader.ReadLine();
            pic = reader.ReadLine();
            screen = reader.ReadLine();
            reader.Close();

            this.textBoxX1.Text = this.name;
            this.textBoxX2.Text = this.home;
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
            String str = name + "\r\n" + home + "\r\n" + pic + "\r\n" + screen;
            writer.Write(str);
            writer.Close();

            MessageBox.Show("Your user account changes will be updated after your next-time login.", "User Account", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Dispose();
        }

    }
}
