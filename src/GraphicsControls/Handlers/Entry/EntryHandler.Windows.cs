using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using Windows.System;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
    {
        protected override GraphicsEntry CreateNativeView()
        {
            UI.Xaml.Thickness margin;

            // TODO: Set the correct margin in every Drawable.
            if (Drawable is MaterialEntryDrawable)
                margin = new UI.Xaml.Thickness();
            else if (Drawable is FluentEntryDrawable)
                margin = new UI.Xaml.Thickness();
            else if (Drawable is CupertinoEntryDrawable)
                margin = new UI.Xaml.Thickness();
            else
                margin = new UI.Xaml.Thickness();

            return new GraphicsEntry() { GraphicsControl = this, Margin = margin };
        }

        protected override void ConnectHandler(GraphicsEntry nativeView)
        {
            if (nativeView.TextBox != null)
            {
                nativeView.TextBox.GotFocus += OnFocusChanged;
                nativeView.TextBox.LostFocus += OnFocusChanged;
                nativeView.TextBox.KeyUp += OnNativeKeyUp;
                nativeView.TextBox.TextChanged += TextChanged;
                nativeView.TextBox.CursorPositionChanged += OnCursorPositionChanged;
                nativeView.TextBox.SelectionLengthChanged += OnSelectionLengthChanged;
            }

            base.ConnectHandler(nativeView);
        }

        protected override void DisconnectHandler(GraphicsEntry nativeView)
        {
            if (nativeView.TextBox != null)
            {
                nativeView.TextBox.GotFocus -= OnFocusChanged;
                nativeView.TextBox.LostFocus -= OnFocusChanged;
                nativeView.TextBox.KeyUp -= OnNativeKeyUp;
                nativeView.TextBox.TextChanged -= TextChanged;
                nativeView.TextBox.CursorPositionChanged -= OnCursorPositionChanged;
                nativeView.TextBox.SelectionLengthChanged -= OnSelectionLengthChanged;
            }

            base.DisconnectHandler(nativeView);
        }

        public static void MapText(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateText(entry);
            (handler as IMixedGraphicsHandler)?.Invalidate();
        }

        public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateCharacterSpacing(entry);
        }

        public static void MapClearButtonVisibility(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateClearButtonVisibility(entry);
        }

        [MissingMapper]
        public static void MapFont(EntryHandler handler, IEntry entry) { }

        public static void MapHorizontalTextAlignment(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateHorizontalTextAlignment(entry);
        }

        public static void MapIsPassword(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateIsPassword(entry);
        }

        public static void MapIsReadOnly(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateIsReadOnly(entry);
        }

        public static void MapIsTextPredictionEnabled(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateIsTextPredictionEnabled(entry);
        }

        public static void MapKeyboard(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateKeyboard(entry);
        }

        public static void MapMaxLength(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateMaxLength(entry);
        }

        public static void MapReturnType(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateReturnType(entry);
        }

        public static void MapTextColor(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.TextBox?.UpdateTextColor(entry);
        }

        [MissingMapper]
        public static void MapCursorPosition(EntryHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapSelectionLength(EntryHandler handler, IEntry entry) { }

        void OnFocusChanged(object sender, RoutedEventArgs e)
        {
            if (Handler != null)
            {
                Handler.Drawable.HasFocus = hasFocus;

                Handler.OnFocusedChange(hasFocus);
            }
        }

        void OnNativeKeyUp(object? sender, KeyRoutedEventArgs args)
        {
            if (args?.Key != VirtualKey.Enter)
                return;

            if (VirtualView?.ReturnType == ReturnType.Next)
            {
                NativeView?.TryMoveFocus(FocusNavigationDirection.Next);
            }
            else
            {
                // TODO: Hide the soft keyboard; this matches the behavior of .NET MAUI on Android/iOS
            }

            VirtualView?.Completed();
        }

        void TextChanged(object sender, TextChangedEventArgs args)
        {
            VirtualView?.UpdateText(NativeView.TextBox?.Text ?? String.Empty);
        }

        void OnCursorPositionChanged(object? sender, EventArgs e)
        {
            if (NativeView.TextBox == null)
                return;

            VirtualView.CursorPosition = NativeView.TextBox.CursorPosition;
        }

        void OnSelectionLengthChanged(object? sender, EventArgs e)
        {
            if (NativeView.TextBox == null)
                return;

            VirtualView.SelectionLength = NativeView.TextBox.ViewSelectionLength;
        }
    }
}