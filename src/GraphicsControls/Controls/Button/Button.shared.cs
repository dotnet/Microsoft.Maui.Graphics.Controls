using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Graphics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Point = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class Button : GraphicsVisualView, IButton, ICornerRadius
    {
        public static class Layers
        {
            public const string Background = "Button.Layers.Background";
            public const string Text = "Button.Layers.Text";
        }

        readonly RippleEffect _rippleEffect;
        RectangleF _backgroundRect;

        public Button()
        {
            _backgroundRect = RectangleF.Zero;

            _rippleEffect = new RippleEffect();

            TextColor = XColor.White;
            CornerRadius = 2;
        }

        public static readonly BindableProperty CommandProperty = ButtonElement.CommandProperty;

        public static readonly BindableProperty CommandParameterProperty = ButtonElement.CommandParameterProperty;

        internal static readonly BindablePropertyKey IsPressedPropertyKey =
            BindableProperty.CreateReadOnly(nameof(IsPressed), typeof(bool), typeof(Button), default(bool));

        public static readonly BindableProperty IsPressedProperty = IsPressedPropertyKey.BindableProperty;

        public static readonly BindableProperty TextProperty = TextElement.TextProperty;

        public static readonly BindableProperty TextColorProperty = TextElement.TextColorProperty;

        public static readonly BindableProperty CornerRadiusProperty = CornerRadiusElement.CornerRadiusProperty;

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public bool IsPressed => (bool)GetValue(IsPressedProperty);

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

        bool IButton.IsEnabledCore
        {
            set { SetValueCore(IsEnabledProperty, value); }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IButton.SetIsPressed(bool isPressed) => SetValue(IsPressedPropertyKey, isPressed);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IButton.PropagateUpClicked() => Clicked?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IButton.PropagateUpPressed() => Pressed?.Invoke(this, EventArgs.Empty);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void IButton.PropagateUpReleased() => Released?.Invoke(this, EventArgs.Empty);

        void IButton.OnCommandCanExecuteChanged(object sender, EventArgs e) =>
            ButtonElement.CommandCanExecuteChanged(this, EventArgs.Empty);


        public List<string> ButtonLayers = new List<string>
        {
            Layers.Background,
            Layers.Text
        };

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

        public override List<string> GraphicsLayers =>
            ButtonLayers;

        public override void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {
            switch (layer)
            {
                case Layers.Background:
                    DrawButtonBackground(canvas, dirtyRect);
                    break;
                case Layers.Text:
                    DrawButtonText(canvas, dirtyRect);
                    break;
            }
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