namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float MaterialThumbOffPosition = 12f;
        const float MaterialThumbOnPosition = 34f;
        const float MaterialSwitchBackgroundWidth = 34;
        const float MaterialSwitchBackgroundMargin = 5;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (view.IsOn)
            {
                canvas.FillColor = view.TrackColor.WithDefault(view.IsEnabled ? Material.Color.LightBlue : Material.Color.Gray4);
                canvas.Alpha = 0.5f;
            }
            else
            {
                if (view.Background != null)
                    canvas.SetFillPaint(view.Background, dirtyRect);
                else
                    canvas.FillColor = view.IsEnabled ? Material.Color.Gray2.ToColor() : Material.Color.Gray3.ToColor();

                canvas.Alpha = 1.0f;
            }

            var margin = MaterialSwitchBackgroundMargin;

            var x = dirtyRect.X + margin;
            var y = dirtyRect.Y + margin;

            var height = 14;
            var width = MaterialSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (view.IsOn)
                canvas.FillColor = view.ThumbColor.WithDefault(view.IsEnabled ? Material.Color.Blue : Material.Color.Gray1);
            else
                canvas.FillColor = view.ThumbColor.WithDefault(view.IsEnabled ? Material.Color.White : Material.Color.Gray3);

            var margin = 2;
            var radius = 10;

            var y = dirtyRect.Y + margin + radius;

            canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);

            var materialThumbPosition = view.IsOn ? MaterialThumbOnPosition : MaterialThumbOffPosition;
            canvas.FillCircle(materialThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 24f);
    }
}
