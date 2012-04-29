using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace EWUScanner
{
    public partial class InitializingForm : Form
    {
        private string[] dots = {".", "..", "..."};
        private int i = 0;

        public InitializingForm()
        {
            InitializeComponent();
            dotTimer.Start();
        }

        private void dotTimer_Tick(object sender, EventArgs e)
        {
            lblDots.Text = dots[i];
            i++;
            if (i == 3)
                i = 0;
        }

        private void InitializingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
