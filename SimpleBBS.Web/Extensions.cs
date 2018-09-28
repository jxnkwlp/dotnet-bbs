using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBBS.Web
{
    public static class Extensions
    {
        public static string ToHumanityString(this DateTime dt)
        {
            var now = DateTime.Now;
            var s = now - dt;
            if (s.TotalSeconds < 10)
            {
                return "刚刚";
            }
            else if (s.TotalSeconds < 60)
            {
                return "1分钟前";
            }
            else if (s.TotalMinutes <= 60)
            {
                return $"{(int)s.TotalMinutes}分钟前";
            }
            else if (s.TotalHours < 24)
            {
                return $"{(int)s.TotalHours}小时前";
            }
            else if (s.TotalDays <= 30)
            {
                return $"{(int)s.TotalDays}天前";
            }
            else if (s.TotalDays <= 60)
            {
                return $"1个月前";
            }
            else if (s.TotalDays <= 90)
            {
                return $"2个月前";
            }
            else if (s.TotalDays <= 120)
            {
                return $"3个月前";
            }
            else if (s.TotalDays <= 150)
            {
                return $"4个月前";
            }
            else if (s.TotalDays <= 180)
            {
                return $"5个月前";
            }
            else if (s.TotalDays <= 210)
            {
                return $"6个月前";
            }

            return "半年前";
        }
    }
}
