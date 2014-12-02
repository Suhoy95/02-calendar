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
     
        public static int Main(string[] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("Пожалуйста введите дату для формаирования календаря");
                return -1;
            }
            DateTime date = new DateTime();
            if (DateTime.TryParse(args[0], out date))
            {
                var img_data = Calendar_data_builder.GetMothMap(date);
                var img = Calendar_renderer.Render(img_data, 80 * 7, 60 * 8);
                img.Save(string.Format("{0}_{1}_{2}.png", date.Day, date.Month, date.Year));
            }
            else
                Console.WriteLine("Не удалось преобразовать дату");
            return 0;
        }
    }
}
