using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class Button
    {
        void DrawCupertinoButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Cupertino.Color.SystemColor.Light.Blue, Cupertino.Color.SystemColor.Dark.Blue);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, (float)CornerRadius);

            canvas.RestoreState();

            _backgroundRect = new RectangleF(x, y, width, height);
        }

        void DrawCupertinoButtonText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = TextColor.ToGraphicsColor(Cupertino.Color.Label.Light.Primary, Cupertino.Color.Label.Dark.Primary);
            canvas.FontSize = 17f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}