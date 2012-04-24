using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace EWUScanner
{
    public partial class MainForm : Form
    {
        //Delagates for updating the UI components from other threads.
        public delegate void InvokeDelegateFilename(string folderPath);
        public delegate void InvokeDelegateScanned(int numScanned);
        public delegate void InvokeDelegateFound(int numFound);
        public delegate void InvokeDelegateProgressBar(int value);
        public delegate void InvokeDelegatePercentage(int percent);
        public delegate void InvokeDelegateProgressBarMax(int value);
        public delegate void InvokeDelegateChangeCursor(int value);
        public delegate void InvokeDelegateInitForm(bool show);
        public delegate void InvokeStopTimer();
        public delegate void InvokeDelegateScanFinished();
        public static bool adminMode = false;
        public static bool creditCardMode = false;
        public static bool socialSecurityMode = true;
        public DateTime startTime;

        public InitializingForm iForm = new InitializingForm();

        public MainForm()
        {
            InitializeComponent();
        }

        public void StartScan()
        {
            //enable and disable menu items
            administratorModeToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
            scanModeToolStripMenuItem.Enabled = false;

            //Start the background worker for traversing files.
            fileTraversalWorker.RunWorkerAsync();
            //set the start time as right now
            startTime = DateTime.Now;
            //update the Start time: label
            lblStartTime.Text = startTime.ToString("hh:mm:ss");
            elapsedTimer.Start();
        }


        #region Event Handlers

        private void fileTraversalWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Program.RunScan(adminMode);
        }

        private void administratorModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if the menu strip item for administrator mode is checked
            if (administratorModeToolStripMenuItem.Checked == true)
            {
                //Set the admin mode flag to true
                adminMode = true;
                //Show the full scan button
                btnFullScan.Visible = true;
                btnFullScan.Enabled = true;
                //Hide the partial scan button
                btnPartialScan.Visible = false;
                btnPartialScan.Enabled = false;
                //change the mode label
                lblMode.Text = "Full Scan Mode";
                //give focus to the full scan button
                btnFullScan.Focus();
            }
            else
            {
                //set the admin flag to false
                adminMode = false;
                //hide the full scan button
                btnFullScan.Visible = false;
                btnFullScan.Enabled = false;
                //show the partial scan button
                btnPartialScan.Visible = true;
                btnPartialScan.Enabled = true;
                //change the mode label
                lblMode.Text = "Partial Scan Mode";
                //give focus to the partial scan button
                btnPartialScan.Focus();
            }
        }

        private void btnFullScan_Click(object sender, EventArgs e)
        {
            btnFullScan.Enabled = false;
            Program.scanning = true;
            this.StartScan();
            iForm.ShowDialog();
        }

        private void btnPartialScan_Click(object sender, EventArgs e)
        {
            btnPartialScan.Enabled = false;
            Program.scanning = true;
            this.StartScan();
            iForm.ShowDialog();
        }

        private void elapsedTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan duration = DateTime.Now.Subtract(startTime);
            lblElapsedHours.Text = duration.Hours.ToString();
            lblElapsedMins.Text = duration.Minutes.ToString();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm about = new AboutForm();
            about.Show();
        }

        private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.scanning = false;
            elapsedTimer.Stop();
            resumeToolStripMenuItem.Enabled = true;
            pauseToolStripMenuItem.Enabled = false;
        }

        private void resumeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.scanning = true;
            elapsedTimer.Start();
            resumeToolStripMenuItem.Enabled = false;
            pauseToolStripMenuItem.Enabled = true;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (adminMode)
                btnFullScan_Click(null, null);
            else
                btnPartialScan_Click(null, null);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ccToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ccToolStripMenuItem.Checked == false && ssnToolStripMenuItem.Checked == false)
            {
                ssnToolStripMenuItem.Checked = true;
                socialSecurityMode = true;
            }

            if (creditCardMode)
                creditCardMode = false;
            else if(!creditCardMode)
                creditCardMode = true;
        }

        private void ssnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ccToolStripMenuItem.Checked == false && ssnToolStripMenuItem.Checked == false)
            {
                ccToolStripMenuItem.Checked = true;
                creditCardMode = true;
            }

            if (socialSecurityMode)
                socialSecurityMode = false;
            else if (!socialSecurityMode)
                socialSecurityMode = true;

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Quit the scan and close?", "Alert!", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            //Catches null reference when Database has not been initialized in the case when no ssn or cc found
            try { Database.CloseSQLConnection(); }
            catch (NullReferenceException) { }

        }

        #endregion 

        #region Methods to Update the UI

        public void InitFormVisible(bool show)
        {
            if (!show)
                iForm.Dispose();
        }

        public void ChangeCursor(int value)
        {
            if (value == 0)
                this.Cursor = Cursors.Default;
            else if (value == 1)
                this.Cursor = Cursors.AppStarting;
        }

        public void UpdateLblCurFolder(string folderPath)
        {
            lblCurDir.Text = folderPath;
        }

        public void UpdateLblItemsFound(int numFound)
        {
            lblItemsFound.Text = numFound.ToString();
        }

        public void UpdateLblItemsScanned(int numScanned)
        {
            lblItemsScanned.Text = numScanned.ToString();
        }

        public void SetProgressBarMax(int value)
        { 
            theProgressBar.Maximum = value;
        }

        public void UpdateProgressBar(int value)
        {
            theProgressBar.Value = value;
        }

        public void UpdateLblPercentage(int percent)
        {
            lblPercentage.Text = percent + " %";
        }

        public void UpdateScanFinished()
        {
            elapsedTimer.Stop();
            lblPercentage.Visible = false;
            theProgressBar.Value = theProgressBar.Maximum;
            picBoxCheck.Visible = true;
            System.Media.SystemSounds.Exclamation.Play();
            pauseToolStripMenuItem.Enabled = false;
            resumeToolStripMenuItem.Enabled = false;
            lblCurDir.Text = "";
        }

        public void StopTimer()
        {
            elapsedTimer.Stop();
        }

        #endregion
    }
}
