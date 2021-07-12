using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Microsoft.Maui.Graphics.Native;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsDatePicker : View, IMixedNativeView
    {
        DateTime _date;
        DateTime _minimumDate;
        DateTime _maximumDate;

        IMixedGraphicsHandler? _graphicsControl;
        readonly NativeCanvas _canvas;
        readonly ScalingCanvas _scalingCanvas;
        readonly float _scale;

        int _width, _height;
        Color? _backgroundColor;
        IDrawable? _drawable;

        DatePickerDialog? _dialog;

        public GraphicsDatePicker(Context context) : base(context)
        {
            _scale = Resources?.DisplayMetrics?.Density ?? 1;
            _canvas = new NativeCanvas(context);
            _scalingCanvas = new ScalingCanvas(_canvas);

            Touch += OnTouch;
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

        public Color? BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                Invalidate();
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

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                if (_dialog != null)
                {
                    _dialog.Dispose();
                    _dialog = null;
                }

                Touch -= OnTouch;
            }
        }

        public override void Draw(Canvas? androidCanvas)
        {
            if (_drawable == null)
                return;

            var dirtyRect = new RectangleF(0, 0, _width, _height);

            _canvas.Canvas = androidCanvas;

            if (_backgroundColor != null)
            {
                _canvas.FillColor = _backgroundColor;
                _canvas.FillRectangle(dirtyRect);
                _canvas.FillColor = Colors.White;
            }

            _scalingCanvas.ResetState();
            _scalingCanvas.Scale(_scale, _scale);

            dirtyRect.Height /= _scale;
            dirtyRect.Width /= _scale;
            _drawable.Draw(_scalingCanvas, dirtyRect);
            _canvas.Canvas = null;
        }

        protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
        {
            base.OnSizeChanged(width, height, oldWidth, oldHeight);
            _width = width;
            _height = height;
        }

        public override bool OnTouchEvent(MotionEvent? e)
        {
            if (e != null)
            {
                var density = Resources?.DisplayMetrics?.Density ?? 1.0f;
                var interceptPoint = new Point(e.GetX() / density, e.GetY() / density);

                if (e.Action == MotionEventActions.Down)
                {
                    PointF[] downPoints = new PointF[] { interceptPoint };
                    GraphicsControl?.StartInteraction(downPoints);
                }
            }

            return base.OnTouchEvent(e);
        }

        void OnTouch(object? sender, TouchEventArgs e)
        {
            if (e.Event?.Action != MotionEventActions.Up)
                return;

            if (_dialog != null)
                _dialog.Dispose();

            CreateDialog(Date);
            UpdateMinimumDate(MinimumDate);
            UpdateMaximumDate(MaximumDate);

            if (_dialog == null)
                return;

            _dialog.CancelEvent += OnCancelButtonClicked;
            _dialog.Show();
        }

        void CreateDialog(DateTime date)
        {
            _dialog = new DatePickerDialog(Context!, (o, e) =>
            {
                UpdateDate(e.Date);
                ClearFocus();

                if (_dialog != null)
                    _dialog.CancelEvent -= OnCancelButtonClicked;

                _dialog = null;
            }, date.Year, date.Month - 1, date.Day);

            _dialog.SetCanceledOnTouchOutside(true);
        }

        void OnCancelButtonClicked(object? sender, EventArgs e)
        {
            ClearFocus();
        }

        void UpdateDate(DateTime date)
        {
            DateSelected?.Invoke(this, new DateSelectedEventArgs(date));
        }

        void UpdateMinimumDate(DateTime minimumDate)
        {
            if (_dialog != null)
                _dialog.DatePicker.MinDate = (long)minimumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }

        void UpdateMaximumDate(DateTime maximumDate)
        {
            if (_dialog != null)
                _dialog.DatePicker.MaxDate = (long)maximumDate.ToUniversalTime().Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds;
        }
    }
}