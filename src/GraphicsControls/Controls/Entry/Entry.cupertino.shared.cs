using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;

namespace GraphicsControls
{
    public partial class Entry
    {
        void DrawCupertinoEntryBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.White, Material.Color.Black);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 8);

            canvas.RestoreState();
        }

        void DrawCupertinoEntryBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;

            canvas.StrokeColor = ColorHelper.GetGraphicsColor(Material.Color.Gray3, Material.Color.Gray6);
            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 8);

            canvas.RestoreState();
        }

        void DrawCupertinoEntryPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (!IsFocused && string.IsNullOrEmpty(Text))
            {
                canvas.SaveState();

                canvas.FontColor = TextColor.ToGraphicsColor(Material.Color.Black, Material.Color.White);
                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(Placeholder, x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }
    }
}