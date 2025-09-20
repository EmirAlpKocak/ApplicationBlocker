using System;
using System.Collections.Specialized;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.IO;
using Microsoft.Win32;
using System.Drawing;

namespace Application_Blocker
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form2 frm2 = new Form2();
        public Form1()
        {
            InitializeComponent();
            /*this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;*/
            changePasswordToolStripMenuItem.Click += changePasswordToolStripMenuItem_Click;
            upgradeSettingsToolStripMenuItem.Click += upgradeSettingsToolStripMenuItem_Click;
            checkForUpdatesToolStripMenuItem.Click += checkForUpdatesToolStripMenuItem_Click;
            checkForUpdatesAutomaticallyToolStripMenuItem.Click += checkForUpdatesAutomaticallyToolStripMenuItem_Click;
            aboutApplicationBlockerToolStripMenuItem.Click += aboutApplicationBlockerToolStripMenuItem_Click;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadColorSettings();
            this.Resizable = false;
            /*listBox1.BackColor = Color.White;
            listBox1.ForeColor = Color.Black;*/
            listBox1.Font = new Font("Segoe UI", 10);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            if (Properties.Settings.Default.isUpgradedMessageShown == false)
            {
                if (MessageBox.Show("If you have upgraded to a new version of Application Blocker, your settings should be upgraded as well to avoid losing the list of blocked applications. If available, would you like to upgrade settings now? To upgrade later, go to More Options -> Upgrade Settings. If you haven't installed Application Blocker before, click No.", "Upgrade Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
            frm2.ShowDialog();
            LoadSavedItems();
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
                        if (version != "2.0.0")
                        {
                            if (MessageBox.Show("A new version is available. Would you like to download and install version " + version + " right now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                try
                                {
                                    client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                    System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                    Application.Exit();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unable to update Application Blocker. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
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
        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.ShowDialog();
        }

        private void aboutApplicationBlockerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Application Blocker v2.0.0 - Coded By: Emir Alp Koçak", "About");
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
                    if (version != "2.0.0")
                    {
                        if (MessageBox.Show("A new version is available. Would you like to download and install version " + version + " right now?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            try
                            {
                                client.DownloadFile("https://github.com/EmirAlpKocak/ApplicationBlocker/raw/refs/heads/main/Latest.msi", Path.GetTempPath() + "\\Setup.msi");
                                System.Diagnostics.Process.Start(Path.GetTempPath() + "\\Setup.msi");
                                Application.Exit();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Unable to update Application Blocker. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
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
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void metroTile1_Click(object sender, EventArgs e)
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

        private void metroTile2_Click(object sender, EventArgs e)
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

        private void metroTile3_Click(object sender, EventArgs e)
        {
            var tile = sender as Control;
            contextMenuStrip1.Show(tile, new Point(0, tile.Height));
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
                    MessageBox.Show("Successfully unblocked " + openFileDialog2.FileName, "Unblock Application", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to unblock application. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Unable to open help file. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
upgradeSettingsToolStripMenuItem.BackColor = Color.FromArgb(0, 120, 215);
upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Black")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Black;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Black;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.Black;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
                upgradeSettingsToolStripMenuItem.BackColor = Color.DimGray;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Green")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Green;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Green;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.Green;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
                upgradeSettingsToolStripMenuItem.BackColor = Color.OliveDrab;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Teal")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Teal;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Teal;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.Teal;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Orange")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.DarkOrange;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.DarkOrange;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.DarkOrange;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
                upgradeSettingsToolStripMenuItem.BackColor = Color.HotPink;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Magenta")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.DeepPink;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.DeepPink;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.DeepPink;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
            }
            else if (Properties.Settings.Default.Color == "Purple")
            {
                unblockAnotherApplicationToolStripMenuItem.BackColor = Color.Purple;
                unblockAnotherApplicationToolStripMenuItem.ForeColor = Color.White;
                changePasswordToolStripMenuItem.BackColor = Color.Purple;
                changePasswordToolStripMenuItem.ForeColor = Color.White;
                upgradeSettingsToolStripMenuItem.BackColor = Color.Purple;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
                upgradeSettingsToolStripMenuItem.BackColor = Color.Red;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.White;
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
                upgradeSettingsToolStripMenuItem.BackColor = Color.Yellow;
                upgradeSettingsToolStripMenuItem.ForeColor = Color.Black;
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
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Black";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void silverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Silver";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Green";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void limeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Lime";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void tealToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Teal";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Orange";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Brown";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Pink";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void magentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Color = "Magenta";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Purple";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Red";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This color theme is experimental and may not give the best experience.", "Experimental", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Properties.Settings.Default.Color = "Yellow";
            Properties.Settings.Default.Save();
            MessageBox.Show("Color has been changed. Please click OK to restart Application Blocker.", "Color", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Restart();
        }
    }
}
