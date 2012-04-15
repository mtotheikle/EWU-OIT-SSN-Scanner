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
using System.Data.SQLite;

namespace SevenZip
{
    class Program
    {
        public static Form1 f1 = new Form1();
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

        public static int Count_Files(string root)
        {
            int num_files = 0;
            Stack<string> dirs = new Stack<string>(20);
            if (!System.IO.Directory.Exists(root))
                throw new ArgumentException();
            dirs.Push(root);
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
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
                    files = System.IO.Directory.GetFiles(currentDir);
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
                foreach (string str in subDirs)
                    dirs.Push(str);
            }
            return num_files;
        }

        public static void TraverseTree(string root)
        {
            int num_files = Count_Files(root);
            int count = 0;
            f1.progressBar1.Minimum = 0;
            f1.progressBar1.Maximum = num_files;

            // Data structure to hold names of subfolders to 
            //be examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root))
                throw new ArgumentException();
            dirs.Push(root);

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
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
                    files = System.IO.Directory.GetFiles(currentDir);
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
                    try
                    {
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        // String st = fi.Name + " " + fi.Length + " " + fi.CreationTime;
                        String st = fi.Name;
                        Console.WriteLine(st);
                        f1.label1.Text = st;
                        Application.DoEvents();
                        if(count < f1.progressBar1.Maximum)
                            count++;
                        f1.progressBar1.Value = count;
                        string ext = fi.Extension.ToString();
                        Determine_Parser(fi, 0);
                    }
                    // If file was deleted by a separate application or 
                    //thread since the call to TraverseTree()
                    catch (System.IO.FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
                foreach (string str in subDirs)// Push the subdirectories onto the stack for traversal. This could also be done before handing the files.
                    dirs.Push(str);
            }
        }

        public static void WriteToLog(string fileName, string path, string priority, int count)
        {
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
                    //MessageBox.Show(fi.FullName);
                    int retCode, count = 0;
                    retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                    Console.WriteLine();
                    if (retCode > 0)
                    {
                        WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                        f1.AddToTable(fi.Name, fi.FullName, count, retCode);
                        f1.LoadData();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("exception thrown: " + e.ToString());
                }
            }
            else if (ext.CompareTo(".rtf") == 0)
            {

            }
            else if (ext.CompareTo(".pdf") == 0)
            {
                //String text = PDFParser.Parser.ParsePDFtoString(fi.FullName);
                //WriteToLog(fi.Name, fi.FullName, "0");
                //int retCode, count = 0;
                //retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                //Console.WriteLine();
                //if (retCode > 0)
                    //WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
            }
            else if (ext.CompareTo(".doc") == 0)
            {

            }
            else if (ext.CompareTo(".docx") == 0)
            {

            }
            else if (ext.CompareTo(".xls") == 0)
            {

            }
            else if (ext.CompareTo(".xlsx") == 0)
            {

            }
            else if (ext.CompareTo(".ppt") == 0)
            {

            }
            else if (ext.CompareTo(".pptx") == 0)
            {

            }
            else if (ext.CompareTo(".odt") == 0)
            {

            }
            else if (ext.CompareTo(".ods") == 0)
            {

            }
            else if (ext.CompareTo(".odp") == 0)
            {

            }
            else if (ext.CompareTo(".xml") == 0)
            {
                String text = XMLParser.Parser.ParseXMLtoString(fi.FullName);
                int retCode, count = 0;
                retCode = UnsafeNativeMethod.ScanAnsi(text.ToCharArray(), text.Length, ref count);
                if (retCode > 0)
                {
                    WriteToLog(fi.Name, fi.FullName, retCode.ToString(), count);
                    f1.AddToTable(fi.Name, fi.FullName, count, retCode);
                    f1.LoadData();
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
                SevenZipFunctions.SevenZipper(fi.FullName, level + 1);
            }
        }
    }
}
