namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            var strokeWidth = 1;

            if (VirtualView.IsEnabled)
            {
                canvas.StrokeColor = Colors.Black;
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Fluent.Color.Primary.ThemePrimary);
            }
            else
            {
                var disabledColor = Fluent.Color.Background.NeutralLighter.ToColor();
                canvas.StrokeColor = canvas.FillColor = disabledColor;
            }

            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x, y, width, height, 0);

            float margin = strokeWidth * 2;
            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 0);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            if (VirtualView.IsEnabled)
                canvas.FontColor = VirtualView.TextColor.WithDefault(Fluent.Color.Foreground.White);
            else
                canvas.FontColor = Fluent.Color.Foreground.NeutralPrimary.ToColor();

            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(VirtualView.Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 32f);
    }
}