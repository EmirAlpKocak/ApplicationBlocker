using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Application_Blocker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static Mutex mutex;
        [STAThread]
        static void Main(string[] args)
        {
            bool createdNew;
            mutex = new Mutex(true, "ApplicationBlocker_MUTEX", out createdNew);
            if (!createdNew)
            {
                MessageBox.Show("Another instance is already running. Check application tray icon to access.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool startup = args.Contains("/startup");
            Application.Run(new Form1(startup));
        }
    }
}
