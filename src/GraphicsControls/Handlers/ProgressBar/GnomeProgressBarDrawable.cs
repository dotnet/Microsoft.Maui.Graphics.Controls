namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float GnomeTrackHeight = 7.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            if (VirtualView.IsEnabled)
                canvas.FillColor = Color.FromHex("#3584E4");
            else
                canvas.FillColor = Color.FromHex("#CFCAC4");

            var strokeWidth = 1;

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - GnomeTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, (float)(width * VirtualView.Progress) - (strokeWidth * 2), GnomeTrackHeight - (strokeWidth * 2), 6);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress view)
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
            var y = (float)((dirtyRect.Height - GnomeTrackHeight) / 2);

            var width = dirtyRect.Width;

            float margin = strokeWidth * 2;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, GnomeTrackHeight - margin, 6);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, GnomeTrackHeight - margin, 6);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 11f);
    }
}