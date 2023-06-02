#nullable disable
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Maui.Graphics.Win2D;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using WSize = Windows.Foundation.Size;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsTimePicker : UserControl, IMixedPlatformView
    {
        TimeSpan _time;

        TimePickerFlyout _timePickerFlyout;
        CanvasControl _canvasControl;
        readonly W2DCanvas _canvas = new W2DCanvas();

        IMixedGraphicsHandler _graphicsControl;
        IDrawable _drawable;
        RectF _dirty;

        public GraphicsTimePicker()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            PointerPressed += OnPointerPressed;
        }

        public TimeSpan Time
        {
            get { return _time; }
            set
            {
                _time = value;
                UpdateTime(_time);
            }
        }

        public IMixedGraphicsHandler GraphicsControl
        {
            get => _graphicsControl;
            set => Drawable = _graphicsControl = value;
        }

        public IDrawable Drawable
        {
            get => _drawable;
            set
            {
                _drawable = value;
                Invalidate();
            }
        }

        public event EventHandler<TimeSelectedEventArgs> TimeSelected;

        static readonly string[] DefaultPlatformLayers = new string[] { };

        public string[] PlatformLayers => DefaultPlatformLayers;

        public void DrawBaseLayer(RectF dirtyRect) { }

        public void Invalidate()
        {
            _canvasControl?.Invalidate();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            _timePickerFlyout = new TimePickerFlyout { Placement = FlyoutPlacementMode.Top };
            _timePickerFlyout.TimePicked += OnTimePicked;

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

            _canvas.Session = args.DrawingSession;
            _canvas.CanvasSize = new WSize(_dirty.Width, _dirty.Height);
            _drawable.Draw(_canvas, _dirty);
        }

        void OnPointerPressed(object sender, UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            _timePickerFlyout?.ShowAt(_canvasControl);
        }

        void OnTimePicked(TimePickerFlyout sender, TimePickedEventArgs args)
        {
            UpdateTime(args.NewTime);
        }

        void UpdateTime(TimeSpan time)
        {
            if (_timePickerFlyout == null)
                return;

            _timePickerFlyout.Time = time;

            TimeSelected?.Invoke(this, new TimeSelectedEventArgs(time));
        }
    }
}