using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Program
    {
        static void Main(string[] args)
        {
            Calendar x = new Calendar(new DateTime(2011, 11, 16));
            for (int week = 0; week < 6; week++)
            {
                for(int day = 0; day < 7; day++)
                    Console.Write(string.Format("{0,4}", x.mouthMap[week][day]));
                Console.WriteLine();
            }

        }
    }
}
