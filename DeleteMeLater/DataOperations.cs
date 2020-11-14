using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DeleteMeLater
{
    public class DataOperations
    {
        private static string ConnectionString = 
            "Data Source=.\\SQLEXPRESS;Initial Catalog=NorthWind2020;Integrated Security=True";

        public static DataTable ReadCustomers()
        {
            var customerTable = new DataTable();

            var selectStatement =
                "SELECT Cust.CustomerIdentifier, Cust.CompanyName, Cust.ContactId, C.FirstName, C.LastName " + 
                "FROM Customers AS Cust INNER JOIN Contacts AS C ON Cust.ContactId = C.ContactId;";

            using (var cn = new SqlConnection {ConnectionString = ConnectionString})
            {
                using (var cmd = new SqlCommand {Connection = cn, CommandText = selectStatement})
                {
                    cn.Open();
                    customerTable.Load(cmd.ExecuteReader());
                }
            }

            return customerTable;
        }
        public static DataTable Read()
        {
            var customerTable = new DataTable();

            var selectStatement =
                "SELECT TOP 4 CONVERT(varchar(100), Cust.CustomerIdentifier) as CustomerIdentifier, " + 
                "Cust.CompanyName, CONVERT(varchar(100), Cust.ContactId) as ContactId, C.FirstName, C.LastName " +
                "FROM Customers AS Cust INNER JOIN Contacts AS C ON Cust.ContactId = C.ContactId;";

            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn, CommandText = selectStatement })
                {
                    cn.Open();
                    customerTable.Load(cmd.ExecuteReader());
                }

                for (int index = 0; index < customerTable.Columns.Count; index++)
                {
                    customerTable.Columns[index].AllowDBNull = true;
                }
            }

            return customerTable;
        }
    }
}
