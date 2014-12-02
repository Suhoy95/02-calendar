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
     
        public static void Main(string[] args)
        {
            var date = new DateTime(2014, 11, 14);
            var img_data = Calendar_data_builder.GetMothMap(date);
            var img = Calendar_renderer.Render(img_data, 80*7, 60*8);
            img.Save(string.Format("{0}_{1}_{2}.png", date.Day, date.Month, date.Year));
        }
    }
}
