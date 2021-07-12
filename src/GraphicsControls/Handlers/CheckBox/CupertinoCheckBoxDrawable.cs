namespace Microsoft.Maui.Graphics.Controls
{
    public class CupertinoCheckBoxDrawable : ViewDrawable<ICheckBox>, ICheckBoxDrawable
    {
        const string CupertinoCheckBoxMark = "M4.78246 10.9434C5.03441 10.9434 5.23363 10.832 5.37426 10.6152L10.9114 1.89648C11.0168 1.72656 11.0579 1.59766 11.0579 1.46289C11.0579 1.14062 10.8469 0.929688 10.5246 0.929688C10.2903 0.929688 10.1614 1.00586 10.0207 1.22852L4.75902 9.61328L2.02855 6.03906C1.88207 5.83398 1.73559 5.75195 1.52465 5.75195C1.19066 5.75195 0.962149 5.98047 0.962149 6.30273C0.962149 6.4375 1.02074 6.58984 1.13207 6.73047L4.17309 10.6035C4.34887 10.832 4.53051 10.9434 4.78246 10.9434Z";

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            canvas.SaveState();

            float size = 24f;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (checkBox.IsChecked)
            {
                canvas.FillColor = checkBox.IsEnabled ? Cupertino.Color.SystemColor.Light.Blue.ToColor() : Cupertino.Color.SystemGray.Light.InactiveGray.ToColor();
                canvas.FillEllipse(x, y, size, size);
            }
            else
            {
                var strokeWidth = 2;

                canvas.StrokeSize = strokeWidth;
                canvas.StrokeColor = checkBox.IsEnabled ? Cupertino.Color.SystemColor.Light.Blue.ToColor() : Cupertino.Color.SystemGray.Light.InactiveGray.ToColor();
                canvas.DrawEllipse(x + strokeWidth / 2, y + strokeWidth / 2, size - strokeWidth, size - strokeWidth);
            }

            canvas.RestoreState();
        }

        public void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {
            if (checkBox.IsChecked)
            {
                canvas.SaveState();

                canvas.Translate(6, 6);

                var vBuilder = new PathBuilder();
                var path = vBuilder.BuildPath(CupertinoCheckBoxMark);

                canvas.StrokeColor = Cupertino.Color.Fill.Light.WhiteAlt.ToColor();

                canvas.DrawPath(path);

                canvas.RestoreState();
            }
        }

        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox checkBox)
        {

        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 24f);
    }
}