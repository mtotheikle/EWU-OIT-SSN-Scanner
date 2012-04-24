using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;

namespace Scan_UI
{
    public partial class Scanner_Form : Form
    {
         private SQLiteConnection sqlCon;
        private SQLiteCommand sqlCmd;
        private SQLiteDataAdapter sqlDataAdapter;
        private DataSet dSet = new DataSet();
        private DataTable dTable = new DataTable();

        public Scanner_Form()
        {
            InitializeComponent();
            SQLDatabaseInitilize();
            LoadData();
            DataGridViewColumn column = dataGridView.Columns[1];
            column.Width = 360;
        }

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

        #region Database Stuff
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

        private void scanButton_Click(object sender, EventArgs e)
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
            if (!Directory.Exists(path.Text))
            {
                MessageBox.Show("Not a valid path. Please enter a valid path.");
                return;
            }
            this.scanButton.Enabled = false;
            this.path.Enabled = false;
            Scanner_Main.scanning = true;
           // Scanner_Main.TraverseTree(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+"\\Cache\\Senior_Project\\Sprint_2");
           // Scanner_Main.TraverseTree("C:\\Users\\gsprint\\Documents\\CS Files\\CSCD 488\\test_cases");
            Scanner_Main.TraverseTree(path.Text);
            Scanner_Main.scanning = false;
            this.scanButton.Enabled = true;
            this.path.Enabled = true;
            this.progressBar.Value = this.progressBar.Maximum;
            this.fileNameLabel.Text = "Scan Complete";
            Application.DoEvents();
        }
        void MainWindow_Closing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
