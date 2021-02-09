using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using DotNetCore_OpeningHoursQuestion.Domain.Models;
using DotNetCore_OpeningHoursQuestion.Domain.Services;
using DotNetCore_OpeningHoursQuestion.Extensions;
using static DotNetCore_OpeningHoursQuestion.Domain.Enums.Enumerations;

namespace DotNetCore_OpeningHoursQuestion.Services
{
    public class OpeningHoursService : IOpeningHoursService
    {
        public string FormatOpeningHours(OpeningHours openingHours)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.monday, nameof(Monday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.tuesday, nameof(Tuesday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.wednesday, nameof(Wednesday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.thursday, nameof(Thursday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.friday, nameof(Friday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.saturday, nameof(Saturday)));
            builder.Append(FormatDayOfWeekOpeningHours(openingHours.sunday, nameof(Sunday), openingHours.monday));

            return builder.ToString();
        }

        private string FormatDayOfWeekOpeningHours(IEnumerable<WeekDay> weekDay, string dayName, IEnumerable<WeekDay> nextDay = null)
        {
            bool closesNextDay = false;
            if (weekDay.Count() > 0)
            {
                var dayTimes = weekDay.OrderBy(i => i.value).ToList();
                var response = "";

                for (int i = 0; i < dayTimes.Count(); i++)
                {
                    var currentTime = dayTimes[i];
                    if (i == 0)
                    {
                        if (dayName == nameof(Monday))
                        {
                            if (currentTime.type == WeekDayTypes.close.ToString())
                            {
                                response += (weekDay.Count() <= 1) ? (dayName + ": Closed") : (dayName + ": ");
                                continue;
                            }
                        }
                        if (currentTime.type == WeekDayTypes.close.ToString())
                        {
                            response += " - " + dayTimes[0].value.ToDateTime().ToShortTimeString() + "<br />";
                            response += dayName;
                            response += (weekDay.Count() <= 1) ? ": Closed" : ": ";
                            continue;
                        }
                        else
                            response += dayName + ": ";
                    }
                    if (currentTime.type == WeekDayTypes.open.ToString())
                        response += currentTime.value.ToDateTime().ToShortTimeString();
                    if (currentTime.type == WeekDayTypes.close.ToString())
                    {
                        response += " - " + currentTime.value.ToDateTime().ToShortTimeString();
                        if (i < dayTimes.Count() - 1)
                            response += ", ";
                    }
                    if (i == dayTimes.Count() - 1 && currentTime.type == WeekDayTypes.open.ToString())
                        closesNextDay = true;
                }

                if (!closesNextDay && dayName != nameof(Sunday))
                    response += "<br />";
                if (closesNextDay)
                    if (dayName == nameof(Sunday) && nextDay != null)
                        response += " - " + nextDay.OrderBy(i => i.value)
                            .ToList()[0].value
                            .ToDateTime()
                            .ToShortTimeString();

                return response;
            }
            else
            {
                return dayName + ": Closed<br />";
            }
        }
    }
}