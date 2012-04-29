using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using SevenZip;
using Scanner.IFilter;

namespace EWUScanner
{
    internal static class Program
    {
        //GUI Form
        public static MainForm mainUIForm = new MainForm();

        private static List<string> exclusionPaths; //These are the paths that will not be scanned by the scanner
        //======Special Folder Paths===========================================================================================================================
        private static string windowsFolderPath = (Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System))).FullName;
        private static string programFilesFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
        private static string usersDirectory = (Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal))).FullName;
        //private static string usersDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)+@"\Testbed";
        private static string logFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + @"\EWUScanner_Output.txt";
        //=====================================================================================================================================================

        public static bool scanning = false; //Used to pause the scan. True if scan is running, false if it is paused.
        private static int numScanned = 0; //Keeps track of the total number of files processed by the scan.
        private static int numFound = 1; //Keeps track of the total number of files that the engine detected social security/credit card numbers in.
        private static int totalSize = 0; //Total length of all targeted file types in Kilobytes.

        [STAThread]
        static void Main()
        {
            //Create the log file
            File.Create(logFilePath);
            Database.SQLDatabaseInitilize();
            
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false); --Don't know why this breaks but it works without it...
            Application.Run(mainUIForm);
        }

        /*
         * Entry point for the scan that is run as a background worker thread. Thread is created and started in MainForm
         * 
         * Admin Mode: Scans all drives on the system.
         * Basic Mode: Scans only the User's folder.
         * 
         * adminMode: Whether or not the scan should be run in Admin Mode. Set by a menu( modeToolStripMenuItem) in the GUI (MainForm.cs)
         * returns: void
         * 
        */
        public static void RunScan(bool adminMode, List<string> paths)
        {
            //getting the exclusion paths from the MainForm. The list should be guaranteed to be only strings.
            exclusionPaths = paths;

            if (adminMode) //Admin Mode
            {
                //List of all the drives on the system.
                DriveInfo[] listOfDrives = DriveInfo.GetDrives();

                //check for drive exclusions in the exclusionPaths string
                foreach (string path in exclusionPaths)
                {//traverse through all the exclusionPaths
                    for (int i = 0; i < listOfDrives.Length; i++)
                    {
                        try
                        {
                            if (listOfDrives[i].Name.StartsWith(path))
                            {
                                listOfDrives[i] = null; //for now, i'll set that index to null. I'm uncomfortable setting it to "", I worry that the logic checking might let that drive name "" slip through somehow. But a NULL is pretty blatant.
                                //I'll keep the NULL and implement the error-checking code.
                            }
                        }
                        catch (NullReferenceException)
                        {
                            continue; //i'm setting certain indices to null, and rescanning all indices everytime. Continue will help the scans keep going.
                        }
                    }
                }

                //====Alerts the user of all targeted drives===============================================================================================
                string drives = "";
                foreach (DriveInfo drive in listOfDrives)
                {
                    try
                    {
                        if ((drive.DriveType == DriveType.Removable && drive.IsReady) || drive.DriveType == DriveType.Fixed)
                            drives += drive.Name + " ";
                    }
                    catch (NullReferenceException)
                    {//Due to my exclusion path checking, certain drives may be excluded and set as null due to my code.
                        //This is to catch that error.
                        continue; //continue iterating through the listOfDrives
                    }
                }

                if (!String.IsNullOrEmpty(drives))
                {
                    MessageBox.Show("The following drive(s) have been detected and will be scanned: " + drives + 
                                    "\nIf you do not wish to scan these drives please remove them now as doing so during the scan is inadvisable.");
                }
                //==========================================================================================================================================

                //====initial pass to count how many files are on each drive.================================================================================
                foreach (DriveInfo drive in listOfDrives)
                {
                    try
                    {
                        if ((drive.DriveType == DriveType.Removable && drive.IsReady) || drive.DriveType == DriveType.Fixed)
                            totalSize += DetermineSize(drive.Name);
                    }
                    catch (NullReferenceException)
                    {//again, due to my coding, there may be a drive missing.
                        continue;
                    }
                }
                //===========================================================================================================================================
                
                //====Update form fields: Set the Maximum of the progress bar. Close the initializing form.==================================================
                try 
                { 
                    mainUIForm.theProgressBar.BeginInvoke(new MainForm.InvokeDelegateProgressBarMax(mainUIForm.SetProgressBarMax), new object[] { totalSize });
                    mainUIForm.BeginInvoke(new MainForm.InvokeDelegateInitForm(mainUIForm.InitFormVisible), new object[] { false });
                }
                catch (InvalidOperationException) { }
                //===========================================================================================================================================

                //====Second pass that parses and scans targeted files.======================================================================================
                foreach (DriveInfo drive in listOfDrives)
                {
                    try
                    {

                        if ((drive.DriveType == DriveType.Removable && drive.IsReady) || drive.DriveType == DriveType.Fixed)
                            ScanDirectories(drive.Name);
                    }
                    catch (NullReferenceException)
                    {//skip the null drive and move on
                        continue;
                    }
                }
                //============================================================================================================================================
            }
            else //Basic Mode
            {
                //initially, check if usersDirectory was excluded from the scan
                if (CheckIfExcluded(usersDirectory) == false) //CheckIfExcluded returns true for excluded and false if the file is included in the scan.
                {
                    totalSize = DetermineSize(usersDirectory);
                }
                else
                {
                    totalSize = 0; //In this basic scan mode, the usersDirectory is the only thing being scanned. If for some reason the usersDirectory is excluded
                    //from the scan and basic scan mode is run..then there will be no paths to scan, so the totalSize of the files will be 0.
                }
                try
                {
                    mainUIForm.theProgressBar.BeginInvoke(new MainForm.InvokeDelegateProgressBarMax(mainUIForm.SetProgressBarMax), new object[] { totalSize });
                    mainUIForm.BeginInvoke(new MainForm.InvokeDelegateInitForm(mainUIForm.InitFormVisible), new object[] { false });
                }
                catch (InvalidOperationException) { }

                //ScanDirectories will have exclusion path checking, hopefully that will be more modularized.
                ScanDirectories(usersDirectory);
            }

            //Update the form to indicate the scan is finished.
            try { mainUIForm.BeginInvoke(new MainForm.InvokeDelegateScanFinished(mainUIForm.UpdateScanFinished), new object[] { }); }
            catch (InvalidOperationException) { }

        }

        //this method will check to see if the passed in path is excluded from the scan
        //It will return a boolean: if the path IS excluded, returns true. If the path is NOT excluded, returns false.
        private static bool CheckIfExcluded(string usersDirectory)
        {
            foreach (string path in exclusionPaths)
            {
                if(usersDirectory.StartsWith(path))
                {
                    return true; //as soon as the path is found to be umbrella'd by the exclusion list, return true, that yes, this path should be excluded
                }
            }
            
            return false; //false should only be returned if the path passed in is not excluded from the scans
        }

        /*
         * Iterates through all files and folders starting from the incoming root folder and calls ProcessFile for every file encountered.
         * 
         * root: The path of the folder to start the iteration at.
         * returns: void.
         * 
         */
        public static void ScanDirectories(string root)
        {//For now, I will test the passed in root to see if it is excluded from scanning

            if (CheckIfExcluded(root) == false) //root is not excluded
            {//if not excluded, do the scanning

                // Testing code for Michael to scan one directory
                //root = "Y:\\Documents\\College\\11-12\\Spring\\CSCD488\\Testbed";

                int currentSize = 0;

                Stack<string> directories = new Stack<string>(100);

                if (!Directory.Exists(root))
                    throw new ArgumentException();

                directories.Push(root);

                while (directories.Count > 0)
                {
                    string currentDirectory = directories.Pop();

                    if (!currentDirectory.Equals(windowsFolderPath) && !currentDirectory.Equals(programFilesFolderPath))
                    {
                        string[] subDirectories;
                        //get the subDirectories of the current directory
                        try
                        {
                            subDirectories = Directory.GetDirectories(currentDirectory);
                        }
                        catch (UnauthorizedAccessException u)
                        {
                            Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, u.ToString());
                            continue;
                        }
                        catch (System.IO.DirectoryNotFoundException d)
                        {
                            Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, d.ToString());
                            continue;
                        }
                        //I will use a list to store the subDirectories at this point. The string[] is there because Directory.GetDirectories returns a string[], but
                        //I'll need to remove excluded paths. To do that, i'll just copy the included scannable paths to a list.

                        List<string> includedSubDirectories = new List<string>();
                        //populate the includedSubDirectories with the actual paths to be scanned
                        foreach (string directory in subDirectories)
                        {
                            if(CheckIfExcluded(directory) == false) //meaning, the directory IS to be scanned, it is not excluded
                            {
                                includedSubDirectories.Add(directory);
                            }
                        }

                        //at the end of this foreach loop, only the directories to be scanned should be in the includedSubDirectories list
                        string[] files = null;

                        try
                        {
                            files = Directory.GetFiles(currentDirectory);
                        }
                        catch (UnauthorizedAccessException u)
                        {
                            Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, u.ToString());
                            continue;
                        }
                        catch (System.IO.DirectoryNotFoundException d)
                        {
                            Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, d.ToString());
                            continue;
                        }

                        try { mainUIForm.lblCurDir.BeginInvoke(new MainForm.InvokeDelegateFilename(mainUIForm.UpdateLblCurFolder), new object[] { currentDirectory }); }
                        catch (InvalidOperationException) { continue; }

                        //iterate through all the files from the current directory
                        foreach (string file in files)
                        {
                            while (!scanning) { }
                            try
                            {
                                Delimon.Win32.IO.FileInfo fInfo = null;

                                //====Extension Validation: Only create the FileInfo object if it is a targeted file type. ===================================================
                                bool extValid = ValidateExtension(Path.GetExtension(file));
                                if (!extValid)
                                    continue;
                                else
                                    fInfo = new Delimon.Win32.IO.FileInfo(file);
                                //============================================================================================================================================

                                ProcessFile(fInfo);

                                //====Update form fields =====================================================================================================================
                                if (currentSize < mainUIForm.theProgressBar.Maximum)
                                    currentSize += (Convert.ToInt32(fInfo.Length) / 1024);

                                int percentage = (int)((double)currentSize / totalSize * 100);
                                numScanned++;

                                try
                                {

                                    mainUIForm.theProgressBar.BeginInvoke(new MainForm.InvokeDelegateProgressBar(mainUIForm.UpdateProgressBar), new object[] { currentSize });
                                    mainUIForm.lblPercentage.BeginInvoke(new MainForm.InvokeDelegatePercentage(mainUIForm.UpdateLblPercentage), new object[] { percentage });
                                    mainUIForm.lblItemsScanned.BeginInvoke(new MainForm.InvokeDelegateScanned(mainUIForm.UpdateLblItemsScanned), new object[] { numScanned });
                                }
                                catch (InvalidOperationException) { continue; }
                                //============================================================================================================================================

                            }
                            catch (FileNotFoundException f)
                            {
                                Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, f.ToString());
                                continue;
                            }
                            catch (UnauthorizedAccessException u)
                            {
                                Database.AddToTableUnScannable(currentDirectory, currentDirectory, Environment.UserName, u.ToString());
                                continue;
                            }
                        }

                        foreach (string directoryName in includedSubDirectories)
                        {
                            directories.Push(directoryName);
                        }
                    }
                }
            }
        }

        /*
         * Iterates through all files & folders starting from the given path and sums the file lengths.
         * 
         * path: The directory to start iterating from.
         * returns: the total size of all files that we are targeting(determined by ValidateExtension) in Kilobytes.
         */

        public static int DetermineSize(string path)
        {
            long totalSize = 0;

            if (CheckIfExcluded(path) == false)
            {//for the possibility that the path passed in is excluded and my code didn't catch it somehow.
                

                Stack<string> directories = new Stack<string>(100);

                if (!Directory.Exists(path))
                    throw new ArgumentException();

                directories.Push(path);

                while (directories.Count > 0)
                {
                    string currentDirectory = directories.Pop();

                    if (!currentDirectory.Equals(windowsFolderPath) && !currentDirectory.Equals(programFilesFolderPath))
                    {
                        string[] subDirectories;
                        try
                        {
                            subDirectories = Directory.GetDirectories(currentDirectory);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        //check for excluded directories
                        List<string> includedSubDirectories = new List<string>();

                        foreach (string directory in subDirectories)
                        {
                            if (CheckIfExcluded(directory) == false)
                            {
                                includedSubDirectories.Add(directory);
                            }
                        }

                        string[] files = null;

                        try
                        {
                            files = Directory.GetFiles(currentDirectory);
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        foreach (string file in files)
                        {
                            try
                            {
                                Delimon.Win32.IO.FileInfo fInfo = null;

                                bool extValid = ValidateExtension(Path.GetExtension(file));

                                if (!extValid)
                                    continue;
                                else
                                    fInfo = new Delimon.Win32.IO.FileInfo(file);

                                //Add the length of the current file to the total: The whole purpose of this method...
                                totalSize += fInfo.Length;
                            }
                            catch (FileNotFoundException)
                            {
                                continue;
                            }
                        }
                        foreach (string directoryName in includedSubDirectories)
                        {
                            directories.Push(directoryName);
                        }
                    }
                }

            }
            //finally, calculate the amount
            //change from bytes to kilobytes
            totalSize /= 1024;
            return Convert.ToInt32(totalSize);
        }

        /*
         * Determines whether the incoming extension is valid by checking if it is in the list of targeted extension.
         * 
         * ext: The extension to be validated.
         * returns: True if the extension is valid, false if it is not.
         */
        public static bool ValidateExtension(string ext)
        {
            string[] fileExtensions = { ".txt", ".rtf", ".csv", ".tsv", ".pdf", ".doc", ".xls", ".ppt", ".docx", ".xlsx", ".pptx", ".odt", 
                                                   ".ods", ".odp", ".xml", ".pst", ".ost", ".zip", ".7z", ".xz", ".bzip2", ".gzip", ".tar", ".rar" };
            bool valid = false;

            foreach (string extension in fileExtensions)
            {
                if (ext.Equals(extension))
                {
                    valid = true;
                    break;
                }
            }

            return valid;
        }

        /*
         * Determines if a file is an archive or not. If it's an archive it is extracted to the System temp forlder for processing. 
         * If it is not an archive it is passed to ProcessNonArchive.
         * 
         * fInfo: Incoming FileInfo object to be processed
         * returns: void
         */
        public static void ProcessFile(Delimon.Win32.IO.FileInfo fInfo)
        {
            string extention = Path.GetExtension(fInfo.FullName);

            if (extention == null)
                extention = "";

            if (IsArchive(extention))
            {
                //extract code
                SevenZipExtractor extractor = null;
                try
                {
                    //path to the systems temporary folder
                    String tempFolderPath = Path.GetTempPath();
                    tempFolderPath += "temp_dir\\";
                    //create a directory to dump everything into inside the temp folder
                    Directory.CreateDirectory(tempFolderPath);

                    //set the path of the 7z.dll (it needs to be in the debug folder)
                    SevenZipExtractor.SetLibraryPath("7z.dll");
                    extractor = new SevenZipExtractor(fInfo.FullName);

                    //Extract the entire file
                    extractor.ExtractArchive(tempFolderPath);
                    extractor.Dispose();

                    bool arcs = true;

                    while (arcs)
                    {
                        arcs = false;
                        // traverse files
                        string[] fileEntries = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories);
                        foreach (string fileName in fileEntries)
                        {
                            //Console.WriteLine("IN ARCHIVE: " + fileName);
                            FileInfo archive = new FileInfo(fileName);
                            if (IsArchive(archive.Extension.ToString()))
                            {
                                arcs = true;
                                extractor = new SevenZipExtractor(fileName);
                                extractor.ExtractArchive(tempFolderPath);
                                File.Delete(fileName);
                            }
                        }
                    }

                    int[][] totals = {new int[5], new int[9]}; //totals[0] count; totals[1] results;
                    string[] fileEntries2 = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories);
                    foreach (string fileName in fileEntries2)
                    {
                        int[][] results = ProcessNonArchive(new Delimon.Win32.IO.FileInfo(fileName), true);
                        if (MainForm.socialSecurityMode)
                        {
                            totals[0][0] += results[0][0];
                            if (results[0][1] > totals[0][1])
                                totals[0][1] = results[0][1];
                        }
                        if (MainForm.creditCardMode)
                        {
                            totals[1][0] += results[1][0];
                            if (results[1][1] > totals[1][1])
                                totals[1][1] = results[1][1];
                        }
                    }
                    //Count how many files in archive
                    //int count = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories).Length;
                    //delete the temporary directory we created at the beginning
                    Directory.Delete(tempFolderPath, true);
                    if (totals[0][1] > 0)
                    {
                        //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + (ScanData.Code)totals[1]);
                        if (MainForm.socialSecurityMode)
                        {
                            ScanData returnedData = new ScanData(totals[0][0], totals[0][1], totals[0][3], totals[0][4]);
                            Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                            try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                            catch (InvalidOperationException) { }
                        }
                        if (MainForm.creditCardMode)
                        {
                            CreditData ccReturnedData = new CreditData(totals[1][0], totals[1][1], totals[1][3], totals[1][4], totals[1][5], totals[1][6], totals[1][7], totals[1][8]);
                            Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                            try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                            catch (InvalidOperationException) { }
                        }
                      
                    }
                }
                catch (Exception e)
                {
                    //get rid of the object because it is unmanaged
                    extractor.Dispose();
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                int[][] ignore = ProcessNonArchive(fInfo, false);
            }
        }

        /*
         * Checks whether the incoming extension is a known archive type.
         * 
         * ext: Extension to be checked.
         * returns: true if it's an archive, false if it's not.
         */
        public static bool IsArchive(string ext)
        {
            if (ext.CompareTo(".zip") == 0 || ext.CompareTo(".7z") == 0 || ext.CompareTo(".xz") == 0 ||
                ext.CompareTo(".gzip") == 0 || ext.CompareTo(".tar") == 0 || ext.CompareTo(".wim") == 0)
            {
                return true;
            }
            return false;
        }

        /*
         * Checks the file extension and calls the corresponding parser.
         * Gets the results returned from the parser and passes it to the engine.
         * Passes the results to the Database
         * 
         * fInfo: Incoming FileInfo object to be processed
         * parentIsArchive: Whether or not the file is in an archive
         * returns: an int[] with the results from the call to the engine  (results[0] = Count, results[1] = RetCode)
         */
        public static int[][] ProcessNonArchive(Delimon.Win32.IO.FileInfo fInfo, bool parentIsArchive)
        {
            int[][] results = { new int[5], new int[9] };
          
            string ext = Path.GetExtension(fInfo.FullName);

            ScanData returnedData;
            CreditData ccReturnedData;

            if (ext.CompareTo(".txt") == 0 || ext.CompareTo(".csv") == 0)
            {
                try
                {
                    StreamReader textFile = new StreamReader(fInfo.FullName);
                    string text = textFile.ReadToEnd();
                    textFile.Close();

                    if (MainForm.socialSecurityMode)
                    {
                        returnedData = Engine.ScanForSocialSecurity(text);

                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1]= returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3]= returnedData.Pattern_D9;
                                results[0][4]= returnedData.Pattern_D3D2D4;
                            }
                            else
                            {
                                //Database entry goes here.
                                //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                                

                            }
                        }
                    }
                    if (MainForm.creditCardMode)
                    {
                        ccReturnedData = Engine.ScanForCreditCard(text);

                        if (ccReturnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[1][0] = ccReturnedData.Count;
                                results[1][1] = ccReturnedData.RetCode;
                                results[1][2] = (int)ccReturnedData.Priority;
                                results[1][3] = ccReturnedData.VisaCount;
                                results[1][4] = ccReturnedData.MC_Count;
                                results[1][5] = ccReturnedData.AmexCount;
                                results[1][6] = ccReturnedData.DisCount;
                                results[1][7] = ccReturnedData.DinnCount;
                                results[1][8] = ccReturnedData.JCB_Count;

                            }
                            else
                            {
                                //Database entry goes here.
                                //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " +ccReturnedData.Priority);
                                Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e) { Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString()); }
            }
            else if (ext.CompareTo(".rtf") == 0)
            {
                try
                {                        
                    RichTextBox rtb = new RichTextBox();
                    rtb.Rtf = System.IO.File.ReadAllText(fInfo.FullName);
                    string text = rtb.Text;

                    if (MainForm.socialSecurityMode)
                    {
                        returnedData = Engine.ScanForSocialSecurity(text);

                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1] = returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3] = returnedData.Pattern_D9;
                                results[0][4] = returnedData.Pattern_D3D2D4;
                            }
                            else
                            {
                                //Database entry
                                //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData); 
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                        if (MainForm.creditCardMode)
                        {
                            ccReturnedData = Engine.ScanForCreditCard(text);

                            if (returnedData.RetCode > 0)
                            {
                                if (parentIsArchive)
                                {
                                    results[1][0] = ccReturnedData.Count;
                                    results[1][1] = ccReturnedData.RetCode;
                                    results[1][2] = (int)ccReturnedData.Priority;
                                    results[1][3] = ccReturnedData.VisaCount;
                                    results[1][4] = ccReturnedData.MC_Count;
                                    results[1][5] = ccReturnedData.AmexCount;
                                    results[1][6] = ccReturnedData.DisCount;
                                    results[1][7] = ccReturnedData.DinnCount;
                                    results[1][8] = ccReturnedData.JCB_Count;
                                }
                                else
                                {
                                    //Database entry
                                    //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                    Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                                    try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                    catch (InvalidOperationException) { }
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
            }
            else if (ext.CompareTo(".pdf") == 0)
            {
                try
                {

                    string text = PDFParser.Parser.ParsePDFtoString(fInfo.FullName);
                    
                    if (MainForm.socialSecurityMode)
                    {
                        returnedData = Engine.ScanForSocialSecurity(text);
                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1] = returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3] = returnedData.Pattern_D9;
                                results[0][4] = returnedData.Pattern_D3D2D4;
                            }
                            else
                            {
                                //Database entry
                                //WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                            }
                    }
                    if (MainForm.creditCardMode)
                    {
                        ccReturnedData = Engine.ScanForCreditCard(text);

                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[1][0] = ccReturnedData.Count;
                                results[1][1] = ccReturnedData.RetCode;
                                results[1][2] = (int)ccReturnedData.Priority;
                                results[1][3] = ccReturnedData.VisaCount;
                                results[1][4] = ccReturnedData.MC_Count;
                                results[1][5] = ccReturnedData.AmexCount;
                                results[1][6] = ccReturnedData.DisCount;
                                results[1][7] = ccReturnedData.DinnCount;
                                results[1][8] = ccReturnedData.JCB_Count;
                            }
                            else
                            {
                                //Database entry
                                //WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                     }
                    }

                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
            }
            else if (ext.CompareTo(".doc") == 0 || ext.CompareTo(".xls") == 0 || ext.CompareTo(".ppt") == 0)
            {
                try
                {
                    TextReader reader = new FilterReader(fInfo.FullName);
                    String text = "";
                    using (reader) { text = reader.ReadToEnd(); }

                    if (MainForm.socialSecurityMode)
                    { 
                        returnedData = Engine.ScanForSocialSecurity(text);
                    
                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1] = returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3] = returnedData.Pattern_D9;
                                results[0][4] = returnedData.Pattern_D3D2D4;
                            }
                            else
                            {
                                //Database entry
                                //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                    }
                    if (MainForm.creditCardMode)
                    {
                        ccReturnedData = Engine.ScanForCreditCard(text);

                        if (ccReturnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[1][0] = ccReturnedData.Count;
                                results[1][1] = ccReturnedData.RetCode;
                                results[1][2] = (int)ccReturnedData.Priority;
                                results[1][3] = ccReturnedData.VisaCount;
                                results[1][4] = ccReturnedData.MC_Count;
                                results[1][5] = ccReturnedData.AmexCount;
                                results[1][6] = ccReturnedData.DisCount;
                                results[1][7] = ccReturnedData.DinnCount;
                                results[1][8] = ccReturnedData.JCB_Count;
                            }
                            else
                            {
                                //Database entry
                                Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
            }
            else if (ext.CompareTo(".docx") == 0 || ext.CompareTo(".xlsx") == 0 || ext.CompareTo(".pptx") == 0 || ext.CompareTo(".odt") == 0 || ext.CompareTo(".ods") == 0 || ext.CompareTo(".odp") == 0)
            {
                try
                {
                    String text = OfficeParser.Parser.Parse(fInfo.FullName, ext);
                    if (text != null)
                    {

                        if (MainForm.socialSecurityMode)
                        {
                            returnedData = Engine.ScanForSocialSecurity(text);

                            if (returnedData.RetCode > 0)
                            {
                                if (parentIsArchive)
                                {
                                    results[0][0] = returnedData.Count;
                                    results[0][1] = returnedData.RetCode;
                                    results[0][2] = (int)returnedData.Priority;
                                    results[0][3] = returnedData.Pattern_D9;
                                    results[0][4] = returnedData.Pattern_D3D2D4;
                                }
                                else
                                {
                                    //Database entry
                                    //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                    Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                                    try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                    catch (InvalidOperationException) { }
                                }
                            }
                        }
                        if (MainForm.creditCardMode)
                        {
                            ccReturnedData = Engine.ScanForCreditCard(text);

                            if (ccReturnedData.RetCode > 0)
                            {
                                if (parentIsArchive)
                                {
                                    results[1][0] = ccReturnedData.Count;
                                    results[1][1] = ccReturnedData.RetCode;
                                    results[1][2] = (int)ccReturnedData.Priority;
                                    results[1][3] = ccReturnedData.VisaCount;
                                    results[1][4] = ccReturnedData.MC_Count;
                                    results[1][5] = ccReturnedData.AmexCount;
                                    results[1][6] = ccReturnedData.DisCount;
                                    results[1][7] = ccReturnedData.DinnCount;
                                    results[1][8] = ccReturnedData.JCB_Count;
                                }
                                else
                                {
                                    //Database entry
                                    Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                                    try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                    catch (InvalidOperationException) { }
                                }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
            }
            else if (ext.CompareTo(".xml") == 0)
            {
                if(fInfo.Name.Equals("iTunes Music Library.xml"))
                    return results;
                try
                {
                    String text = XMLParser.Parser.ParseXMLtoString(fInfo.FullName);
                    if (String.IsNullOrEmpty(text))
                    { 
                        //Log to Unscannable table.
                    }

                    if (MainForm.socialSecurityMode)
                    {
                        returnedData = Engine.ScanForSocialSecurity(text);

                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1] = returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3] = returnedData.Pattern_D9;
                                results[0][4] = returnedData.Pattern_D3D2D4;
                            }
                            else
                            {
                                //Database entry
                                //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                                Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                    }
                    if (MainForm.creditCardMode)
                    {
                        ccReturnedData = Engine.ScanForCreditCard(text);

                        if (ccReturnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[1][0] = ccReturnedData.Count;
                                results[1][1] = ccReturnedData.RetCode;
                                results[1][2] = (int)ccReturnedData.Priority;
                                results[1][3] = ccReturnedData.VisaCount;
                                results[1][4] = ccReturnedData.MC_Count;
                                results[1][5] = ccReturnedData.AmexCount;
                                results[1][6] = ccReturnedData.DisCount;
                                results[1][7] = ccReturnedData.DinnCount;
                                results[1][8] = ccReturnedData.JCB_Count;
                            }
                            else
                            {
                                //Database entry
                                Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                                try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                                catch (InvalidOperationException) { }
                            }
                        }
                    }
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
            }
            else if (ext.CompareTo(".pst") == 0)
            {
                try
                {
                    com.pff.PSTFile pstFile = new com.pff.PSTFile(fInfo.FullName);
                    String text = pstFile.processFolder(pstFile.getRootFolder());
                    com.pff.PSTFolder folder = pstFile.getRootFolder();
                    processFolder(folder); // Process the main folder, once we hit an email we will scan that email

                    /*
                    if (MainForm.socialSecurityMode)
                    {
                        returnedData = Engine.ScanForSocialSecurity(text);

                        if (returnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[0][0] = returnedData.Count;
                                results[0][1] = returnedData.RetCode;
                                results[0][2] = (int)returnedData.Priority;
                                results[0][3] = returnedData.Pattern_D9;
                                results[0][4] = returnedData.Pattern_D3D2D4;
                            }
                            //Database entry
                            //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                            Database.AddToTableScanned(fInfo.Name, fInfo.FullName, returnedData);
                            try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                            catch (InvalidOperationException) { }
                        }
                    }
                    if (MainForm.creditCardMode)
                    {
                        ccReturnedData = Engine.ScanForCreditCard(text);

                        if (ccReturnedData.RetCode > 0)
                        {
                            if (parentIsArchive)
                            {
                                results[1][0] = ccReturnedData.Count;
                                results[1][1] = ccReturnedData.RetCode;
                                results[1][2] = (int)ccReturnedData.Priority;
                                results[1][3] = ccReturnedData.VisaCount;
                                results[1][4] = ccReturnedData.MC_Count;
                                results[1][5] = ccReturnedData.AmexCount;
                                results[1][6] = ccReturnedData.DisCount;
                                results[1][7] = ccReturnedData.DinnCount;
                                results[1][8] = ccReturnedData.JCB_Count;
                            }
                            //Database entry
                            Database.AddToTableCreditCard(fInfo.Name, fInfo.FullName, ccReturnedData);
                            try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                            catch (InvalidOperationException) { }
                        }
                    }
                    */
                }
                catch (UnauthorizedAccessException u)
                {
                    //File is encrypted: Add entry to Uncsannable table with reason: encrypted.
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, u.ToString());
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e.Message);
                    Database.AddToTableUnScannable(fInfo.Name, fInfo.FullName, Environment.UserName, e.ToString());
                }
        
            }

            return results;
        }

        static int depth = -1;

        private static void processFolder(com.pff.PSTFolder folder)
        {
            depth++;

            if (depth > 0)
            {
                Console.Out.WriteLine(folder.getDisplayName());
            }

            if (folder.hasSubfolders())
            {
                java.util.Vector folders = folder.getSubFolders();
                foreach (com.pff.PSTFolder childFolder in folders) {
                    processFolder(childFolder);
                }
            }

            if (folder.getContentCount() > 0)
            {
                com.pff.PSTMessage email = (com.pff.PSTMessage)folder.getNextChild();
                depth++;
                while (email != null)
                {
                    email = (com.pff.PSTMessage) folder.getNextChild();
                    scanEmail(email);

                }
                depth--;
            }

            depth--;
        }

        public static void scanEmail(com.pff.PSTMessage email)
        {
            CreditData ccReturnedData;

            try
            {
                if (MainForm.socialSecurityMode)
                {
                    // @todo Strip strings that are in urls, ex: "<https://someurl.com/1234567890/dod/123456789>"
                    ScanData returnedData = Engine.ScanForSocialSecurity(email.getBody());

                    if (returnedData.RetCode > 0)
                    {
                        //Database entry
                        //WriteToLogFile("Detected: " + fInfo.FullName + " Priority: " + returnedData.Priority);
                        String m = email.getBody();
                        Database.AddToTableScanned(email.getSubject()+email.getEmailAddress(), "Email File", returnedData);
                        try { mainUIForm.lblItemsFound.BeginInvoke(new MainForm.InvokeDelegateFound(mainUIForm.UpdateLblItemsFound), new object[] { numFound++ }); }
                        catch (InvalidOperationException) { }
                    }
                }
                // @todo We don't scan email for credit
            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
                Database.AddToTableUnScannable(email.getSubject(), "Email File", Environment.UserName, e.ToString());
            }
        }

        public static void WriteToLogFile(string text)
        {
            StreamWriter writer = File.AppendText(logFilePath);
            writer.WriteLine(text);
            writer.WriteLine("======================================================================================================================================================================================");
            writer.Flush();
            writer.Close();
        }
    }
}
