using Model;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SQLiteTools
    {
        private DAL.MySQLite.MySQLiteTools mySQLiteTools;

        public SQLiteTools()
        {
            mySQLiteTools = new DAL.MySQLite.MySQLiteTools();
        }

        public void Insert(Schedule schedule)
        {
            mySQLiteTools.Insert(schedule);
        }

        public int SearchSchedule(DateTime fromDateTime)
        {
            List<Schedule> results = mySQLiteTools.GetSchedule(fromDateTime);
            return results.Count();
        }
    }
}
