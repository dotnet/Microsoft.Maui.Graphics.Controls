using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class Entry
    {
        const string FluentEntryIndicatorIcon = "M6.84961 6L12 11.1504L11.1504 12L6 6.84961L0.849609 12L0 11.1504L5.15039 6L0 0.849609L0.849609 0L6 5.15039L11.1504 0L12 0.849609L6.84961 6Z";

        const float FluentEntryHeight = 32.0f;

        void DrawFluentEntryBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Foreground.White, Fluent.Color.Foreground.NeutralPrimaryAlt);
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = FluentEntryHeight;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        void DrawFluentEntryBorder(ICanvas canvas, RectangleF dirtyRect)
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
                var height = FluentEntryHeight;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        void DrawFluentEntryPlaceholder(ICanvas canvas, RectangleF dirtyRect)
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

                var height = FluentEntryHeight;
                var width = dirtyRect.Width;

                canvas.DrawString(Placeholder, x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }

        void DrawFluentEntryIndicators(ICanvas canvas, RectangleF dirtyRect)
        {
            if (!string.IsNullOrEmpty(Text))
            {
                canvas.SaveState();

                var iconMarginX = 24;
                var iconMarginY = 10;

                var tX = dirtyRect.Width - iconMarginX;
                var tY = dirtyRect.Y + iconMarginY;

                canvas.Translate(tX, tY);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(FluentEntryIndicatorIcon);

                canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Gray1, Material.Color.Gray6);
                canvas.FillPath(path);

                canvas.RestoreState();

                _indicatorRect = new RectangleF(tX, tY, FluentEntryHeight / 2, FluentEntryHeight / 2);
            }
        }
    }
}