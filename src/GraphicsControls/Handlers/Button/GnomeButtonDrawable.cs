namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            var strokeWidth = 1;

            if (VirtualView.IsEnabled)
            {
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault("#F8F7F7");
                canvas.StrokeColor = Color.FromHex("#AA9F98");
            }
            else
            {
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault("#F4F4F2");
                canvas.StrokeColor = Color.FromHex("#BABDB6");
            }

            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            float margin = strokeWidth * 2;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 2);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 2);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            if (VirtualView.IsEnabled)
                canvas.FontColor = VirtualView.TextColor.WithDefault("#2E3436");
            else
                canvas.FontColor = VirtualView.TextColor.WithDefault("#909494");

            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(VirtualView.Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 33f);
    }
}