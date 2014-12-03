using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Calendar_data_builder
    {
        public static Calendar_data GetMothMap(DateTime date, bool selection = true)
        {
            return new Calendar_data
            {
                Title = date.ToString("y"),
                Date = date,
                DaysMap = FillDaysMap(date, selection),
                WeekNumbers = FillWeekNumbers(date),
                DayName = new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" }
            };
        }

        private static int[] FillWeekNumbers(DateTime date)
        {
            var firstNumbers = (GetIndexDay(new DateTime(date.Year, 1, 1).DayOfWeek) + date.DayOfYear - date.Day) / 7 + 1;
            return Enumerable.Range(firstNumbers, 6).ToArray();
        }

        private static Day[][] FillDaysMap(DateTime date, bool selection)
        {
            var firstDayOfMouth = new DateTime(date.Year, date.Month, 1);
            var indexFirstDay = GetIndexDay(firstDayOfMouth.DayOfWeek);
            var firstDay = firstDayOfMouth.AddDays(-indexFirstDay);
            var countDays = (DateTime.DaysInMonth(firstDay.Year, firstDay.Month) + indexFirstDay + 6) / 7 * 7;

            return Enumerable.Range(0, countDays)
                             .Select(index => firstDay.AddDays(index))
                             .Select(curDate => new Day { Num = curDate, Type = GetType(curDate, date, selection) })
                             .Select((x, i) => new { Index = i, day = x })
                             .GroupBy(x => x.Index / 7).Select(week => week.Select(day => day.day).ToArray()).ToArray();
        }

        private static Day_type GetType(DateTime curDate, DateTime date, bool selection)
        {
            return (curDate.DayOfWeek == DayOfWeek.Sunday || curDate.DayOfWeek == DayOfWeek.Saturday ? Day_type.Rest : Day_type.None) |
                  (curDate.Month == date.Month ? Day_type.Active : Day_type.None) |
                  (curDate == date && selection ? Day_type.Selected : Day_type.None);
        }

        private static int GetIndexDay(DayOfWeek day)
        {
            return ((int)day + 6) % 7;
        }
    }
}
