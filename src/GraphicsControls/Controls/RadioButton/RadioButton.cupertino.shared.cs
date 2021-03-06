using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class RadioButton
    {
        void DrawCupertinoRadioButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.StrokeSize = 2;
            canvas.StrokeColor = BackgroundColor.ToGraphicsColor(Cupertino.Color.SystemColor.Light.Blue, Cupertino.Color.SystemColor.Dark.Blue);

            var marginX = 2;
            var marginY = 3;

            var x = dirtyRect.X + marginX;
            var y = dirtyRect.Y + marginY;

            var size = 20;

            canvas.DrawOval(x, y, size, size);

            canvas.RestoreState();
        }

        void DrawCupertinoRadioButtonMark(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                canvas.FillColor = BackgroundColor.ToGraphicsColor(Cupertino.Color.SystemColor.Light.Blue, Cupertino.Color.SystemColor.Dark.Blue);

                var x = 6;
                var y = 7;

                var size = 12;

                canvas.FillOval(x, y, size, size);

                canvas.RestoreState();
            }
        }
    }
}