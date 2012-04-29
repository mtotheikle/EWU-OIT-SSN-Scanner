using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace EWUScanner
{
    public partial class ExclusionPaths : Form
    {
        private MainForm mainForm;

        public ExclusionPaths()
        {
            InitializeComponent();
            this.Activate();
            GenerateInitialExclusionList();
        }

        private void GenerateInitialExclusionList()
        {
            int index = dataGridView1.Rows.Add();
            dataGridView1.Rows[index].Cells[0].Value = (Directory.GetParent(System.Environment.GetFolderPath(System.Environment.SpecialFolder.System))).FullName;
        }

        private void AddPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(); //this dialog will allow the user to select a folder path
            //possibly get an array of file names?
            int index = dataGridView1.Rows.Add(); //index will store the index of the newly created row
            dataGridView1.Rows[index].Cells[0].Value = folderBrowserDialog1.SelectedPath; //Cells[0] is okay here, to hard-code, because only excluded paths should be displayed, only that column. Though this could be edited later on.

            //The .NET given folder browser is amazing. I tested it on network folder paths
            //and other variations that I could think of.
        }

        private void DoneWithAddingPaths_Click(object sender, EventArgs e)
        {
            //I decided to use a list because of it's dynamic growth capabilities
            List<string> temp = new List<string>();

            //store the paths into the string[]
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {//rows that were added to the viewer automatically, without any input, will have null as the value
                try
                {
                    temp.Add(dataGridView1.Rows[i].Cells[0].Value.ToString()); //store the paths in the string list
                }
                catch (NullReferenceException)
                {//Since i'm using a list, the null data will not be added to the list. And unlike a string[], I don't have to worry about the size of the list and possibly having unitialized array spots. A list avoids possible null-pointer values.
                    continue;
                }
            }

            mainForm.exclusionPaths = temp; //passing the data to the MainForm via internal string[]..Interesting how it works.

            this.Close(); //close the form.
        }

        private void ExclusionPaths_Shown(object sender, EventArgs e)
        {//this method is to help transfer data between forms.
            //I should add the reference from where I got this.
            mainForm = (MainForm)this.Owner;
        }

        private void HelpExplanation_Click(object sender, EventArgs e)
        {
            string helptext = "Usage:\nTo add a path, either click the add path button, which will open a file-dialog...\nOr manually type the path into a row.\n\nYou can also delete rows by clicking on the row header\nand hitting the Delete key.";

            MessageBox.Show(helptext);
        }
    }
}
