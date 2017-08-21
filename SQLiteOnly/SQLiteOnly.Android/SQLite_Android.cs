using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using SQLiteOnly.Droid;
using SQLiteOnly.Persistence;
using Environment = System.Environment;

[assembly: Dependency(typeof(SQLite_Android))]
namespace SQLiteOnly.Droid
{
    class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFileName = "Contacts.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // documents folder

            string libraryPath = Path.Combine(documentsPath); // Library folder

            var path = Path.Combine(libraryPath, sqliteFileName);

            // This is where we copy in the prepopulated database

            if (!File.Exists(path))
            {
                File.Copy(sqliteFileName, path);
            }

            var conn = new SQLite.SQLiteConnection(path);

            // Return the database connection
            return conn;
        }
    }
}