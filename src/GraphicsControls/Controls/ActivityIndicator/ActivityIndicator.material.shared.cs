using System.Graphics;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls
{
    public partial class ActivityIndicator
    {
        float _materialActivityIndicatorEndAngle;

        float MaterialActivityIndicatorRotate { get; set; }

        float MaterialActivityIndicatorStartAngle { get; set; }

        float MaterialActivityIndicatorEndAngle
        {
            get { return _materialActivityIndicatorEndAngle; }
            set
            {
                _materialActivityIndicatorEndAngle = value;
                InvalidateDraw();
            }
        }

        void DrawMaterialActivityIndicator(ICanvas canvas, RectangleF dirtyRect)
        {
            canvas.SaveState();

            float size = 40f;
            float strokeWidth = 4f;

            canvas.StrokeSize = strokeWidth;

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            if (IsRunning)
            {
                canvas.Rotate(MaterialActivityIndicatorRotate, x + strokeWidth + size / 2, y + strokeWidth + size / 2);
                canvas.StrokeColor = new GColor(Material.Color.Blue);
                canvas.DrawArc(x + strokeWidth, y + strokeWidth, size, size, MaterialActivityIndicatorStartAngle, MaterialActivityIndicatorEndAngle, false, false);
            }
            else
            {
                canvas.Rotate(0, x + strokeWidth + size / 2, y + strokeWidth + size / 2);
                canvas.StrokeColor = new GColor(Material.Color.LightBlue);
                canvas.DrawArc(x + strokeWidth, y + strokeWidth, size, size, 0, 360, false, false);
            }

            canvas.RestoreState();
        }

        void AnimateMaterialActivityIndicatorAnimation()
        {
            var materialActivityIndicatorAngleAnimation = new Animation();

            var startAngle = 90;
            var endAngle = 360;

            var rotateAnimation = new Animation(v => MaterialActivityIndicatorRotate = (int)v, 0, 0, easing: Easing.Linear);
            var startAngleAnimation = new Animation(v => MaterialActivityIndicatorStartAngle = (int)v, startAngle, startAngle - 360, easing: Easing.Linear);
            var endAngleAnimation = new Animation(v => MaterialActivityIndicatorEndAngle = (int)v, endAngle, endAngle - 360, easing: Easing.Linear);

            materialActivityIndicatorAngleAnimation.Add(0, 1, rotateAnimation);
            materialActivityIndicatorAngleAnimation.Add(0, 1, startAngleAnimation);
            materialActivityIndicatorAngleAnimation.Add(0, 1, endAngleAnimation);

            materialActivityIndicatorAngleAnimation.Commit(this, "MaterialActivityIndicator", length: 1400, repeat: () => true, finished: (l, c) => materialActivityIndicatorAngleAnimation = null);
        }
    }
}