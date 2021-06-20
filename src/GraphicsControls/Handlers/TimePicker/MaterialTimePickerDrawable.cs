using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialTimePickerDrawable : ViewDrawable<ITimePicker>, ITimePickerDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            if (timePicker.Background != null)
                canvas.SetFillPaint(timePicker.Background, dirtyRect);
            else
                canvas.FillColor = timePicker.IsEnabled ? Material.Color.Gray5.ToColor() : Material.Color.Gray3.ToColor();

            var width = dirtyRect.Width;

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    $"M0 4C0 1.79086 1.79086 0 4 0H{width - 4}C{width - 2}.209 0 {width} 1.79086 {width} 4V56H0V4Z");

            canvas.FillPath(path);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;
            canvas.FillColor = Material.Color.Black.ToColor();

            var x = dirtyRect.X;
            var y = 53.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            if (timePicker.TextColor == null)
                canvas.FontColor = Material.Color.Gray1.ToColor();
            else
                canvas.FontColor = timePicker.TextColor.WithAlpha(0.75f);
            
            canvas.FontSize = 12f;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            if (timePicker.FlowDirection == FlowDirection.RightToLeft)
            {
                x = dirtyRect.X;
                horizontalAlignment = HorizontalAlignment.Right;
            }

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString("Time", x, 6f, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }

        public void DrawTime(ICanvas canvas, RectangleF dirtyRect, ITimePicker timePicker)
        {
            canvas.SaveState();

            canvas.FontColor = timePicker.TextColor.WithDefault(Material.Color.Dark);
            canvas.FontSize = 16f;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            if (timePicker.FlowDirection == FlowDirection.RightToLeft)
            {
                x = dirtyRect.X;
                horizontalAlignment = HorizontalAlignment.Right;
            }

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var time = timePicker.Time;
            var date = new DateTime(time.Ticks);

            canvas.DrawString(date.ToString(timePicker.Format), x, 22f, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 56f);
    }
}