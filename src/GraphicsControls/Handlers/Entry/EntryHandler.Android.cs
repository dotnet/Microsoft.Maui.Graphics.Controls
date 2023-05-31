using Android.Content.Res;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Microsoft.Maui.Platform;
using static Android.Views.View;
using static Android.Widget.TextView;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
    {
        TextWatcher Watcher { get; } = new TextWatcher();
        EditorActionListener ActionListener { get; } = new EditorActionListener();
        EntryFocusChangeListener FocusChangeListener { get; } = new EntryFocusChangeListener();

        protected override GraphicsEntry CreatePlatformView()
        {
            var platformView = new GraphicsEntry(Context!)
            {
                GraphicsControl = this
            };

            if (Drawable is MaterialEntryDrawable)
                platformView.SetPadding(12, 18, 0, 0);
            else if (Drawable is FluentEntryDrawable)
                platformView.SetPadding(12, 12, 0, 0);
            else if (Drawable is CupertinoEntryDrawable)
                platformView.SetPadding(12, 12, 0, 0);

            return platformView;
        }

        protected override void ConnectHandler(GraphicsEntry platformView)
        {
            Watcher.Handler = this;
            ActionListener.Handler = this;
            FocusChangeListener.Handler = this;

            platformView.AddTextChangedListener(Watcher);
            platformView.SetOnEditorActionListener(ActionListener);
            platformView.OnFocusChangeListener = FocusChangeListener;

            base.ConnectHandler(platformView);
        }

        protected override void DisconnectHandler(GraphicsEntry platformView)
        {
            platformView.RemoveTextChangedListener(Watcher);
            platformView.SetOnEditorActionListener(null);
            platformView.OnFocusChangeListener = null;

            Watcher.Handler = null;
            ActionListener.Handler = null;
            FocusChangeListener.Handler = null;

            base.DisconnectHandler(platformView);
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
        }

        public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
        {
            handler.PlatformView?.UpdateCharacterSpacing(entry);
        }

        public static void MapFont(EntryHandler handler, IEntry entry)
        {
            // TODO: Get require service FontManager
            //IFontManager? fontManager = null;
            //handler.PlatformView?.UpdateFont(editor, fontManager);
        }

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

        void OnTextChanged(string? text)
        {
            if (VirtualView == null || PlatformView == null)
                return;

            // Even though <null> is technically different to "", it has no
            // functional difference to apps. Thus, hide it.
            var mauiText = VirtualView.Text ?? string.Empty;
            var nativeText = text ?? string.Empty;

            if (mauiText != nativeText)
                VirtualView.Text = nativeText;
        }

        void OnFocusedChange(bool hasFocus)
        {
            Drawable.HasFocus = hasFocus;

            AnimatePlaceholder();
        }

        class TextWatcher : Java.Lang.Object, ITextWatcher
        {
            public EntryHandler? Handler { get; set; }

            void ITextWatcher.AfterTextChanged(IEditable? s)
            {
            }

            void ITextWatcher.BeforeTextChanged(Java.Lang.ICharSequence? s, int start, int count, int after)
            {
            }

            void ITextWatcher.OnTextChanged(Java.Lang.ICharSequence? s, int start, int before, int count)
            {
                // We are replacing 0 characters with 0 characters, so skip
                if (before == 0 && count == 0)
                    return;

                Handler?.OnTextChanged(s?.ToString());
            }
        }

        class EditorActionListener : Java.Lang.Object, IOnEditorActionListener
        {
            public EntryHandler? Handler { get; set; }

            public bool OnEditorAction(TextView? v, [GeneratedEnum] ImeAction actionId, KeyEvent? e)
            {
                if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && e?.KeyCode == Keycode.Enter && e?.Action == KeyEventActions.Up))
                {
                    // TODO: Dismiss keyboard for hardware / physical keyboards

                    if (Handler != null)
                    {
                        Handler.Drawable.HasFocus = false;
                        Handler.VirtualView?.Completed();
                    }
                }

                return true;
            }
        }

        class EntryFocusChangeListener : Java.Lang.Object, IOnFocusChangeListener
        {
            public EntryHandler? Handler { get; set; }

            public void OnFocusChange(View? v, bool hasFocus)
            {
                Handler?.OnFocusedChange(hasFocus);
            }
        }
    }
}