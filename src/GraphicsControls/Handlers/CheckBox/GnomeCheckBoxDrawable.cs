namespace Microsoft.Maui.Graphics.Controls
{
    public class GnomeCheckBoxDrawable : ViewDrawable<ICheckBox>, ICheckBoxDrawable
    {
        const string GnomeCheckBoxMark = "M11.9479 1.04779L4.99119 7.87784L3.12583 6.00874L0.994873 5.99314L1.00605 7.6894L3.93279 10.622C4.51741 11.2076 5.46502 11.2076 6.04964 10.622L14.018 2.55951L14.02 0.99173L11.9479 1.04779Z";

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {
            canvas.SaveState();

            float size = 16f;
            var strokeWidth = 1;

            if (VirtualView.IsEnabled)
            {
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault("#FFFFFF");
                canvas.StrokeColor = Color.FromHex("#AA9F98");
            }
            else
            {
                canvas.FillColor = VirtualView.BackgroundColor.WithDefault("#F4F4F2");
                canvas.StrokeColor = Color.FromHex("#BABDB6");
            }

            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            float margin = strokeWidth * 2;

            canvas.FillRoundedRectangle(x + strokeWidth, y + strokeWidth, size - margin, size - margin, 2);
            canvas.DrawRoundedRectangle(x + strokeWidth, y + strokeWidth, size - margin, size - margin, 2);

            canvas.RestoreState();
        }

        public void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {
            if (VirtualView.IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(3, 1);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(GnomeCheckBoxMark);

                if (VirtualView.IsEnabled)
                    canvas.FillColor = Color.FromHex("#2E3436");
                else
                    canvas.FillColor = Color.FromHex("#C7C7C7");

                canvas.FillPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox view)
        {
        
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 16f);
    }
}