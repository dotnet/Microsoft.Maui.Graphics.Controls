using System.Graphics;
using GraphicsControls.Extensions;
using GraphicsControls.Helpers;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class Switch
    {
        const float FluentThumbOffPosition = 10f;
        const float FluentThumbOnPosition = 30f;
        const float FluentSwitchBackgroundWidth = 40;

        float _fluentSwitchThumbPosition = FluentThumbOffPosition;

        float FluentSwitchThumbPosition
        {
            get { return _fluentSwitchThumbPosition; }
            set
            {
                _fluentSwitchThumbPosition = value;
                InvalidateDraw();
            }
        }

        void DrawFluentSwitchBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsEnabled)
            {
                if (IsToggled)
                    canvas.FillColor = OnColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
                else
                    canvas.FillColor = BackgroundColor.ToGraphicsColor(Fluent.Color.Primary.ThemePrimary);
            }
            else
                canvas.FillColor = ColorHelper.GetGraphicsColor(Fluent.Color.Background.NeutralLighter, Fluent.Color.Background.NeutralDark);

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = 20;
            var width = FluentSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        void DrawFluentSwitchThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = ThumbColor.ToGraphicsColor(Fluent.Color.Foreground.White);

            var margin = 4;
            var radius = 6;

            var y = dirtyRect.Y + margin + radius;
            
            canvas.FillCircle(FluentSwitchThumbPosition, y, radius);

            canvas.RestoreState();
        }

        void AnimateFluentSwitchThumb(bool on)
        {
            float start = on ? FluentThumbOffPosition : FluentThumbOnPosition;
            float end = on ? FluentThumbOnPosition : FluentThumbOffPosition;

            var thumbPositionAnimation = new Animation(v => FluentSwitchThumbPosition = (int)v, start, end, easing: Easing.Linear);
            thumbPositionAnimation.Commit(this, "FluentSwitchThumbAnimation", length: 50, finished: (l, c) => thumbPositionAnimation = null);
        }
    }
}