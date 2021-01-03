using System.Graphics;
using GraphicsControls.Extensions;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class Switch
    {
        const float MaterialThumbOffPosition = 12f;
        const float MaterialThumbOnPosition = 34f;
        const float MaterialSwitchBackgroundWidth = 34;
        const float MaterialSwitchBackgroundMargin = 5;

        float _materialSwitchThumbPosition = MaterialThumbOffPosition;

        float MaterialSwitchThumbPosition
        {
            get { return _materialSwitchThumbPosition; }
            set
            {
                _materialSwitchThumbPosition = value;
                InvalidateDraw();
            }
        }

        void DrawMaterialSwitchBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsToggled)
            {
                canvas.FillColor = OnColor.ToGraphicsColor(Material.Color.LightBlue);
                canvas.Alpha = 0.5f;
            }
            else
            {
                canvas.FillColor = BackgroundColor.ToGraphicsColor(Material.Color.Gray2);
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

        void DrawMaterialSwitchThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            if (IsToggled)
                canvas.FillColor = ThumbColor.ToGraphicsColor(Material.Color.Blue);
            else
                canvas.FillColor = ThumbColor.ToGraphicsColor(Fluent.Color.Foreground.White);

            var margin = 2;
            var radius = 10;

            var y = dirtyRect.Y + margin + radius;

            canvas.SetShadow(new SizeF(0, 1), 2, CanvasDefaults.DefaultShadowColor);
            canvas.FillCircle(MaterialSwitchThumbPosition, y, radius);

            canvas.RestoreState();
        }

        void AnimateMaterialSwitchThumb(bool on)
        {
            float start = on ? MaterialThumbOffPosition : MaterialThumbOnPosition;
            float end = on ? MaterialThumbOnPosition : MaterialThumbOffPosition;

            var thumbPositionAnimation = new Animation(v => MaterialSwitchThumbPosition = (int)v, start, end, easing: Easing.Linear);
            thumbPositionAnimation.Commit(this, "MaterialSwitchThumbAnimation", length: 100, finished: (l, c) => thumbPositionAnimation = null);
        }
    }
}