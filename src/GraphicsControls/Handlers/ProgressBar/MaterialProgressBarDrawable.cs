namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float MaterialTrackHeight = 4.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = Material.Color.Blue.ToColor();

            var x = dirtyRect.X;
            var y = (float)((VirtualView.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, (float)(width * VirtualView.Progress), MaterialTrackHeight);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = Fluent.Color.Background.NeutralLight.ToColor();

            var x = dirtyRect.X;
            var y = (float)((VirtualView.Height - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, width, MaterialTrackHeight);

            canvas.RestoreState();
        }
    }
}