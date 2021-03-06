using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;

namespace GraphicsControls
{
    public partial class Editor
    {
        void DrawCupertinoEditorBackground(ICanvas canvas, RectangleF dirtyRect)
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

        void DrawCupertinoEditorBorder(ICanvas canvas, RectangleF dirtyRect)
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

        void DrawCupertinoEditorPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (!IsFocused && string.IsNullOrEmpty(Text))
            {
                canvas.SaveState();

                canvas.FontColor = PlaceholderColor.ToGraphicsColor(Material.Color.Black, Material.Color.White);
                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;
                var y = dirtyRect.Y + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(Placeholder, x, y, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Top);

                canvas.RestoreState();
            }
        }
    }
}