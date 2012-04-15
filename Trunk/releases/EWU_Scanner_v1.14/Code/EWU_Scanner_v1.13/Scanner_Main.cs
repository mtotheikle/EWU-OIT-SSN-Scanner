using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml;
using Microsoft.COM;
using System.Text;
using System.Windows.Forms;
using XMLParser;
using PDFParser;
using OfficeParser;
using SevenZip;
using Scanner.IFilter;
using System.Data.SQLite;

namespace Scanner_UI
{
    class Scanner_Main
    {
        public static Scanner_Form f1 = new Scanner_Form();
        public static bool scanning = false;

        public const String dllName = "SSNScannerDLL.dll";

        internal static class UnsafeNativeMethod
        {
            [DllImport(dllName, CharSet = CharSet.Ansi, 
                CallingConvention = CallingConvention.Cdecl)]
            public static extern int ScanAnsi(Char[] file_stream, 
                int length, ref int count);
        }

        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(f1);
        }

        public static void TraverseDirectories()
        {
            string path = f1.path.Text;
            int num_files = Count_Files(path);
            int count = 0;
            f1.progressBar.BeginInvoke(new Scanner_UI.Scanner_Form.InvokeDelegate(f1.SetProgressBarMax), new object[] { num_files.ToString() });

            Stack<string> directories = new Stack<string>(100);
            if (!System.IO.Directory.Exists(path))
                throw new ArgumentException();
            directories.Push(path);

            while (directories.Count > 0)
            {
                string currDir = directories.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                catch (System.IO.DirectoryNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                foreach (string file in files)
                {
                    while (!scanning)
                    {
                        //wait til the user resumes the scan
                    }
                    try
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        String st = fi.Name;
                        //Console.WriteLine(st);
                        f1.fileNameLabel.BeginInvoke(new Scanner_UI.Scanner_Form.InvokeDelegate(f1.UpdateFileNameLabel), new object[]{st});

                        if(count < f1.progressBar.Maximum)
                            count++;
                        f1.progressBar.BeginInvoke(new Scanner_UI.Scanner_Form.InvokeDelegate(f1.UpdateProgressBar), new object[] { count.ToString() });
                        int percentage = (int) ((double) count / f1.progressBar.Maximum * 100);
                        //Console.WriteLine(percentage);
                        f1.percentage.BeginInvoke(new Scanner_UI.Scanner_Form.InvokeDelegate(f1.UpdatePercentageLabel), new object[] { percentage + " %" });
                        string ext = fi.Extension.ToString();
                        Determine_Parser(fi, 0);
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                foreach (string dir_name in subDirs)
                    directories.Push(dir_name);
            }
        }

        public static int Count_Files(string path)
        {
            int num_files = 0;
            Stack<string> directories = new Stack<string>(100);
            if (!System.IO.Directory.Exists(path))
                throw new ArgumentException();
            directories.Push(path);

            while (directories.Count > 0)
            {
                string currDir = directories.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currDir);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                foreach (string file in files)
                {
                    try
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        num_files++;
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                foreach (string dir_name in subDirs)
                    directories.Push(dir_name);
            }
            return num_files;
        }

        public static void WriteToLog(string fileName, string path, string priority, int count)
        {
            //f1.AddToTable(fileName, path, count, Convert.ToInt32(priority));
            f1.dataGridView.BeginInvoke(new Scanner_UI.Scanner_Form.InvokeDelegate2(f1.UpdateData), new object[] { fileName, path, priority, count.ToString() });
            //f1.LoadData();
            StreamWriter writer = File.AppendText("log.txt");
            writer.WriteLine("File name: " + fileName + "   Path: " + path + "   Priority Level: " + priority + " Count: " + count);
            writer.WriteLine("======================================================================================================================================================================================");
            writer.Flush();
            writer.Close();
        }

        public static void Determine_Parser(System.IO.FileInfo fi, uint level)
        {
            string ext = fi.Extension.ToString();
            if (ext.CompareTo(".txt") == 0)
            {
                try
                {
                    StreamReader textFile = new StreamReader(fi.FullName);
                    string text = textFile.ReadToEnd();
                    textFile.Close();
                   // MessageBox.Show(fi.FullName);
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    if (retCode > 0)
                        WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".rtf") == 0)
            {
                try
                {
                    RichTextBox rtb = new RichTextBox();
                    rtb.Rtf = System.IO.File.ReadAllText(fi.FullName);
                    string text = rtb.Text;
                    //Console.WriteLine(s);
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    if (retCode > 0)
                        WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    //Console.WriteLine("\t" + text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".pdf") == 0)
            {
                try
                {
                    String text = PDFParser.Parser.ParsePDFtoString(fi.FullName);
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    if (retCode > 0)
                    WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    //Console.WriteLine("\t" + text);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".doc") == 0 || ext.CompareTo(".xls") == 0 || ext.CompareTo(".ppt") == 0)
            {
                try
                {
                    TextReader reader = new FilterReader(fi.FullName);
                    String text = "";
                    using (reader) { text = reader.ReadToEnd(); }
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    if (retCode > 0)
                        WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    //Console.WriteLine("\t" + text);
                }
                catch (Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".docx") == 0)
            {
                try
                {
                    String text = OfficeParser.Parser.docxParser(fi.FullName);
                    if (text != null)
                    {
                        int retCode, count = 0;
                        retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                        if (retCode > 0)
                            WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    }
                    //Console.WriteLine("\t" + text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            else if (ext.CompareTo(".xlsx") == 0)
            {
                try
                {
                    String text = OfficeParser.Parser.xlsxParser(fi.FullName);
                    if (text != null)
                    {
                        int retCode, count = 0;
                        retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                        if (retCode > 0)
                            WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                        //Console.WriteLine("\t" + text);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".pptx") == 0)
            {
                try
                {
                    String text = OfficeParser.Parser.pptxParser(fi.FullName);
                    if (text != null)
                    {
                        int retCode, count = 0;
                        retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                        if (retCode > 0)
                            WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                        //Console.WriteLine("\t" + text);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".odt") == 0 || ext.CompareTo(".ods") == 0 || ext.CompareTo(".odp") == 0)
            {
                try
                {
                    String text = OfficeParser.Parser.openOfficeParser(fi.FullName);
                    if (text != null)
                    {
                        int retCode, count = 0;
                        retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                        if (retCode > 0)
                            WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                        //Console.WriteLine("\t" + text);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".xml") == 0)
            {
                try
                {
                    String text = XMLParser.Parser.ParseXMLtoString(fi.FullName);
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    if (retCode > 0)
                        WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    //Console.WriteLine("\t" + text);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (ext.CompareTo(".pst") == 0)
            {

            }
            else if (ext.CompareTo(".zip") == 0 || ext.CompareTo(".7z") == 0 || ext.CompareTo(".xz") == 0 || ext.CompareTo(".bzip2") == 0 ||
                ext.CompareTo(".gzip") == 0 || ext.CompareTo(".tar") == 0 || ext.CompareTo(".wim") == 0 || ext.CompareTo(".arj") == 0 ||
                ext.CompareTo(".cab") == 0 || ext.CompareTo(".chm") == 0 || ext.CompareTo(".cpio") == 0 || ext.CompareTo(".cramfs") == 0 ||
                ext.CompareTo(".deb") == 0 || ext.CompareTo(".fat") == 0 || ext.CompareTo(".hfs") == 0 || ext.CompareTo(".iso") == 0 ||
                ext.CompareTo(".lzh") == 0 || ext.CompareTo(".lzma") == 0 || ext.CompareTo(".mbr") == 0 || ext.CompareTo(".msi") == 0 ||
                ext.CompareTo(".nsis") == 0 || ext.CompareTo(".ntfs") == 0 || ext.CompareTo(".rar") == 0 || ext.CompareTo(".rpm") == 0 ||
                ext.CompareTo(".squashfs") == 0 || ext.CompareTo(".udf") == 0 || ext.CompareTo(".vhd") == 0 || ext.CompareTo(".xar") == 0 ||
                ext.CompareTo(".z") == 0)
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
                    extractor = new SevenZipExtractor(fi.FullName);

                    //Extract the entire file
                    extractor.ExtractArchive(tempFolderPath);
                    extractor.Dispose();

                    //Count how many files in archive
                    int count = Directory.GetFiles(tempFolderPath, "*.*", SearchOption.AllDirectories).Length;

                    // traverse files
                    string[] fileEntries = Directory.GetFiles(tempFolderPath);
                    foreach (string fileName in fileEntries)
                    {
                        //Console.WriteLine("IN ARCHIVE: " + fileName);
                    }

                    //delete the temporary directory we created at the beginning
                    Directory.Delete(tempFolderPath, true);
                }
                catch (Exception e)
                {
                    //get rid of the object because it is unmanaged
                    extractor.Dispose();
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
