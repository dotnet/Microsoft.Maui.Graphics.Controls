using System;
using System.Graphics;
using Xamarin.Forms;

namespace GraphicsControls
{
    public partial class RadioButton : GraphicsVisualView
    {
        internal const string GroupNameChangedMessage = "RadioButtonGroupNameChanged";
        internal const string ValueChangedMessage = "RadioButtonValueChanged";

        public static readonly BindableProperty IsCheckedProperty =
            BindableProperty.Create(nameof(IsChecked), typeof(bool), typeof(RadioButton), false,
                propertyChanged: (b, o, n) => ((RadioButton)b).OnIsCheckedPropertyChanged((bool)n),
                defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty GroupNameProperty =
            BindableProperty.Create(    nameof(GroupName), typeof(string), typeof(RadioButton), null,
                propertyChanged: (b, o, n) => ((RadioButton)b).OnGroupNamePropertyChanged((string)o, (string)n));

        public static readonly BindableProperty ValueProperty =
            BindableProperty.Create(nameof(Value), typeof(object), typeof(RadioButton), null,
                propertyChanged: (b, o, n) => ((RadioButton)b).OnValuePropertyChanged());

        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        public override void Load()
        {
            base.Load();

            switch (VisualType)
            {
                case VisualType.Cupertino:
                    HeightRequest = WidthRequest = 24;
                    break;
                case VisualType.Fluent:
                case VisualType.Material:
                default:
                    HeightRequest = WidthRequest = 22;
                    break;
            }
        }

        public override void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            base.Draw(canvas, dirtyRect);

            DrawRadioButtonBackground(canvas, dirtyRect);
            DrawRadioButtonMark(canvas, dirtyRect);
        }

        protected virtual void DrawRadioButtonBackground(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialRadioButtonBackground(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoRadioButtonBackground(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentRadioButtonBackground(canvas, dirtyRect);
                    break;
            }
        }

        protected virtual void DrawRadioButtonMark(ICanvas canvas, RectangleF dirtyRect)
        {
            switch (VisualType)
            {
                case VisualType.Material:
                default:
                    DrawMaterialRadioButtonMark(canvas, dirtyRect);
                    break;
                case VisualType.Cupertino:
                    DrawCupertinoRadioButtonMark(canvas, dirtyRect);
                    break;
                case VisualType.Fluent:
                    DrawFluentRadioButtonMark(canvas, dirtyRect);
                    break;
            }
        }

        void OnIsCheckedPropertyChanged(bool isChecked)
        {
            if (isChecked)
                RadioButtonGroup.UpdateRadioButtonGroup(this);

            ChangeVisualState();
            CheckedChanged?.Invoke(this, new CheckedChangedEventArgs(isChecked));
        }

        void OnGroupNamePropertyChanged(string oldGroupName, string newGroupName)
        {
            if (!string.IsNullOrEmpty(newGroupName))
            {
                if (string.IsNullOrEmpty(oldGroupName))
                {
                    MessagingCenter.Subscribe<RadioButton, RadioButtonGroupSelectionChanged>(this,
                        RadioButtonGroup.GroupSelectionChangedMessage, HandleRadioButtonGroupSelectionChanged);
                    MessagingCenter.Subscribe<Layout<View>, RadioButtonGroupValueChanged>(this,
                        RadioButtonGroup.GroupValueChangedMessage, HandleRadioButtonGroupValueChanged);
                }

                MessagingCenter.Send(this, GroupNameChangedMessage,
                    new RadioButtonGroupNameChanged(RadioButtonGroup.GetVisualRoot(this), oldGroupName));
            }
            else
            {
                if (!string.IsNullOrEmpty(oldGroupName))
                {
                    MessagingCenter.Unsubscribe<RadioButton, RadioButtonGroupSelectionChanged>(this, RadioButtonGroup.GroupSelectionChangedMessage);
                    MessagingCenter.Unsubscribe<Layout<View>, RadioButtonGroupValueChanged>(this, RadioButtonGroup.GroupValueChangedMessage);
                }
            }
        }

        void OnValuePropertyChanged()
        {
            if (!IsChecked || string.IsNullOrEmpty(GroupName))
            {
                return;
            }

            MessagingCenter.Send(this, ValueChangedMessage,
                new RadioButtonValueChanged(RadioButtonGroup.GetVisualRoot(this)));
        }

        bool MatchesScope(RadioButtonScopeMessage message)
        {
            return RadioButtonGroup.GetVisualRoot(this) == message.Scope;
        }

        void HandleRadioButtonGroupSelectionChanged(RadioButton selected, RadioButtonGroupSelectionChanged args)
        {
            if (!IsChecked || selected == this || string.IsNullOrEmpty(GroupName) || GroupName != selected.GroupName || !MatchesScope(args))
            {
                return;
            }

            IsChecked = false;
        }

        void HandleRadioButtonGroupValueChanged(Layout<View> layout, RadioButtonGroupValueChanged args)
        {
            if (IsChecked || string.IsNullOrEmpty(GroupName) || GroupName != args.GroupName || Value != args.Value || !MatchesScope(args))
            {
                return;
            }

            IsChecked = true;
        }
    }
}