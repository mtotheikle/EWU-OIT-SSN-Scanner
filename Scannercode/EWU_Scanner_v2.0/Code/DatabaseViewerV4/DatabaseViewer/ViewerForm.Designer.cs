namespace DatabaseViewer
{
    partial class ViewerForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.theTabPanel = new System.Windows.Forms.TabControl();
            this.SocialFound = new System.Windows.Forms.TabPage();
            this.dataGridView_Social = new System.Windows.Forms.DataGridView();
            this.CreditFound = new System.Windows.Forms.TabPage();
            this.dataGridView_Credit = new System.Windows.Forms.DataGridView();
            this.NotScanned = new System.Windows.Forms.TabPage();
            this.dataGridView_NotScanned = new System.Windows.Forms.DataGridView();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openDBFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblDescription = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.theTabPanel.SuspendLayout();
            this.SocialFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Social)).BeginInit();
            this.CreditFound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Credit)).BeginInit();
            this.NotScanned.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_NotScanned)).BeginInit();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Maroon;
            this.panel1.Controls.Add(this.theTabPanel);
            this.panel1.Location = new System.Drawing.Point(-2, 148);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 589);
            this.panel1.TabIndex = 0;
            // 
            // theTabPanel
            // 
            this.theTabPanel.Controls.Add(this.SocialFound);
            this.theTabPanel.Controls.Add(this.CreditFound);
            this.theTabPanel.Controls.Add(this.NotScanned);
            this.theTabPanel.Location = new System.Drawing.Point(14, 14);
            this.theTabPanel.Name = "theTabPanel";
            this.theTabPanel.SelectedIndex = 0;
            this.theTabPanel.Size = new System.Drawing.Size(992, 560);
            this.theTabPanel.TabIndex = 0;
            // 
            // SocialFound
            // 
            this.SocialFound.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SocialFound.Controls.Add(this.dataGridView_Social);
            this.SocialFound.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SocialFound.Location = new System.Drawing.Point(4, 22);
            this.SocialFound.Name = "SocialFound";
            this.SocialFound.Padding = new System.Windows.Forms.Padding(3);
            this.SocialFound.Size = new System.Drawing.Size(984, 534);
            this.SocialFound.TabIndex = 0;
            this.SocialFound.Text = "Social Security Numbers Found";
            // 
            // dataGridView_Social
            // 
            this.dataGridView_Social.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_Social.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView_Social.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Social.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Social.Name = "dataGridView_Social";
            this.dataGridView_Social.Size = new System.Drawing.Size(988, 535);
            this.dataGridView_Social.TabIndex = 0;
            // 
            // CreditFound
            // 
            this.CreditFound.Controls.Add(this.dataGridView_Credit);
            this.CreditFound.Location = new System.Drawing.Point(4, 22);
            this.CreditFound.Name = "CreditFound";
            this.CreditFound.Size = new System.Drawing.Size(984, 534);
            this.CreditFound.TabIndex = 2;
            this.CreditFound.Text = "Credit Card Numbers Found";
            this.CreditFound.UseVisualStyleBackColor = true;
            // 
            // dataGridView_Credit
            // 
            this.dataGridView_Credit.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView_Credit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Credit.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dataGridView_Credit.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_Credit.Name = "dataGridView_Credit";
            this.dataGridView_Credit.Size = new System.Drawing.Size(988, 535);
            this.dataGridView_Credit.TabIndex = 0;
            // 
            // NotScanned
            // 
            this.NotScanned.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.NotScanned.Controls.Add(this.dataGridView_NotScanned);
            this.NotScanned.ForeColor = System.Drawing.SystemColors.ControlText;
            this.NotScanned.Location = new System.Drawing.Point(4, 22);
            this.NotScanned.Name = "NotScanned";
            this.NotScanned.Padding = new System.Windows.Forms.Padding(3);
            this.NotScanned.Size = new System.Drawing.Size(984, 534);
            this.NotScanned.TabIndex = 1;
            this.NotScanned.Text = "Files Not Scanned";
            // 
            // dataGridView_NotScanned
            // 
            this.dataGridView_NotScanned.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_NotScanned.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView_NotScanned.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_NotScanned.Location = new System.Drawing.Point(0, 0);
            this.dataGridView_NotScanned.Name = "dataGridView_NotScanned";
            this.dataGridView_NotScanned.Size = new System.Drawing.Size(988, 535);
            this.dataGridView_NotScanned.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1016, 24);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.loadToolStripMenuItem.Text = "Load...";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DatabaseViewer.Properties.Resources.longLogo;
            this.pictureBox1.Location = new System.Drawing.Point(6, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(793, 107);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // openDBFileDialog
            // 
            this.openDBFileDialog.Filter = "Scanner Database File|*.sdb";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.ForeColor = System.Drawing.Color.Maroon;
            this.lblDescription.Location = new System.Drawing.Point(847, 127);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(157, 13);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "EWUScanner Database Viewer";
            // 
            // ViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "ViewerForm";
            this.Text = "EWUScanner Database Viewer";
            this.panel1.ResumeLayout(false);
            this.theTabPanel.ResumeLayout(false);
            this.SocialFound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Social)).EndInit();
            this.CreditFound.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Credit)).EndInit();
            this.NotScanned.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_NotScanned)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl theTabPanel;
        private System.Windows.Forms.TabPage SocialFound;
        private System.Windows.Forms.TabPage NotScanned;
        private System.Windows.Forms.DataGridView dataGridView_Social;
        private System.Windows.Forms.DataGridView dataGridView_NotScanned;
        private System.Windows.Forms.OpenFileDialog openDBFileDialog;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TabPage CreditFound;
        private System.Windows.Forms.DataGridView dataGridView_Credit;
    }
}

