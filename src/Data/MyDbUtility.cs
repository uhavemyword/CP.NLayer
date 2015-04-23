// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 12/24/2012 7:13:16 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Data
{
    using CP.NLayer.Data.Migrations;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.SqlClient;
    using System.IO;
    using System.Xml;

    public class MyDbUtility
    {
        public static void InitializeDatabase(bool force)
        {
            //set the initializer to auto-migration
            Database.SetInitializer<MyDbContext>(new MigrateDatabaseToLatestVersion<MyDbContext, Configuration>());
            //Database.SetInitializer<MyDbContext>(new MyDbInitializer());

            using (var context = new MyDbContext())
            {
                context.Database.Initialize(force);
            }
        }

        /// <summary>
        /// Execute all SQL files under specified folder.
        /// The statements in *.sql should be seperated by Environment.NewLine + "GO"
        /// </summary>
        /// <param name="folderPath"></param>
        public static void ExecuteSqlScripts(string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return;
            }

            using (var context = new MyDbContext())
            {
                var filePaths = Directory.GetFiles(folderPath, "*.sql", SearchOption.AllDirectories);
                foreach (var filePath in filePaths)
                {
                    var sqlText = File.ReadAllText(filePath);
                    //var sqls = sqlText.Split(new string[] { Environment.NewLine + "GO" }, StringSplitOptions.None);

                    //foreach (string sql in sqls)
                    //{
                    //    if (!string.IsNullOrEmpty(sql.Trim()))
                    //    {
                    //        context.Database.ExecuteSqlCommand(sql);
                    //    }
                    //}
                    sqlText = sqlText.Replace(Environment.NewLine + "GO", Environment.NewLine);
                    context.Database.ExecuteSqlCommand(sqlText);
                }
            }
        }

        public static void WriteEdmx(string filePath)
        {
            var context = new MyDbContext();
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            var writer = XmlWriter.Create(filePath, settings);
            EdmxWriter.WriteEdmx(context, writer);
        }

        public static SqlConnection GetConnection()
        {
            var context = new MyDbContext();
            var connection = (SqlConnection)context.Database.Connection;
            return connection;
        }

        public static string GetDatabaseName()
        {
            var name = GetConnection().Database;
            return name;
        }
    }
}