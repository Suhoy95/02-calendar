using System;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace calendar
{
    class Program
    {
        private static Size size = new Size(800, 600);
     
        public static int Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Недостаточно аргументов. Введите '-h' для справки");
                return -1;
            }
            for(var i = 0; i < args.Length; i++)
            {
                if(args[i][0] == '-')
                {
                    switch(GetParams(args[i]))
                    {
                        case 's': changeSize(args, ++i);
                            break;
                        case 'y': printCalendar(args, ++i);
                            break;
                        case 'h': printHelp();
                            break;
                    }
                    continue;
                }                 
                createList(args[i]);
            }
                     
            return 0;
        }

        private static void printHelp()
        {
            Console.WriteLine("Для получения странниц календаря вводите даты через пробел");
            Console.WriteLine("'-s[ize] ???x???' - установить размеры изображения");
            Console.WriteLine("'-y[ear] ????' - создать календарь на год");
        }

        private static void createList(string dateString)
        {
            DateTime date;
            if (DateTime.TryParse(dateString, out date))
                CreateCalendarList(date);
            else
                Console.WriteLine("Не удалось преобразовать дату");
        }

        private static void printCalendar(string[] args, int i)
        {
            if (i >= args.Length)
            {
                Console.WriteLine("После '-y' введите год календаря");
                return;
            }
            int year;
            if (int.TryParse(args[i], out year))
            {
                for (var j = 1; j <= 12; j++)
                    CreateCalendarList(new DateTime(year, j, 1), false);
            }
            else
            {
                Console.WriteLine("Не удалось преобразовать год календаря");
            }
        }

        private static void changeSize(string[] args, int i)
        {
            if (i >= args.Length)
            {
                Console.WriteLine("После '-s' введите размеры (000X000)");
                return;
            }
            var newSize = args[i].Split(new char[] { 'x', 'X' }).Select(x => int.Parse(x)).ToArray();
            size = new Size(newSize[0], newSize[1]);
        }

        private static char GetParams(string p)
        {
            return p.Length < 2 ? ' ' : p[1];
        }

        private static void CreateCalendarList(DateTime date, bool selected = true)
        {
            var img_data = Calendar_data_builder.GetMothMap(date, selected);
            var img = Calendar_renderer.Render(img_data, size);
            img.Save(string.Format("{0}_{1}_{2}.png", date.Day, date.Month, date.Year));
        }
    }
}
