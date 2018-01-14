using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DateTime
    {
        private int year;
        private int month;
        private int day;
        private int hour;
        private int minute;

        public DateTime(string dateTimeString)
        {
            string[] tempArray = dateTimeString.Split(":");
            for (int i = 0; i < 5; i++)
            {
                if(tempArray[i] == null)
                {
                    tempArray[i] = "0";
                }
            }
            int.TryParse(tempArray[0], out year);
            int.TryParse(tempArray[1], out month);
            int.TryParse(tempArray[2], out day);
            int.TryParse(tempArray[3], out hour);
            int.TryParse(tempArray[4], out minute);
        }

        public DateTime(int _year, int _month, int _day, int _hour, int _minute)
        {
            year = _year;
            month = _month;
            day = _day;
            hour = _hour;
            minute = _minute;
        }

        public DateTime(int _year, int _month, int _day)
        {
            year = _year;
            month = _month;
            day = _day;
            hour = 0;
            minute = 0;
        }

        public string GetDateTimeString()
        {
            return year + ":" + month + ":" + day + ":" + hour + ":" + minute;
        }

        public string GetDateString()
        {
            return year + ":" + month + ":" + day;
        }

        public int GetYear()
        {
            return year;
        }

        public int GetMonth()
        {
            return month;
        }

        public int GetDay()
        {
            return day;
        }

        public int GetHour()
        {
            return hour;
        }

        public int GetMinute()
        {
            return minute;
        }
    }
}
