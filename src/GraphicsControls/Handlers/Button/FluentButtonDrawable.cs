namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            var defaultBackgroundColor = Fluent.Color.Primary.ThemePrimary.ToColor();

            if (button.Background != null && button.Background is SolidPaint solidPaint)
                defaultBackgroundColor = solidPaint.Color;

            var disabledColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var backgroundColor = button.IsEnabled ? defaultBackgroundColor : disabledColor;

            var border = new LinearGradientPaint
            {
                GradientStops = new GradientStop[]
                {
                    new GradientStop(0.0f, backgroundColor.Lighter()),
                    new GradientStop(0.9f, backgroundColor.Darker())
                },
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1)
            };

            canvas.SetFillPaint(border, dirtyRect);

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();

            canvas.SaveState();

            if (button.IsEnabled)
            {
                canvas.StrokeColor = Colors.Black;

                if (button.Background != null)
                    canvas.SetFillPaint(button.Background, dirtyRect);
                else
                    canvas.FillColor = defaultBackgroundColor;
            }
            else
                canvas.StrokeColor = canvas.FillColor = disabledColor;
 
            var strokeWidth = 1;
            float margin = strokeWidth * 2;
            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 4);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            if (button.IsEnabled)
                canvas.FontColor = button.TextColor.WithDefault(Fluent.Color.Foreground.White);
            else
                canvas.FontColor = Fluent.Color.Foreground.NeutralPrimary.ToColor();

            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(button.Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 32f);
    }
}