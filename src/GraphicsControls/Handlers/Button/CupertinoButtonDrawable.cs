namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        const float CupertinoDefaultCornerRadius = 2.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            canvas.FillColor = button.BackgroundColor.WithDefault(button.IsEnabled ? Cupertino.Color.SystemColor.Light.Blue : Cupertino.Color.SystemGray.Light.InactiveGray);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, CupertinoDefaultCornerRadius);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            canvas.FontColor = button.TextColor.WithDefault(Cupertino.Color.Label.Light.White);
            canvas.FontSize = 17f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(button.Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 44f);
    }
}