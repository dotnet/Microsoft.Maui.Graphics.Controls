using System;
using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
    {
        protected override GraphicsEntry CreateNativeView()
        {
            return new GraphicsEntry { EdgeInsets = new UIEdgeInsets(0, 12, 0, 36) };
        }

        protected override void ConnectHandler(GraphicsEntry nativeView)
        {
            base.ConnectHandler(nativeView);

            nativeView.EditingChanged += OnEditingChanged;
            nativeView.EditingDidBegin += OnEditingDidBegin;
            nativeView.EditingDidEnd += OnEditingEnded;
        }

        protected override void DisconnectHandler(GraphicsEntry nativeView)
        {
            base.DisconnectHandler(nativeView);

            nativeView.EditingChanged -= OnEditingChanged;
            nativeView.EditingDidBegin -= OnEditingDidBegin;
            nativeView.EditingDidEnd -= OnEditingEnded;
        }

        public override bool StartInteraction(PointF[] points)
        {
            return base.StartInteraction(points);
        }

        public static void MapText(EntryHandler handler, IEntry entry)
        {
            handler.NativeView?.UpdateText(entry);
            (handler as IMixedGraphicsHandler)?.Invalidate();
        }

        [MissingMapper]
        public static void MapCharacterSpacing(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapClearButtonVisibility(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapFont(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapHorizontalTextAlignment(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapIsPassword(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapIsReadOnly(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapIsTextPredictionEnabled(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapKeyboard(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapMaxLength(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapReturnType(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapTextColor(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapCursorPosition(IViewHandler handler, IEntry entry) { }

        [MissingMapper]
        public static void MapSelectionLength(IViewHandler handler, IEntry entry) { }

        void OnEditingChanged(object? sender, EventArgs e) => OnTextChanged();

        void OnEditingDidBegin(object? sender, EventArgs e)
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
    }
}