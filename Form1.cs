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
            Properties.Settings.Default.BlockedApplications = apps;
            Properties.Settings.Default.Save();
        }

        private void blockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(openFileDialog1.FileName);
                string keyPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\" + openFileDialog1.SafeFileName;
                using (RegistryKey blockKey = Registry.LocalMachine.CreateSubKey(keyPath))
                {
                    blockKey.SetValue("Debugger", "ntsd -c qd");
                }
                FileSecurity blockFile = File.GetAccessControl(openFileDialog1.FileName);
                FileSystemAccessRule rule = new FileSystemAccessRule(Environment.UserName, FileSystemRights.ExecuteFile | FileSystemRights.Write, AccessControlType.Deny);
                blockFile.AddAccessRule(rule);
                File.SetAccessControl(openFileDialog1.FileName, blockFile);
                SaveItems();
            }
        }

        private void unblockApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
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
                listBox1.Items.Remove(listBox1.SelectedItem);
                SaveItems();
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
            MessageBox.Show("Application Blocker 1.5 - Coded By: Emir Alp Koçak", "About");
        }
    }
}
