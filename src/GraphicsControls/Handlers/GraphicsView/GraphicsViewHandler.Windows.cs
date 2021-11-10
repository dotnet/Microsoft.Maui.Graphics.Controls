using Microsoft.Maui.Graphics.Win2D;
using Microsoft.Maui.Handlers;
using Microsoft.UI.Xaml.Input;
using System;
using System.Diagnostics;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsViewHandler : ViewHandler<IGraphicsView, W2DGraphicsView>
    {
        protected override W2DGraphicsView CreateNativeView() => new W2DGraphicsView
        {
            Drawable = VirtualView
        };

        protected override void ConnectHandler(W2DGraphicsView nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.PointerPressed += OnPointerPressed;
            nativeView.PointerMoved += OnPointerMoved;
            nativeView.PointerReleased += OnPointerReleased;
            nativeView.PointerCanceled += OnPointerCanceled;
        }

        protected override void DisconnectHandler(W2DGraphicsView nativeView)
        {
            base.DisconnectHandler(nativeView);

            nativeView.PointerPressed -= OnPointerPressed;
            nativeView.PointerMoved -= OnPointerMoved;
            nativeView.PointerReleased -= OnPointerReleased;
            nativeView.PointerCanceled -= OnPointerCanceled;
        }
        
        public static void MapInvalidate(GraphicsViewHandler handler, IGraphicsView graphicsView, object? arg)
        {
            handler.NativeView?.Invalidate();
        }

        void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(NativeView);
                var currentPosition = currentPoint.Position;
                var point = new Point(currentPosition.X, currentPosition.Y);

                VirtualView?.OnTouchDown(point);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch event within the control.", exc);
            }
        }

        void OnPointerMoved(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(NativeView);
                var currentPosition = currentPoint.Position;
                var point = new Point(currentPosition.X, currentPosition.Y);

                VirtualView?.OnTouchMove(point);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch moved event within the control.", exc);
            }
        }

        void OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(NativeView);
                var currentPosition = currentPoint.Position;
                var point = new Point(currentPosition.X, currentPosition.Y);

                VirtualView?.OnTouchUp(point);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch ended event within the control.", exc);
            }
        }

        void OnPointerCanceled(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(NativeView);
                var currentPosition = currentPoint.Position;
                var point = new Point(currentPosition.X, currentPosition.Y);

                VirtualView?.OnTouchUp(point);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured cancelling the touches within the control.", exc);
            }
        }
    }
}