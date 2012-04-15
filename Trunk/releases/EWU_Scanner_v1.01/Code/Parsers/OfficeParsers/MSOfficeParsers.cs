using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SevenZip;
using XMLParser;

namespace SevenZip
{
    class MSOfficeParsers
    {
     
        public static String docxParser(String filename)
        {
            //path to the systems temporary folder
            String tempFolderPath = Path.GetTempPath();

            //set the path of the 7z.dll (it needs to be in the debug folder)
            SevenZipExtractor.SetLibraryPath("7z.dll");

            SevenZipExtractor extractor = new SevenZipExtractor(filename);

            //create a filestream for the file we are going to extract
            FileStream f = new FileStream(tempFolderPath + "document.xml", FileMode.Create);

            //extract the document.xml
            extractor.ExtractFile("word\\document.xml", f);

            //get rid of the object because it is unmanaged
            extractor.Dispose();

            //close the filestream
            f.Close();

            //send document.xml that we extracted from the .docx to the xml parser
            String result = XMLParser.Parser.ParseXMLtoString(tempFolderPath + "document.xml");
       
            //delete the extracted file from the temp folder
            File.Delete(tempFolderPath + "document.xml");

            return result;
        
        }

        public static String xlsxParser(String filename)
        {
            //path to the systems temporary folder
            String tempFolderPath = Path.GetTempPath();
            tempFolderPath += "excelTemp\\";
            //create a directory to dump everything into inside the temp folder
            Directory.CreateDirectory(tempFolderPath);
     

            //set the path of the 7z.dll (it needs to be in the debug folder)
            SevenZipExtractor.SetLibraryPath("7z.dll");

            SevenZipExtractor extractor = new SevenZipExtractor(filename);
            
            //Extract the entrie excel file
            extractor.ExtractArchive(tempFolderPath);
            extractor.Dispose();
            
            //Count how many sheets there are in the workbook
            int count = Directory.GetFiles(tempFolderPath + "xl\\worksheets", "*.xml", SearchOption.TopDirectoryOnly).Length;

            //Create an array of filenames based off how many sheets we counted
            String[] files = new String[count];
            for (int i = 1; i <= count; i++)
            {
                files[i-1] = "xl\\worksheets\\sheet" + i + ".xml"; 
            }

            //send the worksheets to the xml parser
            String result = "";
            foreach(String s in files)
                result += " " + XMLParser.Parser.ParseXMLtoString(tempFolderPath + s);

            result += " " + XMLParser.Parser.ParseXMLtoString(tempFolderPath + "xl\\sharedStrings.xml"); 

            //delete the temporary directory we created at the beginning
            Directory.Delete(tempFolderPath, true);

            return result;
        }

        public static String pptxParser(String filename)
        {
            //path to the systems temporary folder
            String tempFolderPath = Path.GetTempPath();
            tempFolderPath += "excelTemp\\";
            //create a directory to dump everything into inside the temp folder
            Directory.CreateDirectory(tempFolderPath);

            //set the path of the 7z.dll (it needs to be in the debug folder)
            SevenZipExtractor.SetLibraryPath("7z.dll");

            SevenZipExtractor extractor = new SevenZipExtractor(filename);

            //Extract the entrie excel file
            extractor.ExtractArchive(tempFolderPath);
            extractor.Dispose();

            //Count how many slides there are in the presentation
            int count = Directory.GetFiles(tempFolderPath + "ppt\\slides", "*.xml", SearchOption.TopDirectoryOnly).Length;

            //Create an array of filenames based off how many slides we counted
            String[] files = new String[count];
            for (int i = 1; i <= count; i++)
            {
                files[i - 1] = "ppt\\slides\\slide" + i + ".xml";
            }

            //send the slides to the xml parser
            String result = "";
            foreach (String s in files)
                result += " " + XMLParser.Parser.ParseXMLtoString(tempFolderPath + s);

            //delete the temporary directory we created at the beginning
            Directory.Delete(tempFolderPath, true);

            return result;
        }
    }
}
