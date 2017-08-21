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
        public SQLite_iOS()
        {
        }

        public SQLiteConnection GetConnection()
        {
            // variable for sqlite database file
            var sqliteFileName = "Contacts.db3";

            // variable for special folder
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // documents folder

            string libraryPath = Path.Combine(documentsPath); // library folder

            var path = Path.Combine(libraryPath, sqliteFileName);
       
            
            // This is where we copy the prepopulated database
            if (!File.Exists(path))
            {
                File.Copy(sqliteFileName, path);
            }

            var conn = new SQLite.SQLiteConnection(path);

            // return the database connection
            return conn;
        }
    }
}