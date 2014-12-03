using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace calendar
{
    class Calendar_renderer
    {
        //possition
        private static float cellWidth, cellHeight;
        private static int width, height;
        //draw
        private static Font drawFont;
        private static readonly StringFormat stringFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };
        private static SolidBrush RedActive = new SolidBrush(Color.FromArgb(255, 255, 0, 0));
        private static SolidBrush RedNoActive = new SolidBrush(Color.FromArgb(128, 0, 0, 0));

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
            cellWidth = (float)newWidth / 8;
            cellHeight = (float)newHeight / 8;

            var fontSize = Math.Min(cellWidth, cellHeight) / 2;
            drawFont = new Font("Arial", fontSize);
        }

        private static void DrawBackground(Graphics source)
        {
            source.FillRectangle(Brushes.White, new Rectangle(0, 0, width, height));
            source.DrawLine(Pens.Black, 0, cellHeight * 2, width, cellHeight * 2);
            source.DrawLine(Pens.Black, cellWidth, cellHeight, cellWidth, height);
        }

        private static void DrawMouthInfo(Graphics source, Calendar_data data)
        {
            source.DrawString(data.Title, drawFont, Brushes.Black, width / 2, cellHeight / 2, stringFormat);

            for (int i = 0; i < 7; i++)
                DrawDayName(source, data.DayName[i], i);

            for (int i = 0; i < data.DaysMap.Length; i++)
                DrawNumberWeek(source, data.WeekNumbers[i].ToString(), i);
        }

        private static void DrawDayName(Graphics source, string day, int i)
        {
            source.DrawString(day, drawFont, Brushes.Black, cellWidth * i + 1.5f * cellWidth, cellHeight * 1.5f, stringFormat);
        }

        private static void DrawNumberWeek(Graphics source, string number, int i)
        {
            source.DrawString(number, drawFont, Brushes.DarkBlue, cellWidth / 2, cellHeight * 2.5f + cellHeight * i, stringFormat);
        }

        private static void DrawMouthMap(Graphics source, Day[][] data)
        {
            for (int week = 0; week < data.Length; week++)
                for (int day = 0; day < data[week].Length; day++)
                    DrawDay(source, data[week][day], week, day);
        }

        private static void DrawDay(Graphics canvas, Day data, int week, int day)
        {
            if ((data.Type & Day_type.Selected) == Day_type.Selected)
                DrawSeletion(canvas, week, day);

            WriteDayNumber(canvas, data.Num.Day, week, day, GetBrush(data.Type));
        }

        private static Brush GetBrush(Day_type day_type)
        {
            if (day_type.HasFlag(Day_type.Active | Day_type.Rest))
                return RedActive;
            if (day_type.HasFlag(Day_type.Active))
                return Brushes.Black;
            if (day_type.HasFlag(Day_type.Rest))
                return RedNoActive;

            return Brushes.Gray;
        }

        private static void DrawSeletion(Graphics canvas, int week, int day)
        {
            canvas.FillRectangle(Brushes.Orange, (day + 1) * cellWidth, cellHeight * 2 + week * cellHeight, cellWidth, cellHeight);
        }

        private static void WriteDayNumber(Graphics canvas, int Number, int week, int day, Brush brush)
        {
            canvas.DrawString(Number.ToString(), drawFont, brush, (day + 1.5f) * cellWidth, (week + 2.5f) * cellHeight, stringFormat);
        }
    }
}
