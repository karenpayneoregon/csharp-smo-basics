using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using NuGetPackageHelpers.Classes;

namespace NuGetPackageHelpers
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DumpTextBox.Text = "Will process text in Windows Clipboard";
            DumpTextBox.Select(0, 0);
        }

        private void ProcessButton_Click(object sender, EventArgs e)
        {
            Operations.BuilderPackageTable();
        }
    }
}
