using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZoosManagementSystem.Core.Util
{
    public class DateTimeComparer
    {
        public static bool CompareDate(DateTime dateToCompare, DateTime date)
        {
            return dateToCompare.Equals(date);
        }

        public static bool CompareDate(DateTime dateToCompare, TimeSpan date)
        {
            if (dateToCompare.Hour == date.Hours)
            {
                if (dateToCompare.Minute == date.Minutes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public static bool CompareDate(DateTime dateToCompare, DateTime date, int minuteTolerance)
        {
            if (!dateToCompare.Equals(date))
            {
                date = date.AddMinutes(minuteTolerance);

                if (!dateToCompare.Equals(date))
                {
                    date = date.AddMinutes(-2 * minuteTolerance);

                    if (!dateToCompare.Equals(date))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;    
            }
        }

        public static bool CompareDate(DateTime dateToCompare, TimeSpan date, int minuteTolerance)
        {
            if (dateToCompare.Hour == date.Hours)
            {
                if (dateToCompare.Minute == date.Minutes)
                {
                    return true;
                }
                else
                {
                    int fromRange = dateToCompare.Minute - minuteTolerance;
                    int toRange = dateToCompare.Minute + minuteTolerance;

                    if (date.Minutes >= fromRange && date.Minutes <= toRange)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }


        public static bool DateInRange(DateTime date, TimeSpan rangeStart, TimeSpan rangeEnd)
        {
            if ((date.Hour >= rangeStart.Hours && date.Hour <= rangeEnd.Hours) || (date.Hour >= rangeStart.Hours && rangeEnd.Hours< rangeStart.Hours))
            {
                if (date.Hour == rangeStart.Hours && date.Hour == rangeEnd.Hours)
                {
                    if (date.Minute >= rangeStart.Hours && date.Minute <= rangeEnd.Minutes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}
