using Model;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                db.Insert(schedule);
            }
        }

        public List<Schedule> GetSchedule(DateTime fromDateTime)
        {
            using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), path))
            {
                DateTime toDateTime = fromDateTime.AddDays(6);
                TableQuery<Schedule> results = db.Table<Schedule>().Where(x => 
                (x.StartYear >= fromDateTime.Year
                && x.StartYear <= toDateTime.Year
                && x.StartMonth >= fromDateTime.Month
                && x.StartMonth <= toDateTime.Month
                && x.StartDay >= fromDateTime.Day
                && x.StartDay <= toDateTime.Day)
                ||(x.EndYear >= fromDateTime.Year
                && x.EndYear <= toDateTime.Year
                && x.EndMonth >= fromDateTime.Month
                && x.EndMonth <= toDateTime.Month
                && x.EndDay >= fromDateTime.Day
                && x.EndDay <= toDateTime.Day));
                List<Schedule> list = results.Select(x => new Schedule {
                    ID = x.ID,
                    StartYear = x.StartYear,
                    StartMonth = x.StartMonth,
                    StartDay = x.StartDay,
                    StartHour = x.StartHour,
                    StartMinute = x.StartMinute,
                    EndYear = x.EndYear,
                    EndMonth = x.EndMonth,
                    EndDay = x.EndDay,
                    EndHour = x.EndHour,
                    EndMinute = x.EndMinute,
                    Name = x.Name,
                    Address = x.Address,
                    Color = x.Color,
                    Type = x.Type,
                    Remark = x.Remark,
                    ShowAs = x.ShowAs,
                }).ToList();
                return list;
            }
                //SELECT
                //*
                //FROM
                //Schedule
                //WHERE
                //(StartYear >= 2018
                //AND StartYear <= 2018
                //AND StartMonth >= 1
                //AND StartMonth <= 2
                //AND StartDay >= 14
                //AND StartDay <= 20)
                //OR(EndYear >= 2018
                //AND EndYear <= 2018
                //AND EndMonth >= 1
                //AND EndMonth <= 2
                //AND EndDay >= 14
                //AND EndDay <= 20)
        }
        //public List<T> Search <T> (string commandString)
        //{
        //    using (var db = new SQLiteConnection(new SQLitePlatformWinRT(), path))
        //    {
        //        //SQLiteCommand command = db.CreateCommand(commandString, GetConnection());
        //        var list = db.Table<Schedule>();
        //        list.Where("");
        //        List<T> scheduleList =  command.ExecuteQuery<T>();
        //        return scheduleList as List<T>;
        //    }
        //}

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(new SQLitePlatformWinRT(), path);
        }
    }
}
