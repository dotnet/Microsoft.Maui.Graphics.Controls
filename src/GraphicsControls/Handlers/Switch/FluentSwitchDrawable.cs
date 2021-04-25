namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float FluentThumbOffPosition = 10f;
        const float FluentThumbOnPosition = 30f;
        const float FluentSwitchBackgroundWidth = 40;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (VirtualView.IsEnabled)
            {
                if (VirtualView.IsOn)
                    canvas.FillColor = VirtualView.TrackColor.WithDefault(Fluent.Color.Primary.ThemePrimary);
                else
                    canvas.FillColor = VirtualView.BackgroundColor.WithDefault(Fluent.Color.Primary.ThemePrimary);
            }
            else
                canvas.FillColor = Fluent.Color.Background.NeutralLighter.ToColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = 20;
            var width = FluentSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            canvas.FillColor = VirtualView.ThumbColor.WithDefault(Fluent.Color.Foreground.White);

            var margin = 4;
            var radius = 6;

            var y = dirtyRect.Y + margin + radius;

            var fluentThumbPosition = VirtualView.IsOn ? FluentThumbOnPosition : FluentThumbOffPosition;
            canvas.FillCircle(fluentThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}