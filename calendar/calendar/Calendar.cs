using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    class Calendar
    {
        public static int[][] GetMothMap(DateTime date)
        {
            var mouthMap = new int[6][].Select(x => new int[7]).ToArray();
            
            DateTime curDate = date.AddDays(-date.Day);

            while ((int) curDate.DayOfWeek != 1)
                curDate = curDate.AddDays(-1);

           return mouthMap.Select(week => 
                                  week.Select(day => getDay(ref curDate, date)).ToArray()
                                  ).ToArray();
        }

        private static int getDay(ref DateTime curDate, DateTime date)
        {
            int day = (curDate.Month == date.Month ? 1 : -1)*curDate.Day;
            curDate = curDate.AddDays(1);
            return day;
        }
    }

    class Calendar_renderer
    {
        //possition
        private static int width, height;
        private static int fontSize;
        //draw
        private static Font drawFont;
        private static SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);
        private static StringFormat drawFormat = new StringFormat();


        public static void Render(Graphics source, DateTime date, int newWidth, int newHeight)
        {
            InitParams(newWidth, newHeight);
            DrawBackground(source);
            DrawMouthInfo(source, date);
            DrawMouthMap(source, date);
        }
        
        private static void InitParams(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;
            fontSize = width/14;
            drawFont = new Font("Arial", fontSize);
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
        }

        private static void DrawBackground(Graphics source)
        {
            SolidBrush brush = new SolidBrush(Color.White);
            source.FillRectangle(brush, new Rectangle(0, 0, width, height));
            source.DrawLine(new Pen(Color.Black, 5), 0, height/4, width, height/4);
            brush.Dispose();
        }

        private static void DrawMouthMap(Graphics source, DateTime date)
        {
            var mouthMap = Calendar.GetMothMap(date);
            for (int week = 0; week < 6; week++)
                for (int day = 0; day < 7; day++)
                    DrawDay(source, mouthMap[week][day], week, day);
        }

        private static void DrawMouthInfo(Graphics source, DateTime date)
        {
            string mouthName = date.ToString("M");
            Point possition = new Point(width / 2, height / 16);
            source.DrawString(mouthName, drawFont, drawBrush, possition.X, possition.Y, drawFormat);

            for(int i =0; i < 7; i++)
                DrawDayName(source, i);
        }

        private static int DrawDayName(Graphics canvas, int i)
        {
            string[] days = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
            string day = days[i];
            Point possition = new Point(width / 7 * i + width/14, height/8 + height/16);
            canvas.DrawString(day, drawFont, drawBrush, possition.X, possition.Y, drawFormat);
            return 0;
        }

        private static void DrawDay(Graphics canvas, int p, int week, int day)
        {
            SolidBrush dayBrush = new SolidBrush(p < 0 ? Color.Gray : Color.Black);
            Point possition = new Point(width / 7 * day + width/16, height / 8 * week + height/4 + height/16);
            canvas.DrawString(Math.Abs(p).ToString(), drawFont, dayBrush, possition.X, possition.Y, drawFormat);
        }
    }
}
