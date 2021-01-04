using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;

namespace GraphicsControls
{
    public partial class RadioButton
    {
        void DrawFluentRadioButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 2;

            canvas.StrokeSize = strokeWidth;

            if (IsEnabled)
                canvas.StrokeColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            else
                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X + strokeWidth / 2;
            var y = dirtyRect.Y + strokeWidth / 2;

            var size = 20;

            canvas.DrawOval(x, y, size, size);

            canvas.RestoreState();
        }

        void DrawFluentRadioButtonMark(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsChecked)
            {
                canvas.SaveState();

                if (IsEnabled)
                    canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
                else
                    canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

                var x = 6;
                var y = 6;

                var size = 10;

                canvas.FillOval(x, y, size, size);

                canvas.RestoreState();
            }
        }
    }
}