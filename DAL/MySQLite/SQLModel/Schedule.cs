using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.MySQLite.SQLModel
{
    public class Schedule
    {
        [PrimaryKey]// 主键。
        [AutoIncrement]// 自动增长
        public int ID { get; set; }
        public int StartYear { get; set; }//开始时间（年份-分钟）
        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }

        public int EndYear { get; set; }//结束时间（年份-分钟）
        public int EndMonth { get; set; }
        public int EndDay { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }

        public string Name { get; set; }//日程的名称
        public string Address { get; set; }//日程发生的地址
        public string Color { get; set; }//日程显示的颜色
        public string Type { get; set; }//日程的形式、分类，如：课程表、日历、计划

        public string Remark { get; set; }//日程的备注
        public bool Secret { get; set; }//是否不让对方看见，是否保密
        public string ShowAs { get; set; }//在日程发生时显示的状态

        public int RepeatType { get; set; }//1按天、2按周、3按月、4按年、0不重复
        public int RepeatCount { get; set; }//一周、两周、、

        public int RepeatStartYear { get; set; }//重复开始时间（年份-日）
        public int RepeatStartMonth { get; set; }
        public int RepeatStartDay { get; set; }

        public int RepeatEndYear { get; set; }//重复结束时间（年份-日）
        public int RepeatEndMonth { get; set; }
        public int RepeatEndDay { get; set; }
    }
}
