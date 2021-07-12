namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentCheckBoxDrawable : ViewDrawable<ICheckBox>, ICheckBoxDrawable
    {
        const string FluentCheckBoxMark = "M0.00195312 3.49805C0.00195312 3.36133 0.0507812 3.24414 0.148438 3.14648C0.246094 3.04883 0.363281 3 0.5 3C0.636719 3 0.753906 3.04883 0.851562 3.14648L3.5 5.79492L9.14844 0.146484C9.24609 0.0488281 9.36328 0 9.5 0C9.57031 0 9.63477 0.0136719 9.69336 0.0410156C9.75586 0.0644531 9.80859 0.0996094 9.85156 0.146484C9.89844 0.189453 9.93555 0.242187 9.96289 0.304688C9.99023 0.363281 10.0039 0.427734 10.0039 0.498047C10.0039 0.634766 9.95312 0.753906 9.85156 0.855469L3.85156 6.85547C3.75391 6.95312 3.63672 7.00195 3.5 7.00195C3.36328 7.00195 3.24609 6.95312 3.14844 6.85547L0.148438 3.85547C0.0507812 3.75781 0.00195312 3.63867 0.00195312 3.49805Z";

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            canvas.SaveState();

            float size = 20f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (checkBox.IsChecked)
            {
                if (checkBox.IsEnabled)
                    canvas.FillColor = Fluent.Color.Primary.ThemePrimary.ToColor();
                else
                    canvas.FillColor = Fluent.Color.Background.NeutralQuaternaryAlt.ToColor();

                canvas.FillRoundedRectangle(x, y, size, size, 3);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;

                if (checkBox.IsEnabled)
                    canvas.StrokeColor = Color.FromArgb("#8A8A8A");
                else
                    canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

                canvas.DrawRoundedRectangle(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth, 3);
            }

            canvas.RestoreState();
        }

        public void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            if (checkBox.IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(5, 7);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(FluentCheckBoxMark);

                canvas.StrokeColor = Colors.White;
                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}
