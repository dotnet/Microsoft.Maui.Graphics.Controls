namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        const float MaterialBackgroundHeight = 36f;
        const float MaterialDefaultCornerRadius = 2.0f;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            if (button.IsEnabled)
            {
                if (button.Background != null)
                    canvas.SetFillPaint(button.Background, dirtyRect);
                else
                    canvas.FillColor = Material.Color.Blue.ToColor();
            }
            else
                canvas.FillColor = Material.Color.Gray3.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, MaterialBackgroundHeight, MaterialDefaultCornerRadius);

            canvas.RestoreState();
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton button)
        {
            canvas.SaveState();

            canvas.FontName = "Roboto";

            canvas.FontColor = button.TextColor.WithDefault(button.IsEnabled ? Material.Color.White : Material.Color.Gray1);

            canvas.FontSize = Material.Font.Button;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;

            canvas.DrawString(button.Text.ToUpper(), x, y, width, MaterialBackgroundHeight, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, MaterialBackgroundHeight);
    }
}