using System.Graphics;
using GraphicsControls.Extensions;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public class BoxView : GraphicsView, IColor, ICornerRadius
    {
        public static readonly BindableProperty ColorProperty = ColorElement.ColorProperty;

        public static readonly BindableProperty CornerRadiusProperty = CornerRadiusElement.CornerRadiusProperty;

        public XColor Color
        {
            get { return (XColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusElement.CornerRadiusProperty); }
            set { SetValue(CornerRadiusElement.CornerRadiusProperty, value); }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            canvas.SaveState();

            var color = Color != XColor.Default ? Color : BackgroundColor;
            canvas.FillColor = color.ToGraphicsColor();

            var x = dirtyRect.X;
            var y = dirtyRect.Y;

            var height = dirtyRect.Height;
            var width = dirtyRect.Width;

            canvas.FillRoundedRectangle(x, y, width, height, (float)CornerRadius);

            canvas.RestoreState();
        }
    }
}