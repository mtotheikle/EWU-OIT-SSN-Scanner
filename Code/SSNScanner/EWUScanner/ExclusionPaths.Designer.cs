namespace EWUScanner
{
    partial class ExclusionPaths
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExclusionPaths));
            this.AddPath = new System.Windows.Forms.Button();
            this.DoneWithAddingPaths = new System.Windows.Forms.Button();
            this.HelpExplanation = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ExcludedPaths = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // AddPath
            // 
            this.AddPath.BackColor = System.Drawing.SystemColors.Control;
            this.AddPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddPath.Location = new System.Drawing.Point(12, 461);
            this.AddPath.Name = "AddPath";
            this.AddPath.Size = new System.Drawing.Size(125, 85);
            this.AddPath.TabIndex = 1;
            this.AddPath.Text = "Add A Path";
            this.AddPath.UseVisualStyleBackColor = true;
            this.AddPath.Click += new System.EventHandler(this.AddPath_Click);
            // 
            // DoneWithAddingPaths
            // 
            this.DoneWithAddingPaths.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DoneWithAddingPaths.Location = new System.Drawing.Point(219, 461);
            this.DoneWithAddingPaths.Name = "DoneWithAddingPaths";
            this.DoneWithAddingPaths.Size = new System.Drawing.Size(125, 85);
            this.DoneWithAddingPaths.TabIndex = 2;
            this.DoneWithAddingPaths.Text = "Done Adding Paths";
            this.DoneWithAddingPaths.UseVisualStyleBackColor = true;
            this.DoneWithAddingPaths.Click += new System.EventHandler(this.DoneWithAddingPaths_Click);
            // 
            // HelpExplanation
            // 
            this.HelpExplanation.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HelpExplanation.Location = new System.Drawing.Point(421, 461);
            this.HelpExplanation.Name = "HelpExplanation";
            this.HelpExplanation.Size = new System.Drawing.Size(125, 85);
            this.HelpExplanation.TabIndex = 3;
            this.HelpExplanation.Text = "Help";
            this.HelpExplanation.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ExcludedPaths});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(582, 412);
            this.dataGridView1.TabIndex = 4;
            // 
            // ExcludedPaths
            // 
            this.ExcludedPaths.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ExcludedPaths.HeaderText = "Paths Excluded From Scan";
            this.ExcludedPaths.Name = "ExcludedPaths";
            // 
            // ExclusionPaths
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(582, 558);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.HelpExplanation);
            this.Controls.Add(this.DoneWithAddingPaths);
            this.Controls.Add(this.AddPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExclusionPaths";
            this.Text = "Excluded Paths";
            this.Shown += new System.EventHandler(this.ExclusionPaths_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AddPath;
        private System.Windows.Forms.Button DoneWithAddingPaths;
        private System.Windows.Forms.Button HelpExplanation;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExcludedPaths;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}