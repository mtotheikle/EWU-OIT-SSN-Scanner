using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Data;
using System.Windows.Forms;
using System.IO;


namespace EWUScanner
{
    static class Database
    {
        public static SQLiteConnection sqlCon;
        private static SQLiteCommand sqlCmd;
        //private static SQLiteDataAdapter sqlDataAdapter_unScanned;
        private static DataSet dataSet_Scanned = new DataSet();
        private static DataSet dataSet_unScanned = new DataSet();
        private static DataSet dataSet_unScannable = new DataSet();
        private static DataTable dTable_Scanned = new DataTable();
        private static DataTable dTable_unScannable = new DataTable();
        private static DataTable dTable_unScanned = new DataTable();
        //private static SQLiteDataReader sqReader;
        //private static SQLiteParameter fileParameter; //Parameterized Query

        // Creates the three tables: scanned, unScanned, and unScannable
        public static void SQLDatabaseInitilize()
        {
            String dbName = Environment.UserName+"@"+Environment.MachineName+"_"+System.DateTime.Now.ToString("MM_dd_yy_H_mm_ss");
            //MessageBox.Show(dbName);
            try
            {
               //if (File.Exists(".db"))
                    //File.Delete("db.db");
                sqlCon = new SQLiteConnection("Data Source=" + System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\" +dbName + ".sdb;Version=3;New=True;Compress=True;"); //create a new database
                sqlCon.Open(); //open the connection
                String []password = dbName.Split('@');
                String[] dbPassword = password[1].Split('_');
                sqlCon.ChangePassword(password[0] + dbPassword[0]);

                
                sqlCmd = sqlCon.CreateCommand();
                // Create scanned table
                sqlCmd.CommandText = "CREATE TABLE if not exists Scanned (fileIndex integer PRIMARY KEY AUTOINCREMENT, filename text, " +
                                     "filePath text, count integer, priority text, pattern_D9_Count integer, pattern_D324_Count integer);";
                sqlCmd.ExecuteNonQuery();
                // Create unScannable table
                sqlCmd.CommandText = "CREATE TABLE if not exists UnScannable (filename text, filePath text, owner text, reason text);";
                sqlCmd.ExecuteNonQuery();
                // Create CreditCard table
                sqlCmd.CommandText = "CREATE TABLE if not exists CreditCard (filename text, filePath text, count integer, priority text, " +
                                     "visa integer, mastercard integer, americanExpress integer, discover integer, dinerClub integer, JCB integer);";
                sqlCmd.ExecuteNonQuery();
                //Create CrashStatus table
                sqlCmd.CommandText = "CREATE TABLE if not exists CrashStatus (status text);";
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception) { /*MessageBox.Show(e.ToString());*/ }
        }//END METHOD

        // Executes sql query for insert and create (all non-select)
        private static void ExecuteNonQuery(string txtQuery)
        {
            try
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = txtQuery;
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception) { /*MessageBox.Show(txtQuery.ToString(), "ExecuteNonQuery fail");*/ }
        }//END METHOD

        // Populates the Scanned table with files that have possible ssn
        public static void AddToTableScanned(string fileName, string filePath, ScanData returnedData)
        {
            //string fileName, string filePath, int count, string priority, int patternD9Count, int patternD324Count
            String cleanedFileName = CleanFileString(fileName);
            String cleanedFilePath = CleanFileString(filePath);
            try
            {
                string txtQuery = "insert into  Scanned (filename, filePath, count, priority, pattern_D9_Count, pattern_D324_Count) " +
                                  "values ('" + cleanedFileName + "', '" + cleanedFilePath + "', " + returnedData.Count + ", '" + returnedData.Priority + "', '" + returnedData.Pattern_D9 + "', '" + returnedData.Pattern_D3D2D4 + "')";
                ExecuteNonQuery(txtQuery);
            }
            catch (Exception) { /*MessageBox.Show("AddToTableScanned Fail", e.ToString());*/ }
        }//END METHOD


        // Populates the Credit Card table with files that have possible credit card number
        public static void AddToTableCreditCard(string filename, string filePath, CreditData ccReturnedData)
        {
            String cleanedFileName = CleanFileString(filename);
            String cleanedFilePath = CleanFileString(filePath);
            try
            {
                string txtQuery = "insert into  CreditCard (filename, filePath, count, priority, visa, mastercard, americanExpress, discover, dinerClub, JCB) " +
                                  "values ('" + cleanedFileName + "', '" + cleanedFilePath + "', " + ccReturnedData.Count + ", " + ccReturnedData.Priority + ", " + ccReturnedData.VisaCount  + ", " + ccReturnedData.MC_Count + 
                                  ", " + ccReturnedData.AmexCount + ", " + ccReturnedData.DisCount + ", " + ccReturnedData.DinnCount + ", " + ccReturnedData.JCB_Count + ")";
                ExecuteNonQuery(txtQuery);
            }
            catch (Exception) { /*MessageBox.Show("AddToTableCreditCard Fail", e.ToString());*/ }
        }//END METHOD

