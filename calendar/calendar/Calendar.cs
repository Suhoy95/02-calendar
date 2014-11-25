using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Calendar
    {
        public static int[][] GetMothMap(DateTime date)
        {
            var mouthMap = new int[6][].Select(x => new int[7]).ToArray();
            
            DateTime curDate = date.AddDays(-date.Day);

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

            return mouthMap;
        }

    }
}
