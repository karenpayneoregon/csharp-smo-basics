using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SMO_Library
{
    public class BackUpRestoreOperations
    {
        static Server server;
        static ServerConnection serverConnection;

        public static void BackupDatabase(string serverName, string databaseName, string filePath)
        {
            serverConnection = new ServerConnection {ServerInstance = serverName};

            server = new Server(serverConnection);

            try
            {
                Backup bkp = new Backup {Action = BackupActionType.Database, Database = databaseName};

                bkp.Devices.AddDevice(filePath, DeviceType.File);
                bkp.Incremental = false;

                bkp.SqlBackup(server);

                serverConnection.Disconnect();
                serverConnection = null;
                server = null;
            }

            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }

        public static void RestoreDatabase(string serverName, string databaseName, string filePath)
        {

            serverConnection = new ServerConnection {ServerInstance = serverName};
            server = new Server(serverConnection);

            try
            {
                Restore res = new Restore();

                res.Devices.AddDevice(filePath, DeviceType.File);

                RelocateFile dataFile = new RelocateFile
                {
                    LogicalFileName = res.ReadFileList(server).Rows[0][0].ToString(),
                    PhysicalFileName = server.Databases[databaseName].FileGroups[0].Files[0].FileName
                };

                RelocateFile logFile = new RelocateFile();
                string LDF = res.ReadFileList(server).Rows[1][1].ToString();
                logFile.LogicalFileName = res.ReadFileList(server).Rows[1][0].ToString();
                logFile.PhysicalFileName = server.Databases[databaseName].LogFiles[0].FileName;

                res.RelocateFiles.Add(dataFile);
                res.RelocateFiles.Add(logFile);

                res.Database = databaseName;
                res.NoRecovery = false;
                res.ReplaceDatabase = true;
                res.SqlRestore(server);
                serverConnection.Disconnect();
            }
            catch (SmoException ex)
            {
                throw new SmoException(ex.Message, ex.InnerException);
            }
            catch (IOException ex)
            {
                throw new IOException(ex.Message, ex.InnerException);
            }
        }

    }
}
