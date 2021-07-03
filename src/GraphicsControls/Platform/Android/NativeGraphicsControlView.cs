using Android.Content;
using Android.Views;
using Microsoft.Maui.Graphics.Native;

namespace Microsoft.Maui.Graphics.Controls
{
    public class NativeGraphicsControlView : NativeGraphicsView
    {
        readonly float _density;
        IGraphicsHandler? _graphicsControl;
        bool _pressedContained;

        public NativeGraphicsControlView(Context? context) : base(context)
        {
            _density = Resources?.DisplayMetrics?.Density ?? 1.0f;
        }

        public IGraphicsHandler? GraphicsControl
        {
            get => _graphicsControl;
            set => Drawable = _graphicsControl = value;
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e != null)
            {
                var interceptPoint = new Point(e.GetX() / _density, e.GetY() / _density);

                switch (e.Action)
                {
                    case MotionEventActions.Down:
                        PointF[] downPoints = new PointF[] { interceptPoint };
                        GraphicsControl?.StartInteraction(downPoints);
                        _pressedContained = true;
                        break;
                    case MotionEventActions.Move:
                        PointF[] movePoints = new PointF[] { interceptPoint };
                        _pressedContained = GraphicsControl?.PointsContained(movePoints) ?? false;
                        GraphicsControl?.DragInteraction(movePoints);
                        break;
                    case MotionEventActions.Up:
                        PointF[] upPoints = new PointF[] { interceptPoint };
                        GraphicsControl?.EndInteraction(upPoints, _pressedContained);
                        break;
                    case MotionEventActions.Cancel:
                        _pressedContained = false;
                        GraphicsControl?.CancelInteraction();
                        break;
                }
            }

            return base.OnTouchEvent(e);
        }
    }
}