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
        }
    }
}
