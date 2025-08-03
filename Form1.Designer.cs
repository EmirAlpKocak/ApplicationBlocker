
namespace Application_Blocker
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.blockApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unblockApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unblockAnotherApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upgradeSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutApplicationBlockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesAutomaticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(12, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(297, 147);
            this.listBox1.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog1.Title = "Block Application";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.blockApplicationToolStripMenuItem,
            this.unblockApplicationToolStripMenuItem,
            this.otherToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(316, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // blockApplicationToolStripMenuItem
            // 
            this.blockApplicationToolStripMenuItem.Name = "blockApplicationToolStripMenuItem";
            this.blockApplicationToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.blockApplicationToolStripMenuItem.Text = "Block Application";
            this.blockApplicationToolStripMenuItem.Click += new System.EventHandler(this.blockApplicationToolStripMenuItem_Click);
            // 
            // unblockApplicationToolStripMenuItem
            // 
            this.unblockApplicationToolStripMenuItem.Name = "unblockApplicationToolStripMenuItem";
            this.unblockApplicationToolStripMenuItem.Size = new System.Drawing.Size(127, 20);
            this.unblockApplicationToolStripMenuItem.Text = "Unblock Application";
            this.unblockApplicationToolStripMenuItem.Click += new System.EventHandler(this.unblockApplicationToolStripMenuItem_Click);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unblockAnotherApplicationToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.upgradeSettingsToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.aboutApplicationBlockerToolStripMenuItem});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.otherToolStripMenuItem.Text = "Other";
            // 
            // unblockAnotherApplicationToolStripMenuItem
            // 
            this.unblockAnotherApplicationToolStripMenuItem.Name = "unblockAnotherApplicationToolStripMenuItem";
            this.unblockAnotherApplicationToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.unblockAnotherApplicationToolStripMenuItem.Text = "Unblock Another Application";
            this.unblockAnotherApplicationToolStripMenuItem.Click += new System.EventHandler(this.unblockAnotherApplicationToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // upgradeSettingsToolStripMenuItem
            // 
            this.upgradeSettingsToolStripMenuItem.Name = "upgradeSettingsToolStripMenuItem";
            this.upgradeSettingsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.upgradeSettingsToolStripMenuItem.Text = "Upgrade Settings";
            this.upgradeSettingsToolStripMenuItem.Click += new System.EventHandler(this.upgradeSettingsToolStripMenuItem_Click);
            // 
            // aboutApplicationBlockerToolStripMenuItem
            // 
            this.aboutApplicationBlockerToolStripMenuItem.Name = "aboutApplicationBlockerToolStripMenuItem";
            this.aboutApplicationBlockerToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.aboutApplicationBlockerToolStripMenuItem.Text = "About Application Blocker";
            this.aboutApplicationBlockerToolStripMenuItem.Click += new System.EventHandler(this.aboutApplicationBlockerToolStripMenuItem_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog2.Title = "Unblock Another Application";
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesAutomaticallyToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem});
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.updateToolStripMenuItem.Text = "Update";
            // 
            // checkForUpdatesAutomaticallyToolStripMenuItem
            // 
            this.checkForUpdatesAutomaticallyToolStripMenuItem.Checked = true;
            this.checkForUpdatesAutomaticallyToolStripMenuItem.CheckOnClick = true;
            this.checkForUpdatesAutomaticallyToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkForUpdatesAutomaticallyToolStripMenuItem.Name = "checkForUpdatesAutomaticallyToolStripMenuItem";
            this.checkForUpdatesAutomaticallyToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.checkForUpdatesAutomaticallyToolStripMenuItem.Text = "Check for Updates Automatically";
            this.checkForUpdatesAutomaticallyToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesAutomaticallyToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 188);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Blocker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem blockApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unblockApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unblockAnotherApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutApplicationBlockerToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.ToolStripMenuItem upgradeSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesAutomaticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
    }
}

