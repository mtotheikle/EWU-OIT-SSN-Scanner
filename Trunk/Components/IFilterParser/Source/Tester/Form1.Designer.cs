namespace IFilterTester
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
      this.btnBrowse = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnBrowse
      // 
      this.btnBrowse.Location = new System.Drawing.Point(12, 12);
      this.btnBrowse.Name = "btnBrowse";
      this.btnBrowse.Size = new System.Drawing.Size(75, 23);
      this.btnBrowse.TabIndex = 0;
      this.btnBrowse.Text = "&Browse";
      this.btnBrowse.UseVisualStyleBackColor = true;
      this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(12, 72);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(593, 350);
      this.textBox1.TabIndex = 1;
      // 
      // openFileDialog1
      // 
      this.openFileDialog1.FileName = "openFileDialog1";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 56);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(95, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Text loaded from...";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(617, 434);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.btnBrowse);
      this.Name = "Form1";
      this.Text = "IFilter Tester";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button btnBrowse;
    private System.Windows.Forms.TextBox textBox1;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.Label label1;
  }
}

