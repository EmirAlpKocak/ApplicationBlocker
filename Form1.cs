using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.Win32;

namespace Application_Blocker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            if (Properties.Settings.Default.isUpgradedMessageShown == false)
            {
                if (MessageBox.Show("If you have upgraded to a new version of Application Blocker, your settings should be upgraded as well to avoid losing the list of blocked applications. If available, would you like to upgrade settings now? To upgrade later, go to Other -> Upgrade Settings. If you haven't installed Application Blocker before, click No.", "Upgrade Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Properties.Settings.Default.Upgrade();
                    Properties.Settings.Default.isUpgradedMessageShown = true;
                    Properties.Settings.Default.Save();
                }
                else
                {
                    Properties.Settings.Default.isUpgradedMessageShown = true;
                    Properties.Settings.Default.Save();
                }
            }
            if (Properties.Settings.Default.Password == "")
            {
            password:
                string password = Interaction.InputBox("Please set a password.", "Application Blocker");
                if (password == "")
                {
                    if (MessageBox.Show("You must set a password.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        goto password;
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    Properties.Settings.Default.Password = password;
                    Properties.Settings.Default.Save();
                }
            }
            password2:
            string currentPassword = Interaction.InputBox("Please enter your password.", "Application Blocker");
            if (currentPassword == Properties.Settings.Default.Password)
            {
                LoadSavedItems();
            }
            else
            {
                if (MessageBox.Show("Password is incorrect." , "Error" , MessageBoxButtons.RetryCancel , MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    goto password2;
                }
                else
                {
                    Application.Exit();
                }
            }
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
                        if (version != "1.6.0")
                        {
                            if (MessageBox.Show("A new version is available. Would you like to visit SourceForge page to download version " + version + "?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                string page = "https://sourceforge.net/projects/applicationblocker/";
                                System.Diagnostics.Process.Start(page);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to check for updates. Please check your internet connection or your firewall settings. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                checkForUpdatesAutomaticallyToolStripMenuItem.Checked = false;
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
        private void SaveItems()
        {
            StringCollection apps = new StringCollection();
            foreach (string app in listBox1.Items)
            {
                apps.Add(app);
            }
            StringCollection appLocation = new StringCollection();
            Properties.Settings.Default.BlockedApplications = apps;
            Properties.Settings.Default.Save();
        }

        private void blockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Unable to block application. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void unblockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
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
                    MessageBox.Show("Unable to unblock application. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an application from the list.", "Unblock Application");
            }
        }

        private void unblockAnotherApplicationToolStripMenuItem_Click(object sender, EventArgs e)
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
                    MessageBox.Show("Successfully unblocked " + openFileDialog2.FileName, "Unblock Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to unblock application. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            password3:
            string currentPassword = Interaction.InputBox("Please enter your current password.", "Change Password");
            if (currentPassword == Properties.Settings.Default.Password)
            {
                string newPassword = Interaction.InputBox("Please enter your new password.", "Change Password");
                if (newPassword == "")
                {
                    if (MessageBox.Show("Password cannot be empty.", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    {
                        goto password3;
                    }
                }
                else
                {
                    Properties.Settings.Default.Password = newPassword;
                    Properties.Settings.Default.Save();
                    MessageBox.Show("Password is changed.", "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (MessageBox.Show("Current password is incorrect.", "Change Password", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                {
                    goto password3;
                }
            }
        }

        private void aboutApplicationBlockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application Blocker v1.6.0 - Coded By: Emir Alp Koçak", "About");
        }

        private void upgradeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This option will upgrade old settings to this version. Use this if you have lost your list of blocked items after an update. Please note that your password will be reverted to the old version. Would you like to upgrade settings?", "Upgrade Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Properties.Settings.Default.Upgrade();
                Properties.Settings.Default.isUpgradedMessageShown = true;
                Properties.Settings.Default.Save();
                MessageBox.Show("Successfully upgraded settings. Click OK to restart Application Blocker.", "Upgrade Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
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
                    if (version != "1.6.0")
                    {
                        if (MessageBox.Show("A new version is available. Would you like to visit SourceForge page to download version " + version + "?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            string page = "https://sourceforge.net/projects/applicationblocker/";
                            System.Diagnostics.Process.Start(page);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No new versions available.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to check for updates. Please check your internet connection or your firewall settings. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
