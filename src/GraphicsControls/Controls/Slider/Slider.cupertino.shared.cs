using System.Graphics;
using GraphicsControls.Extensions;

namespace GraphicsControls
{
    public partial class Slider
    {
        const string DefaultCupertinoSliderTrackBackgroundColor = "#8E8E93";
        const string DefaultCupertinoSliderTrackProgressColor = "#007AFF";
        const string DefaultCupertinoSliderThumbColor = "#161313";

        void DrawCupertinoSliderTrackBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = MaximumTrackColor.ToGraphicsColor(DefaultCupertinoSliderTrackBackgroundColor);

            var x = dirtyRect.X;

            var width = dirtyRect.Width;
            var height = 1;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 20);

            canvas.RestoreState();

            _trackRect = new RectangleF(x, y, width, height);
        }

        void DrawCupertinoSliderTrackProgress(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = MinimumTrackColor.ToGraphicsColor(DefaultCupertinoSliderTrackProgressColor);

            var x = dirtyRect.X;

            var value = ((double)Value).Clamp(0, 1);
            var width = (float)(dirtyRect.Width * value);

            var height = 1;

            var y = (float)((HeightRequest - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 20);

            canvas.RestoreState();
        }

        void DrawCupertinoSliderThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 28f;
            float strokeWidth = 0.5f;

            canvas.StrokeColor = ThumbColor.ToGraphicsColor(DefaultCupertinoSliderThumbColor);
            canvas.StrokeSize = strokeWidth;

            var value = ((double)Value).Clamp(0, 1);
            var x = (float)((dirtyRect.Width * value) - (size / 2));

            if (x <= strokeWidth)
                x = strokeWidth;

            if (x >= dirtyRect.Width - (size + strokeWidth))
                x = dirtyRect.Width - (size + strokeWidth);

            var y = (float)((HeightRequest - size) / 2);

            canvas.FillColor = ThumbColor.ToGraphicsColor(Colors.White.ToHex());

            canvas.SetShadow(new SizeF(1, 1), 2, CanvasDefaults.DefaultShadowColor);

            canvas.FillOval(x, y, size, size);
            canvas.DrawOval(x, y, size, size);

            canvas.RestoreState();

            _thumbRect = new RectangleF(x, y, size, size);
        }
    }
}