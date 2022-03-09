using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace Microsoft.Maui.Graphics.Controls
{
    public class FluentSwitchDrawable : ViewDrawable<ISwitch>, ISwitchDrawable
    {
        const float FluentThumbOffPosition = 10f;
        const float FluentThumbOnPosition = 30f;
        const float FluentSwitchBackgroundWidth = 40;

        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectF dirtyRect, ISwitch view)
        {
            var strokeWidth = 1;

            canvas.SaveState();

            if (view.IsEnabled)
            {
                if (view.IsOn)
                {
                    canvas.FillColor = canvas.StrokeColor = view.TrackColor.WithDefault(Fluent.Color.Light.Accent.Primary, Fluent.Color.Dark.Accent.Primary);
                }
                else
                {
                    Color? backgroundColor = null;

                    if(view.Background is SolidPaint solidBackground)
                        backgroundColor = solidBackground.Color;

                    if (backgroundColor != null)
                        canvas.StrokeColor = backgroundColor;
                    else
                        canvas.StrokeColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Default.ToColor() : Fluent.Color.Dark.Control.Border.Default.ToColor();

                    if (view.Background != null)
                        canvas.SetFillPaint(view.Background, dirtyRect);
                    else                      
                        canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ?  Fluent.Color.Light.Background.Secondary.ToColor() : Fluent.Color.Dark.Background.Secondary.ToColor();
                }
            }
            else
            {
                // Disabled
                canvas.StrokeColor = (Application.Current?.RequestedTheme == AppTheme.Light) ? Fluent.Color.Light.Control.Border.Disabled.ToColor() : Fluent.Color.Dark.Control.Border.Disabled.ToColor();

                canvas.FillColor = (Application.Current?.RequestedTheme == AppTheme.Light) ?  Fluent.Color.Light.Background.Disabled.ToColor() : Fluent.Color.Dark.Background.Disabled.ToColor();
            }

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = 20;
            var width = FluentSwitchBackgroundWidth;

            canvas.StrokeSize = strokeWidth;

            canvas.DrawRoundedRectangle(x, y, width, height, 10);
            canvas.FillRoundedRectangle(x, y, width, height, 10);

            canvas.RestoreState();
        }

        public void DrawThumb(ICanvas canvas, RectF dirtyRect, ISwitch view)
        {
            canvas.SaveState();

            if (view.IsEnabled)
            {
                if (view.IsOn)
                    canvas.FillColor = view.ThumbColor.WithDefault(Fluent.Color.Dark.Foreground.Primary);
                else
                    canvas.FillColor = view.ThumbColor.WithDefault(Fluent.Color.Light.Foreground.Primary);
            }
            else 
                canvas.FillColor = view.ThumbColor.WithDefault(Fluent.Color.Light.Foreground.Disabled);
          
            var margin = 4;
            var radius = 6;

            var y = dirtyRect.Y + margin + radius;

            var fluentThumbPosition = view.IsOn ? FluentThumbOnPosition : FluentThumbOffPosition;
            canvas.FillCircle(fluentThumbPosition, y, radius);

            canvas.RestoreState();
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, 20f);
    }
}