using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Outlook;

namespace PSTParser
{
    public class Parser
    {
        public static string ParsePstToString(string filePath)
        {
            String pstText = "";
            
            try
            {
                IEnumerable<MailItem> mailItems = ReadPst(filePath);
                foreach (MailItem mailItem in mailItems)
                { 
                    pstText += mailItem.Subject + " " + mailItem.Body;
                  
                }
            }
            catch (System.Exception e)
            {
                pstText = "Exception in PSTParser";
            }

            return pstText;
        }

        private static IEnumerable<MailItem> ReadPst(string pstFilePath)
        {
            List<MailItem> mailItems = new List<MailItem>();
            Application app = new Application();
            NameSpace outlookNameSpace = app.GetNamespace("MAPI");
            // Add PST file (Outlook Data File) to Default Profile
            outlookNameSpace.AddStore(pstFilePath);

            string pstName = null;

            foreach (Store s in outlookNameSpace.Stores)
            {
                if(s.FilePath.Equals(pstFilePath))
                    pstName = s.DisplayName;
            }

            MAPIFolder rootFolder = outlookNameSpace.Stores[pstName].GetRootFolder();
   
            Folders subFolders = rootFolder.Folders;
            foreach (Folder folder in subFolders)
            {
                ExtractItems(mailItems, folder); 
            }
            // Remove PST file from Default Profile
            outlookNameSpace.RemoveStore(rootFolder);
            Console.WriteLine(mailItems.Count);
            return mailItems;
        }

        //recursive method to traverse subfolders.
        private static void ExtractItems(List<MailItem> mailItems, Folder folder)
        {
            Items items = folder.Items;

            int itemcount = items.Count;

            foreach (object item in items)
            {
                if (item is MailItem)
                {
                    MailItem mailItem = item as MailItem;
                    mailItems.Add(mailItem);
                }
            }

            foreach (Folder subfolder in folder.Folders)
            {
                ExtractItems(mailItems, subfolder);
            }
        }

    }
}
