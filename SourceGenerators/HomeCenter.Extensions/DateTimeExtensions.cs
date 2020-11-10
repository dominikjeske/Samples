using System;

namespace HomeCenter.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns time difference between <paramref name="currentPointInTime"/> and <paramref name="previousPointInTime"/>
        /// </summary>
        /// <param name="currentPointInTime"></param>
        /// <param name="previousPointInTime"></param>
        public static TimeSpan Between(this DateTimeOffset currentPointInTime, DateTimeOffset previousPointInTime) => currentPointInTime - previousPointInTime;

        /// <summary>
        /// Check if <paramref name="measuredTime"/> is smaller or equal then <paramref name="comparedTime"/>
        /// </summary>
        /// <param name="measuredTime"></param>
        /// <param name="comparedTime"></param>
        public static bool LastedLessThen(this TimeSpan measuredTime, TimeSpan comparedTime) => measuredTime <= comparedTime;

        /// <summary>
        /// Check if <paramref name="measuredTime"/> is bigger then <paramref name="comparedTime"/>
        /// </summary>
        /// <param name="measuredTime"></param>
        /// <param name="comparedTime"></param>
        public static bool LastedLongerThen(this TimeSpan measuredTime, TimeSpan comparedTime) => measuredTime > comparedTime;

        /// <summary>
        /// Checks is move is physically possible by person to move from one room to other
        /// </summary>
        /// <param name="measuredTime"></param>
        /// <param name="timeWindow"></param>
        public static bool IsPossible(this TimeSpan measuredTime, TimeSpan timeWindow) => measuredTime >= timeWindow;

        /// <summary>
        /// Increase <paramref name="time"/> by <paramref name="percentage"/>
        /// </summary>
        /// <param name="time"></param>
        /// <param name="percentage"></param>
        public static TimeSpan Increase(this TimeSpan time, double percentage) => TimeSpan.FromTicks(time.Ticks + (long)(time.Ticks * (percentage / 100.0)));

        /// <summary>
        /// Checks if <paramref name="givenTame"/> is between <paramref name="from"/> and <paramref name="until"/>
        /// </summary>
        /// <param name="givenTame"></param>
        /// <param name="from"></param>
        /// <param name="until"></param>
        /// <returns></returns>
        public static bool IsTimeInRange(this TimeSpan givenTame, TimeSpan from, TimeSpan until)
        {
            if (from < until)
            {
                return givenTame >= from && givenTame <= until;
            }

            return givenTame >= from || givenTame <= until;
        }
    }
}