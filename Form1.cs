using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.Collections.Generic;

namespace Application_Blocker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form2 frm2 = new Form2();
        private ManagementEventWatcher startWatch;
        public HashSet<int> handledPids = new HashSet<int>();
        private bool _startup = false;
        private bool blockFirst = true;
        public Form1(bool startup)
        {
            InitializeComponent();
            StartProcessMonitor();
            LoadColorSettings();
            _startup = startup;
            /*this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;*/
            changePasswordToolStripMenuItem.Click += changePasswordToolStripMenuItem_Click;
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            checkForUpdatesAutomaticallyToolStripMenuItem.Click += checkForUpdatesAutomaticallyToolStripMenuItem_Click;
            aboutApplicationBlockerToolStripMenuItem.Click += aboutApplicationBlockerToolStripMenuItem_Click;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        protected override void SetVisibleCore(bool value)
        {
            if (_startup && blockFirst)
            {
                value = false;
                blockFirst = false;
            }
            base.SetVisibleCore(value);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (_startup)
            {
                this.Resizable = false;
                /*listBox1.BackColor = Color.White;
                listBox1.ForeColor = Color.Black;*/
                listBox1.Font = new Font("Segoe UI", 10);
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                if (!Properties.Settings.Default.isSettingsUpgraded)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.isSettingsUpgraded = true;
                    Properties.Settings.Default.Save();
                }
                LoadSavedItems();
                LoadPassBlockedItems();
                if (Properties.Settings.Default.isAutoUpdatesEnabled)
                {
                    checkForUpdatesAutomaticallyToolStripMenuItem.Checked = true;
                    try
                    {
                        string version;
                        using (var client = new System.Net.WebClient())
                        {
                            string downloaded = client.DownloadString("https://raw.githubusercontent.com/EmirAlpKocak/ApplicationBlocker/refs/heads/main/version.txt");
                            version = downloaded.Trim();
                            if (version != "2.3.0")
                            {
                                int result;
                                if (Environment.Is64BitOperatingSystem)
                                {
                                    result = Dialog64.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                                }
                                else
                                {
                                    result = Dialog32.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                                }
                                if (result == 1)
                                {
                                    try
                                    {
                                        client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                        System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                        Application.Exit();
                                    }
                                    catch (Exception ex)
                                    {
                                        if (Environment.Is64BitOperatingSystem)
                                        {
                                            Dialog64.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                        }
                                        else
                                        {
                                            Dialog32.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Dialog64.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                        }
                        else
                        {
                            Dialog32.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    checkForUpdatesAutomaticallyToolStripMenuItem.Checked = false;
                }
            }
            else
            {
                this.Resizable = false;
                /*listBox1.BackColor = Color.White;
                listBox1.ForeColor = Color.Black;*/
                listBox1.Font = new Font("Segoe UI", 10);
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                if (!Properties.Settings.Default.isSettingsUpgraded)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.isSettingsUpgraded = true;
                    Properties.Settings.Default.Save();
                }
                frm2.ShowDialog();
                LoadSavedItems();
                LoadPassBlockedItems();
                if (Properties.Settings.Default.isAutoUpdatesEnabled)
                {
                    checkForUpdatesAutomaticallyToolStripMenuItem.Checked = true;
                    try
                    {
                        string version;
                        using (var client = new System.Net.WebClient())
                        {
                            string downloaded = client.DownloadString("https://raw.githubusercontent.com/EmirAlpKocak/ApplicationBlocker/refs/heads/main/version.txt");
                            version = downloaded.Trim();
                            if (version != "2.3.0")
                            {
                                int result;
                                if (Environment.Is64BitOperatingSystem)
                                {
                                    result = Dialog64.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                                }
                                else
                                {
                                    result = Dialog32.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                                }
                                if (result == 1)
                                {
                                    try
                                    {
                                        client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                        System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                        Application.Exit();
                                    }
                                    catch (Exception ex)
                                    {
                                        if (Environment.Is64BitOperatingSystem)
                                        {
                                            Dialog64.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                        }
                                        else
                                        {
                                            Dialog32.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Dialog64.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                        }
                        else
                        {
                            Dialog32.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    checkForUpdatesAutomaticallyToolStripMenuItem.Checked = false;
                }
            }
        }
        private void LoadSavedItems()
        {
            if (Properties.Settings.Default.BlockedApplications != null)
            {
                foreach (string app in Properties.Settings.Default.BlockedApplications)
                {
                    listBox1.Items.Add(app);
                }
            }
        }
        private void LoadPassBlockedItems()
        {
            if (Properties.Settings.Default.PasswordBlockedApps != null)
            {
                foreach (string app in Properties.Settings.Default.PasswordBlockedApps)
                {
                    listBox1.Items.Add(app);
                }
            }
        }
        private void SaveItems()
        {
            StringCollection apps = new StringCollection();
            foreach (string app in listBox1.Items)
            {
                if (app.Contains("\\"))
                {
                    apps.Add(app);
                }
            }
            StringCollection appLocation = new StringCollection();
            Properties.Settings.Default.BlockedApplications = apps;
            Properties.Settings.Default.Save();
        }
        private void SavePassBlockItems()
        {
            StringCollection apps = new StringCollection();

            foreach (string app in listBox1.Items)
            {
                if (!app.Contains("\\"))
                {
                    apps.Add(app);
                }
            }
            Properties.Settings.Default.PasswordBlockedApps = apps;
            Properties.Settings.Default.Save();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.ShowDialog();
        }

        private void aboutApplicationBlockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ShowAboutBox(this.Handle);
            }
            else
            {
                Dialog32.ShowAboutBox(this.Handle);
            }
        }
        private void checkForUpdatesAutomaticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkForUpdatesAutomaticallyToolStripMenuItem.Checked)
            {
                Properties.Settings.Default.isAutoUpdatesEnabled = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.isAutoUpdatesEnabled = false;
                Properties.Settings.Default.Save();
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string version;
                using (var client = new System.Net.WebClient())
                {
                    string downloaded = client.DownloadString("https://raw.githubusercontent.com/EmirAlpKocak/ApplicationBlocker/refs/heads/main/version.txt");
                    version = downloaded.Trim();
                    if (version != "2.3.0")
                    {
                        int result;
                        if (Environment.Is64BitOperatingSystem)
                        {
                            result = Dialog64.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                        }
                        else
                        {
                            result = Dialog32.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                        }
                        if (result == 1)
                        {
                            try
                            {
                                client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                if (Environment.Is64BitOperatingSystem)
                                {
                                    Dialog64.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                }
                                else
                                {
                                    Dialog32.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Dialog64.NoNewVersionMessage(this.Handle);
                        }
                        else
                        {
                            Dialog32.NoNewVersionMessage(this.Handle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                }
                else
                {
                    Dialog32.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                }
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            bool form2Open = false;
            FormCollection collection = Application.OpenForms;
            foreach (Form frm in collection)
            {
                if (frm.Name == "Form2")
                {
                    form2Open = true;
                }
            }
            if (!form2Open)
            {
                var tile = sender as Control;
                contextMenuStrip2.Show(tile, new Point(0, tile.Height));
            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            bool form2Open = false;
            FormCollection collection = Application.OpenForms;
            foreach (Form frm in collection)
            {
                if (frm.Name == "Form2")
                {
                    form2Open = true;
                }
            }
            if (!form2Open)
            {
                if (listBox1.SelectedItems.Count != 0)
                {
                    if (listBox1.SelectedItem.ToString().Contains("\\"))
                    {
                        try
                        {
                            string appFileName = Path.GetFileName(listBox1.SelectedItem.ToString());
                            string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + appFileName;
                            using (RegistryKey blockKey = Registry.LocalMachine.OpenSubKey(keyPath, true))
                            {
                                if (blockKey != null)
                                {
                                    blockKey.DeleteValue("Debugger", false);
                                }
                            }
                            FileSecurity blockFile = File.GetAccessControl(listBox1.SelectedItem.ToString());
                            FileSystemAccessRule rule = new FileSystemAccessRule(Environment.UserName, FileSystemRights.ExecuteFile | FileSystemRights.Write, AccessControlType.Deny);
                            blockFile.RemoveAccessRule(rule);
                            File.SetAccessControl(listBox1.SelectedItem.ToString(), blockFile);
                            string disallowPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\DisallowRun";
                            using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(disallowPath))
                            {
                                regKey.DeleteValue(appFileName, false);
                            }
                            listBox1.Items.Remove(listBox1.SelectedItem);
                            SaveItems();
                        }
                        catch (Exception ex)
                        {
                            if (Environment.Is64BitOperatingSystem)
                            {
                                Dialog64.ShowUnblockError(this.Handle, "Error: " + ex.Message);
                            }
                            else
                            {
                                Dialog32.ShowUnblockError(this.Handle, "Error: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        listBox1.Items.Remove(listBox1.SelectedItem);
                        SavePassBlockItems();
                    }
                    if (Properties.Settings.Default.PasswordBlockedApps != null && Properties.Settings.Default.PasswordBlockedApps.Count > 0)
                    {
                        InstallStartup();
                    }
                    else
                    {
                        UninstallStartup();
                    }
                }
                else
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.ShowSelectApp(this.Handle);
                    }
                    else
                    {
                        Dialog32.ShowSelectApp(this.Handle);
                    }
                }
            }
        }

        private void metroTile3_Click(object sender, EventArgs e)
        {
            bool form2Open = false;
            FormCollection collection = Application.OpenForms;
            foreach (Form frm in collection)
            {
                if (frm.Name == "Form2")
                {
                    form2Open = true;
                }
            }
            if (!form2Open)
            {
                var tile = sender as Control;
                contextMenuStrip1.Show(tile, new Point(0, tile.Height));
            }
        }

        private void unblockAnotherApplicationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + openFileDialog2.SafeFileName;
                    using (RegistryKey blockKey = Registry.LocalMachine.OpenSubKey(keyPath, true))
                    {
                        if (blockKey != null)
                        {
                            blockKey.DeleteValue("Debugger", false);
                        }
                    }
                    FileSecurity blockFile = File.GetAccessControl(openFileDialog2.FileName);
                    FileSystemAccessRule rule = new FileSystemAccessRule(Environment.UserName, FileSystemRights.ExecuteFile | FileSystemRights.Write, AccessControlType.Deny);
                    blockFile.RemoveAccessRule(rule);
                    File.SetAccessControl(openFileDialog2.FileName, blockFile);
                    string disallowPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\DisallowRun";
                    using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(disallowPath))
                    {
                        regKey.DeleteValue(openFileDialog2.SafeFileName, false);
                    }
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.SuccessfulUnblock(this.Handle, "Successfully unblocked " + openFileDialog2.FileName);
                    }
                    else
                    {
                        Dialog32.SuccessfulUnblock(this.Handle, "Successfully unblocked " + openFileDialog2.FileName);
                    }
                }
                catch (Exception ex)
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.ShowUnblockError(this.Handle, "Error: " + ex.Message);
                    }
                    else
                    {
                        Dialog32.ShowUnblockError(this.Handle, "Error: " + ex.Message);
                    }
                }
            }
        }
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Help.chm");
            }
            catch (Exception ex)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.ShowHelpError(this.Handle, "Error: " + ex.Message);
                }
                else
                {
                    Dialog32.ShowHelpError(this.Handle, "Error: " + ex.Message);
                }
            }
        }
        private void LoadColorSettings()
        {
            if (Properties.Settings.Default.Color == "Blue")
            {
                blueDefaultToolStripMenuItem.CheckOnClick = true;
                blueDefaultToolStripMenuItem.Checked = true;
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
changePasswordToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
changePasswordToolStripMenuItem.ForeColor = Color.White;
updateToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
updateToolStripMenuItem.ForeColor = Color.White;
checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
checkForUpdatesToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
aboutApplicationBlockerToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
helpToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
helpToolStripMenuItem.ForeColor = Color.White;
toolStripMenuItem1.BackColor = Color.FromArgb(0, 120, 215);
toolStripMenuItem1.ForeColor = Color.White;
blueDefaultToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
blueDefaultToolStripMenuItem.ForeColor = Color.White;
blackToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
blackToolStripMenuItem.ForeColor = Color.White;
silverToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
silverToolStripMenuItem.ForeColor = Color.White;
greenToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
greenToolStripMenuItem.ForeColor = Color.White;
limeToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
limeToolStripMenuItem.ForeColor = Color.White;
tealToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
tealToolStripMenuItem.ForeColor = Color.White;
orangeToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
orangeToolStripMenuItem.ForeColor = Color.White;
brownToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
brownToolStripMenuItem.ForeColor = Color.White;
pinkToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
pinkToolStripMenuItem.ForeColor = Color.White;
magentaToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
magentaToolStripMenuItem.ForeColor = Color.White;
purpleToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
purpleToolStripMenuItem.ForeColor = Color.White;
redToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
redToolStripMenuItem.ForeColor = Color.White;
yellowToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
yellowToolStripMenuItem.ForeColor = Color.White;
                standartBlockToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.FromArgb(0, 120, 215);
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Black")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Black;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Black;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.Black;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Black;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Black;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Black;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.Black;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.Black;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.Black;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.Black;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.Black;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.Black;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.Black;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.Black;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.Black;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.Black;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.Black;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.Black;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.Black;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.Black;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.Black;
                yellowToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.CheckOnClick = true;
                blackToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Black;
                metroTile1.Style = MetroFramework.MetroColorStyle.Black;
                metroTile2.Style = MetroFramework.MetroColorStyle.Black;
                metroTile3.Style = MetroFramework.MetroColorStyle.Black;
                this.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroTile1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroTile2.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroTile3.Theme = MetroFramework.MetroThemeStyle.Dark;
                listBox1.BackColor = Color.Black;
                listBox1.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Silver")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.DimGray;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.DimGray;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.DimGray;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.DimGray;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.DimGray;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.DimGray;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.DimGray;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.DimGray;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.DimGray;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.DimGray;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.DimGray;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.DimGray;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.DimGray;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.DimGray;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.DimGray;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.DimGray;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.DimGray;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.DimGray;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.DimGray;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.DimGray;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.DimGray;
                yellowToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.CheckOnClick = true;
                silverToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Silver;
                metroTile1.Style = MetroFramework.MetroColorStyle.Silver;
                metroTile2.Style = MetroFramework.MetroColorStyle.Silver;
                metroTile3.Style = MetroFramework.MetroColorStyle.Silver;
                standartBlockToolStripMenuItem.BackColor = Color.DimGray;
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.DimGray;
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.DimGray;
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.DimGray;
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.DimGray;
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.DimGray;
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.DimGray;
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Green")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Green;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Green;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.Green;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Green;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Green;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Green;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.Green;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.Green;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.Green;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.Green;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.Green;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.Green;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.Green;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.Green;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.Green;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.Green;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.Green;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.Green;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.Green;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.Green;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.Green;
                yellowToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.CheckOnClick = true;
                greenToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Green;
                metroTile1.Style = MetroFramework.MetroColorStyle.Green;
                metroTile2.Style = MetroFramework.MetroColorStyle.Green;
                metroTile3.Style = MetroFramework.MetroColorStyle.Green;
            }
            else if (Properties.Settings.Default.Color == "Lime")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.OliveDrab;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.OliveDrab;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.OliveDrab;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.OliveDrab;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.OliveDrab;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.OliveDrab;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.OliveDrab;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.OliveDrab;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.OliveDrab;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.OliveDrab;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.OliveDrab;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.OliveDrab;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.OliveDrab;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.OliveDrab;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.OliveDrab;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.OliveDrab;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.OliveDrab;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.OliveDrab;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.OliveDrab;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.OliveDrab;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.OliveDrab;
                yellowToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.CheckOnClick = true;
                limeToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Lime;
                metroTile1.Style = MetroFramework.MetroColorStyle.Lime;
                metroTile2.Style = MetroFramework.MetroColorStyle.Lime;
                metroTile3.Style = MetroFramework.MetroColorStyle.Lime;
                standartBlockToolStripMenuItem.BackColor = Color.OliveDrab;
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.OliveDrab;
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.OliveDrab;
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.OliveDrab;
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.OliveDrab;
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.OliveDrab;
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.OliveDrab;
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Teal")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Teal;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Teal;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.Teal;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Teal;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Teal;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Teal;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.Teal;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.Teal;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.Teal;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.Teal;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.Teal;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.Teal;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.Teal;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.Teal;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.Teal;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.Teal;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.Teal;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.Teal;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.Teal;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.Teal;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.Teal;
                yellowToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.CheckOnClick = true;
                tealToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Teal;
                metroTile1.Style = MetroFramework.MetroColorStyle.Teal;
                metroTile2.Style = MetroFramework.MetroColorStyle.Teal;
                metroTile3.Style = MetroFramework.MetroColorStyle.Teal;
                standartBlockToolStripMenuItem.BackColor = Color.Teal;
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.Teal;
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.Teal;
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.Teal;
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.Teal;
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.Teal;
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.Teal;
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Orange")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.DarkOrange;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.DarkOrange;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.DarkOrange;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.DarkOrange;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.DarkOrange;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.DarkOrange;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.DarkOrange;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.DarkOrange;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.DarkOrange;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.DarkOrange;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.DarkOrange;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.DarkOrange;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.DarkOrange;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.DarkOrange;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.DarkOrange;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.DarkOrange;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.DarkOrange;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.DarkOrange;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.DarkOrange;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.DarkOrange;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.DarkOrange;
                yellowToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.CheckOnClick = true;
                orangeToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Orange;
                metroTile1.Style = MetroFramework.MetroColorStyle.Orange;
                metroTile2.Style = MetroFramework.MetroColorStyle.Orange;
                metroTile3.Style = MetroFramework.MetroColorStyle.Orange;
            }
            else if (Properties.Settings.Default.Color == "Pink")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.HotPink;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.HotPink;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.HotPink;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.HotPink;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.HotPink;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.HotPink;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.HotPink;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.HotPink;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.HotPink;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.HotPink;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.HotPink;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.HotPink;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.HotPink;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.HotPink;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.HotPink;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.HotPink;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.HotPink;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.HotPink;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.HotPink;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.HotPink;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.HotPink;
                yellowToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.CheckOnClick = true;
                pinkToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Pink;
                metroTile1.Style = MetroFramework.MetroColorStyle.Pink;
                metroTile2.Style = MetroFramework.MetroColorStyle.Pink;
                metroTile3.Style = MetroFramework.MetroColorStyle.Pink;
                standartBlockToolStripMenuItem.BackColor = Color.HotPink;
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.HotPink;
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.HotPink;
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.HotPink;
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.HotPink;
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.HotPink;
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.HotPink;
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Magenta")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.DeepPink;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.DeepPink;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.DeepPink;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.DeepPink;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.DeepPink;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.DeepPink;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.DeepPink;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.DeepPink;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.DeepPink;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.DeepPink;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.DeepPink;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.DeepPink;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.DeepPink;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.DeepPink;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.DeepPink;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.DeepPink;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.DeepPink;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.DeepPink;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.DeepPink;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.DeepPink;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.DeepPink;
                yellowToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.CheckOnClick = true;
                magentaToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Magenta;
                metroTile1.Style = MetroFramework.MetroColorStyle.Magenta;
                metroTile2.Style = MetroFramework.MetroColorStyle.Magenta;
                metroTile3.Style = MetroFramework.MetroColorStyle.Magenta;
                standartBlockToolStripMenuItem.BackColor = Color.DeepPink;
                standartBlockToolStripMenuItem.ForeColor = Color.White;
                passwordLockToolStripMenuItem.BackColor = Color.DeepPink;
                passwordLockToolStripMenuItem.ForeColor = Color.White;
                exitToolStripMenuItem.BackColor = Color.DeepPink;
                exitToolStripMenuItem.ForeColor = Color.White;
                showToolStripMenuItem.BackColor = Color.DeepPink;
                showToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem1.BackColor = Color.DeepPink;
                checkForUpdatesToolStripMenuItem1.ForeColor = Color.White;
                aboutToolStripMenuItem.BackColor = Color.DeepPink;
                aboutToolStripMenuItem.ForeColor = Color.White;
                contactSendFeedbackToolStripMenuItem.BackColor = Color.DeepPink;
                contactSendFeedbackToolStripMenuItem.ForeColor = Color.White;
            }
            else if (Properties.Settings.Default.Color == "Purple")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Purple;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Purple;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.Purple;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Purple;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Purple;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Purple;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.Purple;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.Purple;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.Purple;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.Purple;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.Purple;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.Purple;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.Purple;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.Purple;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.Purple;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.Purple;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.Purple;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.Purple;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.Purple;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.Purple;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.Purple;
                yellowToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.CheckOnClick = true;
                purpleToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Purple;
                metroTile1.Style = MetroFramework.MetroColorStyle.Purple;
                metroTile2.Style = MetroFramework.MetroColorStyle.Purple;
                metroTile3.Style = MetroFramework.MetroColorStyle.Purple;
            }
            if (Properties.Settings.Default.Color == "Red")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Red;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Red;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                updateToolStripMenuItem.BackColor = Color.Red;
                updateToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Red;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.White;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Red;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.White;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Red;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.White;
                helpToolStripMenuItem.BackColor = Color.Red;
                helpToolStripMenuItem.ForeColor = Color.White;
                toolStripMenuItem1.BackColor = Color.Red;
                toolStripMenuItem1.ForeColor = Color.White;
                blueDefaultToolStripMenuItem.BackColor = Color.Red;
                blueDefaultToolStripMenuItem.ForeColor = Color.White;
                blackToolStripMenuItem.BackColor = Color.Red;
                blackToolStripMenuItem.ForeColor = Color.White;
                silverToolStripMenuItem.BackColor = Color.Red;
                silverToolStripMenuItem.ForeColor = Color.White;
                greenToolStripMenuItem.BackColor = Color.Red;
                greenToolStripMenuItem.ForeColor = Color.White;
                limeToolStripMenuItem.BackColor = Color.Red;
                limeToolStripMenuItem.ForeColor = Color.White;
                tealToolStripMenuItem.BackColor = Color.Red;
                tealToolStripMenuItem.ForeColor = Color.White;
                orangeToolStripMenuItem.BackColor = Color.Red;
                orangeToolStripMenuItem.ForeColor = Color.White;
                brownToolStripMenuItem.BackColor = Color.Red;
                brownToolStripMenuItem.ForeColor = Color.White;
                pinkToolStripMenuItem.BackColor = Color.Red;
                pinkToolStripMenuItem.ForeColor = Color.White;
                magentaToolStripMenuItem.BackColor = Color.Red;
                magentaToolStripMenuItem.ForeColor = Color.White;
                purpleToolStripMenuItem.BackColor = Color.Red;
                purpleToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.BackColor = Color.Red;
                redToolStripMenuItem.ForeColor = Color.White;
                yellowToolStripMenuItem.BackColor = Color.Red;
                yellowToolStripMenuItem.ForeColor = Color.White;
                redToolStripMenuItem.CheckOnClick = true;
                redToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Red;
                metroTile1.Style = MetroFramework.MetroColorStyle.Red;
                metroTile2.Style = MetroFramework.MetroColorStyle.Red;
                metroTile3.Style = MetroFramework.MetroColorStyle.Red;
            }
            if (Properties.Settings.Default.Color == "Yellow")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Yellow;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.Black;
                changePasswordToolStripMenuItem.BackColor = Color.Yellow;
                changePasswordToolStripMenuItem.ForeColor = Color.Black;
                updateToolStripMenuItem.BackColor = Color.Yellow;
                updateToolStripMenuItem.ForeColor = Color.Black;
                checkForUpdatesAutomaticallyToolStripMenuItem.BackColor = Color.Yellow;
                checkForUpdatesAutomaticallyToolStripMenuItem.ForeColor = Color.Black;
                checkForUpdatesToolStripMenuItem.BackColor = Color.Yellow;
                checkForUpdatesToolStripMenuItem.ForeColor = Color.Black;
                aboutApplicationBlockerToolStripMenuItem.BackColor = Color.Yellow;
                aboutApplicationBlockerToolStripMenuItem.ForeColor = Color.Black;
                helpToolStripMenuItem.BackColor = Color.Yellow;
                helpToolStripMenuItem.ForeColor = Color.Black;
                toolStripMenuItem1.BackColor = Color.Yellow;
                toolStripMenuItem1.ForeColor = Color.Black;
                blueDefaultToolStripMenuItem.BackColor = Color.Yellow;
                blueDefaultToolStripMenuItem.ForeColor = Color.Black;
                blackToolStripMenuItem.BackColor = Color.Yellow;
                blackToolStripMenuItem.ForeColor = Color.Black;
                silverToolStripMenuItem.BackColor = Color.Yellow;
                silverToolStripMenuItem.ForeColor = Color.Black;
                greenToolStripMenuItem.BackColor = Color.Yellow;
                greenToolStripMenuItem.ForeColor = Color.Black;
                limeToolStripMenuItem.BackColor = Color.Yellow;
                limeToolStripMenuItem.ForeColor = Color.Black;
                tealToolStripMenuItem.BackColor = Color.Yellow;
                tealToolStripMenuItem.ForeColor = Color.Black;
                orangeToolStripMenuItem.BackColor = Color.Yellow;
                orangeToolStripMenuItem.ForeColor = Color.Black;
                brownToolStripMenuItem.BackColor = Color.Yellow;
                brownToolStripMenuItem.ForeColor = Color.Black;
                pinkToolStripMenuItem.BackColor = Color.Yellow;
                pinkToolStripMenuItem.ForeColor = Color.Black;
                magentaToolStripMenuItem.BackColor = Color.Yellow;
                magentaToolStripMenuItem.ForeColor = Color.Black;
                purpleToolStripMenuItem.BackColor = Color.Yellow;
                purpleToolStripMenuItem.ForeColor = Color.Black;
                redToolStripMenuItem.BackColor = Color.Yellow;
                redToolStripMenuItem.ForeColor = Color.Black;
                yellowToolStripMenuItem.BackColor = Color.Yellow;
                yellowToolStripMenuItem.ForeColor = Color.Black;
                yellowToolStripMenuItem.CheckOnClick = true;
                yellowToolStripMenuItem.Checked = true;
                this.Style = MetroFramework.MetroColorStyle.Yellow;
                metroTile1.Style = MetroFramework.MetroColorStyle.Yellow;
                metroTile2.Style = MetroFramework.MetroColorStyle.Yellow;
                metroTile3.Style = MetroFramework.MetroColorStyle.Yellow;
            }
        }

        private void blueDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Blue";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Black";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void silverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Silver";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Green";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void limeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Lime";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Teal";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Orange";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Brown";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Pink";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void magentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Magenta";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Purple";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Red";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Yellow";
            Properties.Settings.Default.Save();
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ColorChanged(this.Handle);
            }
            else
            {
                Dialog32.ColorChanged(this.Handle);
            }
            Application.Restart();
        }
        private void StartProcessMonitor()
        {
            try
            {
                WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace");
                startWatch = new ManagementEventWatcher(query);
                startWatch.EventArrived += new EventArrivedEventHandler(ProcessStarted);
                startWatch.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ProcessStarted(object sender, EventArrivedEventArgs e)
        {
            try
            {
                if (Properties.Settings.Default.PasswordBlockedApps != null)
                {
                    string processName = e.NewEvent["ProcessName"].ToString();
                    int pid = Convert.ToInt32(e.NewEvent["ProcessID"]);
                    string exeName = Path.GetFileNameWithoutExtension(processName);
                    if (handledPids.Contains(pid))
                    {
                        return;
                    }
                    foreach (string blockedApp in Properties.Settings.Default.PasswordBlockedApps)
                    {
                        string blocked = Path.GetFileNameWithoutExtension(blockedApp);
                        if (string.Equals(exeName, blocked, StringComparison.OrdinalIgnoreCase))
                        {
                            handledPids.Add(pid);
                            var process = Process.GetProcessById(pid);
                            SuspendProcess(pid);
                            notifyIcon1.BalloonTipTitle = "Application Blocker";
                            notifyIcon1.BalloonTipText = exeName + " is blocked. Please enter your password to continue.";
                            notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                            notifyIcon1.ShowBalloonTip(3000);
                            Form4 frm4 = new Form4(pid, this);
                            frm4.ShowDialog();

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                 if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.CriticalMonitorError(this.Handle, "A critical error has been occured and Application Blocker will exit. Please contact me if the problem continues. Error: " + ex.Message);
                    Environment.Exit(1);
                }
                 else
                {
                    Dialog32.CriticalMonitorError(this.Handle, "A critical error has been occured and Application Blocker will exit. Please contact me if the problem continues. Error: " + ex.Message);
                    Environment.Exit(1);
                }
            }
        }
        public void SuspendProcess(int pid)
        {
            IntPtr handle = ProcessActions.OpenProcess(ProcessActions.PROCESS_SUSPEND_RESUME | ProcessActions.PROCESS_QUERY_INFORMATION, false, pid);
            if (handle == IntPtr.Zero)
            {
                return;
            }

            ProcessActions.NtSuspendProcess(handle);
            ProcessActions.CloseHandle(handle);
        }
        public void ResumeProcess(int pid)
        {
            IntPtr handle = ProcessActions.OpenProcess(ProcessActions.PROCESS_SUSPEND_RESUME | ProcessActions.PROCESS_QUERY_INFORMATION, false, pid);
            if (handle == IntPtr.Zero)
            {
                return;
            }

            ProcessActions.NtResumeProcess(handle);
            ProcessActions.CloseHandle(handle);
        }

        private void passwordLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(openFileDialog1.SafeFileName);
                SavePassBlockItems();
                if (Properties.Settings.Default.PasswordBlockedApps != null && Properties.Settings.Default.PasswordBlockedApps.Count > 0)
                {
                    InstallStartup();
                }
                else
                {
                    UninstallStartup();
                }
            }
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form6 frm6 = new Form6();
            frm6.ShowDialog();
            if (frm6.passwordOk == true)
            {
                this.Show();
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
            }
        }
        private void Form1_FormClosing (object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.PasswordBlockedApps != null && Properties.Settings.Default.PasswordBlockedApps.Count > 0)
            {
                this.Hide();
                e.Cancel = true;
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 frm5 = new Form5();
            frm5.ShowDialog();
        }

        private void standartBlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + openFileDialog1.SafeFileName;
                    using (RegistryKey blockKey = Registry.LocalMachine.CreateSubKey(keyPath))
                    {
                        blockKey.SetValue("Debugger", "ntsd -c qd");
                    }
                    FileSecurity blockFile = File.GetAccessControl(openFileDialog1.FileName);
                    FileSystemAccessRule rule = new FileSystemAccessRule(Environment.UserName, FileSystemRights.ExecuteFile | FileSystemRights.Write, AccessControlType.Deny);
                    blockFile.AddAccessRule(rule);
                    File.SetAccessControl(openFileDialog1.FileName, blockFile);
                    string explorerPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer";
                    using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(explorerPath))
                    {
                        regKey.SetValue("DisallowRun", 1, RegistryValueKind.DWord);
                    }
                    string disallowPath = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\Explorer\\DisallowRun";
                    using (RegistryKey regKey = Registry.CurrentUser.CreateSubKey(disallowPath))
                    {
                        regKey.SetValue(openFileDialog1.SafeFileName, openFileDialog1.SafeFileName, RegistryValueKind.String);
                    }
                    listBox1.Items.Add(openFileDialog1.FileName);
                    SaveItems();
                }
                catch (Exception ex)
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.ShowBlockError(this.Handle, "Error: " + ex.Message);
                    }
                    else
                    {
                        Dialog32.ShowBlockError(this.Handle, "Error: " + ex.Message);
                    }
                }
                if (Properties.Settings.Default.PasswordBlockedApps != null && Properties.Settings.Default.PasswordBlockedApps.Count > 0)
                {
                    InstallStartup();
                }
                else
                {
                    UninstallStartup();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Environment.Is64BitOperatingSystem)
            {
                Dialog64.ShowAboutBox(this.Handle);
            }
            else
            {
                Dialog32.ShowAboutBox(this.Handle);
            }
        }

        private void checkForUpdatesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string version;
                using (var client = new System.Net.WebClient())
                {
                    string downloaded = client.DownloadString("https://raw.githubusercontent.com/EmirAlpKocak/ApplicationBlocker/refs/heads/main/version.txt");
                    version = downloaded.Trim();
                    if (version != "2.3.0")
                    {
                        int result;
                        if (Environment.Is64BitOperatingSystem)
                        {
                            result = Dialog64.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                        }
                        else
                        {
                            result = Dialog32.ShowUpdateMessage(this.Handle, "Would you like to download and install version " + version + " right now?");
                        }
                        if (result == 1)
                        {
                            try
                            {
                                client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                if (Environment.Is64BitOperatingSystem)
                                {
                                    Dialog64.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                }
                                else
                                {
                                    Dialog32.ShowUpdateError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Dialog64.NoNewVersionMessage(this.Handle);
                        }
                        else
                        {
                            Dialog32.NoNewVersionMessage(this.Handle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                }
                else
                {
                    Dialog32.ShowUpdateCheckError(this.Handle, "Please check your internet connection or your firewall settings. Error: " + ex.Message);
                }
            }
        }
        private void InstallStartup()
        {
            if (Environment.Is64BitOperatingSystem)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Arguments = "/create /xml \"" + Application.StartupPath + "\\Task64.xml\"" + " /tn \"Application Blocker\"";
                info.FileName = "schtasks.exe";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.Verb = "runas";
                Process.Start(info);
            }
            else
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Arguments = "/create /xml \"" + Application.StartupPath + "\\Task32.xml\"" + " /tn \"Application Blocker\"";
                info.FileName = "schtasks.exe";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                info.Verb = "runas";
                Process.Start(info);
            }
        }
        private void UninstallStartup()
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Arguments = "/delete /tn \"Application Blocker\" /f";
            info.FileName = "schtasks.exe";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.Verb = "runas";
            Process.Start(info);
        }

        private void contactSendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string webAddress = "https://forms.gle/FkmrRydVqSJyYry48";
            Process.Start(webAddress);
        }
    }
    public class Dialog64
    {
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern int ShowUpdateMessage(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowAboutBox(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUpdateError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void NoNewVersionMessage(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUpdateCheckError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowBlockError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUnblockError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowSelectApp(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowHelpError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void SuccessfulUnblock(IntPtr hwnd, string msg);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void ColorChanged(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordEmptyError(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void IncorrectPasswordError(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordWarning(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void CurrentPasswordIncorrect(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordChanged(IntPtr hwnd);
        [DllImport("TaskDlg64.dll", CharSet = CharSet.Unicode)]
        public static extern void CriticalMonitorError(IntPtr hwnd, string msg);
    }
    public class Dialog32
    {
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern int ShowUpdateMessage(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowAboutBox(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUpdateError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void NoNewVersionMessage(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUpdateCheckError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowBlockError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowUnblockError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowSelectApp(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ShowHelpError(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void SuccessfulUnblock(IntPtr hwnd, string msg);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void ColorChanged(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordEmptyError(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void IncorrectPasswordError(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordWarning(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void CurrentPasswordIncorrect(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void PasswordChanged(IntPtr hwnd);
        [DllImport("TaskDlg32.dll", CharSet = CharSet.Unicode)]
        public static extern void CriticalMonitorError(IntPtr hwnd, string msg);
    }
    public class ProcessActions
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtSuspendProcess(IntPtr processHandle);

        [DllImport("ntdll.dll", SetLastError = true)]
        public static extern int NtResumeProcess(IntPtr processHandle);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int access, bool inheritHandle, int processId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        public const int PROCESS_SUSPEND_RESUME = 0x0800;
        public const int PROCESS_QUERY_INFORMATION = 0x0400;
    }
}