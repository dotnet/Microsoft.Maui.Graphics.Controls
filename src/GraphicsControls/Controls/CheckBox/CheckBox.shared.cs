using System;
using System.Collections.Generic;
using System.Graphics;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Point = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public partial class CheckBox : GraphicsVisualView, IColor
    {
        public static class Layers
        {
            public const string Background = "CheckBox.Layers.Background";
            public const string Mark = "CheckBox.Layers.Mark";
            public const string Text = "CheckBox.Layers.Text";
        }

        public const string IsCheckedVisualState = "IsChecked";

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(CheckBox), false,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CheckBox)bindable).CheckedChanged?.Invoke(bindable, new CheckedChangedEventArgs((bool)newValue));
                    ((CheckBox)bindable).ChangeVisualState();
                }, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(CheckBox), string.Empty);

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(XColor), typeof(CheckBox), XColor.Default);

        public static readonly BindableProperty ColorProperty = ColorElement.ColorProperty;

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public XColor TextColor
        {
            get { return (XColor)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public XColor Color
        {
            get { return (XColor)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public List<string> CheckBoxLayers = new List<string>
        {
            Layers.Background,
            Layers.Mark,
            Layers.Text
        };

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        public override List<string> GraphicsLayers =>
            CheckBoxLayers;

        public override void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {
            switch (layer)
            {
                case Layers.Background:
                    DrawCheckBoxBackground(canvas, dirtyRect);
                    break;
                case Layers.Mark:
                    DrawCheckBoxMark(canvas, dirtyRect);
                    break;
                case Layers.Text:
                    DrawCheckBoxText(canvas, dirtyRect);
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsCheckedProperty.PropertyName ||
                propertyName == BackgroundColorProperty.PropertyName ||
                propertyName == FlowDirectionProperty.PropertyName)
                InvalidateDraw();
        }

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Cupertino:
                    HeightRequest = 24;
                    WidthRequest = 65;
                    break;
                case VisualType.Material:
                default:
                    HeightRequest = 18;
                    WidthRequest = 65;
                    break;
                case VisualType.Fluent:
                    HeightRequest = 20;
                    WidthRequest = 65;
                    break;
            }
        }

        protected override void ChangeVisualState()
        {
            if (IsEnabled && IsChecked)
                VisualStateManager.GoToState(this, IsCheckedVisualState);
            else
                base.ChangeVisualState();
        }

        public override void OnTouchDown(Point point)
        {
            UpdateIsChecked();
        }

        protected virtual void DrawCheckBoxBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialCheckBoxBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoCheckBoxBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentCheckBoxBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawCheckBoxMark(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialCheckBoxMark(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoCheckBoxMark(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentCheckBoxMark(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawCheckBoxText(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialCheckBoxText(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoCheckBoxText(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentCheckBoxText(canvas, dirtyRect);
                    break;
            }
        }

        void UpdateIsChecked()
        {
            IsChecked = !IsChecked;
        }
    }
}