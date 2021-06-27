using System;
using Foundation;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
    {
        static UIColor? DefaultTextColor;

        protected override GraphicsEntry CreateNativeView()
        {
            UIEdgeInsets edgeInsets;

            if (Drawable is MaterialEntryDrawable)
                edgeInsets = new UIEdgeInsets(12, 12, 0, 36);
            else if (Drawable is FluentEntryDrawable)
                edgeInsets = new UIEdgeInsets(0, 8, 2, 36);
            else if (Drawable is CupertinoEntryDrawable)
                edgeInsets = new UIEdgeInsets(0, 8, 0, 8);
            else
                edgeInsets = new UIEdgeInsets();

            return new GraphicsEntry { EdgeInsets = edgeInsets };
        }

        protected override void ConnectHandler(GraphicsEntry nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.Started += OnStarted;
            nativeView.EditingChanged += OnEditingChanged;
            nativeView.EditingDidEnd += OnEditingEnded;
            nativeView.ShouldChangeCharacters += OnShouldChangeCharacters;
        }

        protected override void DisconnectHandler(GraphicsEntry nativeView)
        {
            base.DisconnectHandler(nativeView);

            nativeView.Started -= OnStarted;
            nativeView.EditingChanged -= OnEditingChanged;
            nativeView.EditingDidEnd -= OnEditingEnded;
            nativeView.ShouldChangeCharacters -= OnShouldChangeCharacters;
        }

        protected override void SetupDefaults(GraphicsEntry nativeView)
        {
            DefaultTextColor = nativeView.TextColor;

            base.SetupDefaults(nativeView);
        }

        public static void MapText(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateText(entry);
            (handler as IMixedGraphicsHandler)?.Invalidate();
        }

        public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateIsPassword(entry);
        }

        [MissingMapper]
        public static void MapFont(IViewHandler handler, IEntry entry) { }

        public static void MapHorizontalTextAlignment(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateHorizontalTextAlignment(entry);
        }

        public static void MapIsPassword(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateIsPassword(entry);
        }

        public static void MapIsReadOnly(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateIsReadOnly(entry);
        }

        public static void MapIsTextPredictionEnabled(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateIsTextPredictionEnabled(entry);
        }

        public static void MapKeyboard(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateKeyboard(entry);
        }

        public static void MapMaxLength(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateMaxLength(entry);
        }

        public static void MapReturnType(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateReturnType(entry);
        }

        public static void MapTextColor(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateTextColor(entry, DefaultTextColor);
        }

        public static void MapCursorPosition(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateCursorPosition(entry);
        }

        public static void MapSelectionLength(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateSelectionLength(entry);
        }

        void OnEditingChanged(object? sender, EventArgs e)
            => OnTextChanged();

        void OnStarted(object sender, EventArgs e)
        {
            Drawable.HasFocus = true;
            Invalidate();
        }

        void OnEditingEnded(object? sender, EventArgs e)
        {
            Drawable.HasFocus = false;
            Invalidate();

            OnTextChanged();
        }

        void OnTextChanged()
        {
            if (VirtualView == null || NativeView == null)
                return;

            // Even though <null> is technically different to "", it has no
            // functional difference to apps. Thus, hide it.
            var mauiText = VirtualView!.Text ?? string.Empty;
            var nativeText = NativeView.Text ?? string.Empty;

            if (mauiText != nativeText)
                VirtualView.Text = nativeText;
        }

        bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            var currLength = textField?.Text?.Length ?? 0;

            // Fix a crash on undo
            if (range.Length + range.Location > currLength)
                return false;

            if (VirtualView == null || NativeView == null)
                return false;

            if (VirtualView.MaxLength < 0)
                return true;

            var addLength = replacementString?.Length ?? 0;
            var remLength = range.Length;

            var newLength = currLength + addLength - remLength;

            return newLength <= VirtualView.MaxLength;
        }
    }
}