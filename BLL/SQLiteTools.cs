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

        public void Insert(Model.Schedule schedule)
        {
            mySQLiteTools.Insert(schedule);
        }

    }
}
