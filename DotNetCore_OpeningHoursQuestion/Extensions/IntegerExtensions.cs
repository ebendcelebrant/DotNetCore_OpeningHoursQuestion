using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore_OpeningHoursQuestion.Extensions
{
    public static class IntegerExtensions
    {
        public static DateTime ToDateTime(this int i)
        {
            DateTime baseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            DateTime utcDateTime = baseDateTime.AddSeconds(i).ToLocalTime();
            return utcDateTime;
        }
    }
}