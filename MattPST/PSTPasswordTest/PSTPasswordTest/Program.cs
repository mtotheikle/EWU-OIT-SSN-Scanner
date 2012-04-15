using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PSTPasswordTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a new proccess
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //Path to the executable to run
            proc.StartInfo.FileName = "PstPassword.exe";
            //Command line arguments, paths to output file and .pst
            proc.StartInfo.Arguments = "/stext \"passwords.txt\" /pstfiles \"putyourfilenamehere.pst\"";
            proc.Start();
            proc.WaitForExit();

            System.IO.StreamReader reader = new System.IO.StreamReader("passwords.txt");
            string pw1, pw2, pw3;
            //buffer to dump unwanted character in for reading purposes.
            char[] c = new char[20];
           
            //Read past the first 6 lines
            reader.ReadLine();reader.ReadLine();reader.ReadLine();reader.ReadLine();reader.ReadLine();
            
            //read past Password 1      :
            reader.Read(c, 0, 20);
            //read in password
            pw1 = reader.ReadLine();
            //read past Password 2      :
            reader.Read(c, 0, 20);
            //read in password
            pw2 = reader.ReadLine();
            //read past Password 3      :
            reader.Read(c, 0, 20);
            //read in password
            pw3 = reader.ReadLine();

            Console.WriteLine(pw1 + ", " + pw2 + ", " + pw3);
        }
    }
}
