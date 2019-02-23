using System;

namespace WebApplication.Shared
{
    public static class FormatterExtension
    {
        public static string FormatTimes(this int count)
        {
            if (count == 0)
            {
                return "无人";
            }
            else if (count >= 1000)
            {
                return $"{count / 1000d}千次 ";
            }

            return $"{count}次 ";
        }

        public static string FormatTimeAgo(this DateTime dateTime)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - dateTime.Ticks);
            var delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "1 秒前" : ts.Seconds + " 秒前";

            if (delta < 2 * MINUTE)
                return "1 分钟前";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " 分钟前";

            if (delta < 90 * MINUTE)
                return "1 小时前";

            if (delta < 24 * HOUR)
                return ts.Hours + " 小时前";

            if (delta < 48 * HOUR)
                return "昨天";

            if (delta < 30 * DAY)
                return ts.Days + " 天前";

            if (delta < 12 * MONTH)
            {
                var months = Convert.ToInt32(Math.Floor((double) ts.Days / 30));
                return months <= 1 ? "1 个月前" : months + " 月前";
            }
            else
            {
                var years = Convert.ToInt32(Math.Floor((double) ts.Days / 365));
                return years <= 1 ? "1 年前" : years + " 年前";
            }
        }
    }
}