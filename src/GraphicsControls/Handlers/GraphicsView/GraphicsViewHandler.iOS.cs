using Foundation;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Handlers;
using System;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public class TouchEventArgs : EventArgs
    {
        public TouchEventArgs(Point point)
        {
            Point = point;
        }

        public Point Point { get; set; }
    }

    public class TouchNativeGraphicsView : PlatformGraphicsView
    {
        public event EventHandler<TouchEventArgs>? TouchDown;
        public event EventHandler<TouchEventArgs>? TouchMove;
        public event EventHandler<TouchEventArgs>? TouchUp;

        public override void TouchesBegan(NSSet touches, UIEvent? evt)
        {
            base.TouchesBegan(touches, evt);

            var viewPoints = this.GetPointsInView(evt);
            PointF viewPoint = viewPoints.Length > 0 ? viewPoints[0] : PointF.Zero;
            var point = new Point(viewPoint.X, viewPoint.Y);

            TouchDown?.Invoke(this, new TouchEventArgs(point));
        }

        public override void TouchesMoved(NSSet touches, UIEvent? evt)
        {
            base.TouchesMoved(touches, evt);

            var viewPoints = this.GetPointsInView(evt);
            PointF viewPoint = viewPoints.Length > 0 ? viewPoints[0] : PointF.Zero;
            var point = new Point(viewPoint.X, viewPoint.Y);

            TouchMove?.Invoke(this, new TouchEventArgs(point));
        }

        public override void TouchesEnded(NSSet touches, UIEvent? evt)
        {
            base.TouchesEnded(touches, evt);

            var viewPoints = this.GetPointsInView(evt);
            PointF viewPoint = viewPoints.Length > 0 ? viewPoints[0] : PointF.Zero;
            var point = new Point(viewPoint.X, viewPoint.Y);

            TouchUp?.Invoke(this, new TouchEventArgs(point));
        }

        public override void TouchesCancelled(NSSet touches, UIEvent? evt)
        {
            base.TouchesCancelled(touches, evt);

            var viewPoints = this.GetPointsInView(evt);
            PointF viewPoint = viewPoints.Length > 0 ? viewPoints[0] : PointF.Zero;
            var point = new Point(viewPoint.X, viewPoint.Y);

            TouchUp?.Invoke(this, new TouchEventArgs(point));
        }
    }

    public partial class GraphicsViewHandler : ViewHandler<IGraphicsView, TouchNativeGraphicsView>
    {
        const NSKeyValueObservingOptions observingOptions = NSKeyValueObservingOptions.Initial | NSKeyValueObservingOptions.OldNew | NSKeyValueObservingOptions.Prior;

        IDisposable? _isLoadedObserverDisposable;

        protected override TouchNativeGraphicsView CreatePlatformView()
        {
            var nativeGraphicsView = new TouchNativeGraphicsView
            {
                UserInteractionEnabled = true,
                BackgroundColor = UIColor.Clear,
                Drawable = VirtualView
            };

            return nativeGraphicsView;
        }

        protected override void ConnectHandler(TouchNativeGraphicsView platformView)
        {
            base.ConnectHandler(platformView);

            var key = platformView.Superview == null ? "subviews" : "superview";
            _isLoadedObserverDisposable = platformView.AddObserver(key, observingOptions, OnViewLoadedObserver);

            platformView.TouchDown += OnTouchDown;
            platformView.TouchMove += OnTouchMove;
            platformView.TouchUp += OnTouchUp;
        }

        protected override void DisconnectHandler(TouchNativeGraphicsView platformView)
        {
            base.DisconnectHandler(platformView);

            _isLoadedObserverDisposable?.Dispose();
            _isLoadedObserverDisposable = null;

            platformView.TouchDown -= OnTouchDown;
            platformView.TouchMove -= OnTouchMove;
            platformView.TouchUp -= OnTouchUp;
        }
        
        public static void MapInvalidate(GraphicsViewHandler handler, IGraphicsView graphicsView, object? arg)
        {
            handler.PlatformView?.InvalidateDrawable();
        }

        void OnViewLoadedObserver(NSObservedChange nSObservedChange)
        {
            if (!nSObservedChange?.NewValue?.Equals(NSNull.Null) ?? false)
            {
                VirtualView?.Load();
            }
            else if (!nSObservedChange?.OldValue?.Equals(NSNull.Null) ?? false)
            {
                VirtualView?.Unload();

                _isLoadedObserverDisposable?.Dispose();
                _isLoadedObserverDisposable = null;
            }
        }

        void OnTouchDown(object? sender, TouchEventArgs e)
        {
            VirtualView?.OnTouchDown(e.Point);
        }

        void OnTouchMove(object? sender, TouchEventArgs e)
        {
            VirtualView?.OnTouchMove(e.Point);
        }

        void OnTouchUp(object? sender, TouchEventArgs e)
        {
            VirtualView?.OnTouchUp(e.Point);
        }
    }
}