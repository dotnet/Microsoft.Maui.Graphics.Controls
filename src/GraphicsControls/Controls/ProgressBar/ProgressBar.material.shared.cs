using System.Graphics;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class ProgressBar
    {
        const float MaterialTrackHeight = 4.0f;

        void DrawMaterialProgressTrack(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Fluent.Color.Background.NeutralLight);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, width, MaterialTrackHeight);

            canvas.RestoreState();
        }

        void DrawMaterialProgressBar(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Material.Color.Blue);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - MaterialTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRectangle(x, y, (float)(width * Progress), MaterialTrackHeight);

            canvas.RestoreState();
        }
    }
}