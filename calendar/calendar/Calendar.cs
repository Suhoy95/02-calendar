using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calendar
{
    [Flags]
    enum Day_type
    {
        Active = 1,
        Rest = 2,
        Selected = 4
    }
    struct Day
    {
        public int Num;
        public Day_type Type;
    }

    struct Calendar_data
    {
        public DateTime Date;
        public Day[][] DaysMap;
    }

    class Calendar
    {
        public static Calendar_data GetMothMap(DateTime date)
        {
            Calendar_data data = new Calendar_data();
            data.Date = date;
            data.DaysMap = FillDaysMap(date);
            return data;
        }

        private static Day[][] FillDaysMap(DateTime date)
        {
            DateTime curDate = date.AddDays(-date.Day);
            while ((int)curDate.DayOfWeek != 1)//Находим понедельник перед первым числом данного месяцаы
                curDate = curDate.AddDays(-1);

            return new Day[6][].Select(week =>
                                new Day[7].Select(day => getDay(ref curDate, date)).ToArray()
                                ).ToArray();
        }

        private static Day getDay(ref DateTime curDate, DateTime date)
        {
            Day day = new Day();
            day.Num = curDate.Day;
            day.Type = ((int)curDate.DayOfWeek == 0 || (int)curDate.DayOfWeek == 6 ? Day_type.Rest : 0) | 
                       (curDate.Month == date.Month ? Day_type.Active : 0) | 
                       (curDate == date ? Day_type.Selected : 0);
            curDate = curDate.AddDays(1);
            return day;
        }
    }

    class Calendar_renderer
    {
        //possition
        private static float cellWidth, cellHeight;
        private static int width, height;
        //draw
        private static Font drawFont;
        private static StringFormat stringFormat
        {
            get
            {
                var Format = new StringFormat();
                Format.Alignment = StringAlignment.Center;
                Format.LineAlignment = StringAlignment.Center;
                return Format;
            }
        }
        

        public static Bitmap Render(Calendar_data data, int newWidth, int newHeight)
        {
            Bitmap img = new Bitmap(newWidth, newHeight);
            Graphics canvas = Graphics.FromImage(img);

            InitParams(newWidth, newHeight);
            DrawBackground(canvas);
            DrawMouthInfo(canvas, data.Date.ToString("y"));
            DrawMouthMap(canvas, data.DaysMap);
            
            return img;
        }
        
        private static void InitParams(int newWidth, int newHeight)
        {
            width = newWidth;
            height = newHeight;
            cellWidth = (float)newWidth/7;
            cellHeight = (float)newHeight/8;

            var fontSize = Math.Min(cellWidth, cellHeight)/2;
            drawFont = new Font("Arial", fontSize);
        }

        private static void DrawBackground(Graphics source)
        {
            source.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            source.DrawLine(Pens.Black, 0, cellHeight*2, width, cellHeight*2);
        }

        private static void DrawMouthInfo(Graphics source, String title)
        {
            Point possition = new Point(width / 2, height / 16);
            source.DrawString(title, drawFont, Brushes.Black, possition.X, possition.Y, stringFormat);

            for (int i = 0; i < 7; i++)
                DrawDayName(source, i);
        }
        private static int DrawDayName(Graphics canvas, int i)
        {
            string[] days = { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" };
            Point possition = new Point(width / 7 * i + width / 14, height / 8 + height / 16);
            canvas.DrawString(days[i], drawFont, Brushes.Black, possition.X, possition.Y, stringFormat);
            return 0;
        }

        private static void DrawMouthMap(Graphics source, Day[][] data)
        {
            for (int week = 0; week < data.Length; week++)
                for (int day = 0; day < data[week].Length; day++)
                    DrawDay(source, data[week][day], week, day);
        }        

        private static void DrawDay(Graphics canvas, Day data, int week, int day)
        {
            if((data.Type & Day_type.Selected) == Day_type.Selected)
                DrawSeletion(canvas, week, day);
            var dayBrush = (data.Type & Day_type.Active) == Day_type.Active ? Brushes.Black : Brushes.Gray;
            Point possition = new Point(width / 7 * day + width/16, height / 8 * week + height/4 + height/16);
            canvas.DrawString(Math.Abs(data.Num).ToString(), drawFont, dayBrush, possition.X, possition.Y, stringFormat);
        }

        private static void DrawSeletion(Graphics canvas, int week, int day)
        {
            canvas.FillRectangle(Brushes.Orange, cellHeight*2+week*cellHeight, day*cellWidth, cellWidth, cellHeight);
        }
    }
}
