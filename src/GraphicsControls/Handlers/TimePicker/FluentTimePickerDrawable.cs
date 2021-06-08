using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentTimePickerDrawable : ViewDrawable<ITimePicker>, ITimePickerDrawable
    {
        const float FluentTimePickerHeight = 32.0f;
        const float FluentDatePickerWidth = 250.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            if (timePicker.IsEnabled)
                canvas.FillColor = timePicker.BackgroundColor.WithDefault(Fluent.Color.Foreground.White);
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = FluentDatePickerWidth;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            if (timePicker.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = FluentDatePickerWidth;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();

                var divided = FluentDatePickerWidth / 3;

                canvas.SaveState();

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                canvas.Alpha = 0.5f;

                canvas.DrawLine(new PointF(divided, 0), new PointF(divided, dirtyRect.Height));

                canvas.RestoreState();

                canvas.SaveState();

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                canvas.Alpha = 0.5f;

                canvas.DrawLine(new PointF(divided * 2, 0), new PointF(divided * 2, dirtyRect.Height));

                canvas.RestoreState();
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {

        }

        public void DrawTime(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            var height = FluentTimePickerHeight;
            var divided = FluentDatePickerWidth / 3;

            var time = timePicker.Time;
            var date = new DateTime(time.Ticks);

            Color textColor;

            if (timePicker.IsEnabled)
                textColor = Fluent.Color.Foreground.Black.ToColor();
            else
                textColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

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

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 32f);
    }
}