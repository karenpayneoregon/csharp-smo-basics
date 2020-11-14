using System;
using System.Linq;
using System.Windows.Forms;

namespace DeleteMeLater
{
    public static class Extensions
    {

        public static string ExcelColumnNameFromNumber(this int pIndex)
        {
            var chars = Enumerable.Range(0, 26).Select((i) => 
                ((char)(Convert.ToInt32('A') + i)).ToString()).ToArray();

            pIndex -= 1;

            string columnName = null;
            var quotient = pIndex / 26;

            if (quotient > 0)
            {
                columnName = ExcelColumnNameFromNumber(quotient) + chars[pIndex % 26];
            }
            else
            {
                columnName = chars[pIndex % 26].ToString();
            }

            return columnName;

        }
        public static string PadBoth(this string sender, int length)
        {
            int spaces = length - sender.Length;
            int padLeft = spaces / 2 + sender.Length;
            return sender.PadLeft(padLeft).PadRight(length);
        }
        public static void ExpandColumns(this DataGridView sender)
        {
            foreach (DataGridViewColumn col in sender.Columns)
            {
                //col.HeaderText = string.Join(" ", System.Text.RegularExpressions.Regex.Split(col.HeaderText, "(?=[A-Z])"));
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}