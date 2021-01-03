using System.Graphics;
using GraphicsControls.Extensions;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class ProgressBar
    {
        const float CupertinoTrackHeight = 4.0f;

        void DrawCupertinoProgressTrack(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Cupertino.Color.SystemGray.Light.Gray5);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }

        void DrawCupertinoProgressBar(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = ProgressColor.ToGraphicsColor(Cupertino.Color.SystemColor.Light.Blue, Cupertino.Color.SystemColor.Dark.Blue);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - CupertinoTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * Progress), CupertinoTrackHeight, 2);

            canvas.RestoreState();
        }
    }
}