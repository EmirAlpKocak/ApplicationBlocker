using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Application_Blocker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool startup = args.Contains("/startup");
            Application.Run(new Form1(startup));
        }
    }
}
