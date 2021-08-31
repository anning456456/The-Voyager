using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FrameworkExtension
{
	public static class TimeUtility
	{
	    public static long Time1970Ms = new DateTime(1970, 1, 1, 0, 0, 0).Ticks;

        private static TimeSpan tempTimeSpan;

        /// <summary>
        /// 毫秒时间
        /// </summary>
        public  static long currentTimeMillis
        {
            get
            {
                return (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
            }
        }

        public static long GetTimeMillis(DateTime dateTime)
        {
            return (long)TimeSpan.FromTicks(dateTime.Ticks).TotalMilliseconds;
        }

        /// <summary>
        /// 秒时间
        /// </summary>
        public static long currentTimeSecs
        {
            get
            {
                return (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalSeconds;
            }
        }

        public static long GetTimeSecs(DateTime dateTime)
        {
            return (long)TimeSpan.FromTicks(dateTime.Ticks).TotalSeconds;
        }

        public static DateTime MillisToDateTime(long timeMillis)
        {
            return DateTime.MinValue.AddMilliseconds(timeMillis);
        }

        public static DateTime SecsToDateTime(long timeSecs)
        {
            return DateTime.MinValue.AddSeconds(timeSecs);
        }

        public static DateTime GetToday()
        {
            return DateTime.Now;
        }

        public static string SecToMinSec(long sec)
        {
            return string.Format("{0}:{1}", sec / 60, sec % 60);
        }

        public static string SecToTime(int sec) {
            tempTimeSpan = new TimeSpan(0, 0, sec);
            return tempTimeSpan.ToString(@"mm\:ss");
        }

        public static string SecToHour(int sec)
        {
            tempTimeSpan = new TimeSpan(0, 0, sec);
            return tempTimeSpan.ToString(@"hh\:mm\:ss");
        }

        public static bool IsToday(long sec) {
            return GetToday().Date == SecsToDateTime(sec).Date;
        }

        public static bool BigToday(long sec)
        {
            return GetToday().Date < SecsToDateTime(sec).Date;
        }

        public static bool SmallToday(long sec)
        {
            return GetToday().Date > SecsToDateTime(sec).Date;
        }

    }
}
