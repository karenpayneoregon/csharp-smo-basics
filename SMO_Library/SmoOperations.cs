//using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Sdk.Sfc;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.SqlServer.Management.Common;

namespace SMO_Library 
{
    /// <summary>
    /// Various methods to demo working with smo objects.
    /// </summary>
    /// <remarks>
    /// References needed are under the following folder where may be different
    /// depending on the version of SQL-Server installed, most likely at the 
    /// 130 part.
    /// C:\Program Files\Microsoft SQL Server\130\SDK\Assemblies
    /// 
    /// </remarks>
    public class SmoOperations : BaseExceptionProperties
    {

        /// <summary>
        /// Your server name e.g. could be (local) - does not work for SQLEXPRESS
        /// </summary>
        public string ServerName { get => ".\\SQLEXPRESS"; }
        private Server _mServer;
        public Server Server { get { return _mServer; } }
        public SmoOperations()
        {
            _mServer = InitializeServer();
        }
        Server InitializeServer()
        {
            var connection = new ServerConnection(ServerName);
            var sqlServer = new Server(connection);
            return sqlServer;
        }

        public DataTable AvailableServers() => SmoApplication.EnumAvailableSqlServers(true);

        public List<LocalServer> LocalServers() =>
            SmoApplication.EnumAvailableSqlServers(true).AsEnumerable()
                .Select(row => new LocalServer()
                {
                    Name = row.Field<string>("Name"),
                    Instance = row.Field<string>("Instance"),
                    ServerName = row.Field<string>("Server")
                }).ToList();

        public List<string> DatabaseNames() => _mServer.Databases.OfType<Database>().Select(db => db.Name).ToList();

