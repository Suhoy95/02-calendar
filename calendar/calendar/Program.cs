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
            var img_data = Calendar.GetMothMap(new DateTime(2014,11,14));
            var img = Calendar_renderer.Render(img_data, 50*7, 25*8);
            img.Save("test.png");
        }
    }
}
