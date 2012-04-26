using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EWUScanner
{
    public partial class ExclusionPaths : Form
    {
        private MainForm mainForm;

        public ExclusionPaths()
        {
            InitializeComponent();
        }

        private void AddPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            //possibly get the array of file names?
            int index = dataGridView1.Rows.Add(); //index will store the index of the newly created row
            dataGridView1.Rows[index].Cells[0].Value = folderBrowserDialog1.SelectedPath; //Cells[0] is okay here, to hard-code, because only excluded paths should be displayed, not some cells. Though this could be edited later on.

            //The .NET given folder browser is amazing. I tested it on network folder paths
            //and other variations I could think of.
        }

        private void DoneWithAddingPaths_Click(object sender, EventArgs e)
        {

            //Read the rows and store them into a string[]
            string[] temp = new string[dataGridView1.Rows.Count]; //create a string[] as big as the number of rows

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                temp[i] = dataGridView1.Rows[i].Cells[0].Value.ToString(); //store the paths in the string array
            }

            mainForm.exclusionPaths = temp; //passing the data to the MainForm via internal string[]..Interesting how it works.

            this.Close(); //close the form.
        }

        private void ExclusionPaths_Shown(object sender, EventArgs e)
        {
            mainForm = (MainForm)this.Owner;
        }
    }
}
