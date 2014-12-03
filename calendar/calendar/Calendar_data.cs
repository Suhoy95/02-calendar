using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    [Flags]
    enum Day_type
    {
        None = 0,
        Active = 1,
        Rest = 2,
        Selected = 4
    }
    struct Day
    {
        public DateTime Num;
        public Day_type Type;
    }

    struct Calendar_data
    {
        public string Title;
        public DateTime Date;
        public Day[][] DaysMap;
        public int[] WeekNumbers;
        public string[] DayName;
    }   
}
