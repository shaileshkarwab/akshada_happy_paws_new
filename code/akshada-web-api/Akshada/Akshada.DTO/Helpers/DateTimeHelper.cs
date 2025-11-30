using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Helpers
{
    public static class DateTimeHelper
    {
        public static System.DateTime ConverDateTimeToTime(string inputDateTime)
        {
            if (DateTime.TryParseExact(inputDateTime, "hh:mm tt", CultureInfo.InvariantCulture,
                                   DateTimeStyles.None, out DateTime parsedTime))
            {
                // Combine with MinValue date
                DateTime dateTime = DateTime.MinValue.Date
                                    .AddHours(parsedTime.Hour)
                                    .AddMinutes(parsedTime.Minute);

                return dateTime;
            }
            else
            {
                return System.DateTime.Now;
            }
        }

        public static System.DateTime GetDate(string inputDate)
        {
            return DateTime.ParseExact(inputDate, new[] { "dd-MM-yyyy", "dd-MM-yyyy hh:mm:ss tt", "d/M/yyyy", "d/M/yyyy h:mm:ss tt", "dd/MM/yyyy", "dd/MM/yyyy h:mm:ss tt" }, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static System.DateOnly GetDateOnly(string inputDate)
        {
            return DateOnly.ParseExact(inputDate, new[] { "M/dd/yyyy", "yyyy/MM/d", "dd-MM-yyyy", "dd-MM-yyyy hh:mm:ss tt", "d/M/yyyy", "d/M/yyyy h:mm:ss tt" }, System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None);
        }

        public static System.DateTime ConvertTimeStringToDate(string timeStr)
        {
            DateTime timeOnly = DateTime.ParseExact(timeStr,new[] { "h:mm tt", "M/d/yyyy h:mm:ss tt" }, null);
            DateTime result = new DateTime(
                DateTime.MinValue.Year,
                DateTime.MinValue.Month,
                DateTime.MinValue.Day,
                timeOnly.Hour,
                timeOnly.Minute,
                timeOnly.Second
            );

            return result;
        }
    }
}
