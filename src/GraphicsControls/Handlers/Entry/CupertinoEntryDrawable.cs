namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoEntryDrawable : ViewDrawable<IEntry>, IEntryDrawable
    {
        RectangleF indicatorRect = new RectangleF();
        public RectangleF IndicatorRect => indicatorRect;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntry view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Material.Color.White);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 8);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntry view)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;

            canvas.StrokeColor = Material.Color.Gray3.ToColor();
            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 8);

            canvas.RestoreState();
        }

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IEntry view)
        {

        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntry view)
        {
            if (string.IsNullOrEmpty(VirtualView.Text))
            {
                canvas.SaveState();

                canvas.FontColor = VirtualView.TextColor.WithDefault(Material.Color.Black);
                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(VirtualView.Placeholder, x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }
    }
}