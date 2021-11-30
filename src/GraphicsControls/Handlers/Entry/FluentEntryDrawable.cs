using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentEntryDrawable : ViewDrawable<IEntry>, IEntryDrawable
    {
        const string FluentEntryIndicatorIcon = "M6.84961 6L12 11.1504L11.1504 12L6 6.84961L0.849609 12L0 11.1504L5.15039 6L0 0.849609L0.849609 0L6 5.15039L11.1504 0L12 0.849609L6.84961 6Z";
        const float FluentEntryHeight = 32.0f;

        RectangleF indicatorRect = new RectangleF();
        public RectangleF IndicatorRect => indicatorRect;

        public bool HasFocus { get; set; }

        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            Color? backgroundColor = null;

            if (entry.Background is SolidPaint solidBackground)
                backgroundColor = solidBackground.Color;

            if (backgroundColor != null)
            {
                canvas.FillColor = backgroundColor;
            }
            else
            {
                if (entry.Background != null)
                    canvas.SetFillPaint(entry.Background, dirtyRect);
                else
                {
                    if (entry.IsEnabled)
                        canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Background.Default.ToColor() : Fluent.Color.Dark.Control.Background.Default.ToColor();
                    else
                        canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Background.Disabled.ToColor() : Fluent.Color.Dark.Control.Background.Disabled.ToColor();
                }
            }

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = FluentEntryHeight;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;

            canvas.StrokeSize = strokeWidth;

            if (entry.IsEnabled)
                canvas.StrokeColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();
            else
                canvas.StrokeColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Border.Disabled.ToColor() : Fluent.Color.Dark.Control.Border.Disabled.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = FluentEntryHeight;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

            canvas.RestoreState();

            if (entry.IsEnabled)
            {
                canvas.SaveState();

                canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Foreground.Primary.ToColor() : Fluent.Color.Dark.Foreground.Primary.ToColor();

                if (HasFocus)
                {
                    strokeWidth = 2.0f;
                    canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Accent.Primary.ToColor() : Fluent.Color.Dark.Accent.Primary.ToColor();
                }

                x = strokeWidth;
                y = height - strokeWidth;
                width -= strokeWidth * 2;
                height = strokeWidth;

                canvas.FillRoundedRectangle(x, y, width, height, 4);

                canvas.RestoreState();
            }
        }

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            if (!string.IsNullOrEmpty(entry.Text))
            {
                canvas.SaveState();

                var iconMarginX = 24;
                var iconMarginY = 10;

                var tX = dirtyRect.Width - iconMarginX;
                var tY = dirtyRect.Y + iconMarginY;

                canvas.Translate(tX, tY);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(FluentEntryIndicatorIcon);

                canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Foreground.Secondary.ToColor() : Fluent.Color.Dark.Foreground.Secondary.ToColor();

                canvas.FillPath(path);

                canvas.RestoreState();

                indicatorRect = new RectangleF(tX, tY, FluentEntryHeight / 2, FluentEntryHeight / 2);
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            if (!HasFocus && string.IsNullOrEmpty(entry.Text))
            {
                canvas.SaveState();

                if (entry.IsEnabled)
                    canvas.FontColor = entry.PlaceholderColor.WithDefault(Fluent.Color.Light.Foreground.Secondary, Fluent.Color.Dark.Foreground.Secondary);
                else
                    canvas.FontColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Foreground.Disabled.ToColor() : Fluent.Color.Dark.Foreground.Disabled.ToColor();

                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;

                var height = FluentEntryHeight;
                var width = dirtyRect.Width;

                canvas.DrawString(entry.Placeholder, x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 36f);
    }
}