using System;
using System.Graphics;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class Switch : GraphicsVisualView
    {
        public const string SwitchOnVisualState = "On";
        public const string SwitchOffVisualState = "Off";

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(Switch), false, propertyChanged: (bindable, oldValue, newValue) =>
        {
            ((Switch)bindable).Toggled?.Invoke(bindable, new ToggledEventArgs((bool)newValue));
            ((Switch)bindable).ChangeVisualState();
        }, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty OnColorProperty =
            BindableProperty.Create(nameof(OnColor), typeof(XColor), typeof(Switch), XColor.Default);

        public static readonly BindableProperty ThumbColorProperty =
            BindableProperty.Create(nameof(ThumbColor), typeof(XColor), typeof(Switch), XColor.Default);

        public bool IsToggled
        {
            get { return (bool)GetValue(IsToggledProperty); }
            set { SetValue(IsToggledProperty, value); }
        }

        public XColor OnColor
        {
            get { return (XColor)GetValue(OnColorProperty); }
            set { SetValue(OnColorProperty, value); }
        }

        public XColor ThumbColor
        {
            get { return (XColor)GetValue(ThumbColorProperty); }
            set { SetValue(ThumbColorProperty, value); }
        }

        public event EventHandler<ToggledEventArgs> Toggled;

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Cupertino:
                    HeightRequest = 30;
                    WidthRequest = 51;
                    break;
                case VisualType.Material:
                    HeightRequest = 24;
                    WidthRequest = 42;
                    break;
                case VisualType.Fluent:
                default:
                    HeightRequest = 20;
                    WidthRequest = 40;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawSwitchBackground(canvas, dirtyRect);
            DrawSwitchThumb(canvas, dirtyRect);
        }

        protected override void ChangeVisualState()
        {
            base.ChangeVisualState();
            if (IsEnabled && IsToggled)
                VisualStateManager.GoToState(this, SwitchOnVisualState);
            else if (IsEnabled && !IsToggled)
                VisualStateManager.GoToState(this, SwitchOffVisualState);
        }

        public override void OnTouchDown(Point point)
        {
            base.OnTouchDown(point);

            UpdateIsToggled();
        }

        protected virtual void DrawSwitchBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialSwitchBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoSwitchBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentSwitchBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawSwitchThumb(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialSwitchThumb(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoSwitchThumb(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentSwitchThumb(canvas, dirtyRect);
                    break;
            }
        }

        void UpdateIsToggled()
        {
            IsToggled = !IsToggled;

            UpdateSwitchThumb();
        }

        void UpdateSwitchThumb()
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    AnimateMaterialSwitchThumb(IsToggled);
                    break;
                case VisualType.Cupertino:
                    AnimateCupertinoSwitchThumb(IsToggled);
                    break;
                case VisualType.Fluent:
                    AnimateFluentSwitchThumb(IsToggled);
                    break;
            }
        }
    }
}