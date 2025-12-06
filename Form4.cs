using System;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Text;
using System.Drawing;

namespace Application_Blocker
{
    public partial class Form4 : MetroFramework.Forms.MetroForm
    {
        private int _pid;
        private Form1 _mainForm;
        private bool exitClose = true;
        public Form4(int pid, Form1 mainForm)
        {
            InitializeComponent();
            LoadColorSettings();
            textBox1.KeyDown += new KeyEventHandler(TextBox1_KeyDown);
            this.FormClosing += new FormClosingEventHandler(Form4_FormClosing);
            _pid = pid;
            _mainForm = mainForm;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Resizable = false;
        }
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string currentPassword = textBox1.Text;
                string encryptedStoredPassword = Properties.Settings.Default.Password;
                string decryptedStoredPassword = Decrypt(encryptedStoredPassword);
                if (currentPassword == decryptedStoredPassword)
                {
                    _mainForm.ResumeProcess(_pid);
                    _mainForm.handledPids.Remove(_pid);
                    exitClose = false;
                    this.Close();
                }
                else
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.IncorrectPasswordError(this.Handle);
                    }
                    else
                    {
                        Dialog32.IncorrectPasswordError(this.Handle);
                    }
                    textBox1.Clear();
                }
            }
        }
        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (exitClose == true)
                {
                    var process = Process.GetProcessById(_pid);
                    process.Kill();
                }
            }
            catch
            {

            }
        }
        public string Decrypt(string encryptedPassword)
        {
            try
            {
                byte[] decrypted = ProtectedData.Unprotect(Convert.FromBase64String(encryptedPassword), null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception)
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.PasswordWarning(this.Handle);
                }
                else
                {
                    Dialog32.PasswordWarning(this.Handle);
                }
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
                Application.Restart();
                return "";
            }
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            string currentPassword = textBox1.Text;
            string encryptedStoredPassword = Properties.Settings.Default.Password;
            string decryptedStoredPassword = Decrypt(encryptedStoredPassword);
            if (currentPassword == decryptedStoredPassword)
            {
                _mainForm.ResumeProcess(_pid);
                _mainForm.handledPids.Remove(_pid);
                exitClose = false;
                this.Close();
            }
            else
            {
                if (Environment.Is64BitOperatingSystem)
                {
                    Dialog64.IncorrectPasswordError(this.Handle);
                }
                else
                {
                    Dialog32.IncorrectPasswordError(this.Handle);
                }
                textBox1.Clear();
            }
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            var process = Process.GetProcessById(_pid);
            process.Kill();
            this.Close();
        }
        private void LoadColorSettings()
        {
            if (Properties.Settings.Default.Color == "Pink")
            {
                this.Style = MetroFramework.MetroColorStyle.Pink;
                metroTile1.Style = MetroFramework.MetroColorStyle.Pink;
                metroTile2.Style = MetroFramework.MetroColorStyle.Pink;
            }
            else if (Properties.Settings.Default.Color == "Magenta")
            {
                this.Style = MetroFramework.MetroColorStyle.Magenta;
                metroTile1.Style = MetroFramework.MetroColorStyle.Magenta;
                metroTile2.Style = MetroFramework.MetroColorStyle.Magenta;
            }
            else if (Properties.Settings.Default.Color == "Purple")
            {
                this.Style = MetroFramework.MetroColorStyle.Purple;
                metroTile1.Style = MetroFramework.MetroColorStyle.Purple;
                metroTile2.Style = MetroFramework.MetroColorStyle.Purple;
            }
            else if (Properties.Settings.Default.Color == "Red")
            {
                this.Style = MetroFramework.MetroColorStyle.Red;
                metroTile1.Style = MetroFramework.MetroColorStyle.Red;
                metroTile2.Style = MetroFramework.MetroColorStyle.Red;
            }
            else if (Properties.Settings.Default.Color == "Yellow")
            {
                this.Style = MetroFramework.MetroColorStyle.Yellow;
                metroTile1.Style = MetroFramework.MetroColorStyle.Yellow;
                metroTile2.Style = MetroFramework.MetroColorStyle.Yellow;
            }
            else if (Properties.Settings.Default.Color == "Silver")
            {
                this.Style = MetroFramework.MetroColorStyle.Silver;
                metroTile1.Style = MetroFramework.MetroColorStyle.Silver;
                metroTile2.Style = MetroFramework.MetroColorStyle.Silver;
            }
            else if (Properties.Settings.Default.Color == "Green")
            {
                this.Style = MetroFramework.MetroColorStyle.Green;
                metroTile1.Style = MetroFramework.MetroColorStyle.Green;
                metroTile2.Style = MetroFramework.MetroColorStyle.Green;
            }
            else if (Properties.Settings.Default.Color == "Lime")
            {
                this.Style = MetroFramework.MetroColorStyle.Lime;
                metroTile1.Style = MetroFramework.MetroColorStyle.Lime;
                metroTile2.Style = MetroFramework.MetroColorStyle.Lime;
            }
            else if (Properties.Settings.Default.Color == "Teal")
            {
                this.Style = MetroFramework.MetroColorStyle.Teal;
                metroTile1.Style = MetroFramework.MetroColorStyle.Teal;
                metroTile2.Style = MetroFramework.MetroColorStyle.Teal;
            }
            else if (Properties.Settings.Default.Color == "Orange")
            {
                this.Style = MetroFramework.MetroColorStyle.Orange;
                metroTile1.Style = MetroFramework.MetroColorStyle.Orange;
                metroTile2.Style = MetroFramework.MetroColorStyle.Orange;
            }
            else if (Properties.Settings.Default.Color == "Blue")
            {
                this.Style = MetroFramework.MetroColorStyle.Blue;
                metroTile1.Style = MetroFramework.MetroColorStyle.Blue;
                metroTile2.Style = MetroFramework.MetroColorStyle.Blue;
            }
            else if (Properties.Settings.Default.Color == "Black")
            {
                this.Style = MetroFramework.MetroColorStyle.Black;
                metroTile1.Style = MetroFramework.MetroColorStyle.Black;
                metroTile2.Style = MetroFramework.MetroColorStyle.Black;
                this.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroTile1.Theme = MetroFramework.MetroThemeStyle.Dark;
                metroTile2.Theme = MetroFramework.MetroThemeStyle.Dark;
                textBox1.ForeColor = Color.White;
                textBox1.BackColor = Color.Black;
                label1.ForeColor = Color.White;
                label1.BackColor = Color.Black;
            }
        }
    }
}