        /// <summary>
        /// Determine if database exists on the server.
        /// </summary>
        /// <param name="pDatabaseName"></param>
        /// <returns></returns>
        public bool DatabaseExists(string pDatabaseName)
        {
            var databaseNames = new List<string>();

            var item = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(db => db.Name == pDatabaseName);

            if (item != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Return a valid Database based on a database name
        /// </summary>
        /// <param name="pDatabaseName"></param>
        /// <returns></returns>
        public Database GetDatabase(string pDatabaseName) => 
            _mServer.Databases.OfType<Database>().FirstOrDefault(db => db.Name == pDatabaseName);

        /// <summary>
        /// Create a new database
        /// </summary>
        /// <param name="pDatabaseName">Name of new database</param>
        /// <returns></returns>
        public SqlDatabase CreateDatabase(string pDatabaseName)
        {
            var database = new SqlDatabase() { Name = pDatabaseName, Exists = false };

            try
            {
                var db = new Database(_mServer, pDatabaseName);

                db.Create();

                db = _mServer.Databases[pDatabaseName];
                database.Database = db;
                database.Exists = true;

            }
            catch (Exception ex)
            {
                mHasException = true;
                mLastException = ex;
            }

            return database;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pDatabaseName"></param>
        /// <param name="pKill">If true Deletes the specified database and drops any active connection. else drop the database </param>
        /// <returns></returns>
        /// <remarks>
        /// Database.Drop can fail it there is an open connection e.g. running a query
        /// in SSMS or perhaps some code that recently ran did not release it's connection
        /// to the database to be dropped.
        /// </remarks>
        public bool DropDatabase(string pDatabaseName, bool pKill = false)
        {
            bool success = true;

            try
            {
                if (pKill)
                {
                    _mServer.KillDatabase(pDatabaseName);
                }
                else
                {
                    GetDatabase(pDatabaseName).Drop();
                }
                
            }
            catch (Exception ex)
            {
                mHasException = true;
                mLastException = ex;
                success = false;
            }

            return success;
        }
        /// <summary>
        /// Get table names for database
        /// </summary>
        /// <param name="pDatabaseName">Exists SQL-Server database</param>
        /// <returns></returns>
        /// <remarks>System objects/tables are filtered out</remarks>
        public List<string> TableNames(string pDatabaseName)
        {
            var tableNames = new List<string>(); 

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(tbl => tbl.Name == pDatabaseName);

            if (database != null)
            {
                tableNames = database.Tables.OfType<Table>()
                    .Where(tbl => !tbl.IsSystemObject).Select(tbl => tbl.Name).ToList();
            }

            return tableNames;

        }
        public Table GetTableByName(string pDatabaseName, string pTableName)
        {
            Table tblResult = null;

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(tbl => tbl.Name == pDatabaseName);

            if (database != null)
            {
                tblResult = database.Tables.OfType<Table>()
                    .Where(tbl => !tbl.IsSystemObject)
                    .Select(tbl => tbl).FirstOrDefault();
            }

            return tblResult;
        }
        /// <summary>
        /// Does the table exists in the specified database
        /// </summary>
        /// <param name="pDatabaseName">valid SQL-Server database</param>
        /// <param name="pTableName">Table name to see if it exists in pDatabaseName</param>
        /// <returns></returns>
        public bool TableExists(string pDatabaseName, string pTableName)
        {
            bool exists = false;

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(tbl => tbl.Name == pDatabaseName);

            if (database != null)
            {
                exists = (database.Tables.OfType<Table>()
                              .Where(tbl => !tbl.IsSystemObject)
                              .FirstOrDefault(tbl => tbl.Name == pTableName) != null);
            }

            return exists;
        }
        /// <summary>
        /// Get column names for table in database.
        /// </summary>
        /// <param name="pDatabaseName">valid SQL-Server database</param>
        /// <param name="pTableName">Exists table in pDatabaseName</param>
        /// <returns></returns>
        public List<string> TableColumnNames(string pDatabaseName, string pTableName)
        {
            var columnNames = new List<string>();

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(db => db.Name == pDatabaseName);

            if (database != null)
            {
                var table = database.Tables.OfType<Table>().FirstOrDefault(tbl => tbl.Name == pTableName);

                if (table != null)
                {
                    
                    columnNames = table.Columns.OfType<Column>().Select(col => col.Name).ToList();
                }
            }

            return columnNames;
        }
        /// <summary>
        /// An example for creating a database with one table
        /// </summary>
        /// <returns></returns>
        public bool CreateTable(string pDatabaseName)
        {
            try
            {
                SqlDatabase db = CreateDatabase(pDatabaseName);

                var tblCustomer = new Table(db.Database, "Customer");
                var colPrimaryKey = new Column(tblCustomer, "Id", DataType.Int)
                {
                    Identity = true,
                    IdentityIncrement = 1,
                    IdentitySeed = 1,
                    Nullable = false
                };      
                
                tblCustomer.Columns.Add(colPrimaryKey);

                var idxPrimary = new Index(tblCustomer, "PK_Customer_id");
                tblCustomer.Indexes.Add(idxPrimary);

                idxPrimary.IndexedColumns.Add(new IndexedColumn(idxPrimary, colPrimaryKey.Name));
                idxPrimary.IsClustered = true;
                idxPrimary.IsUnique = true;
                idxPrimary.IndexKeyType = IndexKeyType.DriPrimaryKey;

                var colFirstName = new Column(tblCustomer, "FirstName", DataType.NVarCharMax)
                {
                    Nullable = true
                };
                tblCustomer.Columns.Add(colFirstName);

                var colLastName = new Column(tblCustomer, "LastName", DataType.NVarCharMax)
                {
                    Nullable = true
                };
                tblCustomer.Columns.Add(colLastName);

                var colBirthDate = new Column(tblCustomer, "BirthDate", DataType.Date)
                {
                    Nullable = true
                } ;
                tblCustomer.Columns.Add(colBirthDate);


                tblCustomer.Create();

                return true;

            }
            catch (Exception ex)
            {
                mHasException = true;
                mLastException = ex;
                return false;
            }
        }
        /// <summary>
        /// Return default server name
        /// </summary>
        /// <returns></returns>
        public string GetDefaultServerName()
        {
            return _mServer.Name;
        }
        public string DefaultServerName()
        {
            var connection = new ServerConnection();
            return connection.TrueName;

        }
        /// <summary>
        /// Return SQL-Server install path
        /// </summary>
        /// <returns></returns>
        public string SqlServerInstallPath()
        {
            return _mServer.RootDirectory;
        }
        /// <summary>
        /// Does a column name exists in a table within a specific database
        /// </summary>
        /// <param name="pDatabaseName">valid SQL-Server database</param>
        /// <param name="pTableName">Exists table in pDatabaseName</param>
        /// <param name="pColumnName">Column to check if it exists in pTableName in pDatabaseName</param>
        /// <returns></returns>
        public bool ColumnExists(string pDatabaseName, string pTableName, string pColumnName)
        {
            bool exists = false;

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(db => db.Name == pDatabaseName);

            if (database != null)
            {
                var table = database.Tables.OfType<Table>().FirstOrDefault(tbl => tbl.Name == pTableName);
                if (table != null)
                {
                    exists = (table.Columns.OfType<Column>().FirstOrDefault(col => col.Name == pColumnName) != null);
                }
            }

            return exists;
        }
        /// <summary>
        /// Get details for each column in a table within a database.
        /// There are more details then returned here so feel free to explore.
        /// </summary>
        /// <param name="pDatabaseName">valid SQL-Server database</param>
        /// <param name="pTableName">Exists table in pDatabaseName</param>
        /// <returns></returns>
        public List<ColumnDetails> GetColumnDetails(string pDatabaseName, string pTableName)
        {
            var columnDetails = new List<ColumnDetails>();
            var columnNames = new List<string>();

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(db => db.Name == pDatabaseName);

            if (database != null)
            {
                var table = database.Tables.OfType<Table>().FirstOrDefault(tbl => tbl.Name == pTableName);

                if (table != null)
                {
                    columnDetails = table.Columns.OfType<Column>().Select(col => new ColumnDetails()
                    {
                        Identity = col.Identity,
                        DataType = col.DataType,
                        Name = col.Name,
                        InPrimaryKey = col.InPrimaryKey,
                        Nullable = col.Nullable
                    }).ToList();
                }
            }

            return columnDetails;
        }
        /// <summary>
        /// Get foreign key details for specified table in specified database
        /// </summary>
        /// <param name="pDatabaseName">valid SQL-Server database</param>
        /// <param name="pTableName">Exists table in pDatabaseName</param>
        /// <returns></returns>
        public List<ForeignKeysDetails> TableKeys(string pDatabaseName, string pTableName)
        {
            var keyList = new List<ForeignKeysDetails>();

            var database = _mServer.Databases.OfType<Database>()
                .FirstOrDefault(db => db.Name == pDatabaseName);

            var table = database?.Tables.OfType<Table>().FirstOrDefault(tbl => tbl.Name == pTableName);

            if (table != null)
            {
                foreach (Column item in table.Columns.OfType<Column>())
                {
                    var fkds = item.EnumForeignKeys().AsEnumerable().Select(row => new ForeignKeysDetails
                    {
                        TableSchema = row.Field<string>("Table_Schema"),
                        TableName = row.Field<string>("Table_Name"),
                        SchemaName = row.Field<string>("Name")
                    }).ToList();

                    foreach (ForeignKeysDetails ts in fkds)
                    {
                        keyList.Add(ts);
                    }
                }
            }

            return keyList;

        }
    }
}
