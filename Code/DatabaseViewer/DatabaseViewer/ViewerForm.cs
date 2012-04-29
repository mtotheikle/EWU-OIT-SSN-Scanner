using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DatabaseViewer
{
    public partial class ViewerForm : Form
    {
        private SQLiteConnection sqlCon;
        private SQLiteCommand sqlCmd;
        private SQLiteDataAdapter sqlDataAdapter;
        private DataSet dSetScanned = new DataSet();
        private DataSet dSetCreditCard = new DataSet();
        private DataSet dSetNotScanned = new DataSet();
        private DataTable dTableScanned = new DataTable();
        private DataTable dTableNotScanned = new DataTable();
        private DataTable dTableCreditCard = new DataTable();

        public ViewerForm()
        {
            InitializeComponent();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openDBFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadDatabase(openDBFileDialog.FileName);
            }

        }

        private void LoadDatabase(string databasePath)
        {
            try
            {
                sqlCon = new SQLiteConnection("Data Source=" + databasePath + ";Version=3;New=True;Compress=True;");
                String dbFilename = System.IO.Path.GetFileName(databasePath);
                String[] password = dbFilename.Split('@');
                String[] dbPassword = password[1].Split('_');
                String p = password[0] + dbPassword[0];
                sqlCon.SetPassword(password[0] + dbPassword[0]);
                sqlCon.Open(); //open the connection
                

                sqlCmd = sqlCon.CreateCommand();
                //Update this command to nates new table structure...
                string commandText = "select filename, filePath, count, priority, pattern_D9_Count, pattern_D324_Count from Scanned";
                sqlDataAdapter = new SQLiteDataAdapter(commandText, sqlCon);
                dSetScanned.Reset();
                sqlDataAdapter.Fill(dSetScanned);
                dTableScanned = dSetScanned.Tables[0];
                dataGridView_Social.DataSource = dTableScanned;

                commandText = "select filename, filePath, count, priority, visa, mastercard, americanExpress, discover, dinerClub, JCB from CreditCard";
                sqlDataAdapter = new SQLiteDataAdapter(commandText, sqlCon);
                dSetCreditCard.Reset();
                sqlDataAdapter.Fill(dSetCreditCard);
                dTableCreditCard = dSetCreditCard.Tables[0];
                dataGridView_Credit.DataSource = dTableCreditCard;

                commandText = "select filePath from UnScannable";
                sqlDataAdapter = new SQLiteDataAdapter(commandText, sqlCon);
                dSetNotScanned.Reset();
                sqlDataAdapter.Fill(dSetNotScanned);
                dTableNotScanned = dSetNotScanned.Tables[0];
                dataGridView_NotScanned.DataSource = dTableNotScanned;
            }
            catch (SQLiteException e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
