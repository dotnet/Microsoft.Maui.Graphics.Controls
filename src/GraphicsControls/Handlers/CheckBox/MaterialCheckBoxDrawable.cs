namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialCheckBoxDrawable : ViewDrawable<ICheckBox>, ICheckBoxDrawable
    {
        const string MaterialCheckBoxMark = "M13.3516 1.35156L5 9.71094L0.648438 5.35156L1.35156 4.64844L5 8.28906L12.6484 0.648438L13.3516 1.35156Z";
    
        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            canvas.SaveState();

            float size = 18f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (checkBox.IsChecked)
            {
                if (checkBox.IsEnabled)
                    canvas.FillColor = Material.Color.Blue.ToColor();
                else
                    canvas.FillColor = Material.Color.Gray3.ToColor();

                canvas.FillRoundedRectangle(x, y, size, size, 2);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;
                canvas.StrokeColor = Material.Color.Gray1.ToColor();
                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 2);
            }

            canvas.RestoreState();
        }

        public void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            if (checkBox.IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(2, 4);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(MaterialCheckBoxMark);

                if (VirtualView.IsEnabled)
                    canvas.StrokeColor = Material.Color.White.ToColor();
                else
                    canvas.StrokeColor = Material.Color.Gray1.ToColor();

                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 18f);
    }
}
