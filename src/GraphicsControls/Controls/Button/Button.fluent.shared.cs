using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Button
    {
        protected virtual void DrawFluentButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 1;

            if (IsEnabled)
            {
                canvas.StrokeColor = Colors.Black;
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            }
            else
            {
                var disabledColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);
                canvas.StrokeColor = canvas.FillColor = disabledColor;
            }

            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x, y, width, height, (float)CornerRadius);

            float margin = strokeWidth * 2;
            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, width - margin, height - margin, (float)CornerRadius);

            canvas.RestoreState();

            _backgroundRect = new RectangleF(x + strokeWidth, y + strokeWidth, width - margin, height - margin);
        }

        protected virtual void DrawFluentButtonText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FontColor = TextColor.ToGraphicsColor(Fluent.Color.Foreground.White);
            else
                canvas.FontColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralPrimary, Fluent.Color.Foreground.NeutralTertiary);

            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(Text, 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}