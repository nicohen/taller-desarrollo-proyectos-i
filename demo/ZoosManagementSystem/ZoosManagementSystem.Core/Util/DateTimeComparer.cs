using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagementSystem.Core.Util
{
    public class DateTimeComparer
    {
        public static int CompareDate(DateTime dateToCompare, DateTime date)
        {
            return 0;
        }

        public static int CompareDate(DateTime dateToCompare, TimeSpan date)
        {
            return 0;
        }

        public static int CompareDate(DateTime dateToCompare, DateTime date, int minuteTolerance)
        {
            return 0;
        }

        public static int CompareDate(DateTime dateToCompare, TimeSpan date, int minuteTolerance)
        {
            return 0;
        }

        public static bool DateInRange(DateTime date, TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            if (date.Hour >= rangeStart.Hours && date.Hour <= rangeEnd.Hours)
            {
                if (date.Minute >= rangeStart.Hours && date.Minute <= rangeEnd.Minutes)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
