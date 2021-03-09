using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;

namespace GraphicsControls
{
    public partial class Slider
    {
        const float TextSize = 36f;

        void DrawFluentSliderTrackBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = MaximumTrackColor.ToGraphicsColor(Fluent.Color.Primary.ThemeLight);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;

            var width = dirtyRect.Width - TextSize;
            var height = 4;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 0);

            canvas.RestoreState();

            _trackRect = new RectangleF(x, y, width, height);
        }

        void DrawFluentSliderTrackProgress(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = MinimumTrackColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralTertiaryAlt, Fluent.Color.Background.NeutralQuaternaryAlt);

            var x = dirtyRect.X;

            var value = ((double)Value).Clamp(0, 1);
            var width = (float)((dirtyRect.Width - TextSize) * value);

            var height = 4;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 0);

            canvas.RestoreState();
        }

        void DrawFluentSliderThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 16f;
            float strokeWidth = 2f;

            if (IsEnabled)
                canvas.StrokeColor = ThumbColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            else
                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralTertiaryAlt, Fluent.Color.Background.NeutralQuaternaryAlt);

            canvas.StrokeSize = strokeWidth;

            var value = ((double)Value).Clamp(0, 1);
            var x = (float)(((dirtyRect.Width - TextSize) * value) - (size / 2));

            if (x <= strokeWidth)
                x = strokeWidth;

            if (x >= dirtyRect.Width - (size + strokeWidth))
                x = dirtyRect.Width - (size + strokeWidth);

            var y = (float)((HeightRequest - size) / 2);

            canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.White, Fluent.Color.Foreground.Black);

            canvas.FillEllipse(x, y, size, size);
            canvas.DrawEllipse(x, y, size, size);

            canvas.RestoreState();

            _thumbRect = new RectangleF(x, y, size, size);
        }

        void DrawFluentSliderText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FontColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
            canvas.FontSize = 14f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            var margin = 6;
            float x = (float)(width - TextSize + margin);
            var y = 2;

            canvas.SetToBoldSystemFont();

            string value = ((double)Value).Clamp(0, 1).ToString("####0.00");

            canvas.DrawString(value, x, y, TextSize, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}