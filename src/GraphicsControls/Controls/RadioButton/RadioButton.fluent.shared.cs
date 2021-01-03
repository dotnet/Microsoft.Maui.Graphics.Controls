using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class RadioButton
    {
        void DrawFluentRadioButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 2;

            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);

            var x = dirtyRect.X + strokeWidth / 2;
            var y = dirtyRect.Y + strokeWidth / 2;

            var size = 20;

            canvas.DrawOval(x, y, size, size);

            canvas.RestoreState();
        }

        void DrawFluentRadioButtonMark(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);

            var x = 6;
            var y = 6;

            var size = 10;

            canvas.FillOval(x, y, size, size);

            canvas.RestoreState();
        }
    }
}