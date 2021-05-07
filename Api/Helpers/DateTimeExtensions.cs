using System;
using System.Collections.Generic;
using Api.Models;

namespace Api.Helpers
{
    public static class DateTimeExtensions
    {
        public static int GetCurrentAge(this DateTime dateTime)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTime.Year;

            if (currentDate < dateTime.AddYears(age))
                age--;

            return age;
        }

        public static int calculeMonth(int month, int year, params DateTime[] holydays)
        {
            int days = DateTime.DaysInMonth(year, month);

            DateTime startDate = DateTime.Parse($"01-{month}-{year}");

            DateTime endDate = endDate = DateTime.Parse($"{days}-{month}-{year}");

            if (month == 2 && days == 29)
                days--;

            foreach (DateTime holiday in holydays)
            {
                DateTime holiday_date = holiday.Date;
                if (startDate <= holiday_date && holiday_date <= endDate)
                    days--;
            }

            return days;
        }

        public static int calculeYear(int year, params DateTime[] holydays)
        {
            int days = 365;

            DateTime startDate = DateTime.Parse($"01-01-{year}");

            DateTime endDate = endDate = DateTime.Parse($"31-12-{year}");

            foreach (DateTime holiday in holydays)
            {
                DateTime holiday_date = holiday.Date;
                if (startDate <= holiday_date && holiday_date <= endDate)
                    days--;
            }

            return days;
        }

        public static DateTime[] getHolydays(string year)
        {
            string all_year_holydays = JSONHelper.GetJSONString($"https://api.calendario.com.br/?json=true&ano={year}&estado=SP&cidade=SAO_PAULO&token=ZmVsaXBlbWVuZXNlc21lQGhvdG1haWwuY29tJmhhc2g9MTYwMzMwMzQ0");

            Holyday[] holydays = JSONHelper.GetArrayFromJSONString<Holyday>(all_year_holydays);

            List<DateTime> holydaysInMonth = new List<DateTime>();

            foreach (var item in holydays)
            {
                if (item.type_code >= 0 && (item.type.StartsWith("Feriado") || item.type.StartsWith("Facultativo")))
                    holydaysInMonth.Add(DateTime.Parse(item.date.Replace("/", "-")));
            }

            return holydaysInMonth.ToArray();
        }
    }
}
