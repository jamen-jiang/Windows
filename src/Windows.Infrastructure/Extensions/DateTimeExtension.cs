using System;

namespace Windows.Infrastructure.Extensions
{
    public static class DateTimeExtension
    {
        private static DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
        /// <summary>
        /// 时间戳转成时间类型
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TimeStamp2DateTime(this object timeStamp)
        {
            if (timeStamp == null) return dateStart;
            return dateStart.AddMilliseconds(Convert.ToInt64(timeStamp));
        }
        /// <summary>
        /// 时间类型转成时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string DateTime2TimeStamp(this DateTime time)
        {
            return Convert.ToInt64(time.Subtract(dateStart).TotalMilliseconds).ToString();
        }
    }
}
