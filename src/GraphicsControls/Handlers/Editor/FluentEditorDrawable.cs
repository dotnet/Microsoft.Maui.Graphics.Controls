namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentEditorDrawable : ViewDrawable<IEditor>, IEditorDrawable
    {
        public bool HasFocus { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            if (editor.Background != null)
                canvas.SetFillPaint(editor.Background, dirtyRect);
            else
                canvas.FillColor = editor.IsEnabled ? Fluent.Color.Foreground.White.ToColor() : Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            if (editor.IsEnabled)
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

                canvas.SaveState();

                canvas.FillColor = Fluent.Color.Primary.ThemeDarker.ToColor();

                if (HasFocus)
                {
                    strokeWidth = 2.0f;
                    canvas.FillColor = Fluent.Color.Primary.ThemePrimary.ToColor();
                }

                x = strokeWidth;
                y = height - strokeWidth;
                width -= strokeWidth * 2;
                height = strokeWidth;

                canvas.FillRoundedRectangle(x, y, width, height, 4);

                canvas.RestoreState();
            }
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            if (!HasFocus && string.IsNullOrEmpty(editor.Text))
            {
                canvas.SaveState();

                if (editor.IsEnabled)
                    canvas.FontColor = editor.PlaceholderColor.WithDefault(Fluent.Color.Foreground.Black);
                else
                    canvas.FontColor = Fluent.Color.Foreground.NeutralTertiary.ToColor();

                canvas.FontSize = 14f;

                float margin = 8f;

                var x = dirtyRect.X + margin;
                var y = dirtyRect.Y + margin;

                var height = dirtyRect.Height;
                var width = dirtyRect.Width;

                canvas.DrawString(editor.Placeholder, x, y, width - margin, height, HorizontalAlignment.Left, VerticalAlignment.Top);

                canvas.RestoreState();
            }
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 112.0d);
    }
}
