using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Calendar
    {
        /**
         * Класс описывающий сетку месяца
         * date - выделенный день
         * mouthMap - карта месяца, каждое число - день месяца
         */
        public DateTime date;
        public int[][] mouthMap;

        public Calendar(DateTime newdate)
        {
            date = newdate;
            mouthMap = new int[6][].Select(x => new int[7]).ToArray();
            
            DateTime curDate = newdate.AddDays(-newdate.Day);
            while ((int) curDate.DayOfWeek != 1)
                curDate = curDate.AddDays(-1);

            for (int week = 0; week < 6; week++)
            {
                for (int day = 0; day < 7; day++)
                {
                    mouthMap[week][day] = curDate.Month == date.Month ? curDate.Day : - curDate.Day;
                    curDate = curDate.AddDays(1);
                }
            }
        }

    }
}
