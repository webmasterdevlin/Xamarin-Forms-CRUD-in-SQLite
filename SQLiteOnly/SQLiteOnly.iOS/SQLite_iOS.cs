using SQLite;
using SQLiteOnly.iOS;
using System;
using System.IO;
using SQLiteOnly.Persistence;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace SQLiteOnly.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "sqliteonly.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}