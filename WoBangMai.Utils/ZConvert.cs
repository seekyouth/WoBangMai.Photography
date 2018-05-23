using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace WoBangMai.Utils
{
    public class ZConvert
    {
        /// <summary>
        /// 转换为string类型 defult为string.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToString(object obj)
        {
            string result = "";
            if (obj != null)
            {
                result = obj.ToString();
            }
            return result;
        }


        /// <summary>
        /// 转换为bool类型 defult为string.Empty
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBool(object obj)
        {
            bool result = false;
            if (obj != null)
                bool.TryParse(obj.ToString(), out result);

            return result;
        }



        /// <summary>
        /// 转换为int类型 defult为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(object obj)
        {
            int result = 0;
            if (obj != null)
            { int.TryParse(obj.ToString(), out result); }
            return result;
        }

        /// <summary>
        /// 转换为int类型 defult为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(object obj)
        {
            DateTime result = DateTime.Now;
            if (obj != null)
            {
                DateTime.TryParse(obj.ToString(), out result);
                return result;
            }
            return null;

        }


        /// <summary>
        /// 转换为decimal类型 defult为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object obj)
        {
            decimal result = 0;
            if (obj != null)
                decimal.TryParse(obj.ToString(), out result);

            return result;
        }


        /// <summary>
        /// 获得字符串的长度,一个汉字的长度为1
        /// </summary>
        public static int GetStringLength(string s)
        {
            if (!string.IsNullOrEmpty(s))
                return Encoding.Default.GetBytes(s).Length;
            return 0;
        }

        public static string GetSecondDateTime(int Second)
        {
            TimeSpan ts = new TimeSpan(0, 0, 0, Second);
            string d = ts.Days.ToString().Length <= 1 ? ("0" + ts.Days.ToString()) : ts.Days.ToString();
            string H = ts.Hours.ToString().Length <= 1 ? ("0" + ts.Hours.ToString()) : ts.Hours.ToString();
            string M = ts.Minutes.ToString().Length <= 1 ? ("0" + ts.Minutes.ToString()) : ts.Minutes.ToString();
            string S = ts.Seconds.ToString().Length <= 1 ? ("0" + ts.Seconds.ToString()) : ts.Seconds.ToString();

            return string.Format("{0} {1}:{2}:{3}", d, H, M, S);

        }

        public static string GetMinuteDateTime(int Minute)
        {
            TimeSpan ts = new TimeSpan(0, 0, Minute, 0);
            string d = ts.Days.ToString().Length <= 1 ? ("0" + ts.Days.ToString()) : ts.Days.ToString();
            string H = ts.Hours.ToString().Length <= 1 ? ("0" + ts.Hours.ToString()) : ts.Hours.ToString();
            string M = ts.Minutes.ToString().Length <= 1 ? ("0" + ts.Minutes.ToString()) : ts.Minutes.ToString();
            string S = ts.Seconds.ToString().Length <= 1 ? ("0" + ts.Seconds.ToString()) : ts.Seconds.ToString();

            return string.Format("{0} {1}:{2}:{3}", d, H, M, S);
        }


        /// <summary>
        /// 将时间格式（00:00:00）转换为秒
        /// </summary>
        /// <param name="time"> 时间格式字符串</param>
        /// <returns> 秒数</returns>
        public static int ConvertTimeToSecond(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                return 0;
            }
            string[] str = time.Split(':');
            int h = 0;
            int m = 0;
            int s = 0;
            if (!int.TryParse(str[0], out h))
            {
            }
            if (!int.TryParse(str[1], out m))
            {
            }
            if (!int.TryParse(str[2], out s))
            {
            }
            h = h * 3600;
            m = m * 60;
            s = h + m + s;
            return s;
        }

        /// <summary>
        /// 将时间格式（00:00:00）转换为分
        /// </summary>
        /// <param name="time"> 时间格式字符串</param>
        /// <returns> 秒数</returns>
        public static int ConvertTimeToMinute(string time)
        {
            if (string.IsNullOrWhiteSpace(time))
            {
                return 0;
            }
            string[] str = time.Split(':');
            int h = 0;
            int m = 0;
            int s = 0;
            if (!int.TryParse(str[0], out h))
            {
            }
            if (!int.TryParse(str[1], out m))
            {
            }
            if (!int.TryParse(str[2], out s))
            {
            }
            h = h * 3600;
            m = m * 60;
            s = h + m + s;
            return (s % 60 == 0 ? (s / 60) : (s / 60) + 1);
        }

        /// <summary>
        ///  月份差(同月份返回1)
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static int GetDateDiffMonth(string DateTime1, string DateTime2)
        {
            try
            {
                DateTime startTime = (DateTime)ToDateTime(DateTime1);
                DateTime endTime = (DateTime)ToDateTime(DateTime2);
                return (endTime.Month - startTime.Month) + 1;
            }
            catch
            {
                return 0;
            }
        }


        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                string hours = ts.Hours.ToString(), minutes = ts.Minutes.ToString(), seconds = ts.Seconds.ToString();
                if (ts.Hours < 10)
                {
                    hours = "0" + ts.Hours.ToString();
                }
                if (ts.Minutes < 10)
                {
                    minutes = "0" + ts.Minutes.ToString();
                }
                if (ts.Seconds < 10)
                {
                    seconds = "0" + ts.Seconds.ToString();
                }
                dateDiff = hours + ":" + minutes + ":" + seconds;
            }
            catch
            {

            }
            return dateDiff;
        }
    }

}