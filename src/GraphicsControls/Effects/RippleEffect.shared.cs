using System;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms;
using GColor = System.Graphics.Color;

namespace GraphicsControls.Effects
{
    public class RippleEffect : IGraphicsEffect
    {
        GraphicsView _graphicsView;
        float _rippleEffectSize;

        public RippleEffect()
        {
            RippleColor = Colors.White;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        float RippleEffectSize
        {
            get { return _rippleEffectSize; }
            set
            {
                _rippleEffectSize = value;
                _graphicsView?.InvalidateDraw();
            }
        }

        public GColor RippleColor { get; set; }

        public RectangleF ClipRectangle { get; set; }

        public PointF TouchPoint { get; set; }
        
        public void AttachTo(GraphicsView graphicsView)
        {
            _graphicsView = graphicsView;
            graphicsView.TouchUp += OnGraphicsViewTouchUp;
        }

        public void DetachFrom(GraphicsView graphicsView)
        {
            _graphicsView = null;
            graphicsView.TouchUp -= OnGraphicsViewTouchUp;
        }

        public void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            if (ClipRectangle == RectangleF.Zero || ClipRectangle.Contains(TouchPoint))
            {
                canvas.SaveState();

                if (ClipRectangle == RectangleF.Zero)
                    ClipRectangle = dirtyRect;

                canvas.ClipRectangle(ClipRectangle);

                canvas.FillColor = RippleColor;
                canvas.Alpha = 0.25f;
                canvas.FillCircle((float)TouchPoint.X, (float)TouchPoint.Y, RippleEffectSize);

                canvas.RestoreState();
            }
        }

        void OnGraphicsViewTouchUp(object sender, EventArgs e)
        {
            if (ClipRectangle == RectangleF.Zero || ClipRectangle.Contains(TouchPoint))
                AnimateDrawRipple();
        }

        void AnimateDrawRipple()
        {
            var from = 0;
            var to = ClipRectangle != RectangleF.Zero ? ClipRectangle.Width : 1000;

            var thumbSizeAnimation = new Animation(v => RippleEffectSize = (int)v, from, to, easing: Easing.SinInOut);
            thumbSizeAnimation.Commit(_graphicsView, "RippleEffectAnimation", length: 350, finished: (l, c) =>
            {
                _rippleEffectSize = 0;
                thumbSizeAnimation = null;
            });
        }
    }
}
