namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoProgressBarDrawable : ViewDrawable<IProgress>, IProgressBarDrawable
    {
        const float CupertinoTrackHeight = 4.0f;

        public void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = Cupertino.Color.SystemColor.Light.Blue.ToColor();

            var x = dirtyRect.X;
            var y = (float)((VirtualView.Height - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * VirtualView.Progress), CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }

        public void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress view)
        {
            canvas.SaveState();

            canvas.FillColor = Cupertino.Color.SystemGray.Light.Gray5.ToColor();

            var x = dirtyRect.X;
            var y = (float)((VirtualView.Height - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }
    }
}