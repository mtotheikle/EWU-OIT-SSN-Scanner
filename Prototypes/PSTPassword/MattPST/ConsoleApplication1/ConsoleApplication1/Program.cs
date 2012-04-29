using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PSTParser;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamWriter writer = File.AppendText("log.txt");
                writer.WriteLine(PSTParser.Parser.ParsePstToString(@"C:\Users\Cyrus\Desktop\test.pst"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
