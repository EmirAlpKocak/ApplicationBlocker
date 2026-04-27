using System;
using System.IO;
using System.Windows.Forms;

namespace Protect_Process
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.Visible = false;
            this.WindowState = FormWindowState.Minimized;
            this.Opacity = 0;
            if (Environment.Is64BitOperatingSystem)
            {
                CheckFile64();
            }
            else
            {
                CheckFile32();
            }
        }
        private void CheckFile32()
        {
            try
            {
                FileStream stream = new FileStream("C:\\Program Files\\Application Blocker\\Application Blocker.exe", FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured. The Protect-Process will be closed. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
        private void CheckFile64()
        {
            try
            {
                FileStream stream = new FileStream("C:\\Program Files (x86)\\Application Blocker\\Application Blocker.exe", FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error has occured. The Protect-Process will be closed. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }
        }
    }
}
