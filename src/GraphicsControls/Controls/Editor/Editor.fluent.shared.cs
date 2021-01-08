using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Editor
    {
        void DrawFluentEditorBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Foreground.White, Fluent.Color.Foreground.NeutralPrimaryAlt);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        void DrawFluentEditorBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.NeutralSecondary, Fluent.Color.Foreground.White);
                canvas.StrokeSize = strokeWidth;

                if (IsFocused)
                {
                    strokeWidth = 2.0f;
                    canvas.StrokeColor = new GColor(Fluent.Color.Primary.ThemePrimary);
                    canvas.StrokeSize = strokeWidth;
                }

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = dirtyRect.Width;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        void DrawFluentEditorPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            if (!IsFocused && string.IsNullOrEmpty(Text))
            {
                canvas.SaveState();

                if (IsEnabled)
                    canvas.FontColor = ColorHelper.GetGraphicsColor(Fluent.Color.Foreground.Black, Fluent.Color.Foreground.White);
                else
                    canvas.FontColor = new GColor(Fluent.Color.Foreground.NeutralTertiary);

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