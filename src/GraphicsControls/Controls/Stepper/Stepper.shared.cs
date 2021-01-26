using System;
using System.Graphics;
using System.Runtime.CompilerServices;
using GraphicsControls.Effects;
using GraphicsControls.Helpers;
using Xamarin.Forms;
using Point = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class Stepper : GraphicsVisualView, IRange
    {
        int _digits = 4;
        RectangleF _minusRect;
        RectangleF _plusRect;

        readonly RippleEffect _minusRippleEffect;
        readonly RippleEffect _plusRippleEffect;

        public Stepper()
        {
            _minusRect = RectangleF.Zero;
            _plusRect = RectangleF.Zero;

            _minusRippleEffect = new RippleEffect
            {
                RippleColor = ColorHelper.GetGraphicsColor(XColor.LightGray, XColor.DarkGray)
            };

            _plusRippleEffect = new RippleEffect
            {
                RippleColor = ColorHelper.GetGraphicsColor(XColor.LightGray, XColor.DarkGray)
            };
        }

        public Stepper(double min, double max, double val, double increment) : this()
        {
            if (min >= max)
                throw new ArgumentOutOfRangeException(nameof(min));

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

            Increment = increment;
            Value = val.Clamp(min, max);
        }

        public static readonly BindableProperty MinimumProperty = RangeElement.MinimumProperty;

        public static readonly BindableProperty MaximumProperty = RangeElement.MaximumProperty;

        public static readonly BindableProperty IncrementProperty =
            BindableProperty.Create(nameof(Increment), typeof(double), typeof(Stepper), 1.0,
                propertyChanged: (b, o, n) => { ((Stepper)b)._digits = (int)(-Math.Log10((double)n) + 4).Clamp(1, 15); });

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(double), typeof(Stepper), 0.0, BindingMode.TwoWay,
                coerceValue: (bindable, value) =>
                {
                    var stepper = (Stepper)bindable;
                    return Math.Round((double)value, stepper._digits).Clamp(stepper.Minimum, stepper.Maximum);
                },
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    var stepper = (Stepper)bindable;
                    stepper.ValueChanged?.Invoke(stepper, new ValueChangedEventArgs((double)oldValue, (double)newValue));
                });


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

        public double Increment
        {
            get => (double)GetValue(IncrementProperty);
            set => SetValue(IncrementProperty, value);
        }

        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Cupertino:
                    HeightRequest = CupertinoStepperHeight;
                    WidthRequest = CupertinoStepperWidth;
                    break;
                case VisualType.Material:
                default:
                    HeightRequest = MaterialStepperHeight;
                    WidthRequest = MaterialStepperWidth;

                    GraphicsEffects.Add(_minusRippleEffect);
                    GraphicsEffects.Add(_plusRippleEffect);
                    break;
                case VisualType.Fluent:
                    HeightRequest = FluentStepperHeight;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawStepperBackground(canvas, dirtyRect);
            DrawStepperSeparator(canvas, dirtyRect);
            DrawStepperMinus(canvas, dirtyRect);
            DrawStepperPlus(canvas, dirtyRect);
            DrawStepperText(canvas, dirtyRect);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == FlowDirectionProperty.PropertyName)
                InvalidateDraw();
        }

        public override void OnTouchDown(Point point)
        {
            base.OnTouchDown(point);
            
            var touchDownPoint = new PointF((float)point.X, (float)point.Y);

            if (VisualType == VisualType.Material)
            {
                _minusRippleEffect.ClipRectangle = _minusRect;
                _plusRippleEffect.ClipRectangle = _plusRect;
                _minusRippleEffect.TouchPoint = _plusRippleEffect.TouchPoint = touchDownPoint;
            }

            if (_minusRect.Contains(touchDownPoint))
                Value -= Increment;

            if (_plusRect.Contains(touchDownPoint))
                Value += Increment;
        }

        protected virtual void DrawStepperBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Cupertino:
                    DrawCupertinoStepperBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentStepperBackground(canvas, dirtyRect);
                    DrawFluentStepperBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawStepperSeparator(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Cupertino:
                    DrawCupertinoStepperSeparator(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawStepperMinus(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialStepperMinus(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoStepperMinus(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentStepperDown(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawStepperPlus(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialStepperPlus(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoStepperPlus(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentStepperUp(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawStepperText(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Fluent:
                    DrawFluentStepperText(canvas, dirtyRect);
                    break;
            }
        }
    }
}