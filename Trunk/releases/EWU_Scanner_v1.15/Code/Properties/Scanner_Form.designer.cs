using System.Drawing;
using System;
using System.Windows.Forms;

namespace Scanner_UI
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
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startScanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutEWUScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutEWUSDSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.percentage = new System.Windows.Forms.Label();
            this.path = new System.Windows.Forms.TextBox();
            this.scanDirectory = new System.Windows.Forms.Label();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pauseButton = new System.Windows.Forms.Button();
            this.scanButton = new System.Windows.Forms.Button();
            this.logo = new System.Windows.Forms.PictureBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(258, 219);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(398, 22);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 9;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.fileNameLabel.Location = new System.Drawing.Point(258, 162);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(398, 20);
            this.fileNameLabel.TabIndex = 8;
            //this.fileNameLabel.Text = "temp";
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.helpToolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(733, 24);
            this.menuStrip.TabIndex = 10;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startScanToolStripMenuItem1,
            this.pauseScanToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // startScanToolStripMenuItem1
            // 
            this.startScanToolStripMenuItem1.Name = "startScanToolStripMenuItem1";
            this.startScanToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.startScanToolStripMenuItem1.Text = "Start Scan";
            this.startScanToolStripMenuItem1.Click += new System.EventHandler(this.startScanToolStripMenuItem_Click);
            // 
            // pauseScanToolStripMenuItem
            // 
            this.pauseScanToolStripMenuItem.Enabled = false;
            this.pauseScanToolStripMenuItem.Name = "pauseScanToolStripMenuItem";
            this.pauseScanToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.pauseScanToolStripMenuItem.Text = "Pause Scan";
            this.pauseScanToolStripMenuItem.Click += new System.EventHandler(this.pauseScanToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem1,
            this.aboutEWUScannerToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // viewHelpToolStripMenuItem1
            // 
            this.viewHelpToolStripMenuItem1.Name = "viewHelpToolStripMenuItem1";
            this.viewHelpToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.viewHelpToolStripMenuItem1.Text = "View Help";
            // 
            // aboutEWUScannerToolStripMenuItem
            // 
            this.aboutEWUScannerToolStripMenuItem.Name = "aboutEWUScannerToolStripMenuItem";
            this.aboutEWUScannerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutEWUScannerToolStripMenuItem.Text = "About EWU Scanner";
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
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
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
            // percentage
            // 
            this.percentage.AutoSize = true;
            this.percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.percentage.Location = new System.Drawing.Point(674, 219);
            this.percentage.Name = "percentage";
            this.percentage.Size = new System.Drawing.Size(38, 20);
            this.percentage.TabIndex = 6;
            this.percentage.Text = "0 %";
            // 
            // path
            // 
            this.path.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.path.Location = new System.Drawing.Point(409, 27);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(312, 26);
            this.path.TabIndex = 1;
            this.path.Text = "Testbed";
            // 
            // scanDirectory
            // 
            this.scanDirectory.AutoSize = true;
            this.scanDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.scanDirectory.Location = new System.Drawing.Point(258, 30);
            this.scanDirectory.Name = "scanDirectory";
            this.scanDirectory.Size = new System.Drawing.Size(145, 20);
            this.scanDirectory.TabIndex = 5;
            this.scanDirectory.Text = "Directory to Scan:";
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 275);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(709, 254);
            this.dataGridView.TabIndex = 4;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.pauseButton.Image = global::Scanner_UI.Properties.Resources.pause;
            this.pauseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pauseButton.Location = new System.Drawing.Point(515, 75);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(206, 45);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "     Pause Scan";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // scanButton
            // 
            this.scanButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.scanButton.Image = global::Scanner_UI.Properties.Resources.start;
            this.scanButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.scanButton.Location = new System.Drawing.Point(262, 75);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(205, 45);
            this.scanButton.TabIndex = 2;
            this.scanButton.Text = "     Start Scan";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // logo
            // 
            this.logo.Image = global::Scanner_UI.Properties.Resources.eagle_tr;
            this.logo.Location = new System.Drawing.Point(12, 27);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(240, 227);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.logo.TabIndex = 7;
            this.logo.TabStop = false;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // Scanner_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(733, 541);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.scanDirectory);
            this.Controls.Add(this.path);
            this.Controls.Add(this.percentage);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Scanner_Form";
            this.Text = "EWU SSN Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_Closing);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem startScanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutEWUSDSToolStripMenuItem;
        private System.Windows.Forms.PictureBox logo;
        public Label percentage;
        public TextBox path;
        public Label scanDirectory;
        public DataGridView dataGridView;
        private Button scanButton;
        private Button pauseButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private ToolStripMenuItem fileToolStripMenuItem1;
        private ToolStripMenuItem startScanToolStripMenuItem1;
        private ToolStripMenuItem pauseScanToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem viewHelpToolStripMenuItem1;
        private ToolStripMenuItem aboutEWUScannerToolStripMenuItem;
    }
}