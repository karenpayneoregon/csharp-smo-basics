using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SMO_Library;

namespace WindowsFormsAppSmo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private SmoOperations _operations = new SmoOperations();
        private void cmdLoadDatabaseNames_Click(object sender, EventArgs e)
        {
            //var ops = new SmoOperations();
            lstDatabaseNames.DataSource = _operations.DatabaseNames();
            var Index = lstDatabaseNames.FindString("NorthWindAzure");
            if (Index > -1)
            {
                lstDatabaseNames.SelectedIndex = Index;
                lstTableNames.DataSource = _operations.TableNames(lstDatabaseNames.Text);
                Index = lstTableNames.FindString("Orders");
                if (Index > -1)
                {
                    lstTableNames.SelectedIndex = Index;
                }
            }

            lstDatabaseNames.SelectedIndexChanged += LstDatabaseNames_SelectedIndexChanged;
        }

        private void LstDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstTableNames.DataSource = _operations.TableNames(lstDatabaseNames.Text);
        }
    }
}
