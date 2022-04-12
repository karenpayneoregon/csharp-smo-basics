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

        private readonly SmoOperations _operations = new SmoOperations();

        private void cmdLoadDatabaseNames_Click(object sender, EventArgs e)
        {

            DatabaseNamesListBox.DataSource = _operations.DatabaseNames();

            /*
             * If Karen's computer there is a database NorthWindAzure
             * (there is a script for this database in the root of the solution.)
             */
            if (Environment.UserName == "PayneK")
            {
                var index = DatabaseNamesListBox.FindString("NorthWindAzure");

                if (index > -1)
                {
                    DatabaseNamesListBox.SelectedIndex = index;
                    TableNamesListBox.DataSource = _operations.TableNames(DatabaseNamesListBox.Text);
                    index = TableNamesListBox.FindString("Orders");

                    if (index > -1)
                    {
                        TableNamesListBox.SelectedIndex = index;
                    }
                }
            }


            DatabaseNamesListBox.SelectedIndexChanged += LstDatabaseNames_SelectedIndexChanged;
        }

        private void LstDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableNamesListBox.DataSource = _operations.TableNames(DatabaseNamesListBox.Text);
        }

        private void IsSqlClrEnabledButton_Click(object sender, EventArgs e)
        {
            _operations.IsSqlClrEnabled();
        }
    }
}
