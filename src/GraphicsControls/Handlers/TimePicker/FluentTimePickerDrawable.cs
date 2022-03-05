using Microsoft.Maui.Controls;
using System;
using System.Globalization;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentTimePickerDrawable : ViewDrawable<ITimePicker>, ITimePickerDrawable
    {
        const float FluentDatePickerHeight = 32.0f;
        const float FluentDatePickerWidth = 250.0f;

        public void DrawBackground(ICanvas canvas, RectF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            if (timePicker.Background != null)
                canvas.SetFillPaint(timePicker.Background, dirtyRect);
            else
            {
                if (timePicker.IsEnabled)
                    canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Background.Default.ToColor() : Fluent.Color.Dark.Control.Background.Default.ToColor();
                else
                    canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Background.Disabled.ToColor() : Fluent.Color.Dark.Control.Background.Disabled.ToColor();
            }

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = FluentDatePickerWidth;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            Color strokeColor;
            var strokeWidth = 1.0f;

            if (timePicker.IsEnabled)
            {
                if (timePicker.TextColor != null)
                    strokeColor = timePicker.TextColor;
                else
                    strokeColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();
            }
            else
                strokeColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Disabled.ToColor() : Fluent.Color.Dark.Control.Border.Disabled.ToColor();

            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = strokeColor;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = FluentDatePickerWidth;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

            canvas.RestoreState();

            var divided = FluentDatePickerWidth / 3;

            canvas.SaveState();
                        
            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = strokeColor;

            canvas.Alpha = 0.5f;

            canvas.DrawLine(new PointF(divided, 0), new PointF(divided, dirtyRect.Height));

            canvas.RestoreState();

            canvas.SaveState();

            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = strokeColor;

            canvas.Alpha = 0.5f;

            canvas.DrawLine(new PointF(divided * 2, 0), new PointF(divided * 2, dirtyRect.Height));

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectF dirtyRect, ITimePicker timePicker)
        {

        }

        public void DrawTime(ICanvas canvas, RectF dirtyRect, ITimePicker timePicker)
        {
            var height = FluentDatePickerHeight;
            var divided = FluentDatePickerWidth / 3;

            var time = timePicker.Time;
            var date = new DateTime(time.Ticks);

            Color textColor;

            if (timePicker.IsEnabled)
                textColor = timePicker.TextColor.WithDefault(Fluent.Color.Light.Foreground.Primary, Fluent.Color.Dark.Foreground.Primary);
            else
                textColor = timePicker.TextColor.WithDefault(Fluent.Color.Light.Foreground.Disabled, Fluent.Color.Dark.Foreground.Disabled);

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

            canvas.DrawString(date.ToString("tt", CultureInfo.InvariantCulture), divided * 2, 0, divided, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, FluentDatePickerHeight);
    }
}