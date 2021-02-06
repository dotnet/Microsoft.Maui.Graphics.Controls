using System;
using System.Collections.Generic;
using System.Graphics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;
using Point = System.Graphics.Point;

namespace GraphicsControls
{
    public partial class Slider : GraphicsVisualView, IRange
    {
        public static class Layers
        {
            public const string TrackBackground = "Slider.Layers.TrackBackground";
            public const string TrackProgress = "Slider.Layers.TrackProgress";
            public const string Thumb = "Slider.Layers.Thumb";
            public const string Text = "Slider.Layers.Text";
        }

        RectangleF _trackRect;
        RectangleF _thumbRect;
        bool _isThumbSelected;
        bool _isDragging;

        public Slider()
        {
            _trackRect = new RectangleF();
            _thumbRect = new RectangleF();
        }

        public Slider(double min, double max, double val) : this()
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException("min");

            if (max > Minimum)
            {
                Maximum = max;
                Minimum = min;
            }
            else
            {
                Minimum = min;
                Maximum = max;
            }

            Value = val.Clamp(min, max);
        }

        public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(Slider), 0d,
            validateValue: (bindable, value) =>
            {
                var slider = (Slider)bindable;
                return (double)value < slider.Maximum;
            }, coerceValue: (bindable, value) =>
            {
                var slider = (Slider)bindable;
                slider.Value = slider.Value.Clamp((double)value, slider.Maximum);
                return value;
            });

        public static readonly BindableProperty MaximumProperty = BindableProperty.Create("Maximum", typeof(double), typeof(Slider), 1d,
            validateValue: (bindable, value) =>
            {
                var slider = (Slider)bindable;
                return (double)value > slider.Minimum;
            }, coerceValue: (bindable, value) =>
            {
                var slider = (Slider)bindable;
                slider.Value = slider.Value.Clamp(slider.Minimum, (double)value);
                return value;
            });

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(Slider), 0d, BindingMode.TwoWay, coerceValue: (bindable, value) =>
            {
                var slider = (Slider)bindable;
                return ((double)value).Clamp(slider.Minimum, slider.Maximum);
            }, propertyChanged: (bindable, oldValue, newValue) =>
            {
                var slider = (Slider)bindable;
                slider.ValueChanged?.Invoke(slider, new ValueChangedEventArgs((double)oldValue, (double)newValue));
            });

        public static readonly BindableProperty MinimumTrackColorProperty =
            BindableProperty.Create(nameof(MinimumTrackColor), typeof(XColor), typeof(Slider), XColor.Default);

        public static readonly BindableProperty MaximumTrackColorProperty =
            BindableProperty.Create(nameof(MaximumTrackColor), typeof(XColor), typeof(Slider), XColor.Default);

        public static readonly BindableProperty ThumbColorProperty =
            BindableProperty.Create(nameof(ThumbColor), typeof(XColor), typeof(Slider), XColor.Default);

        public static readonly BindableProperty DragStartedCommandProperty =
            BindableProperty.Create(nameof(DragStartedCommand), typeof(ICommand), typeof(Slider), default(ICommand));

        public static readonly BindableProperty DragCompletedCommandProperty =
            BindableProperty.Create(nameof(DragCompletedCommand), typeof(ICommand), typeof(Slider), default(ICommand));

        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public XColor MinimumTrackColor
        {
            get { return (XColor)GetValue(MinimumTrackColorProperty); }
            set { SetValue(MinimumTrackColorProperty, value); }
        }

        public XColor MaximumTrackColor
        {
            get { return (XColor)GetValue(MaximumTrackColorProperty); }
            set { SetValue(MaximumTrackColorProperty, value); }
        }

        public XColor ThumbColor
        {
            get { return (XColor)GetValue(ThumbColorProperty); }
            set { SetValue(ThumbColorProperty, value); }
        }

        public ICommand DragStartedCommand
        {
            get { return (ICommand)GetValue(DragStartedCommandProperty); }
            set { SetValue(DragStartedCommandProperty, value); }
        }

        public ICommand DragCompletedCommand
        {
            get { return (ICommand)GetValue(DragCompletedCommandProperty); }
            set { SetValue(DragCompletedCommandProperty, value); }
        }

        public event EventHandler DragStarted;
        public event EventHandler DragCompleted;
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public List<string> SliderLayers = new List<string>
        {
            Layers.TrackBackground,
            Layers.TrackProgress,
            Layers.Thumb,
            Layers.Text
        };

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Cupertino:
                    HeightRequest = 36;
                    break;
                case VisualType.Material:
                default:
                case VisualType.Fluent:
                    HeightRequest = 18;
                    break;
            }

            Value /= Maximum - Minimum;
        }

        public override List<string> GraphicsLayers =>
            SliderLayers;

        public override void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {
            switch (layer)
            {
                case Layers.TrackBackground:
                    DrawSliderTrackBackground(canvas, dirtyRect);
                    break;
                case Layers.TrackProgress:
                    DrawSliderTrackProgress(canvas, dirtyRect);
                    break;
                case Layers.Thumb:
                    DrawSliderThumb(canvas, dirtyRect);
                    break;
                case Layers.Text:
                    DrawSliderText(canvas, dirtyRect);
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == ValueProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == FlowDirectionProperty.PropertyName)
                InvalidateDraw();
        }

        public override void OnTouchDown(Point point)
        {
            base.OnTouchDown(point);

            _isThumbSelected = _thumbRect.Contains(new PointF((float)point.X, (float)point.Y));

            if (_isThumbSelected)
                AnimateMaterialThumbSize(true);

            _isDragging = false;

            UpdateValue(point);
        }

        public override void OnTouchMove(Point point)
        {
            base.OnTouchMove(point);

            UpdateValue(point);

            if (!_isDragging)
                SendDragStarted();

            _isDragging = true;
        }

        public override void OnTouchUp(Point point)
        {
            base.OnTouchUp(point);

            if (_isThumbSelected)
            {
                _isThumbSelected = false;
                AnimateMaterialThumbSize(false);
            }

            if (_isDragging)
                SendDragCompleted();
            
            _isDragging = false;
        }

        protected virtual void DrawSliderTrackBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialSliderTrackBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoSliderTrackBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentSliderTrackBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawSliderTrackProgress(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialSliderTrackProgress(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoSliderTrackProgress(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentSliderTrackProgress(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawSliderThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialSliderThumb(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoSliderThumb(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentSliderThumb(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawSliderText(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Cupertino:
                case VisualType.Material:
                default:
                    break;
                case VisualType.Fluent:
                    DrawFluentSliderText(canvas, dirtyRect);
                    break;
            }
        }

        void UpdateValue(Point point)
        {
            Value = (point.X - _trackRect.X) / _trackRect.Width;
            Value = Value.Clamp(Minimum, Maximum);
        }

        void SendDragStarted()
        {
            if (IsEnabled)
            {
                DragStartedCommand?.Execute(null);
                DragStarted?.Invoke(this, null);
            }
        }

        void SendDragCompleted()
        {
            if (IsEnabled)
            {
                DragCompletedCommand?.Execute(null);
                DragCompleted?.Invoke(this, null);
            }
        }
    }
}