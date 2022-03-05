using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float MaterialThumbOffPosition = 12f;
        const float MaterialThumbOnPosition = 34f;
        const float MaterialSwitchBackgroundWidth = 34;
        const float MaterialSwitchBackgroundMargin = 5;

        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (view.IsOn)
            {
                if (view.IsEnabled)
                    canvas.FillColor = view.TrackColor.WithDefault(Material.Color.LightBlue);
                else
                    canvas.FillColor = view.TrackColor.WithDefault(Material.Color.Light.Gray4, Material.Color.Dark.Gray4);
            }
            else
            {
                if (view.Background != null)
                    canvas.SetFillPaint(view.Background, dirtyRect);
                else
                {
                    if (Application.Current?.RequestedTheme == AppTheme.Light)
                        canvas.FillColor = view.IsEnabled ? Material.Color.Light.Gray2.ToColor() : Material.Color.Light.Gray3.ToColor();
                    else
                        canvas.FillColor = view.IsEnabled ? Material.Color.Dark.Gray2.ToColor() : Material.Color.Dark.Gray3.ToColor();
                }
            }

            canvas.Alpha = 1.0f.Lerp(0.5f, AnimationPercent);

            var margin = MaterialSwitchBackgroundMargin;

            var x = dirtyRect.X + margin;
            var y = dirtyRect.Y + margin;

            var height = 14;
            var width = MaterialSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (view.IsOn)
            {
                if (view.IsEnabled)
                    canvas.FillColor = view.ThumbColor.WithDefault(Material.Color.Blue);
                else
                    canvas.FillColor = view.ThumbColor.WithDefault(Material.Color.Light.Gray1, Material.Color.Dark.Gray1);
            }
            else
            {
                if (view.IsEnabled)
                    canvas.FillColor = view.ThumbColor.WithDefault(Material.Color.White, Material.Color.Black);
                else
                    canvas.FillColor = view.ThumbColor.WithDefault(Material.Color.Light.Gray1, Material.Color.Dark.Gray1);
            }

            var margin = 2;
            var radius = 10;

            var y = dirtyRect.Y + margin + radius;

            canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);

            var materialThumbPosition = MaterialThumbOffPosition.Lerp(MaterialThumbOnPosition, AnimationPercent);
                     
            canvas.FillCircle(materialThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 24f);
    }
}
