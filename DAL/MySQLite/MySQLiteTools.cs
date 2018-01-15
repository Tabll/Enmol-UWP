using DAL.MySQLite.SQLModel;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System.IO;

namespace DAL.MySQLite
{
    public class MySQLiteTools
    {
        private string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Enmol-" + new LocalSettings().GetLocalSettings("Phone") + ".db");
        
        public MySQLiteTools()
        {
            using(var db = GetConnection())
            {
                db.CreateTable<Schedule>();//创建日程表
            }
        }

        public void Insert(Schedule schedule)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                db.CreateTable<SQLModel.Schedule>();
                Schedule item = schedule;
                db.Insert(item);
            }
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformWinRT(), path);
        }
    }
}
