using System;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
    {
        protected override GraphicsEntry CreatePlatformView()
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

            var platformView = new GraphicsEntry {  GraphicsControl = this, EdgeInsets = edgeInsets };

            return platformView;
        
        }

        protected override void ConnectHandler(GraphicsEntry platformView)
        {
            base.ConnectHandler(platformView);

            platformView.Started += OnStarted;
            platformView.EditingChanged += OnEditingChanged;
            platformView.EditingDidEnd += OnEditingEnded;
            platformView.ShouldChangeCharacters += OnShouldChangeCharacters;
        }

        protected override void DisconnectHandler(GraphicsEntry platformView)
        {
            base.DisconnectHandler(platformView);

            platformView.Started -= OnStarted;
            platformView.EditingChanged -= OnEditingChanged;
            platformView.EditingDidEnd -= OnEditingEnded;
            platformView.ShouldChangeCharacters -= OnShouldChangeCharacters;
        }

        public override bool StartInteraction(PointF[] points)
        {
            if (points.Length > 0)
            {
                PointF touchPoint = points[0];

                if (Drawable.IndicatorRect.Contains(touchPoint) && PlatformView != null)
                    PlatformView.Text = string.Empty;
            }

            return base.StartInteraction(points);
        }

        public static void MapText(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateText(entry);
            (handler as IMixedGraphicsHandler)?.Invalidate();
        }

        public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateIsPassword(entry);
        }

        [MissingMapper]
        public static void MapFont(IViewHandler handler, IEntry entry) { }

        public static void MapHorizontalTextAlignment(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateHorizontalTextAlignment(entry);
        }

        public static void MapIsPassword(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateIsPassword(entry);
        }

        public static void MapIsReadOnly(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateIsReadOnly(entry);
        }

        public static void MapIsTextPredictionEnabled(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateIsTextPredictionEnabled(entry);
        }

        public static void MapKeyboard(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateKeyboard(entry);
        }

        public static void MapMaxLength(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateMaxLength(entry);
        }

        public static void MapReturnType(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateReturnType(entry);
        }

        public static void MapTextColor(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateTextColor(entry);
        }

        public static void MapCursorPosition(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateCursorPosition(entry);
        }

        public static void MapSelectionLength(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateSelectionLength(entry);
        }

        void OnEditingChanged(object? sender, EventArgs e)
            => OnTextChanged();

        void OnStarted(object? sender, EventArgs e)
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
            if (VirtualView == null || PlatformView == null)
                return;

            // Even though <null> is technically different to "", it has no
            // functional difference to apps. Thus, hide it.
            var mauiText = VirtualView!.Text ?? string.Empty;
            var nativeText = PlatformView.Text ?? string.Empty;

            if (mauiText != nativeText)
                VirtualView.Text = nativeText;
        }

        bool OnShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            var currLength = textField?.Text?.Length ?? 0;

            // Fix a crash on undo
            if (range.Length + range.Location > currLength)
                return false;

            if (VirtualView == null || PlatformView == null)
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