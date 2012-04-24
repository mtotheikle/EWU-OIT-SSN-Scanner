namespace EWUScanner
{
    partial class InitializingForm
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
            this.lblWaiting = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblDots = new System.Windows.Forms.Label();
            this.dotTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblWaiting
            // 
            this.lblWaiting.AutoSize = true;
            this.lblWaiting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWaiting.Location = new System.Drawing.Point(73, 56);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(64, 13);
            this.lblWaiting.TabIndex = 0;
            this.lblWaiting.Text = "Initializing";
            // 
            // lblMessage
            // 
            this.lblMessage.Location = new System.Drawing.Point(12, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(195, 43);
            this.lblMessage.TabIndex = 1;
            this.lblMessage.Text = "Please wait while the scan is prepared. This may take some time.";
            // 
            // lblDots
            // 
            this.lblDots.AutoSize = true;
            this.lblDots.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDots.Location = new System.Drawing.Point(135, 56);
            this.lblDots.Name = "lblDots";
            this.lblDots.Size = new System.Drawing.Size(0, 13);
            this.lblDots.TabIndex = 2;
            // 
            // dotTimer
            // 
            this.dotTimer.Enabled = true;
            this.dotTimer.Interval = 1000;
            this.dotTimer.Tick += new System.EventHandler(this.dotTimer_Tick);
            // 
            // InitializingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(209, 84);
            this.ControlBox = false;
            this.Controls.Add(this.lblDots);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblWaiting);
            this.MaximumSize = new System.Drawing.Size(217, 92);
            this.MinimumSize = new System.Drawing.Size(217, 92);
            this.Name = "InitializingForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.InitializingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWaiting;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblDots;
        public System.Windows.Forms.Timer dotTimer;
    }
}