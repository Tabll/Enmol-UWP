using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace Model
{
    public class Schedule2
    {
        private DateTime startDateTime;//开始时间（年份-分钟）
        private DateTime endDateTime;//结束时间（年份-分钟）
        private string scheduleName;//日程的名称
        private string scheduleAddress;//日程发生的地址
        private Color scheduleColor;//日程显示的颜色

        private string scheduleType;//日程的形式、分类，如：课程表、日历、计划
        private string scheduleRemark;//日程的备注
        private bool scheduleSecret = false;//是否不让对方看见，是否保密
        private string scheduleShowAs;//在日程发生时显示的状态

        private int repeatType;//1按天、2按周、3按月、4按年、0不重复
        private int repeatCount;//一周、两周、、
        private DateTime repeatStartDateTime;
        private DateTime repeatEndDateTime;
        
        public Schedule2(DateTime _startDateTime, DateTime _endDateTime, string _scheduleName, string _scheduleAddress, Color _scheduleColor)
        {
            startDateTime = _startDateTime;
            endDateTime = _endDateTime;
            scheduleName = _scheduleName;
            scheduleAddress = _scheduleAddress;
            scheduleColor = _scheduleColor;
        }

        public void SetScheduleOtherInfo(string _scheduleType, string _scheduleRemark, bool _scheduleSecret, string _scheduleShowAs)
        {
            scheduleType = _scheduleType;
            scheduleRemark = _scheduleRemark;
            scheduleSecret = _scheduleSecret;
            scheduleShowAs = _scheduleShowAs;
        }

        public void SetScheduleRepeat(int _repeatType, int _repeatCount, DateTime _repeatStartDateTime, DateTime _repeatEndDateTime)
        {
            repeatType = _repeatType;
            repeatCount = _repeatCount;
            repeatStartDateTime = _repeatStartDateTime;
            repeatEndDateTime = _repeatEndDateTime;
        }
    }
}
