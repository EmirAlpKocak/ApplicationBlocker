using System;
using System.ServiceProcess;
using System.IO;

namespace Protect_Process
{
    public partial class Service1 : ServiceBase
    {
        private string logPath = Path.GetTempPath() + "\\PPLog.log";
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                CheckFile64();
            }
            else
            {
                CheckFile32();
            }
        }

        protected override void OnStop()
        {
        }
        private void CheckFile32()
        {
            foreach (string file in Directory.GetFiles("C:\\Program Files\\Application Blocker", "*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                } 
                catch (Exception ex)
                {
                    File.AppendAllText(logPath, DateTime.Now.ToString() + " Error: " + ex.Message);
                    continue;
                }
            }
        }
        private void CheckFile64()
        {
            foreach (string file in Directory.GetFiles("C:\\Program Files (x86)\\Application Blocker", "*.*", SearchOption.TopDirectoryOnly))
            {
                try
                {
                    FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
                }
                catch (Exception ex)
                {
                    File.AppendAllText(logPath, DateTime.Now.ToString() + " Error: " + ex.Message);
                    continue;
                }
            }
        }
    }
}
