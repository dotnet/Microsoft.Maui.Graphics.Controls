using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
    {
        protected override GraphicsEditor CreateNativeView()
        {
            UI.Xaml.Thickness margin;

            // TODO: Set the correct margin in every Drawable.
            if (Drawable is MaterialEditorDrawable)
                margin = new UI.Xaml.Thickness();
            else if (Drawable is FluentEditorDrawable)
                margin = new UI.Xaml.Thickness();
            else if (Drawable is CupertinoEditorDrawable)
                margin = new UI.Xaml.Thickness();
            else
                margin = new UI.Xaml.Thickness();

            return new GraphicsEditor { GraphicsControl = this, Margin = margin };
        }

        protected override void ConnectHandler(GraphicsEditor nativeView)
        {
            if (nativeView.TextBox != null)
            {
                nativeView.TextBox.TextChanged += OnFocusChanged;
                nativeView.TextBox.GotFocus += OnFocusChanged;
                nativeView.TextBox.LostFocus += OnFocusChanged;
            }

            base.ConnectHandler(nativeView);
        }

        protected override void DisconnectHandler(GraphicsEditor nativeView)
        {
            if (nativeView.TextBox != null)
            {
                nativeView.TextBox.TextChanged -= OnTextChanged;
                nativeView.TextBox.GotFocus -= OnFocusChanged;
                nativeView.TextBox.LostFocus -= OnFocusChanged;
            }

            base.DisconnectHandler(nativeView);
        }

        public static void MapText(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateText(editor);
            (handler as IMixedGraphicsHandler)?.Invalidate();
        }

        public static void MapTextColor(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateTextColor(editor);
        }

        public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateCharacterSpacing(editor);
        }

        [MissingMapper]
        public static void MapFont(EditorHandler handler, IEditor editor) { }

        public static void MapIsReadOnly(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateIsReadOnly(editor);
        }

        public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateIsTextPredictionEnabled(editor);
        }

        public static void MapMaxLength(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateMaxLength(editor);
        }

        public static void MapKeyboard(EditorHandler handler, IEditor editor)
        {
            handler.NativeView?.TextBox?.UpdateKeyboard(editor);
        }

        void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            VirtualView?.UpdateText(NativeView.TextBox?.Text ?? string.Empty);
        }

        void OnFocusChanged(object sender, RoutedEventArgs e)
        {
            var mauiTextBox = sender as MauiTextBox;

            if (mauiTextBox == null)
                return;

            Drawable.HasFocus = mauiTextBox.FocusState != FocusState.Unfocused; 
            Invalidate();
        }
    }
}