        // Populates the UnScannedable table for files that could not be parsed
        public static void AddToTableUnScannable(string fileName, string filePath, string userName, string exception)
        {
            String cleanedFileName = CleanFileString(fileName);
            String cleanedFilePath = CleanFileString(filePath);
            String cleanedException = CleanFileString(exception);
            try
            {
                string txtQuery = "insert into UnScannable (filename, filePath, owner, reason) " + "values ('" + cleanedFileName + "', '" + cleanedFilePath + "', '" + userName + "', '" + cleanedException + "');";
                ExecuteNonQuery(txtQuery);
            }
            catch (Exception) { /*MessageBox.Show("AddToTableunScannable Fail", e.ToString());*/ }
        }//END METHOD

        // In order to execute a non-query all single quotes have to be escaped in order to avoid sql error
        private static String CleanFileString(String file)
        {
            return file.Replace("'", "''");
        }

        public static void CloseSQLConnection()
        {
            sqlCon.Close();
        }


/*
        // Reads from the unScanned table to get next file path
        //http://www.devart.com/dotconnect/sqlite/docs/Devart.Data.SQLite~Devart.Data.SQLite.SQLiteCommand~ExecuteReader%28%29.html
        public static String UnScannedDataReader()
        {
            try
            {
                String filePath = null;
                string query = "SELECT filePath FROM UnScanned order by file_num asc limit 1;";
                sqlCmd = new SQLiteCommand(query, sqlCon);
             //   sqlCmd.Connection.Open();
                sqReader = sqlCmd.ExecuteReader();
                while (sqReader.Read())
                {
                    filePath = (String)sqReader.GetValue(0);
                }
                sqReader.Close(); //Close sqReader to avoid problems with mult sql connections
                RemoveCurFile();  //Removes the file from UnScanned
                return filePath;
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return null;
        }//END METHOD

        // Populates the table for files that need to be scanned
        public static void AddToTableUnScanned(String filePath)
        {
            String cleanedFilePath = CleanFileString(filePath);
            try
            {
                string txtQuery = "insert into  UnScanned (filePath) values ('" + cleanedFilePath + "')";
                ExecuteNonQuery(txtQuery);
            }
            catch (Exception e) { MessageBox.Show("AddToTableunScanned Fail", e.ToString()); }
        }//END METHOD

        // Removes the current file being scanned from the UnScanned Table
        public static void RemoveCurFile()
        {
            try
            {
                string txtQuery = "DELETE FROM UnScanned WHERE file_num = (SELECT file_num FROM UnScanned order by file_num asc limit 1);";
                ExecuteNonQuery(txtQuery);
            }
            catch (Exception e) { MessageBox.Show("RemoveCurFile Fail", e.ToString()); }
        }

        // Executes sql query for insert using Parameterized value to improve insert performance into the UnScanned table
        //http://www.vistadb.net/tutorials/insertrows-csharp.aspx
        public static void InitializeParameterizedQuery()
        {
            try
            {
                sqlCmd = sqlCon.CreateCommand();
                sqlCmd.CommandText = @"INSERT INTO [UnScanned] (filePath) VALUES (?)";
                fileParameter = new SQLiteParameter();
                sqlCmd.Parameters.Add(fileParameter);
            }
            catch (Exception e) { MessageBox.Show("InitializeParameterizedQuery fail"); }
        }//END METHOD



        // Populates the table for files that need to be scanned
        public static void AddToTableUnScannedUsingParameterizedQuery(String filePath)
        {
            String cleanedFilePath = CleanFileString(filePath);
            try
            {
                fileParameter.Value = cleanedFilePath;
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception e) { MessageBox.Show(e.ToString(), "AddToTableUnScannedUsingParameterizedQuery Fail"); }
        }//END METHOD

        public static void LoadData_unScanned()
        {
            try
            {
                sqlCmd = sqlCon.CreateCommand();
                String commandText = "select file_num, location";
                sqlDataAdapter_unScanned = new SQLiteDataAdapter(commandText, sqlCon);
                dataSet_unScanned.Reset();
                sqlDataAdapter_unScanned.Fill(dataSet_unScanned);
                dTable_unScanned = dataSet_unScanned.Tables[0];
                //                dataGridView2.DataSource = dTable_unScanned;
            }
            catch (Exception e) { MessageBox.Show(e.ToString(), "LoadData_unScanned Fail"); }
        }//END METHOD
*/

    }//END CLASS Database
}//END NAMESPACE Scanner_UI
