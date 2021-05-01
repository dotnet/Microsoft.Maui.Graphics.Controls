namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        const float CupertinoDefaultCornerRadius = 2.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Cupertino.Color.SystemColor.Light.Blue);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, CupertinoDefaultCornerRadius);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton view)
        {
            canvas.SaveState();

            canvas.FontColor = VirtualView.TextColor.WithDefault(Cupertino.Color.Label.Light.Primary);
            canvas.FontSize = 17f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(VirtualView.Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 44f);
    }
}