
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.metroTile1 = new MetroFramework.Controls.MetroTile();
            this.metroTile2 = new MetroFramework.Controls.MetroTile();
            this.metroTile3 = new MetroFramework.Controls.MetroTile();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unblockAnotherApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upgradeSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesAutomaticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutApplicationBlockerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.Location = new System.Drawing.Point(23, 120);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(443, 160);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "C:\\Program Files";
            this.openFileDialog1.Title = "Block Application";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "Applications (*.exe)|*.exe|All files (*.*)|*.*";
            this.openFileDialog2.InitialDirectory = "C:\\Program Files";
            this.openFileDialog2.Title = "Unblock Another Application";
            // 
            // metroTile1
            // 
            this.metroTile1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.metroTile1.Location = new System.Drawing.Point(23, 63);
            this.metroTile1.Name = "metroTile1";
            this.metroTile1.Size = new System.Drawing.Size(138, 51);
            this.metroTile1.TabIndex = 11;
            this.metroTile1.Text = "Block Application";
            this.metroTile1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroToolTip1.SetToolTip(this.metroTile1, "Select an application and block access to it on this computer.");
            this.metroTile1.Click += new System.EventHandler(this.metroTile1_Click);
            // 
            // metroTile2
            // 
            this.metroTile2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.metroTile2.Location = new System.Drawing.Point(180, 63);
            this.metroTile2.Name = "metroTile2";
            this.metroTile2.Size = new System.Drawing.Size(140, 51);
            this.metroTile2.TabIndex = 12;
            this.metroTile2.Text = "Unblock Application";
            this.metroTile2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroToolTip1.SetToolTip(this.metroTile2, "Select a blocked application from the list and unblock it.");
            this.metroTile2.Click += new System.EventHandler(this.metroTile2_Click);
            // 
            // metroTile3
            // 
            this.metroTile3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.metroTile3.Location = new System.Drawing.Point(337, 63);
            this.metroTile3.Name = "metroTile3";
            this.metroTile3.Size = new System.Drawing.Size(129, 51);
            this.metroTile3.TabIndex = 13;
            this.metroTile3.Text = "More Options";
            this.metroTile3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.metroToolTip1.SetToolTip(this.metroTile3, "Show more options.");
            this.metroTile3.Click += new System.EventHandler(this.metroTile3_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unblockAnotherApplicationToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.upgradeSettingsToolStripMenuItem,
            this.updateToolStripMenuItem,
            this.aboutApplicationBlockerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(229, 114);
            // 
            // unblockAnotherApplicationToolStripMenuItem
            // 
            this.unblockAnotherApplicationToolStripMenuItem.Name = "unblockAnotherApplicationToolStripMenuItem";
            this.unblockAnotherApplicationToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.unblockAnotherApplicationToolStripMenuItem.Text = "Unblock Another Application";
            this.unblockAnotherApplicationToolStripMenuItem.ToolTipText = "Unblock an application that isn\'t listed.";
            this.unblockAnotherApplicationToolStripMenuItem.Click += new System.EventHandler(this.unblockAnotherApplicationToolStripMenuItem_Click_1);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.changePasswordToolStripMenuItem.Text = "Change Password";
            this.changePasswordToolStripMenuItem.ToolTipText = "Change application password.";
            // 
            // upgradeSettingsToolStripMenuItem
            // 
            this.upgradeSettingsToolStripMenuItem.Name = "upgradeSettingsToolStripMenuItem";
            this.upgradeSettingsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.upgradeSettingsToolStripMenuItem.Text = "Upgrade Settings";
            this.upgradeSettingsToolStripMenuItem.ToolTipText = "Upgrade settings to new version.";
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
            this.checkForUpdatesAutomaticallyToolStripMenuItem.ToolTipText = "Automatically check for updates when program launches.";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.ToolTipText = "Check for latest updates.";
            // 
            // aboutApplicationBlockerToolStripMenuItem
            // 
            this.aboutApplicationBlockerToolStripMenuItem.Name = "aboutApplicationBlockerToolStripMenuItem";
            this.aboutApplicationBlockerToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.aboutApplicationBlockerToolStripMenuItem.Text = "About Application Blocker";
            this.aboutApplicationBlockerToolStripMenuItem.ToolTipText = "Show about page.";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 294);
            this.Controls.Add(this.metroTile3);
            this.Controls.Add(this.metroTile2);
            this.Controls.Add(this.metroTile1);
            this.Controls.Add(this.listBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Application Blocker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private MetroFramework.Controls.MetroTile metroTile1;
        private MetroFramework.Controls.MetroTile metroTile2;
        private MetroFramework.Controls.MetroTile metroTile3;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem changePasswordToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem upgradeSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesAutomaticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutApplicationBlockerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unblockAnotherApplicationToolStripMenuItem;
    }
}

