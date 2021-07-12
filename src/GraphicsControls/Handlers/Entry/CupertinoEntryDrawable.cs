namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoEntryDrawable : ViewDrawable<IEntry>, IEntryDrawable
    {
        RectangleF indicatorRect = new RectangleF();
        public RectangleF IndicatorRect => indicatorRect;

        public bool HasFocus { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntry editor)
        {
            canvas.SaveState();

            if (editor.IsEnabled)
            {
                if (editor.Background != null)
                    canvas.SetFillPaint(editor.Background, dirtyRect);
                else
                    canvas.FillColor = Cupertino.Color.Fill.Light.White.ToColor();
            }
            else
                canvas.FillColor = Cupertino.Color.SystemGray.Light.Gray6.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 8);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntry editor)
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

        public void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IEntry editor)
        {

        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntry editor)
        {
            if (!HasFocus && string.IsNullOrEmpty(editor.Text))
            {
                canvas.SaveState();

                canvas.FontColor = editor.TextColor.WithDefault(Material.Color.Black);
                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(editor.Placeholder, x, 0, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Center);

                canvas.RestoreState();
            }
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 36f);
    }
}