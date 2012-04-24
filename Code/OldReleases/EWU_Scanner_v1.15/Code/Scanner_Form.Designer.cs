using System.Drawing;
using System;
using System.Windows.Forms;
namespace Scan_UI
{
    partial class Scanner_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Scanner_Form));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutEWUSDSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanButton = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.percentage = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.scanDirectory = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(258, 219);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 22);
            this.progressBar.TabIndex = 0;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.fileNameLabel.Location = new System.Drawing.Point(281, 162);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(0, 20);
            this.fileNameLabel.TabIndex = 1;
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(733, 24);
            this.menuStrip.TabIndex = 3;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // startScanToolStripMenuItem
            // 
            this.startScanToolStripMenuItem.Name = "startScanToolStripMenuItem";
            this.startScanToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startScanToolStripMenuItem.Text = "Scan";
            this.startScanToolStripMenuItem.Click += new System.EventHandler(this.startScanToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.aboutEWUSDSToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            // 
            // aboutEWUSDSToolStripMenuItem
            // 
            this.aboutEWUSDSToolStripMenuItem.Name = "aboutEWUSDSToolStripMenuItem";
            this.aboutEWUSDSToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.aboutEWUSDSToolStripMenuItem.Text = "About EWU SDS";
            // 
            // scanButton
            // 
            this.scanButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.scanButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scanButton.Location = new System.Drawing.Point(285, 82);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(136, 45);
            this.scanButton.TabIndex = 4;
            this.scanButton.Text = "     Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // logo
            // 
            this.logo.Image = global::SevenZip.Properties.Resources.eagle;
            this.logo.Location = new System.Drawing.Point(12, 27);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(240, 227);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logo.TabIndex = 2;
            this.logo.TabStop = false;
            // 
            // percentage
            // 
            this.percentage.AutoSize = true;
            this.percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.percentage.Location = new System.Drawing.Point(672, 219);
            this.percentage.Name = "percentage";
            this.percentage.Size = new System.Drawing.Size(38, 20);
            this.percentage.TabIndex = 5;
            this.percentage.Text = "0 %";
            // 
            // path
            // 
            this.path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.path.Location = new System.Drawing.Point(430, 27);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(280, 26);
            this.path.TabIndex = 6;
            this.path.Text = "C:\\Users\\gsprint\\Documents\\CS Files\\CSCD 488\\test_cases";
            // 
            // scanDirectory
            // 
            this.scanDirectory.AutoSize = true;
            this.scanDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.scanDirectory.Location = new System.Drawing.Point(281, 30);
            this.scanDirectory.Name = "scanDirectory";
            this.scanDirectory.Size = new System.Drawing.Size(145, 20);
            this.scanDirectory.TabIndex = 7;
            this.scanDirectory.Text = "Directory to Scan:";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 275);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(709, 254);
            this.dataGridView.TabIndex = 8;
            // 
            // Scanner_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(733, 541);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.scanDirectory);
            this.Controls.Add(this.path);
            this.Controls.Add(this.percentage);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Scanner_Form";
            this.Text = "EWU Sensitive Data Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ProgressBar progressBar;
        public System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.ToolStripMenuItem startScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutEWUSDSToolStripMenuItem;
        private System.Windows.Forms.PictureBox logo;
        public Label percentage;
        public TextBox path;
        public Label scanDirectory;
        private DataGridView dataGridView;
    }
}