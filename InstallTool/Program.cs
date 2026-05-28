using System;
using System.Linq;
using System.Diagnostics;

namespace InstallTool
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Contains("del"))
            {
                try
                {
                    ProcessStartInfo info = new ProcessStartInfo();
                    info.FileName = "net.exe";
                    info.Arguments = "stop Protect-Process";
                    info.CreateNoWindow = true;
                    info.UseShellExecute = true;
                    info.Verb = "runas";
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(info);
                    info.FileName = "sc.exe";
                    info.Arguments = "delete \"Protect-Process\"";
                    Process.Start(info);
                    info.FileName = "schtasks.exe";
                    info.Arguments = "/delete /tn \"Application Blocker\" /f";
                    Process.Start(info);
                    info.FileName = "taskkill";
                    info.Arguments = "-f -im \"Application Blocker.exe\"";
                    Process.Start(info);
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(-1);
                }
            }
            else if (args.Contains("ins"))
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    try
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = "sc.exe";
                        info.Arguments = "create \"Protect-Process\" binPath= \"C:\\Program Files (x86)\\Application Blocker\\Protect-Process.exe\" start= auto";
                        info.CreateNoWindow = true;
                        info.UseShellExecute = true;
                        info.Verb = "runas";
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                        Process.Start(info);
                        info.FileName = "net.exe";
                        info.Arguments = "start Protect-Process";
                        Process.Start(info);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(-1);
                    }
                }
                else
                {
                    try
                    {
                        ProcessStartInfo info = new ProcessStartInfo();
                        info.FileName = "sc.exe";
                        info.Arguments = "create \"Protect-Process\" binPath= \"C:\\Program Files\\Application Blocker\\Protect-Process.exe\" start= auto";
                        info.CreateNoWindow = true;
                        info.UseShellExecute = true;
                        info.Verb = "runas";
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                        Process.Start(info);
                        info.FileName = "net.exe";
                        info.Arguments = "start Protect-Process";
                        Process.Start(info);
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Environment.Exit(-1);
                    }
                }
            }
        }
    }
}
