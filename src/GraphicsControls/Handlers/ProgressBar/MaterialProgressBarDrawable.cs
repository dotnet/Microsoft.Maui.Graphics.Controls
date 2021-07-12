namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float MaterialTrackHeight = 4.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            if (progressBar.IsEnabled)
                canvas.FillColor = Material.Color.Blue.ToColor();
            else
                canvas.FillColor = Material.Color.Gray3.ToColor();

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, (float)(width * progressBar.Progress), MaterialTrackHeight);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress progressBar)
        {
            canvas.SaveState();

            if (progressBar.Background != null)
                canvas.SetFillPaint(progressBar.Background, dirtyRect);
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLight.ToColor();

            var x = dirtyRect.X;
            var y = (float)((dirtyRect.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, width, MaterialTrackHeight);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 12f);
    }
}