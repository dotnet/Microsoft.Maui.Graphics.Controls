namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentEditorDrawable : ViewDrawable<IEditor>, IEditorDrawable
    {
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            canvas.SaveState();

            if (VirtualView.IsEnabled)
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Fluent.Color.Foreground.White);
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 2);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            if (VirtualView.IsEnabled)
            {
                canvas.SaveState();

                var strokeWidth = 1.0f;

                canvas.StrokeColor = Fluent.Color.Foreground.NeutralSecondary.ToColor();
                canvas.StrokeSize = strokeWidth;

                var x = dirtyRect.X;
                var y = dirtyRect.Y;

                var width = dirtyRect.Width;
                var height = dirtyRect.Height;

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

                canvas.RestoreState();
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            if (string.IsNullOrEmpty(VirtualView.Text))
            {
                canvas.SaveState();

                if (VirtualView.IsEnabled)
                    canvas.FontColor = VirtualView.PlaceholderColor.WithDefault(Fluent.Color.Foreground.Black);
                else
                    canvas.FontColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;
                var y = dirtyRect.Y + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(VirtualView.Placeholder, x, y, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Top);

                canvas.RestoreState();
            }
        }
    }
}
