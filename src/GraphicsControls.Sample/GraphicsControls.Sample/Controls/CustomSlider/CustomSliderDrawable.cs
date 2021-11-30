using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Controls;

namespace GraphicsControls.Sample.Controls
{
    public class CustomSliderDrawable : Slider
    {

    }

    public class CustomSliderDrawableHandler : SliderHandler
    {
        protected override ISliderDrawable CreateDrawable() => new CustomDrawable();
    }

    public class CustomDrawable : ViewDrawable<ISlider>, ISliderDrawable
    {
        public bool IsDragging { get; set; }

        RectangleF trackRect = new RectangleF();
        public RectangleF TrackRect => trackRect;

        RectangleF touchTargetRect = new RectangleF(0, 0, 44, 44);
        public RectangleF TouchTargetRect => touchTargetRect;

        public void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            canvas.SaveState();

            canvas.FillColor = Color.FromArgb("#7630FE"); //slider.MaximumTrackColor.WithDefault(Colors.White.ToArgbHex());

            var x = dirtyRect.X;

            var width = dirtyRect.Width;
            var height = 1;

            trackRect.X = x;
            trackRect.Width = width;

            var y = (float)((dirtyRect.Height - height) / 2);

            canvas.FillRoundedRectangle(x, y, width, height, 6);

            canvas.RestoreState();
        }
        
        public void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {

        }

        public void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {
            float size = 38f;
            float strokeWidth = 0.5f;

            var value = slider.Value / slider.Maximum - slider.Minimum;
            var x = (float)((dirtyRect.Width * value) - (size / 2));

            if (x <= strokeWidth)
                x = strokeWidth;

            if (x >= dirtyRect.Width - (size + strokeWidth))
                x = dirtyRect.Width - (size + strokeWidth);

            var y = (float)((dirtyRect.Height - size) / 2);

            canvas.SaveState();

            canvas.StrokeColor = Color.FromArgb("#4F01E0"); //slider.ThumbColor.WithDefault(Colors.Black.ToArgbHex());
            canvas.StrokeSize = strokeWidth;

            var linearGradientPaint = new LinearGradientPaint
            {
                StartColor = Color.FromArgb("#B589D6"),
                EndColor = Color.FromArgb("#552586")
            };

            linearGradientPaint.StartPoint = new Point(0, 0);
            linearGradientPaint.EndPoint = new Point(0.8, 1.0);

            canvas.SetFillPaint(linearGradientPaint, new RectangleF(x, y, size, size)); //slider.ThumbColor.WithDefault(Colors.White.ToArgbHex());

            canvas.SetShadow(new SizeF(6, 6), 6, Color.FromArgb("#99330194"));

            touchTargetRect.Center(new PointF(x, y));

            canvas.FillRoundedRectangle(x, y, size, size, 6);

            canvas.RestoreState();

            canvas.SaveState();

            canvas.SetShadow(new SizeF(-6, -6), 6, Color.FromArgb("#999E27FF"));

            canvas.DrawRoundedRectangle(x, y, size, size, 6);

            canvas.RestoreState(); 
            
            canvas.RestoreState();

            float marginX = 3;
            float marginY = 13;

            canvas.Translate(x + marginX, y + marginY);

            var vBuilder = new PathBuilder();
            var path = vBuilder.BuildPath("M6.0190067,0.16500799L7.4250088,1.5890061 3.9923439,4.979006 14.419033,4.979006 14.419033,6.9799988 3.699379,6.9799988 7.4300079,10.708995 6.015007,12.123994 0,6.1090004z M25.982016,0L32.001003,5.9430005 25.985983,11.958999 24.571004,10.543993 28.167773,6.9479963 18.027001,6.9479963 18.027001,4.9470035 28.144115,4.9470035 24.576009,1.4240091z");

            canvas.FillColor = Colors.White;
            canvas.FillPath(path);

            canvas.SaveState();
        }

        public void DrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISlider slider)
        {

        }
    }
}