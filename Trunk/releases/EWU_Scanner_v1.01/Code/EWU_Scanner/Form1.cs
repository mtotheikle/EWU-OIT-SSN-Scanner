using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SevenZip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start_scan();
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start_scan();
        }
        private void start_scan()
        {
            this.button1.Enabled = false;
            Program.scanning = true;
            Program.TraverseTree(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Cache\\Senior_Project\\Sprint_2");
           // Program.TraverseTree("C:\\Users\\gsprint\\Documents");
            Program.scanning = false;
            this.button1.Enabled = true;
            this.progressBar1.Value = this.progressBar1.Maximum;
            this.label1.Text = "Scan Complete";
            Application.DoEvents();
        }
        void MainWindow_Closing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
