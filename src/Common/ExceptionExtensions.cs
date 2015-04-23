// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/25/2013 2:14:55 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;

    public static class ExceptionExtensions
    {
        /// <summary>
        /// Iterates the current exception stack and returns Sql Errors from all applicable Sql Exceptions
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>An enumeration of the <see cref="SqlError"/> from all applicable <see cref="SqlException"/> instances </returns>
        public static IEnumerable<SqlError> AllSqlErrors(this Exception exception)
        {
            if (exception != null)
            {
                var sqlExceptions = exception.ExceptionStack().OfType<SqlException>();
                return sqlExceptions.SelectMany(e => e.Errors.Cast<SqlError>());
            }

            return Enumerable.Empty<SqlError>();
        }

        public static bool IsDeleteStatementConflictedWithReference(this Exception exception)
        {
            return exception
                .AllSqlErrors()
                .Any(e =>
                        (e.Number == 547 && e.Message.IndexOf("delete", StringComparison.OrdinalIgnoreCase) >= 0
                        && e.Message.IndexOf("reference", StringComparison.OrdinalIgnoreCase) >= 0));
        }

        /// <summary>
        /// Checks if a given exception represents a Unique Constraint Violation with a given name
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <param name="name">Constraint Name or part of it</param>
        /// <returns>True if unique constraint violation was detected. False otherwise</returns>
        public static bool IsUniqueConstraintViolation(this Exception exception, string name = null)
        {
            return exception
                    .AllSqlErrors()
                    .Any(e =>
                            (e.Number == 2627 || e.Number == 2601) /* SELECT * FROM master.dbo.sysmessages */ &&
                            (string.IsNullOrWhiteSpace(name) || e.Message.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0));
        }

        /// <summary>
        /// Gets the Table & column name from unique violation error.
        /// The SQL indexer name must follow naming convention: [PK|FK|IX]_<ColumName>
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <returns>Tuple(TableName, columnName) or null</returns>
        public static Tuple<string, string> GetUniqueViolationInfo(this Exception exception)
        {
            if (exception.IsUniqueConstraintViolation())
            {
                //2627 is unique constraint (includes primary key)
                //2627 Error Message: Violation of PRIMARY KEY constraint 'PK_Name'. Cannot insert duplicate key in object 'dbo.User'. The duplicate key value is (abc).
                //2601 is unique index
                //2601 Error Message: Cannot insert duplicate key row in object 'dbo.User' with unique index 'IX_Name'. The duplicate key value is (abc).
                //Note: The table and column sequence can be different in different language.
                Regex r = new Regex(@"'(?<name1>[\w\W]+)'[\w\W]+'(?<name2>[\w\W]+)'", RegexOptions.None);
                var message = exception.MostInnerException().Message.Replace("“", "'").Replace("”", "'").Replace("\"", "'");
                Match m = r.Match(message);
                if (m.Success)
                {
                    var indexName = string.Empty;
                    var fullTableName = string.Empty;
                    var name1 = m.Groups["name1"].Value;
                    string convertion = "PK_|FK_|IX_";
                    bool isName1IndexName = false;
                    convertion.Split(new char[] { '|' }).ToList().ForEach(x => isName1IndexName = name1.StartsWith(x, StringComparison.OrdinalIgnoreCase));
                    if (isName1IndexName)
                    {
                        indexName = m.Groups["name1"].Value;
                        fullTableName = m.Groups["name2"].Value;
                    }
                    else
                    {
                        indexName = m.Groups["name2"].Value; //IX_Name
                        fullTableName = m.Groups["name1"].Value; //dbo.User
                    }

                    var columnName = indexName.Substring(indexName.IndexOf('_') + 1, indexName.Length - indexName.IndexOf('_') - 1); //Name
                    var tableName = fullTableName.Substring(fullTableName.LastIndexOf('.') + 1, fullTableName.Length - fullTableName.LastIndexOf('.') - 1); //User
                    return new Tuple<string, string>(tableName, columnName);
                }
            }

            return null;
        }

        /// <summary>
        /// Checks if a given exception represents that entities may have been modified or deleted since entities were loaded.
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <returns>True if concurrency exception was detected. False otherwise</returns>
        public static bool IsDbConcurrencyException(this Exception exception)
        {
            return exception.ExceptionStack().OfType<OptimisticConcurrencyException>().Count() > 0
                    || exception.ExceptionStack().OfType<DBConcurrencyException>().Count() > 0;
        }

        /// <summary>
        /// Gets the most inner exception associated with a given exception instance
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <returns>Most inner exception</returns>
        public static Exception MostInnerException(this Exception exception)
        {
            var next = exception;
            while (next.InnerException != null)
            {
                next = next.InnerException;
            }
            return next;
        }

        /// <summary>
        /// Generates flat list of inner exceptions associated with a given exception instance
        /// </summary>
        /// <param name="exception">Exception instance</param>
        /// <returns>Enumeration of inner exceptions</returns>
        public static IEnumerable<Exception> InnerExceptions(this Exception exception)
        {
            var next = exception.InnerException;
            while (next != null)
            {
                yield return next;
                next = next.InnerException;
            }

            yield break;
        }

        /// <summary>
        /// Iterates the current exception and all its inner exceptions.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>An enumeration of the exception and all its inner exceptions</returns>
        public static IEnumerable<Exception> ExceptionStack(this Exception exception)
        {
            yield return exception;

            foreach (var ex in exception.InnerExceptions())
            {
                yield return ex;
            }

            yield break;
        }

        public static Exception FindDatabaseException(this Exception exception)
        {
            return exception.ExceptionStack().FirstOrDefault(ex => ex is DbException || ex is DataException);
        }

        /// <summary>
        /// Gets response status from Web Exception
        /// </summary>
        /// <param name="exception">Original <see cref="WebException"/></param>
        /// <returns>Response Status Code</returns>
        public static int GetResponseStatus(this WebException exception)
        {
            if (exception != null && exception.Response != null)
            {
                var webResponse = exception.Response as HttpWebResponse;
                if (webResponse != null)
                {
                    return (int)webResponse.StatusCode;
                }
            }

            return -1;
        }
    }
}