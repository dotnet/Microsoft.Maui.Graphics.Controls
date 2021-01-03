using System.Graphics;
using GraphicsControls.Helpers;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Slider
    {
        const float TextSize = 36f;

        void DrawFluentSliderTrackBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = new GColor(Fluent.Color.Primary.ThemeLight);

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

            canvas.FillColor = new GColor(Fluent.Color.Primary.ThemePrimary);

            var x = dirtyRect.X;

            var width = (float)((dirtyRect.Width - TextSize) * Value);
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

            canvas.StrokeColor = new GColor(Fluent.Color.Primary.ThemePrimary);
            canvas.StrokeSize = strokeWidth;

            var x = (float)(((dirtyRect.Width - TextSize) * Value) - (size / 2));

            if (x <= strokeWidth)
                x = strokeWidth;

            if (x >= dirtyRect.Width - (size + strokeWidth))
                x = dirtyRect.Width - (size + strokeWidth);

            var y = (float)((HeightRequest - size) / 2);

            canvas.FillColor = Colors.White;

            canvas.FillOval(x, y, size, size);
            canvas.DrawOval(x, y, size, size);

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

            string value = Value.ToString("####0.00");

            canvas.DrawString(value, x, y, TextSize, height, HorizontalAlignment.Left, VerticalAlignment.Center);

            canvas.RestoreState();
        }
    }
}