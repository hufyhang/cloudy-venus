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
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception)
            {
                MessageBox.Show("Oops! We are so sorry that it seems there are something wrong with Cloudy Venus.\r\nCloudy Venus is trying to reboot now.",
                    "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Restart();
            }
        }
    }
}
