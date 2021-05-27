namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialEditorDrawable : ViewDrawable<IEditor>, IEditorDrawable
    {
        const float UnfocusedMaterialPlaceholderFontSize = 16f;
        const float UnfocusedMaterialPlaceholderPosition = 22f;

        readonly float PlaceholderPosition = UnfocusedMaterialPlaceholderPosition;
        readonly float PlaceholderFontSize = UnfocusedMaterialPlaceholderFontSize;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Material.Color.Gray5);

            var width = dirtyRect.Width;

            var vBuilder = new PathBuilder();
            var path =
                vBuilder.BuildPath(
                    $"M0 4C0 1.79086 1.79086 0 4 0H{width - 4}C{width - 2}.209 0 {width} 1.79086 {width} 4V114.95H0V4Z");

            canvas.FillPath(path);

            canvas.RestoreState();
        }

        public void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            canvas.SaveState();

            var strokeWidth = 1.0f;
            canvas.FillColor = Material.Color.Black.ToColor();

            var x = dirtyRect.X;
            var y = 112.91f;

            var width = dirtyRect.Width;
            var height = strokeWidth;

            canvas.FillRectangle(x, y, width, height);

            canvas.RestoreState();
        }

        public void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor view)
        {
            canvas.SaveState();

            canvas.FontColor = VirtualView.PlaceholderColor.WithDefault(Material.Color.Dark);
            canvas.FontSize = PlaceholderFontSize;

            float margin = 12f;

            var horizontalAlignment = HorizontalAlignment.Left;

            var x = dirtyRect.X + margin;

            if (VirtualView.FlowDirection == FlowDirection.RightToLeft)
            {
                x = dirtyRect.X;
                horizontalAlignment = HorizontalAlignment.Right;
            }

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString(VirtualView.Placeholder, x, PlaceholderPosition, width - margin, height, horizontalAlignment, VerticalAlignment.Top);

            canvas.RestoreState();
        }
    }
}
