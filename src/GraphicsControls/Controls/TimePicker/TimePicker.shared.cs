using System;
using System.Graphics;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class TimePicker : GraphicsVisualView
    {
        public static readonly BindableProperty TimeProperty =
            BindableProperty.Create(nameof(Time), typeof(TimeSpan), typeof(TimePicker), new TimeSpan(0), BindingMode.TwoWay, (bindable, value) =>
            {
                var time = (TimeSpan)value;
                return time.TotalHours < 24 && time.TotalMilliseconds >= 0;
            });

        public TimeSpan Time
        {
            get { return (TimeSpan)GetValue(TimeProperty); }
            set { SetValue(TimeProperty, value); }
        }

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = 56;
                    break;
                case VisualType.Cupertino:
                    HeightRequest = 36;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 32;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawTimePickerBackground(canvas, dirtyRect);
            DrawTimePickerBorder(canvas, dirtyRect);
            DrawTimePickerPlaceholder(canvas, dirtyRect);
            DrawTimePickerDate(canvas, dirtyRect);
        }

        protected virtual void DrawTimePickerBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerBorder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                    DrawMaterialTimePickerPlaceholder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawTimePickerDate(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialTimePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoTimePickerDate(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentTimePickerDate(canvas, dirtyRect);
                    break;
            }
        }
    }
}