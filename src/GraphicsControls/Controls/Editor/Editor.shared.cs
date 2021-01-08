using System.Graphics;
using System.Runtime.CompilerServices;
using GraphicsControls.Extensions;
using Xamarin.Forms;
using XColor = Xamarin.Forms.Color;

namespace GraphicsControls
{
    public class BorderlessEditor : Xamarin.Forms.Editor { }

    public partial class Editor : GraphicsVisualView, IInput
    {
        readonly BorderlessEditor _editor;

        public Editor()
        {
            _editor = new BorderlessEditor
            {
                BackgroundColor = XColor.Transparent,
                VerticalOptions = LayoutOptions.Start
            };

            Content = _editor;
        }

        public static readonly BindableProperty TextProperty =
           BindableProperty.Create(nameof(IInput.Text), typeof(string), typeof(IInput), string.Empty,
               propertyChanged: OnTextChanged);

        static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Editor)?.UpdateText();
        }

        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(IInput.TextColor), typeof(XColor), typeof(IInput), XColor.Default,
                propertyChanged: OnTextColorChanged);

        static void OnTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Editor)?.UpdateTextColor();
        }

        public static readonly BindableProperty CharacterSpacingProperty =
            BindableProperty.Create(nameof(IInput.CharacterSpacing), typeof(double), typeof(IInput), 0.0d,
                propertyChanged: OnCharacterSpacingChanged);

        private static void OnCharacterSpacingChanged(BindableObject bindable, object oldValue, object newValue)
        {
            (bindable as Editor)?.UpdateCharacterSpacing();
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

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    HeightRequest = _editor.HeightRequest = 114.95d;
                    break;
                case VisualType.Cupertino:
                case VisualType.Fluent:
                    HeightRequest = _editor.HeightRequest = 60;
                    break;
            }

            _editor.TextChanged += OnEditorTextChanged;
            _editor.Focused += OnEditorFocused;
            _editor.Unfocused += OnEditorUnfocused;

            if (VisualType == VisualType.Material)
                AnimateMaterialPlaceholder(IsFocused);

            UpdateEditorPosition();
            UpdateText();
            UpdateTextColor();
            UpdateCharacterSpacing();
            UpdateFlowDirection();
        }

        public override void Unload()
        {
            base.Unload();

            _editor.TextChanged -= OnEditorTextChanged;
            _editor.Focused -= OnEditorFocused;
            _editor.Unfocused -= OnEditorUnfocused;
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawEditorBackground(canvas, dirtyRect);
            DrawEditorBorder(canvas, dirtyRect);
            DrawEntryPlaceholder(canvas, dirtyRect);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == VisualTypeProperty.PropertyName)
                UpdateEditorPosition();
            else if (propertyName == IsFocusedProperty.PropertyName)
                UpdateIsFocused();
            else if (propertyName == FlowDirectionProperty.PropertyName)
                UpdateFlowDirection();
        }

        public override void OnTouchDown(Point point)
        {
            base.OnTouchDown(point);

            FocusInternalEditorIfNeeded();
        }

        protected virtual void DrawEditorBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEditorBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEditorBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEditorBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawEditorBorder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEditorBorder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEditorBorder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEditorBorder(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawEntryPlaceholder(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialEditorPlaceholder(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoEditorPlaceholder(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentEditorPlaceholder(canvas, dirtyRect);
                    break;
            }
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

        void UpdateEditorPosition()
        { 
            switch (VisualType)
            {
                case VisualType.Cupertino:
                case VisualType.Fluent:
                    _editor.Margin = new Thickness(6, 2);
                    break;
                case VisualType.Material:
                default:
                    _editor.Margin = new Thickness(6, 16, 6, 6);
                    break;
            }
        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }

        void OnEditorFocused(object sender, FocusEventArgs e)
        {
            UpdateIsFocused(true);
        }

        void OnEditorUnfocused(object sender, FocusEventArgs e)
        {
            UpdateIsFocused(false);
        }

        void FocusInternalEditorIfNeeded()
        {
            if (!_editor.IsFocused)
                _editor.Focus();
        }

        void UpdateIsFocused(bool isFocused)
        {
            var isFocusedPropertyKey = this.GetInternalField<BindablePropertyKey>("IsFocusedPropertyKey");
            ((IElementController)this).SetValueFromRenderer(isFocusedPropertyKey, isFocused);
        }

        void UpdateText()
        {
            _editor.Text = Text;
        }

        void UpdateTextColor()
        {
            if (TextColor != XColor.Default)
                _editor.TextColor = TextColor;
            else
            {
                var textColor = Application.Current?.RequestedTheme == OSAppTheme.Light ? XColor.Black : XColor.White;
                _editor.TextColor = textColor;
            }
        }

        void UpdateCharacterSpacing()
        {
            _editor.CharacterSpacing = CharacterSpacing;
        }

        void UpdateFlowDirection()
        {
            _editor.FlowDirection = FlowDirection;
        }
    }
}