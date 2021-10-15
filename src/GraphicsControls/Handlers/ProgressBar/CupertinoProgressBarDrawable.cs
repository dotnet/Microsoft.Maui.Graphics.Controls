namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float CupertinoTrackHeight = 4.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            canvas.FillColor = progressBar.ProgressColor.WithDefault(progressBar.IsEnabled ? Cupertino.Color.SystemColor.Light.Blue : Cupertino.Color.SystemGray.Light.InactiveGray);

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * progressBar.Progress), CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            canvas.FillColor = Cupertino.Color.SystemGray.Light.Gray5.ToColor();

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 12f);
    }
}