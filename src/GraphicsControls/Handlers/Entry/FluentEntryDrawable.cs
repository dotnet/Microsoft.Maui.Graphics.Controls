namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentEntryDrawable : ViewDrawable<IEntry>, IEntryDrawable
    {
        const string FluentEntryIndicatorIcon = "M6.84961 6L12 11.1504L11.1504 12L6 6.84961L0.849609 12L0 11.1504L5.15039 6L0 0.849609L0.849609 0L6 5.15039L11.1504 0L12 0.849609L6.84961 6Z";
        const float FluentEntryHeight = 32.0f;

        RectangleF indicatorRect = new RectangleF();
        public RectangleF IndicatorRect => indicatorRect;

        public bool HasFocus { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            canvas.SaveState();

            if (entry.IsEnabled)
                canvas.FillColor = entry.BackgroundColor.WithDefault(Fluent.Color.Foreground.White);
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = FluentEntryHeight;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            if (entry.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = dirtyRect.Width;
                var height = FluentEntryHeight;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

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

                canvas.FillColor = entry.BackgroundColor.WithDefault(Material.Color.Gray1);
                canvas.FillPath(path);

                canvas.RestoreState();

                indicatorRect = new RectangleF(tX, tY, FluentEntryHeight / 2, FluentEntryHeight / 2);
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntry entry)
        {
            if (string.IsNullOrEmpty(entry.Text))
            {
                canvas.SaveState();

                if (entry.IsEnabled)
                    canvas.FontColor = Fluent.Color.Foreground.Black.ToColor();
                else
                    canvas.FontColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

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