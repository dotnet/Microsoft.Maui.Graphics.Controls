using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentDatePickerDrawable : ViewDrawable<IDatePicker>, IDatePickerDrawable
    {
        const string FluentDatePickerIcon = "M4.5 4.5H5.25V5.25H4.5V4.5ZM6.75 9H7.5V9.75H6.75V9ZM9 4.5H9.75V5.25H9V4.5ZM6.75 4.5H7.5V5.25H6.75V4.5ZM4.5 6H5.25V6.75H4.5V6ZM2.25 6H3V6.75H2.25V6ZM9 6H9.75V6.75H9V6ZM6.75 6H7.5V6.75H6.75V6ZM4.5 7.5H5.25V8.25H4.5V7.5ZM2.25 7.5H3V8.25H2.25V7.5ZM9 7.5H9.75V8.25H9V7.5ZM6.75 7.5H7.5V8.25H6.75V7.5ZM4.5 9H5.25V9.75H4.5V9ZM2.25 9H3V9.75H2.25V9ZM12 0.75V11.25H0V0.75H2.25V0H3V0.75H9V0H9.75V0.75H12ZM0.75 1.5V3H11.25V1.5H9.75V2.25H9V1.5H3V2.25H2.25V1.5H0.75ZM11.25 10.5V3.75H0.75V10.5H11.25Z";
        const float FluentDatePickerHeight = 32.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            if (datePicker.Background != null)
                canvas.SetFillPaint(datePicker.Background, dirtyRect);
            else
                canvas.FillColor = datePicker.IsEnabled ? Fluent.Color.Foreground.White.ToColor() : Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            if (datePicker.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = dirtyRect.Width;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        public void DrawDate(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            if (datePicker.IsEnabled)
                canvas.FontColor = datePicker.TextColor.WithDefault(Fluent.Color.Foreground.Black);
            else
                canvas.FontColor = datePicker.TextColor.WithDefault(Fluent.Color.Foreground.NeutralTertiary);

            canvas.FontSize = 14f;

            float margin = 8f;

            var x = dirtyRect.X + margin;

            var height = FluentDatePickerHeight;
            var width = dirtyRect.Width;

            var date = datePicker.Date;

            canvas.DrawString(date.ToShortDateString(), x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {
            canvas.SaveState();

            var margin = 10;
            canvas.Translate(dirtyRect.Width - (2 * margin), margin);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath(FluentDatePickerIcon);

            canvas.FillColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
            canvas.FillPath(path);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IDatePicker datePicker)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 32f);
    }
}
