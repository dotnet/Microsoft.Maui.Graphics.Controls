using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoDatePickerDrawable : ViewDrawable<IDatePicker>, IDatePickerDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            if (datePicker.IsEnabled)
            {
                if (datePicker.Background != null)
                    canvas.SetFillPaint(datePicker.Background, dirtyRect);
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

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
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

        public void DrawDate(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            canvas.FontColor = datePicker.TextColor.WithDefault(Material.Color.Black);
            canvas.FontSize = 14f;

            float margin = 8f;

            var x = dirtyRect.X + margin;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var date = datePicker.Date;

            canvas.DrawString(date.ToShortDateString(), x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {

        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 36f);
    }
}