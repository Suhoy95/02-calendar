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
            int width = 80 * 7, height = width/7*8;
            Bitmap img = new Bitmap(width, height);
            Graphics canvas = Graphics.FromImage(img);
            Calendar_renderer.Render(canvas, new DateTime(2014, 11, 12), width, height);
            img.Save("test.png");
        }
    }
}
