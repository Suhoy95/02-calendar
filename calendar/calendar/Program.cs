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
            var img_data = Calendar.GetMothMap(date);
            var img = Calendar_renderer.Render(img_data, 20*7, 65*8);
            img.Save(date.ToString("dd MMMM yyyy") + ".png");
        }
    }
}
