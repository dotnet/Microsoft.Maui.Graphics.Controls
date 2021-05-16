namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float GnomeThumbOffPosition = 12f;
        const float GnomeThumbOnPosition = 34f;
        const float GnomeSwitchBackgroundWidth = 48f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var strokeWidth = 1;
            canvas.StrokeSize = strokeWidth;

            if (VirtualView.IsOn)
            {
                canvas.FillColor = VirtualView.TrackColor.WithDefault(VirtualView.IsEnabled ? "#3081E3" : "#C0BFBC");
                canvas.StrokeColor = VirtualView.TrackColor.WithDefault(VirtualView.IsEnabled ? "#2B73CC" : "#AA9F98");
            }
            else
            {
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault(VirtualView.IsEnabled ? "#E1DEDB" : "#E1DEDB");
                canvas.StrokeColor = VirtualView.BackgroundColor.WithDefault(VirtualView.IsEnabled ? "#CDC7C2" : "#AA9F98");
            }

            var height = 26;
            var width = GnomeSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - (strokeWidth * 2), height - (strokeWidth * 2), 36.5f);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, width - (strokeWidth * 2), height - (strokeWidth * 2), 36.5f);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            var strokeWidth = 1;
            canvas.StrokeSize = strokeWidth;

            canvas.FillColor = VirtualView.ThumbColor.WithDefault("#FFFFFF");
            canvas.StrokeColor = Color.FromHex("#AA9F98");

            var margin = 0;
            var radius = 12;

            var y = dirtyRect.Y + margin + radius;

            var gnomeThumbPosition = VirtualView.IsOn ? GnomeThumbOnPosition : GnomeThumbOffPosition;

            canvas.FillCircle(gnomeThumbPosition + strokeWidth, y + strokeWidth, radius);
            canvas.DrawCircle(gnomeThumbPosition + strokeWidth, y + strokeWidth, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 28f);
    }
}