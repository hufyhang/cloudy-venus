using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LZ_Marina
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            //}
            /*
            catch (Exception exp)
            {
                MessageBox.Show("We are so sorry that there are something wrong with Cloudy Venus.\r\nCloudy Venus is trying to reboot now.", "Known Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Run(new Form1());
            }
             */
        }
    }
}
