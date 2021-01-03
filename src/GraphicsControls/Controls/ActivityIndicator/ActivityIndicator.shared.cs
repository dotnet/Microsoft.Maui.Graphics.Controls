using System.Graphics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class ActivityIndicator : GraphicsVisualView, IColor
    {
        public static readonly BindableProperty ColorProperty = ColorElement.ColorProperty;

        public static readonly BindableProperty IsRunningProperty =
            BindableProperty.Create(nameof(IsRunning), typeof(bool), typeof(ActivityIndicator), default(bool),
                propertyChanged: OnIsRunningChanged);

        static void OnIsRunningChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ActivityIndicator activityIndicator)
            {
                if ((bool)newValue)
                    activityIndicator.StartActivityIndicatorAnimation();
                else
                    activityIndicator.StopActivityIndicatorAnimation();
            }
        }

        public XColor Color
        {
            get { return (XColor)GetValue(ColorElement.ColorProperty); }
            set { SetValue(ColorElement.ColorProperty, value); }
        }

        public bool IsRunning
        {
            get { return (bool)GetValue(IsRunningProperty); }
            set { SetValue(IsRunningProperty, value); }
        }

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = 48;
                    WidthRequest = 48;
                    break;
                case VisualType.Cupertino:
                    HeightRequest = 24;
                    WidthRequest = 24;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 52;
                    WidthRequest = 60;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawActivityIndicatorBackground(canvas, dirtyRect);
            DrawActivityIndicator(canvas, dirtyRect);
            DrawActivityIndicatorText(canvas, dirtyRect);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsRunningProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == FlowDirectionProperty.PropertyName)
                InvalidateDraw();
        }

        protected virtual void DrawActivityIndicatorBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Fluent:
                    DrawFluentActivityIndicatorBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawActivityIndicator(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialActivityIndicator(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoActivityIndicator(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentActivityIndicator(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawActivityIndicatorText(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Fluent:
                    DrawFluentActivityIndicatorText(canvas, dirtyRect);
                    break;
            }
        }

        void StartActivityIndicatorAnimation()
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    AnimateMaterialActivityIndicatorAnimation();
                    break;
                case VisualType.Cupertino:
                    AnimateCupertinoActivityIndicatorAnimation();
                    break;
                case VisualType.Fluent:
                    AnimateFluentActivityIndicatorAnimation();
                    break;
            }
        }

        void StopActivityIndicatorAnimation()
        {
            ViewExtensions.CancelAnimations(this);
            InvalidateDraw();
        }
    }
}