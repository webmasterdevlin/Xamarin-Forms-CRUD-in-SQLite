using SQLite;
using SQLiteOnly.Droid;
using SQLiteOnly.Persistence;
using System.IO;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(SQLite_Android))]

namespace SQLiteOnly.Droid
{
    internal class SQLite_Android : ISQLite
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, "sqliteonly.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}