namespace CP.NLayer.Common
{
    using Microsoft.SqlServer.Management.Common;
    using Microsoft.SqlServer.Management.Smo;
    using System.Data.SqlClient;
    using System.IO;

    //TODO: PercentComplete not working on Microsoft SQL Server 2008 R2 (RTM) - 10.50.1600.1 (X64)
    public class DbManager
    {
        #region backup/restore, refers to http://msdn.microsoft.com/en-us/library/ms162133.aspx

        public void BackupDatabase(SqlConnection connection, string dbName, string filePath)
        {
            Utility.EnsureFloderWritable(Path.GetDirectoryName(filePath));
            using (connection)
            {
                Server srv = new Server(new ServerConnection(connection));
                Database db = GetDatabase(srv, dbName);

                // Define a Backup object variable.
                Backup bk = new Backup();

                //overwriting any existing backup sets on the media.
                bk.Initialize = true;

                // Specify the type of backup, the description, the name, and the database to be backed up.
                bk.Action = BackupActionType.Database;
                //bk.BackupSetDescription = "Full backup of Adventureworks2012";
                //bk.BackupSetName = "AdventureWorks2012 Backup";
                bk.Database = db.Name;

                // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.
                BackupDeviceItem bdi = default(BackupDeviceItem);
                bdi = new BackupDeviceItem(filePath, DeviceType.File);

                // Add the device to the Backup object.
                bk.Devices.Add(bdi);
                // Set the Incremental property to False to specify that this is a full database backup.
                bk.Incremental = false;

                //// Set the expiration date.
                //System.DateTime backupdate = new System.DateTime();
                //backupdate = new System.DateTime(2006, 10, 5);
                //bk.ExpirationDate = backupdate;

                // Specify that the log must be truncated after the backup is complete.
                bk.LogTruncation = BackupTruncateLogType.Truncate;

                bk.PercentCompleteNotification = 1;
                bk.PercentComplete += Backup_PercentComplete;
                bk.Complete += Backup_Complete;

                // Run SqlBackup to perform the full database backup on the instance of SQL Server.
                bk.SqlBackup(srv);
            }
        }

        public delegate void BackupPercentCompleteHandler(double percent);

        public delegate void BackupCompleteHandler(string message);

        public event BackupPercentCompleteHandler BackupPercentComplete;

        public event BackupCompleteHandler BackupComplete;

        private void Backup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            var handler = BackupPercentComplete;
            if (handler != null)
            {
                handler(e.Percent);
            }
        }

        private void Backup_Complete(object sender, ServerMessageEventArgs e)
        {
            var handler = BackupComplete;
            if (handler != null)
            {
                handler(e.Error == null ? string.Empty : e.Error.Message);
            }
        }

        public void RestoreDatabase(SqlConnection connection, string dbName, string filePath)
        {
            using (connection)
            {
                connection.Open();
                Server srv = new Server(new ServerConnection(connection));
                Database db = GetDatabase(srv, dbName);
                //srv.KillAllProcesses(db.Name);

                // Define a Restore object variable.
                Restore rs = new Restore();

                if (db.UserAccess != DatabaseUserAccess.Single)
                {
                    db.UserAccess = DatabaseUserAccess.Single;
                    db.Alter(TerminationClause.RollbackTransactionsImmediately);
                    db.Refresh();
                }

                try
                {
                    rs.Action = RestoreActionType.Database;
                    rs.ReplaceDatabase = true;
                    //rs.Restart = true;

                    // Set the NoRecovery property to true, so the transactions are not recovered.
                    rs.NoRecovery = false;
                    //http://social.msdn.microsoft.com/Forums/sqlserver/en-US/fe8dd4c4-3550-48cd-baaf-fae9246ff87a/smo-restore-unable-to-complete-restore?forum=sqlsmoanddmo

                    // Specify the database name.
                    rs.Database = db.Name;

                    // Declare a BackupDeviceItem by supplying the backup device file name in the constructor, and the type of device is a file.
                    BackupDeviceItem bdi = default(BackupDeviceItem);
                    bdi = new BackupDeviceItem(filePath, DeviceType.File);

                    // Add the device that contains the full database backup to the Restore object.
                    rs.Devices.Add(bdi);

                    var dataFile = new RelocateFile
                    {
                        LogicalFileName = rs.ReadFileList(srv).Rows[0][0].ToString(),
                        PhysicalFileName = srv.Databases[dbName].FileGroups[0].Files[0].FileName
                    };

                    var logFile = new RelocateFile
                    {
                        LogicalFileName = rs.ReadFileList(srv).Rows[1][0].ToString(),
                        PhysicalFileName = srv.Databases[dbName].LogFiles[0].FileName
                    };

                    rs.RelocateFiles.Add(dataFile);
                    rs.RelocateFiles.Add(logFile);

                    connection.ChangeDatabase("master"); // You cannot restore a database that you are connected to

                    rs.PercentCompleteNotification = 1;
                    rs.PercentComplete += Restore_PercentComplete;
                    rs.Complete += Restore_Complete;

                    // Restore the full database backup with no recovery.
                    rs.SqlRestore(srv); //SqlRestoreAsync won't fire Complete event ?
                }
                finally
                {
                    db.UserAccess = DatabaseUserAccess.Multiple;
                    db.Alter(TerminationClause.RollbackTransactionsImmediately);
                    db.Refresh();
                }
            }
        }

        public delegate void RestorePercentCompleteHandler(double percent);

        public event RestorePercentCompleteHandler RestorePercentComplete;

        private void Restore_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            var handler = RestorePercentComplete;
            if (handler != null)
            {
                handler(e.Percent);
            }
        }

        public delegate void RestoreCompleteHandler(string message);

        public event RestoreCompleteHandler RestoreComplete;

        private void Restore_Complete(object sender, ServerMessageEventArgs e)
        {
            var handler = RestoreComplete;
            if (handler != null)
            {
                handler(e.Error == null ? string.Empty : e.Error.Message);
            }
        }

        private Database GetDatabase(Server srv, string dbName)
        {
            Database db = default(Database);
            for (int i = 0; i < srv.Databases.Count; i++)
            {
                // TODO: if the provider is localdb, the dbName might be the file path of attached db.
                if (srv.Databases[i].Name.Contains(dbName))
                {
                    db = srv.Databases[i];
                    break;
                }
            }
            return db;
        }

        #endregion
    }
}