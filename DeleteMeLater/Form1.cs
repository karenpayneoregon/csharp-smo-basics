using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeleteMeLater
{
    public partial class Form1 : Form
    {
        private BindingSource _customersBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
            Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            _customersBindingSource.DataSource = DataOperations.ReadCustomers();

            FirstNameTextBox.DataBindings.Add("Text", _customersBindingSource, "FirstName");
        }

        private void FindByCompanyNameButton_Click(object sender, EventArgs e)
        {
            var companyName = "Around the Horn";
            var index = _customersBindingSource.Find("CompanyName", "Blauer See Delikatessen");
            if (index > -1)
            {
                _customersBindingSource.Position = index;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dt = DataOperations.Read();
            
            var clone = dt.Clone();

            for (int index = 0; index < dt.Columns.Count; index++)
            {
                var colIndex = index + 1;
                clone.Columns[index].ColumnName = colIndex.ExcelColumnNameFromNumber();
            }

            

            var columns = dt.Columns.OfType<DataColumn>().Select(col => col.ColumnName).ToArray();
            clone.Rows.Add(columns);


            for (int index = 0; index < dt.Rows.Count; index++)
            {
                clone.Rows.Add(dt.Rows[index].ItemArray);
            }



            dataGridView1.DataSource = dt;



            dataGridView2.DataSource = clone;

            //for (int index = 1; index < 40; index++)
            //{
            //    Console.WriteLine(index.ExcelColumnNameFromNumber());
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataOperations.ReadCustomers();

            dataGridViewHeader.ColumnCount = dataGridView1.Columns.Count;
            dataGridViewHeader.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewHeader.EnableHeadersVisualStyles = false;

            for (int index = 0; index < dataGridView1.Columns.Count; index++)
            {
                var colIndex = index + 1;
                dataGridViewHeader.Columns[index].HeaderText = colIndex.ExcelColumnNameFromNumber();
            }
        }
    }
}
