using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace calendar
{
    class Program
    {
        static void Main(string[] args)
        {
           
        }

        static void WriteMouthMapInConsole()
        {
            var x = Calendar.GetMothMap(new DateTime(2011, 11, 16));
            for (int week = 0; week < 6; week++)
            {
                for (int day = 0; day < 7; day++)
                    Console.Write(string.Format("{0,4}", x[week][day]));
                Console.WriteLine();
            } 
        }
    }
}
