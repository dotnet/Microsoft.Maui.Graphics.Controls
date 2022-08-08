using Microsoft.Maui.Animations;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MaterialButtonDrawable : ViewDrawable<IButton>, IButtonDrawable
    {
        const float MaterialBackgroundHeight = 36f;
        const float MaterialDefaultCornerRadius = 2.0f;

        public PointF TouchPoint { get; set; }
        public double AnimationPercent { get; set; }

        public void DrawBackground(ICanvas canvas, RectF dirtyRect, IButton button)
        {
            canvas.SaveState();

            if (button.IsEnabled)
            {
                if (button.Background != null)
                    canvas.SetFillPaint(button.Background, dirtyRect);
                else
                    canvas.FillColor = Material.Color.Blue.ToColor();
            }
            else
            {
                if (Application.Current?.RequestedTheme == AppTheme.Light)
                    canvas.FillColor = Material.Color.Light.Gray3.ToColor();
                else
                    canvas.FillColor = Material.Color.Dark.Gray4.ToColor();
            }

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, MaterialBackgroundHeight, MaterialDefaultCornerRadius);

            canvas.RestoreState();

            DrawRippleEffect(canvas, dirtyRect, button);
        }

        public void DrawText(ICanvas canvas, RectF dirtyRect, IButton button)
        {
            canvas.SaveState();

            var textColor = (button as ITextStyle)?.TextColor;
            canvas.FontColor = textColor?.WithDefault(button.IsEnabled ? Material.Color.White : Material.Color.Gray1);

            canvas.FontSize = Material.Font.Button;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var width = dirtyRect.Width;

            var text = (button as IText)?.Text;
            canvas.DrawString(text?.ToUpper(), x, y, width, MaterialBackgroundHeight, HorizontalAlignment.Center, VerticalAlignment.Center);

            canvas.RestoreState();
        }

        internal void DrawRippleEffect(ICanvas canvas, RectF dirtyRect, IButton button)
        {
            if (dirtyRect.Contains(TouchPoint))
            {
                canvas.SaveState();

                var border = new PathF();
                border.AppendRoundedRectangle(dirtyRect, MaterialDefaultCornerRadius);

                canvas.ClipPath(border);

                canvas.FillColor = Material.Color.White.ToColor().WithAlpha(0.75f);

                canvas.Alpha = 0.25f;

                float minimumRippleEffectSize = 0.0f;

                var rippleEffectSize = minimumRippleEffectSize.Lerp(dirtyRect.Width, AnimationPercent);

                canvas.FillCircle((float)TouchPoint.X, (float)TouchPoint.Y, rippleEffectSize);

                canvas.RestoreState();
            }
        }

        public override Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint) =>
            new Size(widthConstraint, MaterialBackgroundHeight);
    }
}