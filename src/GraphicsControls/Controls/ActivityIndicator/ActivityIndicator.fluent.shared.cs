using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class ActivityIndicator
    {
        float _fluentActivityIndicatorEndAngle;

        float FluentActivityIndicatorStartAngle { get; set; }

        float FluentActivityIndicatorEndAngle
        {
            get { return _fluentActivityIndicatorEndAngle; }
            set
            {
                _fluentActivityIndicatorEndAngle = value;
                InvalidateDraw();
            }
        }

        void DrawFluentActivityIndicatorBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 28f;
            float strokeWidth = 1.5f;

            if (IsEnabled)
                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Primary.ThemeLight, Fluent.Color.Primary.ThemeDark);
            else
                canvas.StrokeColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralTertiaryAlt, Fluent.Color.Background.NeutralQuaternaryAlt);

            canvas.StrokeSize = strokeWidth;

            var x = (dirtyRect.Width - size) / 2;
            var y = dirtyRect.Y + strokeWidth;

            canvas.DrawOval(x, y, size, size);

            canvas.RestoreState();
        }

        void DrawFluentActivityIndicator(ICanvas canvas, RectangleF dirtyRect)
        {
            if (IsRunning)
            {
                canvas.SaveState();

                float size = 28f;
                float strokeWidth = 1.5f;

                canvas.StrokeColor = Color.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);

                canvas.StrokeSize = strokeWidth;

                var x = (dirtyRect.Width - size) / 2;
                var y = dirtyRect.Y + strokeWidth;

                canvas.DrawArc(x, y, size, size, FluentActivityIndicatorStartAngle, FluentActivityIndicatorEndAngle, true, false);

                canvas.RestoreState();
            }
        }

        void DrawFluentActivityIndicatorText(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
                canvas.FontColor = Color.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            else
                canvas.FontColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralTertiaryAlt, Fluent.Color.Background.NeutralQuaternaryAlt);

            canvas.FontSize = 12f;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.DrawString("Loading...", 0, 0, width, height, HorizontalAlignment.Center, VerticalAlignment.Bottom);

            canvas.RestoreState();
        }

        void AnimateFluentActivityIndicatorAnimation()
        {
            var fluentActivityIndicatorAnimation = new Animation();

            var startAngle = 90;
            var endAngle = 360;

            var startAngleAnimation = new Animation(v => FluentActivityIndicatorStartAngle = (int)v, startAngle, startAngle - 360, easing: Easing.Linear);
            var endAngleAnimation = new Animation(v => FluentActivityIndicatorEndAngle = (int)v, endAngle, endAngle - 360, easing: Easing.Linear);

            fluentActivityIndicatorAnimation.Add(0, 1, startAngleAnimation);
            fluentActivityIndicatorAnimation.Add(0, 1, endAngleAnimation);

            fluentActivityIndicatorAnimation.Commit(this, "FluentActivityIndicator", length: 1000, repeat: () => true, finished: (l, c) => fluentActivityIndicatorAnimation = null);
        }
    }
}