﻿namespace EWUScanner
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblMode = new System.Windows.Forms.Label();
            this.picBoxEWULogo = new System.Windows.Forms.PictureBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excludedPathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administratorModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ssnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ccToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileTraversalWorker = new System.ComponentModel.BackgroundWorker();
            this.elapsedTimer = new System.Windows.Forms.Timer(this.components);
            this.bodyPanel = new System.Windows.Forms.Panel();
            this.picBoxCheck = new System.Windows.Forms.PictureBox();
            this.lblElapsedMins = new System.Windows.Forms.Label();
            this.lblMins = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.btnFullScan = new System.Windows.Forms.Button();
            this.lblCurDir = new System.Windows.Forms.Label();
            this.lblCurDirDesc = new System.Windows.Forms.Label();
            this.lblItemsFound = new System.Windows.Forms.Label();
            this.lblItemsScanned = new System.Windows.Forms.Label();
            this.lblElapsedHours = new System.Windows.Forms.Label();
            this.lblStartTime = new System.Windows.Forms.Label();
            this.lblItemsFoundDesc = new System.Windows.Forms.Label();
            this.lblItemsScannedDesc = new System.Windows.Forms.Label();
            this.lblElapsedTimeDesc = new System.Windows.Forms.Label();
            this.lblStartTimeDesc = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.theProgressBar = new System.Windows.Forms.ProgressBar();
            this.btnPartialScan = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEWULogo)).BeginInit();
            this.mainMenu.SuspendLayout();
            this.bodyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCheck)).BeginInit();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.lblMode);
            this.headerPanel.Controls.Add(this.picBoxEWULogo);
            this.headerPanel.Location = new System.Drawing.Point(0, 31);
            this.headerPanel.Margin = new System.Windows.Forms.Padding(4);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(741, 153);
            this.headerPanel.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.ForeColor = System.Drawing.Color.Maroon;
            this.lblMode.Location = new System.Drawing.Point(500, 122);
            this.lblMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(203, 28);
            this.lblMode.TabIndex = 1;
            this.lblMode.Text = "Partial Scan Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // picBoxEWULogo
            // 
            this.picBoxEWULogo.Image = global::EWUScanner.Properties.Resources.newLogo;
            this.picBoxEWULogo.Location = new System.Drawing.Point(8, 2);
            this.picBoxEWULogo.Margin = new System.Windows.Forms.Padding(4);
            this.picBoxEWULogo.Name = "picBoxEWULogo";
            this.picBoxEWULogo.Size = new System.Drawing.Size(515, 153);
            this.picBoxEWULogo.TabIndex = 0;
            this.picBoxEWULogo.TabStop = false;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.modeToolStripMenuItem,
            this.scanToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.mainMenu.Size = new System.Drawing.Size(716, 31);
            this.mainMenu.TabIndex = 2;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excludedPathsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(47, 27);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // excludedPathsToolStripMenuItem
            // 
            this.excludedPathsToolStripMenuItem.Name = "excludedPathsToolStripMenuItem";
            this.excludedPathsToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.excludedPathsToolStripMenuItem.Text = "Excluded Paths";
            this.excludedPathsToolStripMenuItem.Click += new System.EventHandler(this.excludedPathsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(195, 28);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // modeToolStripMenuItem
            // 
            this.modeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administratorModeToolStripMenuItem,
            this.scanModeToolStripMenuItem});
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(66, 27);
            this.modeToolStripMenuItem.Text = "Mode";
            // 
            // administratorModeToolStripMenuItem
            // 
            this.administratorModeToolStripMenuItem.CheckOnClick = true;
            this.administratorModeToolStripMenuItem.Name = "administratorModeToolStripMenuItem";
            this.administratorModeToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.administratorModeToolStripMenuItem.Text = "Administrator Mode";
            this.administratorModeToolStripMenuItem.Click += new System.EventHandler(this.administratorModeToolStripMenuItem_Click);
            // 
            // scanModeToolStripMenuItem
            // 
            this.scanModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssnToolStripMenuItem,
            this.ccToolStripMenuItem});
            this.scanModeToolStripMenuItem.Name = "scanModeToolStripMenuItem";
            this.scanModeToolStripMenuItem.Size = new System.Drawing.Size(233, 28);
            this.scanModeToolStripMenuItem.Text = "Scan Modes";
            // 
            // ssnToolStripMenuItem
            // 
            this.ssnToolStripMenuItem.Checked = true;
            this.ssnToolStripMenuItem.CheckOnClick = true;
            this.ssnToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ssnToolStripMenuItem.Name = "ssnToolStripMenuItem";
            this.ssnToolStripMenuItem.Size = new System.Drawing.Size(264, 28);
            this.ssnToolStripMenuItem.Text = "Social Security Numbers";
            this.ssnToolStripMenuItem.Click += new System.EventHandler(this.ssnToolStripMenuItem_Click);
            // 
            // ccToolStripMenuItem
            // 
            this.ccToolStripMenuItem.CheckOnClick = true;
            this.ccToolStripMenuItem.Name = "ccToolStripMenuItem";
            this.ccToolStripMenuItem.Size = new System.Drawing.Size(264, 28);
            this.ccToolStripMenuItem.Text = "Credit Cards";
            this.ccToolStripMenuItem.Click += new System.EventHandler(this.ccToolStripMenuItem_Click);
            // 
            // scanToolStripMenuItem
            // 
            this.scanToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.pauseToolStripMenuItem,
            this.resumeToolStripMenuItem});
            this.scanToolStripMenuItem.Name = "scanToolStripMenuItem";
            this.scanToolStripMenuItem.Size = new System.Drawing.Size(58, 27);
            this.scanToolStripMenuItem.Text = "Scan";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(140, 28);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // pauseToolStripMenuItem
            // 
            this.pauseToolStripMenuItem.Enabled = false;
            this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
            this.pauseToolStripMenuItem.Size = new System.Drawing.Size(140, 28);
            this.pauseToolStripMenuItem.Text = "Pause";
            this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
            // 
            // resumeToolStripMenuItem
            // 
            this.resumeToolStripMenuItem.Enabled = false;
            this.resumeToolStripMenuItem.Name = "resumeToolStripMenuItem";
            this.resumeToolStripMenuItem.Size = new System.Drawing.Size(140, 28);
            this.resumeToolStripMenuItem.Text = "Resume";
            this.resumeToolStripMenuItem.Click += new System.EventHandler(this.resumeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(57, 27);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(127, 28);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // fileTraversalWorker
            // 
            this.fileTraversalWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.fileTraversalWorker_DoWork);
            // 
            // elapsedTimer
            // 
            this.elapsedTimer.Interval = 60000;
            this.elapsedTimer.Tick += new System.EventHandler(this.elapsedTimer_Tick);
            // 
            // bodyPanel
            // 
            this.bodyPanel.BackgroundImage = global::EWUScanner.Properties.Resources.bg;
            this.bodyPanel.Controls.Add(this.picBoxCheck);
            this.bodyPanel.Controls.Add(this.lblElapsedMins);
            this.bodyPanel.Controls.Add(this.lblMins);
            this.bodyPanel.Controls.Add(this.lblHours);
            this.bodyPanel.Controls.Add(this.btnFullScan);
            this.bodyPanel.Controls.Add(this.lblCurDir);
            this.bodyPanel.Controls.Add(this.lblCurDirDesc);
            this.bodyPanel.Controls.Add(this.lblItemsFound);
            this.bodyPanel.Controls.Add(this.lblItemsScanned);
            this.bodyPanel.Controls.Add(this.lblElapsedHours);
            this.bodyPanel.Controls.Add(this.lblStartTime);
            this.bodyPanel.Controls.Add(this.lblItemsFoundDesc);
            this.bodyPanel.Controls.Add(this.lblItemsScannedDesc);
            this.bodyPanel.Controls.Add(this.lblElapsedTimeDesc);
            this.bodyPanel.Controls.Add(this.lblStartTimeDesc);
            this.bodyPanel.Controls.Add(this.lblPercentage);
            this.bodyPanel.Controls.Add(this.theProgressBar);
            this.bodyPanel.Controls.Add(this.btnPartialScan);
            this.bodyPanel.Location = new System.Drawing.Point(-1, 185);
            this.bodyPanel.Margin = new System.Windows.Forms.Padding(4);
            this.bodyPanel.Name = "bodyPanel";
            this.bodyPanel.Size = new System.Drawing.Size(743, 249);
            this.bodyPanel.TabIndex = 1;
            // 
            // picBoxCheck
            // 
            this.picBoxCheck.BackColor = System.Drawing.Color.Transparent;
            this.picBoxCheck.Image = global::EWUScanner.Properties.Resources.check;
            this.picBoxCheck.Location = new System.Drawing.Point(664, 172);
            this.picBoxCheck.Margin = new System.Windows.Forms.Padding(4);
            this.picBoxCheck.Name = "picBoxCheck";
            this.picBoxCheck.Size = new System.Drawing.Size(21, 20);
            this.picBoxCheck.TabIndex = 17;
            this.picBoxCheck.TabStop = false;
            this.picBoxCheck.Visible = false;
            // 
            // lblElapsedMins
            // 
            this.lblElapsedMins.AutoSize = true;
            this.lblElapsedMins.BackColor = System.Drawing.Color.Transparent;
            this.lblElapsedMins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedMins.ForeColor = System.Drawing.Color.White;
            this.lblElapsedMins.Location = new System.Drawing.Point(577, 58);
            this.lblElapsedMins.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedMins.Name = "lblElapsedMins";
            this.lblElapsedMins.Size = new System.Drawing.Size(17, 17);
            this.lblElapsedMins.TabIndex = 16;
            this.lblElapsedMins.Text = "0";
            // 
            // lblMins
            // 
            this.lblMins.AutoSize = true;
            this.lblMins.BackColor = System.Drawing.Color.Transparent;
            this.lblMins.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMins.ForeColor = System.Drawing.Color.White;
            this.lblMins.Location = new System.Drawing.Point(597, 57);
            this.lblMins.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMins.Name = "lblMins";
            this.lblMins.Size = new System.Drawing.Size(74, 17);
            this.lblMins.TabIndex = 15;
            this.lblMins.Text = "minutes(s)";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.BackColor = System.Drawing.Color.Transparent;
            this.lblHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHours.ForeColor = System.Drawing.Color.White;
            this.lblHours.Location = new System.Drawing.Point(513, 57);
            this.lblHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(61, 17);
            this.lblHours.TabIndex = 14;
            this.lblHours.Text = "hours(s)";
            // 
            // btnFullScan
            // 
            this.btnFullScan.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFullScan.Enabled = false;
            this.btnFullScan.Image = global::EWUScanner.Properties.Resources.fullV3;
            this.btnFullScan.Location = new System.Drawing.Point(16, 15);
            this.btnFullScan.Margin = new System.Windows.Forms.Padding(4);
            this.btnFullScan.Name = "btnFullScan";
            this.btnFullScan.Size = new System.Drawing.Size(311, 148);
            this.btnFullScan.TabIndex = 13;
            this.btnFullScan.UseVisualStyleBackColor = false;
            this.btnFullScan.Visible = false;
            this.btnFullScan.Click += new System.EventHandler(this.btnFullScan_Click);
            // 
            // lblCurDir
            // 
            this.lblCurDir.AccessibleName = "";
            this.lblCurDir.BackColor = System.Drawing.Color.Transparent;
            this.lblCurDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurDir.ForeColor = System.Drawing.Color.White;
            this.lblCurDir.Location = new System.Drawing.Point(133, 214);
            this.lblCurDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurDir.Name = "lblCurDir";
            this.lblCurDir.Size = new System.Drawing.Size(543, 16);
            this.lblCurDir.TabIndex = 12;
            // 
            // lblCurDirDesc
            // 
            this.lblCurDirDesc.AutoSize = true;
            this.lblCurDirDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblCurDirDesc.ForeColor = System.Drawing.Color.White;
            this.lblCurDirDesc.Location = new System.Drawing.Point(13, 214);
            this.lblCurDirDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurDirDesc.Name = "lblCurDirDesc";
            this.lblCurDirDesc.Size = new System.Drawing.Size(118, 17);
            this.lblCurDirDesc.TabIndex = 11;
            this.lblCurDirDesc.Text = "Current directory:";
            // 
            // lblItemsFound
            // 
            this.lblItemsFound.AutoSize = true;
            this.lblItemsFound.BackColor = System.Drawing.Color.Transparent;
            this.lblItemsFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsFound.ForeColor = System.Drawing.Color.White;
            this.lblItemsFound.Location = new System.Drawing.Point(493, 128);
            this.lblItemsFound.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemsFound.Name = "lblItemsFound";
            this.lblItemsFound.Size = new System.Drawing.Size(0, 17);
            this.lblItemsFound.TabIndex = 10;
            // 
            // lblItemsScanned
            // 
            this.lblItemsScanned.AutoSize = true;
            this.lblItemsScanned.BackColor = System.Drawing.Color.Transparent;
            this.lblItemsScanned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemsScanned.ForeColor = System.Drawing.Color.White;
            this.lblItemsScanned.Location = new System.Drawing.Point(493, 92);
            this.lblItemsScanned.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemsScanned.Name = "lblItemsScanned";
            this.lblItemsScanned.Size = new System.Drawing.Size(0, 17);
            this.lblItemsScanned.TabIndex = 9;
            // 
            // lblElapsedHours
            // 
            this.lblElapsedHours.AutoSize = true;
            this.lblElapsedHours.BackColor = System.Drawing.Color.Transparent;
            this.lblElapsedHours.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblElapsedHours.ForeColor = System.Drawing.Color.White;
            this.lblElapsedHours.Location = new System.Drawing.Point(493, 58);
            this.lblElapsedHours.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedHours.Name = "lblElapsedHours";
            this.lblElapsedHours.Size = new System.Drawing.Size(17, 17);
            this.lblElapsedHours.TabIndex = 8;
            this.lblElapsedHours.Text = "0";
            // 
            // lblStartTime
            // 
            this.lblStartTime.AutoSize = true;
            this.lblStartTime.BackColor = System.Drawing.Color.Transparent;
            this.lblStartTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartTime.ForeColor = System.Drawing.Color.White;
            this.lblStartTime.Location = new System.Drawing.Point(493, 21);
            this.lblStartTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartTime.Name = "lblStartTime";
            this.lblStartTime.Size = new System.Drawing.Size(72, 17);
            this.lblStartTime.TabIndex = 7;
            this.lblStartTime.Text = "00:00:00";
            // 
            // lblItemsFoundDesc
            // 
            this.lblItemsFoundDesc.AutoSize = true;
            this.lblItemsFoundDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblItemsFoundDesc.ForeColor = System.Drawing.Color.White;
            this.lblItemsFoundDesc.Location = new System.Drawing.Point(371, 128);
            this.lblItemsFoundDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemsFoundDesc.Name = "lblItemsFoundDesc";
            this.lblItemsFoundDesc.Size = new System.Drawing.Size(85, 17);
            this.lblItemsFoundDesc.TabIndex = 6;
            this.lblItemsFoundDesc.Text = "Items found:";
            // 
            // lblItemsScannedDesc
            // 
            this.lblItemsScannedDesc.AutoSize = true;
            this.lblItemsScannedDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblItemsScannedDesc.ForeColor = System.Drawing.Color.White;
            this.lblItemsScannedDesc.Location = new System.Drawing.Point(371, 92);
            this.lblItemsScannedDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemsScannedDesc.Name = "lblItemsScannedDesc";
            this.lblItemsScannedDesc.Size = new System.Drawing.Size(103, 17);
            this.lblItemsScannedDesc.TabIndex = 5;
            this.lblItemsScannedDesc.Text = "Items scanned:";
            // 
            // lblElapsedTimeDesc
            // 
            this.lblElapsedTimeDesc.AutoSize = true;
            this.lblElapsedTimeDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblElapsedTimeDesc.ForeColor = System.Drawing.Color.White;
            this.lblElapsedTimeDesc.Location = new System.Drawing.Point(371, 58);
            this.lblElapsedTimeDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblElapsedTimeDesc.Name = "lblElapsedTimeDesc";
            this.lblElapsedTimeDesc.Size = new System.Drawing.Size(97, 17);
            this.lblElapsedTimeDesc.TabIndex = 4;
            this.lblElapsedTimeDesc.Text = "Elapsed time: ";
            // 
            // lblStartTimeDesc
            // 
            this.lblStartTimeDesc.AutoSize = true;
            this.lblStartTimeDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblStartTimeDesc.ForeColor = System.Drawing.Color.White;
            this.lblStartTimeDesc.Location = new System.Drawing.Point(371, 21);
            this.lblStartTimeDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStartTimeDesc.Name = "lblStartTimeDesc";
            this.lblStartTimeDesc.Size = new System.Drawing.Size(72, 17);
            this.lblStartTimeDesc.TabIndex = 3;
            this.lblStartTimeDesc.Text = "Start time:";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.ForeColor = System.Drawing.Color.White;
            this.lblPercentage.Location = new System.Drawing.Point(660, 177);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(30, 17);
            this.lblPercentage.TabIndex = 2;
            this.lblPercentage.Text = "0%";
            // 
            // theProgressBar
            // 
            this.theProgressBar.BackColor = System.Drawing.SystemColors.Control;
            this.theProgressBar.Location = new System.Drawing.Point(15, 172);
            this.theProgressBar.Margin = new System.Windows.Forms.Padding(4);
            this.theProgressBar.Name = "theProgressBar";
            this.theProgressBar.Size = new System.Drawing.Size(641, 28);
            this.theProgressBar.TabIndex = 1;
            // 
            // btnPartialScan
            // 
            this.btnPartialScan.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPartialScan.Image = global::EWUScanner.Properties.Resources.partialV2;
            this.btnPartialScan.Location = new System.Drawing.Point(16, 15);
            this.btnPartialScan.Margin = new System.Windows.Forms.Padding(4);
            this.btnPartialScan.Name = "btnPartialScan";
            this.btnPartialScan.Size = new System.Drawing.Size(311, 148);
            this.btnPartialScan.TabIndex = 0;
            this.btnPartialScan.UseVisualStyleBackColor = false;
            this.btnPartialScan.Click += new System.EventHandler(this.btnPartialScan_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(716, 415);
            this.Controls.Add(this.bodyPanel);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(734, 457);
            this.MinimumSize = new System.Drawing.Size(734, 457);
            this.Name = "MainForm";
            this.Text = "EagleSIS: Sensitive Information Scanner";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.headerPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxEWULogo)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.bodyPanel.ResumeLayout(false);
            this.bodyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCheck)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Panel bodyPanel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.PictureBox picBoxEWULogo;
        private System.Windows.Forms.Button btnFullScan;
        public System.Windows.Forms.Label lblCurDir;
        private System.Windows.Forms.Label lblCurDirDesc;
        public System.Windows.Forms.Label lblItemsFound;
        public System.Windows.Forms.Label lblItemsScanned;
        public System.Windows.Forms.Label lblElapsedHours;
        public System.Windows.Forms.Label lblStartTime;
        private System.Windows.Forms.Label lblItemsFoundDesc;
        private System.Windows.Forms.Label lblItemsScannedDesc;
        private System.Windows.Forms.Label lblElapsedTimeDesc;
        private System.Windows.Forms.Label lblStartTimeDesc;
        public System.Windows.Forms.Label lblPercentage;
        public System.Windows.Forms.ProgressBar theProgressBar;
        private System.Windows.Forms.Button btnPartialScan;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resumeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lblMode;
        private System.ComponentModel.BackgroundWorker fileTraversalWorker;
        public System.Windows.Forms.Timer elapsedTimer;
        public System.Windows.Forms.Label lblElapsedMins;
        public System.Windows.Forms.Label lblMins;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administratorModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scanModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ssnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ccToolStripMenuItem;
        private System.Windows.Forms.PictureBox picBoxCheck;
        private System.Windows.Forms.ToolStripMenuItem excludedPathsToolStripMenuItem;
    }
}

