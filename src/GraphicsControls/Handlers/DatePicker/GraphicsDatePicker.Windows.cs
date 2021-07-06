using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.Maui.Graphics.Win2D;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsDatePicker : UserControl, IMixedNativeView
    {
        DateTime _date;
        DateTime _minimumDate;
        DateTime _maximumDate;

        DatePickerFlyout? _datePickerFlyout;
        CanvasControl? _canvasControl;
        readonly W2DCanvas _canvas = new W2DCanvas();

        IMixedGraphicsHandler? _graphicsControl;
        IDrawable? _drawable;
        RectangleF _dirty;

        public GraphicsDatePicker()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
            PointerPressed += OnPointerPressed;
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                UpdateDate(_date);
            }
        }

        public DateTime MinimumDate
        {
            get { return _minimumDate; }
            set
            {
                _minimumDate = value;
                UpdateMinimumDate(_minimumDate);
            }
        }

        public DateTime MaximumDate
        {
            get { return _maximumDate; }
            set
            {
                _maximumDate = value;
                UpdateMaximumDate(_maximumDate);
            }
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

        public event EventHandler<DateSelectedEventArgs>? DateSelected;

        static readonly string[] DefaultNativeLayers = new string[] { };

        public string[] NativeLayers => DefaultNativeLayers;

        public void DrawBaseLayer(RectangleF dirtyRect) { }

        public void Invalidate()
        {
            _canvasControl?.Invalidate();
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            _datePickerFlyout = new DatePickerFlyout { Placement = FlyoutPlacementMode.Top };
            _datePickerFlyout.DatePicked += OnDatePicked;

            _canvasControl = new CanvasControl();
            _canvasControl.Draw += OnDraw;
            Content = _canvasControl;
        }

        void OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            UpdateDate(args.NewDate.Date);
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
            _datePickerFlyout?.ShowAt(_canvasControl);
        }

        void UpdateDate(DateTime date)
        {
            if (_datePickerFlyout == null)
                return;

            _datePickerFlyout.Date = date;

            DateSelected?.Invoke(this, new DateSelectedEventArgs(date));
        }

        void UpdateMaximumDate(DateTime maximumDate)
        {
            if (_datePickerFlyout != null)
                _datePickerFlyout.MaxYear = maximumDate;
        }

        void UpdateMinimumDate(DateTime minimumDate)
        {
            if (_datePickerFlyout != null)
                _datePickerFlyout.MinYear = minimumDate;
        }
    }
}