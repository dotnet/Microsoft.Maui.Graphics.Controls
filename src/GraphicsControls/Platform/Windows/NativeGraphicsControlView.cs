using Microsoft.Maui.Graphics.Win2D;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Diagnostics;

namespace Microsoft.Maui.Graphics.Controls
{
    public class NativeGraphicsControlView : UserControl
    {
        readonly W2DGraphicsView _w2DGraphicsView;
        IGraphicsHandler? _graphicsControl;
        bool _pressedContained;

        public NativeGraphicsControlView()
        {
            _w2DGraphicsView = new W2DGraphicsView();

            Content = _w2DGraphicsView;

            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;
            PointerCanceled += OnPointerCanceled;
        }

        public IGraphicsHandler? GraphicsControl
        {
            get => _graphicsControl;
            set => _w2DGraphicsView.Drawable = _graphicsControl = value;
        }

        public void Invalidate()
        {
            _w2DGraphicsView?.Invalidate();
        }

        void OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(this);
                var point = currentPoint.Position;
                PointF[] pressedPoints = new PointF[] { new PointF((float)point.X, (float)point.Y) };

                GraphicsControl?.StartInteraction(pressedPoints);
                _pressedContained = true;
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
                var currentPoint = e.GetCurrentPoint(this);
                var point = currentPoint.Position;
                PointF[] movedPoints = new PointF[] { new PointF((float)point.X, (float)point.Y) };

                _pressedContained = GraphicsControl?.PointsContained(movedPoints) ?? false;
                GraphicsControl?.DragInteraction(movedPoints);
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
                var currentPoint = e.GetCurrentPoint(this);
                var point = currentPoint.Position;
                PointF[] releasedPoints = new PointF[] { new PointF((float)point.X, (float)point.Y) };

                GraphicsControl?.EndInteraction(releasedPoints, _pressedContained);
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
                _pressedContained = false;
                GraphicsControl?.CancelInteraction();
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured cancelling the touches within the control.", exc);
            }
        }
    }
}