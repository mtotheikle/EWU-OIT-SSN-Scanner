using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EPocalipse.IFilter;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace IFilterTester
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      if (openFileDialog1.ShowDialog()==DialogResult.OK)
      {
        TextReader reader=new FilterReader(openFileDialog1.FileName);
        using (reader)
        {
          textBox1.Text=reader.ReadToEnd();
          label1.Text="Text loaded from "+openFileDialog1.FileName;
        }
      }
    }
  }
}