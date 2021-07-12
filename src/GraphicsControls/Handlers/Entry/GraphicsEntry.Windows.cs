using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Maui.Graphics.Win2D;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Diagnostics;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEntry : UserControl, IMixedNativeView
    {
        CanvasControl? _canvasControl;
        readonly W2DCanvas _canvas = new W2DCanvas();
        IMixedGraphicsHandler? _graphicsControl;
        IDrawable? _drawable;
        RectangleF _dirty;

        public GraphicsEntry()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            PointerPressed += OnPointerPressed;
        }

        public IMixedGraphicsHandler? GraphicsControl
        {
            get => _graphicsControl;
            set => Drawable = _graphicsControl = value;
        }
             
        public IDrawable? Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                Invalidate();
            }
        }

        static readonly string[] DefaultNativeLayers = new[] { nameof(IEntry.Text) };

        public string[] NativeLayers => DefaultNativeLayers;

        public void DrawBaseLayer(RectangleF dirtyRect) { }

        public void Invalidate()
        {
            _canvasControl?.Invalidate();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            _canvasControl = new CanvasControl();
            _canvasControl.Draw += OnDraw;
            Content = _canvasControl;
        }

        void OnUnloaded(object sender, RoutedEventArgs e)
        {
            // Explicitly remove references to allow the Win2D controls to get garbage collected
            if (_canvasControl != null)
            {
                _canvasControl.RemoveFromVisualTree();
                _canvasControl = null;
            }
        }

        void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            if (_drawable == null)
                return;

            _dirty.X = 0f;
            _dirty.Y = 0f;
            _dirty.Width = (float)sender.ActualWidth;
            _dirty.Height = (float)sender.ActualHeight;

            W2DGraphicsService.ThreadLocalCreator = sender;
            _canvas.Session = args.DrawingSession;
            _canvas.CanvasSize = new Windows.Foundation.Size(_dirty.Width, _dirty.Height);
            _drawable.Draw(_canvas, _dirty);
            W2DGraphicsService.ThreadLocalCreator = null;
        }

        void OnPointerPressed(object sender, UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            try
            {
                var currentPoint = e.GetCurrentPoint(this);
                var point = currentPoint.Position;
                PointF[] pressedPoints = new PointF[] { new PointF((float)point.X, (float)point.Y) };

                GraphicsControl?.StartInteraction(pressedPoints);
            }
            catch (Exception exc)
            {
                Debug.WriteLine("An unexpected error occured handling a touch event within the control.", exc);
            }
        }
    }
}