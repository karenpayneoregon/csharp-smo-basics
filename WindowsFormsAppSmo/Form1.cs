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

            lstDatabaseNames.DataSource = _operations.DatabaseNames();

            /*
             * If Karen's computer there is a database NorthWindAzure
             */
            if (Environment.UserName == "PayneK")
            {
                var index = lstDatabaseNames.FindString("NorthWindAzure");

                if (index > -1)
                {
                    lstDatabaseNames.SelectedIndex = index;
                    lstTableNames.DataSource = _operations.TableNames(lstDatabaseNames.Text);
                    index = lstTableNames.FindString("Orders");

                    if (index > -1)
                    {
                        lstTableNames.SelectedIndex = index;
                    }
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
