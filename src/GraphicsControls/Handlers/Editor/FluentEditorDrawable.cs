using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentEditorDrawable : ViewDrawable<IEditor>, IEditorDrawable
    {
        public bool HasFocus { get; set; }
        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            Color? backgroundColor = null;

            if (editor.Background is SolidPaint solidBackground)
                backgroundColor = solidBackground.Color;

            if (backgroundColor != null)
            {
                canvas.FillColor = backgroundColor;
            }
            else
            {
                if (editor.Background != null)
                    canvas.SetFillPaint(editor.Background, dirtyRect);
                else
                {
                    if (editor.IsEnabled)
                        canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Background.Default.ToColor() : Fluent.Color.Dark.Control.Background.Default.ToColor();
                    else
                        canvas.FillColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Background.Disabled.ToColor() : Fluent.Color.Dark.Control.Background.Disabled.ToColor();
                }
            }
         
            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.FillRoundedRectangle(x, y, width, height, 4);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;

            canvas.StrokeSize = strokeWidth;

            if (editor.IsEnabled)
                canvas.StrokeColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();
            else
                canvas.StrokeColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Control.Border.Disabled.ToColor() : Fluent.Color.Dark.Control.Border.Disabled.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;
            var height = dirtyRect.Height;

            canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, width - strokeWidth, height - strokeWidth, 2);

            canvas.RestoreState();

            if (editor.IsEnabled)
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

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor editor)
        {
            if (!HasFocus && string.IsNullOrEmpty(editor.Text))
            {
                canvas.SaveState();

                if (editor.IsEnabled)
                    canvas.FontColor = editor.PlaceholderColor.WithDefault(Fluent.Color.Light.Foreground.Secondary, Fluent.Color.Dark.Foreground.Secondary);
                else
                    canvas.FontColor = (Application.Current?.RequestedTheme == OSAppTheme.Light) ? Fluent.Color.Light.Foreground.Disabled.ToColor() : Fluent.Color.Dark.Foreground.Disabled.ToColor();

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
