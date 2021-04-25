namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentCheckBoxDrawable : ViewDrawable<ICheckBox>, ICheckBoxDrawable
    {
        const string FluentCheckBoxMark = "M13.3516 1.35156L5 9.71094L0.648438 5.35156L1.35156 4.64844L5 8.28906L12.6484 0.648438L13.3516 1.35156Z";

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {
            canvas.SaveState();

            float size = 20f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (VirtualView.IsChecked)
            {
                if (VirtualView.IsEnabled)
                    canvas.FillColor = Fluent.Color.Primary.ThemePrimary.ToColor();
                else
                    canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

                canvas.FillRoundedRectangle(x, y, size, size, 2);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;

                if (VirtualView.IsEnabled)
                    canvas.StrokeColor = Fluent.Color.Primary.ThemePrimary.ToColor();
                else
                    canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 2);
            }

            canvas.RestoreState();
        }

        public void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {
            if (VirtualView.IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(3, 5);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(FluentCheckBoxMark);

                canvas.StrokeColor = Colors.White;
                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}
