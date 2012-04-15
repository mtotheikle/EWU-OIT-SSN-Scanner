using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Threading;

namespace Scanner_UI
{
    public partial class Scanner_Form : Form
    {
        private SQLiteConnection sqlCon;
        private SQLiteCommand sqlCmd;
        private SQLiteDataAdapter sqlDataAdapter;
        private DataSet dSet = new DataSet();
        private DataTable dTable = new DataTable();
        public delegate void InvokeDelegate(string text);
        public delegate void InvokeDelegate2(string filename, string path, string priority, string count);

        public Scanner_Form()
        {
            InitializeComponent();
            SQLDatabaseInitilize();
            LoadData();
            DataGridViewColumn column = dataGridView.Columns[1];
            column.Width = 360;
        }

        #region Database Stuff
        public void LoadData()
        {
            sqlCmd = sqlCon.CreateCommand();
            string commandText = "select filename, location, count, priority from main";
            sqlDataAdapter = new SQLiteDataAdapter(commandText, sqlCon);
            dSet.Reset();
            sqlDataAdapter.Fill(dSet);
            dTable = dSet.Tables[0];
            
            dataGridView.DataSource = dTable;


        }

        public void SQLDatabaseInitilize()
        {
            if (File.Exists("log.db"))
                File.Delete("log.db");
            sqlCon = new SQLiteConnection("Data Source=log.db;Version=3;New=True;Compress=True;"); //create a new database
            sqlCon.Open(); //open the connection
            sqlCmd = sqlCon.CreateCommand();
            //create tables needed for database;
            sqlCmd.CommandText = "CREATE TABLE if not exists main (filename text, location text, count integer, priority integer);";
            sqlCmd.ExecuteNonQuery();
        }

        private void ExecuteQuery(string txtQuery)
        {
            try
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = txtQuery;
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void AddToTable(string filename, string location, int count, int priority)
        {
            string txtSQLQuery = "insert into  main (filename, " +
                "location, count, priority) values ('" + filename + "', '" +
                location + "', " + count + ", " + priority + ")";
            ExecuteQuery(txtSQLQuery);
        }

        #endregion

        #region Update UI Thread
        public void UpdateFileNameLabel(string text)
        {
            fileNameLabel.Text = text;
        }
        public void UpdatePercentageLabel(string percent)
        {
            percentage.Text = percent;
        }

        public void SetProgressBarMax(string val)
        {
            progressBar.Maximum = Convert.ToInt32(val);
        }

        public void UpdateProgressBar(string val)
        {
            progressBar.Value = Convert.ToInt32(val);
        }

        public void UpdateData(string fileName, string path, string priority, string count)
        {
            this.AddToTable(fileName, path, Convert.ToInt32(count), Convert.ToInt32(priority));
            LoadData();
        }
        #endregion

        #region Event Handlers and Methods
        private void scanButton_Click(object sender, EventArgs e)
        {
            start_scan();
        }

        private void startScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start_scan();
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            pause_scan();
        }

        private void pauseScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pause_scan();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Scanner_Main.TraverseDirectories();
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pauseButton.Enabled = false;
            this.pauseScanToolStripMenuItem.Enabled = false;
            this.pauseButton.Text = "     Pause Scan";
            this.pauseScanToolStripMenuItem.Text = "Pause Scan";
            Scanner_Main.scanning = false;
            this.scanButton.Enabled = true;
            this.startScanToolStripMenuItem1.Enabled = true;
            this.path.Enabled = true;
            this.progressBar.Value = this.progressBar.Maximum;
            this.fileNameLabel.Text = "Scan Complete";
            Application.DoEvents();
        }

        private void start_scan()
        {
            if (!Directory.Exists(path.Text))
            {
                MessageBox.Show("Not a valid path. Please enter a valid path.");
                return;
            }
            this.scanButton.Enabled = false;
            this.startScanToolStripMenuItem1.Enabled = false;
            this.path.Enabled = false;
            this.pauseButton.Text = "     Pause Scan";
            this.pauseScanToolStripMenuItem.Text = "Pause Scan";
            Scanner_Main.scanning = true;
            this.pauseButton.Enabled = true;
            this.pauseScanToolStripMenuItem.Enabled = true;

            backgroundWorker.RunWorkerAsync();
        }

        private void pause_scan()
        {
            if (Scanner_Main.scanning)
            {
                this.pauseButton.Text = "     Resume Scan";
                this.pauseScanToolStripMenuItem.Text = "Resume Scan";
                Scanner_Main.scanning = false;
            }
            else
            {
                this.pauseButton.Text = "     Pause Scan";
                this.pauseScanToolStripMenuItem.Text = "Pause Scan";
                Scanner_Main.scanning = true;
            }
        }
        #endregion
    }
}
