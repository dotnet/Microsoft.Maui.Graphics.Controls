namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float CupertinoThumbOffPosition = 15f;
        const float CupertinoThumbOnPosition = 36f;
        const float CupertinoSwitchBackgroundWidth = 51;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (VirtualView.IsOn)
                canvas.FillColor = VirtualView.TrackColor.WithDefault(Cupertino.Color.SystemColor.Light.Green);
            else
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Cupertino.Color.SystemGray.Light.Gray4);

            var height = 30;
            var width = CupertinoSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 36.5f);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.ThumbColor.WithDefault(Fluent.Color.Foreground.White);

            var margin = 2;
            var radius = 13;

            var y = dirtyRect.Y + margin + radius;

            canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);

            var cupertinoThumbPosition = VirtualView.IsOn ? CupertinoThumbOnPosition : CupertinoThumbOffPosition;

            canvas.FillCircle(cupertinoThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 30f);
    }
}
