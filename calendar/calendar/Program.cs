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
        private static int width = 800, height = 600;
     
        public static int Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Пожалуйста введите дату для формаирования календаря");
                return -1;
            }
            for(var i = 0; i < args.Length; i++)
            {
                if(args[i][0] == '-')
                {
                    switch(GetParams(args[i]))
                    {
                        case 's':
                            if(++i >= args.Length)
                            {
                                Console.WriteLine("Введите размеры (000X000)");
                                return -1;
                            }
                            var size = args[i].Split(new char[] { 'x', 'X' }).Select(x => int.Parse(x)).ToArray();
                            width = size[0];
                            height = size[1];
                            break;
                        case 'y':
                            if (++i >= args.Length)
                            {
                                Console.WriteLine("Введите год календаря");
                                return -1;
                            }
                            int year;
                            if(int.TryParse(args[i], out year))
                            {
                                for (var j = 1; j <= 12; j++)
                                    CreateCalendarList(new DateTime(year, j, 1), false);
                            }
                            else
                            {
                                Console.WriteLine("Не удалось преобразовать год календаря");
                            }
                            break;
                    }
                } 
                else
                {
                    DateTime date = new DateTime();
                    if (DateTime.TryParse(args[i], out date))
                        CreateCalendarList(date);
                    else
                    {
                        Console.WriteLine("Не удалось преобразовать дату");
                        return -1;
                    }
                    break;
                }
            }
                     
            return 0;
        }

        private static char GetParams(string p)
        {
            return p.Length < 2 ? ' ' : p[1];
        }

        private static void CreateCalendarList(DateTime date, bool selected = true)
        {
            var img_data = Calendar_data_builder.GetMothMap(date, selected);
            var img = Calendar_renderer.Render(img_data, width, height);
            img.Save(string.Format("{0}_{1}_{2}.png", date.Day, date.Month, date.Year));
        }
    }
}
