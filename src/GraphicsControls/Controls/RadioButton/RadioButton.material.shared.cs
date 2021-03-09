using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class RadioButton
    {
        void DrawMaterialRadioButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 2;

            canvas.StrokeSize = strokeWidth;
            canvas.StrokeColor = BackgroundColor.ToGraphicsColor(Material.Color.Blue);

            var x = dirtyRect.X + strokeWidth / 2;
            var y = dirtyRect.Y + strokeWidth / 2;

            var size = 20;

            canvas.DrawEllipse(x, y, size, size);

            canvas.RestoreState();
        }

        void DrawMaterialRadioButtonMark(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Blue);

                var x = 6;
                var y = 6;

                var size = 10;

                canvas.FillEllipse(x, y, size, size);
              
                canvas.RestoreState();
            }
        }
    }
}