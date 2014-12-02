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
        public DateTime Num;
        public Day_type Type;
    }

    struct Calendar_data
    {
        public string Title;
        public DateTime Date;
        public Day[][] DaysMap;
        public string[] DayName;
    }

    class Calendar_data_builder
    {
        public static Calendar_data GetMothMap(DateTime date)
        {
            return new Calendar_data {Title = date.ToString("y"), 
                                      Date = date, 
                                      DaysMap = FillDaysMap(date),
                                      DayName = new string[] { "Пн", "Вт", "Ср", "Чт", "Пт", "Сб", "Вс" }};
        }

        private static Day[][] FillDaysMap(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            var indexFirstDay = ((int)firstDay.DayOfWeek + 6) % 7;
            var countDays = (DateTime.DaysInMonth(firstDay.Year, firstDay.Month) + indexFirstDay+6) / 7 * 7;

            return Enumerable.Range(0, countDays).Select(shift => { var curDate = firstDay.AddDays(shift-indexFirstDay);
                                                                     return new Day{Num = curDate, Type = GetType(curDate,date)}; })
                                                 .GroupBy(day => day.Num.DayOfWeek).Select(week => week.ToArray()).ToArray();
        }

        private static Day_type GetType(DateTime curDate, DateTime date)
        {
           return (curDate.DayOfWeek == DayOfWeek.Sunday || curDate.DayOfWeek == DayOfWeek.Saturday ? Day_type.Rest : 0) |
                  (curDate.Month == date.Month ? Day_type.Active : 0) |
                  (curDate == date ? Day_type.Selected : 0);
        }
    }

    class Calendar_renderer
    {
        //possition
        private static float cellWidth, cellHeight;
        private static int width, height;
        //draw
        private static Font drawFont;
        private static readonly StringFormat stringFormat = new StringFormat { 
                                            Alignment = StringAlignment.Center, 
                                            LineAlignment = StringAlignment.Center };

        public static Bitmap Render(Calendar_data data, int newWidth, int newHeight)
        {
            Bitmap img = new Bitmap(newWidth, newHeight);
            Graphics canvas = Graphics.FromImage(img);

            InitParams(newWidth, newHeight);
            DrawBackground(canvas);
            DrawMouthInfo(canvas, data);
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

        private static void DrawMouthInfo(Graphics source, Calendar_data data)
        {
            source.DrawString(data.Title, drawFont, Brushes.Black, width/2, cellHeight/2, stringFormat);

            for (int i = 0; i < 7; i++)
                DrawDayName(source, data.DayName, i);
        }

        private static int DrawDayName(Graphics canvas,string[] days , int i)
        {
            canvas.DrawString(days[i], drawFont, Brushes.Black, cellWidth*i+cellWidth/2, cellHeight*1.5f, stringFormat);
            return 0;
        }

        private static void DrawMouthMap(Graphics source, Day[][] data)
        {
            for (int day = 0; day < data.Length; day++)
                for (int week = 0; week < data[day].Length; week++)
                    DrawDay(source, data[day][week], week, day);
        }        

        private static void DrawDay(Graphics canvas, Day data, int week, int day)
        {
            if((data.Type & Day_type.Selected) == Day_type.Selected)
                DrawSeletion(canvas, week, day);

            var brush = new SolidBrush(Color.FromArgb( ((data.Type & Day_type.Active) == Day_type.Active ? 255 : 128),
                                                    ((data.Type & Day_type.Rest) == Day_type.Rest ? 255 : 0), 0, 0));
            
            WriteDayNumber(canvas, data.Num.Day, week, day, brush);
        }

        private static void DrawSeletion(Graphics canvas, int week, int day)
        {
            canvas.FillRectangle(Brushes.Orange, day*cellWidth, cellHeight*2+week*cellHeight, cellWidth, cellHeight);
        }

        private static void WriteDayNumber(Graphics canvas, int Number, int week, int day, Brush brush)
        {
            canvas.DrawString(Number.ToString(), drawFont, brush, (day + 0.5f)*cellWidth, (week + 2.5f)*cellHeight, stringFormat);
        }
    }
}
