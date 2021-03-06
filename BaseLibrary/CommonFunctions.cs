using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.Sqlite;

namespace BaseLibrary
{
    public static class CommonFunctions
    {
        #region Database
        // This can probably be turned into a class itself 
        // TODO: error handling

        public static SqliteConnection DatabaseOpenConnection(string fileName)
        {
            var dbConnection = new SqliteConnection(string.Format("Data Source={0}", fileName));
            dbConnection.Open();

            return dbConnection;
        }

        public static SqliteCommand DatabaseCreateCommand(SqliteConnection databaseConnection, string command)
        {
            var newCommand = databaseConnection.CreateCommand();
            newCommand.CommandText = command;

            return newCommand;
        }

        #endregion


    }
}
