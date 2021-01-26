using System;
using System.Graphics;
using System.Runtime.CompilerServices;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Point = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class Button : GraphicsVisualView, ICornerRadius
    {
        readonly RippleEffect _rippleEffect;
        RectangleF _backgroundRect;

        public Button()
        {
            _backgroundRect = RectangleF.Zero;

            _rippleEffect = new RippleEffect();

            TextColor = XColor.White;
            CornerRadius = 2;
        }

        public static readonly BindableProperty TextProperty = TextElement.TextProperty;

        public static readonly BindableProperty TextColorProperty = TextElement.TextColorProperty;

        public static readonly BindableProperty CornerRadiusProperty = CornerRadiusElement.CornerRadiusProperty;

        public string Text
        {
            get { return (string)GetValue(TextElement.TextProperty); }
            set { SetValue(TextElement.TextProperty, value); }
        }

        public XColor TextColor
        {
            get { return (XColor)GetValue(TextElement.TextColorProperty); }
            set { SetValue(TextElement.TextColorProperty, value); }
        }

        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusElement.CornerRadiusProperty); }
            set { SetValue(CornerRadiusElement.CornerRadiusProperty, value); }
        }

        public event EventHandler Clicked;
        public event EventHandler Pressed;
        public event EventHandler Released;

        public override void Load()
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = MaterialBackgroundHeight + MaterialShadowOffset;

                    GraphicsEffects.Add(_rippleEffect);
                    break;
                case VisualType.Cupertino:
                    HeightRequest = 44;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 32;
                    break;
            }

            base.Load();
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            DrawButtonBackground(canvas, dirtyRect);
            DrawButtonText(canvas, dirtyRect);

            base.Draw(canvas, dirtyRect);
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

            if (VisualType == VisualType.Material)
            {
                _rippleEffect.ClipRectangle = _backgroundRect;

                var touchDownPoint = new PointF((float)point.X, (float)point.Y);
                _rippleEffect.TouchPoint = touchDownPoint;
            }

            Pressed?.Invoke(this, EventArgs.Empty);
            Clicked?.Invoke(this, EventArgs.Empty);
        }

        public override void OnTouchUp(Point point)
        {
            base.OnTouchUp(point);

            Released?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void DrawButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialButtonBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoButtonBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentButtonBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawButtonText(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialButtonText(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoButtonText(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentButtonText(canvas, dirtyRect);
                    break;
            }
        }
    }
}