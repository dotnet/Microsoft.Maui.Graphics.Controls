using Android.Views;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;
using APointF = Android.Graphics.PointF;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsViewHandler : ViewHandler<IGraphicsView, PlatformGraphicsView>
    {
        protected override PlatformGraphicsView CreateNativeView()
        {
            var nativeGraphicsView = new PlatformGraphicsView(Context)
            {
                Drawable = VirtualView
            };

            return nativeGraphicsView;
        }

        protected override void ConnectHandler(PlatformGraphicsView nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.ViewAttachedToWindow += OnViewAttachedToWindow;
            nativeView.ViewDetachedFromWindow += OnViewDetachedFromWindow;
            nativeView.Touch += OnTouch;
        }

        protected override void DisconnectHandler(PlatformGraphicsView nativeView)
        {
            base.DisconnectHandler(nativeView); 
            
            nativeView.ViewAttachedToWindow -= OnViewAttachedToWindow;
            nativeView.ViewDetachedFromWindow -= OnViewDetachedFromWindow; 
            nativeView.Touch -= OnTouch;
        }
        
        public static void MapInvalidate(GraphicsViewHandler handler, IGraphicsView graphicsView, object? arg)
        {
            handler.NativeView?.Invalidate();
        }

        void OnViewAttachedToWindow(object? sender, View.ViewAttachedToWindowEventArgs e)
        {
            VirtualView?.Load();
        }

        void OnViewDetachedFromWindow(object? sender, View.ViewDetachedFromWindowEventArgs e)
        {
            VirtualView?.Unload();
        }

        void OnTouch(object? sender, View.TouchEventArgs e)
        {
            if (e.Event == null)
                return;

            float density = Context?.Resources?.DisplayMetrics?.Density ?? 1.0f;
            APointF point = new APointF(e.Event.GetX() / density, e.Event.GetY() / density);

            switch (e.Event.Action)
            {
                case MotionEventActions.Down:
                    VirtualView?.OnTouchDown(new Point(point.X, point.Y));
                    break;
                case MotionEventActions.Move:
                    VirtualView?.OnTouchMove(new Point(point.X, point.Y));
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Cancel:
                    VirtualView?.OnTouchUp(new Point(point.X, point.Y));
                    break;
                default:
                    break;
            }
        }
    }
}
