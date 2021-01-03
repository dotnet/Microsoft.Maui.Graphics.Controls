using System.Graphics;
using GraphicsControls.Extensions;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class Switch
    {
        const float CupertinoThumbOffPosition = 15f;
        const float CupertinoThumbOnPosition = 36f;
        const float CupertinoSwitchBackgroundWidth = 51;

        float _cupertinoSwitchThumbPosition = CupertinoThumbOffPosition;

        float CupertinoSwitchThumbPosition
        {
            get { return _cupertinoSwitchThumbPosition; }
            set
            {
                _cupertinoSwitchThumbPosition = value;
                InvalidateDraw();
            }
        }

        void DrawCupertinoSwitchBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (IsToggled)
                canvas.FillColor = OnColor.ToGraphicsColor(Cupertino.Color.SystemColor.Light.Green, Cupertino.Color.SystemColor.Dark.Green);
            else
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Cupertino.Color.SystemGray.Light.Gray4, Cupertino.Color.SystemGray.Dark.Gray4);

            var height = 30;
            var width = CupertinoSwitchBackgroundWidth;

            canvas.FillRoundedRectangle(x, y, width, height, 36.5f);

            canvas.RestoreState();
        }

        void DrawCupertinoSwitchThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            canvas.FillColor = ThumbColor.ToGraphicsColor(Fluent.Color.Foreground.White);

            var margin = 2;
            var radius = 13;

            var y = dirtyRect.Y + margin + radius;

            canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);
            canvas.FillCircle(CupertinoSwitchThumbPosition, y, radius);

            canvas.RestoreState();
        }

        void AnimateCupertinoSwitchThumb(bool on)
        {
            float start = on ? CupertinoThumbOffPosition : CupertinoThumbOnPosition;
            float end = on ? CupertinoThumbOnPosition : CupertinoThumbOffPosition;

            var thumbPositionAnimation = new Animation(v => CupertinoSwitchThumbPosition = (int)v, start, end, easing: Easing.Linear);
            thumbPositionAnimation.Commit(this, "CupertinoSwitchThumbAnimation", length: 100, finished: (l, c) => thumbPositionAnimation = null);
        }
    }
}