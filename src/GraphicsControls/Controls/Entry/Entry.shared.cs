using System.Collections.Generic;
using System.ComponentModel;
using System.Graphics;
using System.Runtime.CompilerServices;
using GraphicsControls.Extensions;
using Xamarin.Forms;
using Point = System.Graphics.Point;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class BorderlessEntry : Xamarin.Forms.Entry { }

    public partial class Entry : GraphicsVisualView, IInput
    {
        public static class Layers
        {
            public const string Background = "Entry.Layers.Background";
            public const string Border = "Entry.Layers.Border";
            public const string Placeholder = "Entry.Layers.Placeholder";
            public const string Indicator = "Entry.Layers.Indicator";
        }

        RectangleF _indicatorRect;
        readonly BorderlessEntry _entry;

        public Entry()
        {
            _indicatorRect = RectangleF.Zero;

            _entry = new BorderlessEntry
            {
                BackgroundColor = XColor.Transparent,
                ClearButtonVisibility = ClearButtonVisibility.Never,
                Text = Text,
                TextColor = TextColor,
                VerticalOptions = LayoutOptions.Center
            };

            Content = _entry;
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(IInput.Text), typeof(string), typeof(IInput), string.Empty,
                propertyChanged: OnTextChanged);

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Entry)?.UpdateText();
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(IInput.TextColor), typeof(XColor), typeof(IInput), XColor.Default,
                propertyChanged: OnTextColorChanged);

        static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Entry)?.UpdateTextColor();
        }

        public static readonly BindableProperty CharacterSpacingProperty =
            BindableProperty.Create(nameof(IInput.CharacterSpacing), typeof(double), typeof(IInput), 0.0d,
                propertyChanged: OnCharacterSpacingChanged);

        private static void OnCharacterSpacingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Entry)?.UpdateCharacterSpacing();
        }

        public static readonly BindableProperty PlaceholderProperty = InputElement.PlaceholderProperty;

        public static readonly BindableProperty PlaceholderColorProperty = InputElement.PlaceholderColorProperty;

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

        public double CharacterSpacing
        {
            get { return (double)GetValue(CharacterSpacingProperty); }
            set { SetValue(CharacterSpacingProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        public XColor PlaceholderColor
        {
            get { return (XColor)GetValue(PlaceholderColorProperty); }
            set { SetValue(PlaceholderColorProperty, value); }
        }

        public List<string> EntryLayers = new List<string>
        {
            Layers.Background,
            Layers.Border,
            Layers.Placeholder,
            Layers.Indicator
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
                    HeightRequest = 56;
                    break;
                case VisualType.Fluent:
                    HeightRequest = FluentEntryHeight;
                    break;
            }

            _entry.TextChanged += OnEntryTextChanged;
            _entry.Focused += OnEntryFocused;
            _entry.Unfocused += OnEntryUnfocused;

            if (VisualType == VisualType.Material)
                AnimateMaterialPlaceholder(IsFocused);

            UpdateEntryPosition();
            UpdateText();
            UpdateTextColor();
            UpdateCharacterSpacing();
            UpdateFlowDirection();
        }

        public override void Unload()
        {
            base.Unload();

            _entry.TextChanged -= OnEntryTextChanged;
            _entry.Focused -= OnEntryFocused;
            _entry.Unfocused -= OnEntryUnfocused;
        }

        public override List<string> GraphicsLayers =>
            EntryLayers;

        public override void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {
            switch (layer)
            {
                case Layers.Background:
                    DrawEntryBackground(canvas, dirtyRect);
                    break;
                case Layers.Border:
                    DrawEntryBorder(canvas, dirtyRect);
                    break;
                case Layers.Placeholder:
                    DrawEntryPlaceholder(canvas, dirtyRect);
                    break;
                case Layers.Indicator:
                    DrawEntryIndicators(canvas, dirtyRect);
                    break;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == VisualTypeProperty.PropertyName)
                UpdateEntryPosition();
            else if (propertyName == IsFocusedProperty.PropertyName)
                UpdateIsFocused();
            else if (propertyName == FlowDirectionProperty.PropertyName)
                UpdateFlowDirection();
        }

        public override void OnTouchDown(Point point)
        {
            base.OnTouchDown(point);

            FocusInternalEntryIfNeeded();
            ClearTextIfNeeded(point);
        }

        protected virtual void DrawEntryBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEntryBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEntryBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEntryBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawEntryBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEntryBorder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEntryBorder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEntryBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawEntryPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEntryPlaceholder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEntryPlaceholder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEntryPlaceholder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawEntryIndicators(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEntryIndicators(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    break;
                case VisualType.Fluent:
                    DrawFluentEntryIndicators(canvas, dirtyRect);
                    break;
            }
        }

        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        void OnEntryFocused(object sender, FocusEventArgs e)
        {
            UpdateIsFocused(true);
        }

        void OnEntryUnfocused(object sender, FocusEventArgs e)
        {
            UpdateIsFocused(false);
        }

        void UpdateEntryPosition()
        {
            bool isRtl = FlowDirection == FlowDirection.RightToLeft;

            switch (VisualType)
            {
                case VisualType.Cupertino:
                case VisualType.Fluent:
                    _entry.Margin = isRtl ? new Thickness(40, 0, 8, 0) : new Thickness(8, 0, 40, 0);
                    break;
                case VisualType.Material:
                default:
                    if (Device.RuntimePlatform == Device.macOS)
                        _entry.Margin = isRtl ? new Thickness(40, 0, 10, 12) : new Thickness(10, 0, 40, 12);
                    else
                        _entry.Margin = isRtl ? new Thickness(40, 12, 12, 0) : new Thickness(12, 12, 40, 0);
                    break;
            }
        }

        void FocusInternalEntryIfNeeded()
        {
            if (!_entry.IsFocused)
                _entry.Focus();
        }

        void UpdateIsFocused(bool isFocused)
        {
            var isFocusedPropertyKey = this.GetInternalField<BindablePropertyKey>("IsFocusedPropertyKey");
            ((IElementController)this).SetValueFromRenderer(isFocusedPropertyKey, isFocused);
        }

        void ClearTextIfNeeded(Point point)
        {
            PointF touchPoint = new PointF((float)point.X, (float)point.Y);

            if (_indicatorRect.Contains(touchPoint))
                _entry.Text = string.Empty;
        }

        void UpdateIsFocused()
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    AnimateMaterialPlaceholder(IsFocused);
                    break;
            }
        }

        void UpdateText()
        {
            _entry.Text = Text;
        }

        void UpdateTextColor()
        {
            if (TextColor != XColor.Default)
                _entry.TextColor = TextColor;
            else
            {
                var textColor = Application.Current?.RequestedTheme == OSAppTheme.Light ? XColor.Black : XColor.White;
                _entry.TextColor = textColor;
            }
        }

        void UpdateCharacterSpacing()
        {
            _entry.CharacterSpacing = CharacterSpacing;
        }

        void UpdateFlowDirection()
        {
            _entry.FlowDirection = FlowDirection;
        }
    }
}