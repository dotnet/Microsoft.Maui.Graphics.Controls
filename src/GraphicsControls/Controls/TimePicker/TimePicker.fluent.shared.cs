using System;
using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;

namespace GraphicsControls
{
    public partial class TimePicker
    {
        const float FluentTimePickerHeight = 32.0f;
        const float FluentDatePickerWidth = 250.0f;

        void DrawFluentTimePickerBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Foreground.White, Fluent.Color.Foreground.NeutralPrimaryAlt);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = FluentDatePickerWidth;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        void DrawFluentTimePickerBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralSecondary, Fluent.Color.Foreground.White);
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = FluentDatePickerWidth;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();

                var divided = FluentDatePickerWidth / 3;

                canvas.SaveState();

                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralSecondary, Fluent.Color.Foreground.White);
                canvas.StrokeSize = strokeWidth;

                canvas.Alpha = 0.5f;

                canvas.DrawLine(new PointF(divided, 0), new PointF(divided, dirtyRect.Height));

                canvas.RestoreState();

                canvas.SaveState();

                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralSecondary, Fluent.Color.Foreground.White);
                canvas.StrokeSize = strokeWidth;

                canvas.Alpha = 0.5f;

                canvas.DrawLine(new PointF(divided * 2, 0), new PointF(divided * 2, dirtyRect.Height));

                canvas.RestoreState();
            }
        }

        void DrawFluentTimePickerDate(ICanvas canvas, RectangleF dirtyRect)
        {
            var height = FluentTimePickerHeight;
            var divided = FluentDatePickerWidth / 3;
            var date = new DateTime(Time.Ticks);

            Color textColor;

            if (IsEnabled)
                textColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
            else
                textColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralTertiary, Fluent.Color.Foreground.White);

            float fontSize = 14f;

            canvas.SaveState();

            canvas.FontColor = textColor;
            canvas.FontSize = fontSize;

            canvas.DrawString(date.ToString("HH"), 0, 0, divided, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();

            canvas.SaveState();

            canvas.FontColor = textColor;
            canvas.FontSize = fontSize;

            canvas.DrawString(date.ToString("mm"), divided, 0, divided, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();

            canvas.SaveState();

            canvas.FontColor = textColor;
            canvas.FontSize = fontSize;

            canvas.DrawString(date.ToString("tt"), divided * 2, 0, divided, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}