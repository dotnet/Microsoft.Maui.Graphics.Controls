namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeSliderDrawable : ViewDrawable<ISlider>, ISliderDrawable
    {
        RectangleF trackRect = new RectangleF();
        public RectangleF TrackRect => trackRect;

        RectangleF touchTargetRect = new RectangleF();
        public RectangleF TouchTargetRect => touchTargetRect;

        public override void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IView view)
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

            var x = dirtyRect.X;

            var width = dirtyRect.Width;
            var height = 5;

            var y = (float)((dirtyRect.Height - height) / 2);

            trackRect.X = x;
            trackRect.Width = width;

            float margin = strokeWidth * 2;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 6);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, 6);

            canvas.RestoreState();
        }

        public void DrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISlider view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.MinimumTrackColor.WithDefault(VirtualView.IsEnabled ? "#3584E4" : "#E1DEDB");

            var value = (VirtualView.Value / VirtualView.Maximum - VirtualView.Minimum).Clamp(0, 1);

            var width = (float)(dirtyRect.Width * value);
            var height = 3;

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 6);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISlider view)
        {
            canvas.SaveState();

            var size = 15.85f;
            var strokeWidth = 1f;

            canvas.StrokeColor = VirtualView.ThumbColor.WithDefault("#BCBFB7");

            canvas.StrokeSize = strokeWidth;

            var value = (VirtualView.Value / VirtualView.Maximum - VirtualView.Minimum).Clamp(0, 1);

            var x = (float)((dirtyRect.Width * value) - (size / 2));

            if (x <= strokeWidth)
                x = strokeWidth / 2;

            if (x >= dirtyRect.Width - (size + strokeWidth))
                x = dirtyRect.Width - (size + strokeWidth);

            var y = (float)((dirtyRect.Height - size) / 2);

            touchTargetRect.Center(new PointF(x, y));

            canvas.FillColor = Color.FromHex("#F4F4F2");

            canvas.FillEllipse(x, y, size, size);
            canvas.DrawEllipse(x, y, size, size);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider view)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 18f);
    }
}