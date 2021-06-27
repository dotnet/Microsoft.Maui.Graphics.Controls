using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoTimePickerDrawable : ViewDrawable<ITimePicker>, ITimePickerDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            if (timePicker.IsEnabled)
            {
                if (timePicker.Background != null)
                    canvas.SetFillPaint(timePicker.Background, dirtyRect);
                else
                    canvas.FillColor = Cupertino.Color.Fill.Light.White.ToColor();
            }
            else
                canvas.FillColor = Cupertino.Color.SystemGray.Light.Gray6.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 8);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;

            canvas.StrokeColor = Material.Color.Gray3.ToColor();
            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 8);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {

        }

        public void DrawTime(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            canvas.FontColor = Material.Color.Black.ToColor();
            canvas.FontSize = 14f;

            float margin = 8f;

            var x = dirtyRect.X + margin;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var time = timePicker.Time;
            var date = new DateTime(time.Ticks);

            canvas.DrawString(date.ToString(timePicker.Format), x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 36f);
    }
}