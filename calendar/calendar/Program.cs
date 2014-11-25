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
        //possition 
        private static int width = 50*7, height = 50*8;
        private static int padding = 10;
        private static int fontSize = width / 14 - padding;
        //draw
        private static Font drawFont = new Font("Arial", fontSize);
        private static SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
        private static StringFormat drawFormat = new StringFormat();

        public static void Main(string[] args)
        {
            DateTime date = new DateTime(2012, 12, 12);
            Bitmap img = new Bitmap(width, height);
            Graphics canvas = Graphics.FromImage(img);
            DrawBackground(canvas);
            DrawMouthInfo(canvas, date);
            DrawMouthMap(canvas, date);
            img.Save("test.png");
        }

        private static void DrawMouthMap(Graphics canvas, DateTime date)
        {
            var mouthMap = Calendar.GetMothMap(date);

            for (int week = 0; week < 6; week++)
                for (int day = 0; day < 7; day++)
                    DrawDay(canvas, mouthMap[week][day], week, day);
        }

        private static void DrawDay(Graphics canvas, int p, int week, int day)
        {
            SolidBrush dayBrush = new SolidBrush(p < 0 ? Color.Gray: Color.Black);
            Point possition = new Point(width/7*day+padding/2, height/8*week+100+padding/2);
            canvas.DrawString(Math.Abs(p).ToString(), drawFont, dayBrush, possition.X, possition.Y, drawFormat);
        }

        private static void DrawBackground(Graphics canvas)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            canvas.FillRectangle(brush, new Rectangle(0, 0, width, height));
            brush.Dispose();
        }

        private static void DrawMouthInfo(Graphics canvas, DateTime date)
        {
            string mouthName = date.ToString("M");
            DrawMouthName(canvas, mouthName);
            for (int i = 0; i < 7; i++)
                DrawDayName(canvas, i);
        }

        private static void DrawDayName(Graphics canvas, int i)
        {
            string[] days = {"Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс"};
            string day = days[i];
            Point possition= new Point(width/7*i+padding/2, 50+padding/2);
            canvas.DrawString(day, drawFont, drawBrush, possition.X, possition.Y, drawFormat);
        }

        private static void DrawMouthName(Graphics canvas, string mouthName)
        {
            Point possition = new Point(padding / 2, padding / 2);
            canvas.DrawString(mouthName, drawFont, drawBrush, possition.X, possition.Y, drawFormat);
        }

        public static void WriteMouthMapInConsole()
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
