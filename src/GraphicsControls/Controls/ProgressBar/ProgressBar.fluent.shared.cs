using System.Graphics;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class ProgressBar
    {
        const float FluentTrackHeight = 2.0f;

        void DrawFluentProgressTrack(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Fluent.Color.Background.NeutralLight);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - FluentTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, FluentTrackHeight, 0);

            canvas.RestoreState();
        }

        void DrawFluentProgressBar(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Fluent.Color.Primary.ThemePrimary);

            var x = dirtyRect.X;
            var y = (float)((HeightRequest - FluentTrackHeight) / 2);

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, (float)(width * Progress), FluentTrackHeight, 0);

            canvas.RestoreState();
        }
    }
}