using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Application_Blocker
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        private bool exitButton = true;
        public Form2()
        {
            InitializeComponent();
            LoadColorSettings();
            textBox1.KeyDown += new KeyEventHandler(TextBox1_KeyDown);
            this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Resizable = false;
            if (Properties.Settings.Default.Password != "")
            {
                label1.Text = "Please enter your password.";
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Properties.Settings.Default.Password == "")
                {
                    string password = textBox1.Text;
                    if (password == "")
                    {
                        if (Environment.Is64BitOperatingSystem)
                        {
                            Dialog64.PasswordEmptyError(this.Handle);
                        }
                        else
                        {
                            Dialog32.PasswordEmptyError(this.Handle);
                        }
                    }
                    else
                    {
                        string encryptedPassowrd = Encrypt(password);
                        Properties.Settings.Default.Password = encryptedPassowrd;
                        Properties.Settings.Default.Save();
                        textBox1.Clear();
                        label1.Text = "Please enter your password.";
                    }
                }
                else
                {
                    string currentPassword = textBox1.Text;
                    string encryptedStoredPassword = Properties.Settings.Default.Password;
                    string decryptedStoredPassword = Decrypt(encryptedStoredPassword);
                    if (currentPassword == decryptedStoredPassword)
                    {
                        textBox1.Clear();
                        exitButton = false;
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
        }

        private void metroTile2_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Environment.Exit(0);
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.Password == "")
            {
                string password = textBox1.Text;
                if (password == "")
                {
                    if (Environment.Is64BitOperatingSystem)
                    {
                        Dialog64.PasswordEmptyError(this.Handle);
                    }
                    else
                    {
                        Dialog32.PasswordEmptyError(this.Handle);
                    }
                }
                else
                {
                    string encryptedPassowrd = Encrypt(password);
                    Properties.Settings.Default.Password = encryptedPassowrd;
                    Properties.Settings.Default.Save();
                    textBox1.Clear();
                    label1.Text = "Please enter your password.";
                }
            }
            else
            {
                string currentPassword = textBox1.Text;
                string encryptedStoredPassword = Properties.Settings.Default.Password;
                string decryptedStoredPassword = Decrypt(encryptedStoredPassword);
                if (currentPassword == decryptedStoredPassword)
                {
                    textBox1.Clear();
                    exitButton = false;
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
        public string Encrypt(string plainPassword)
        {
            try
            {
                byte[] encrypted = ProtectedData.Protect(Encoding.UTF8.GetBytes(plainPassword), null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encrypted);
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
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exitButton == true)
            {
                Environment.Exit(0);
            }
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
