using Android.Content.Res;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Microsoft.Maui.Platform;
using static Android.Views.View;
using ATextAlignment = Android.Views.TextAlignment;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		static ColorStateList? DefaultTextColors { get; set; }

		EditorFocusChangeListener FocusChangeListener { get; } = new EditorFocusChangeListener();

		protected override GraphicsEditor CreatePlatformView()
		{
			var nativeView = new GraphicsEditor(Context!)
			{
				GraphicsControl = this,
				ImeOptions = ImeAction.Done
			};

			nativeView.SetSingleLine(false);
			nativeView.Gravity = GravityFlags.Top;
			nativeView.TextAlignment = ATextAlignment.ViewStart;
			nativeView.SetHorizontallyScrolling(false);

			if (Drawable is MaterialEditorDrawable)
				nativeView.SetPadding(12, 18, 0, 0);
			else if (Drawable is FluentEditorDrawable)
				nativeView.SetPadding(12, 12, 0, 0);
			else if (Drawable is CupertinoEditorDrawable)
				nativeView.SetPadding(12, 12, 0, 0);

			DefaultTextColors = nativeView.TextColors;

			return nativeView;
		}

		protected override void ConnectHandler(GraphicsEditor nativeView)
		{
			FocusChangeListener.Handler = this;

			nativeView.OnFocusChangeListener = FocusChangeListener;

			nativeView.TextChanged += OnTextChanged;
		}

		protected override void DisconnectHandler(GraphicsEditor nativeView)
		{
			nativeView.OnFocusChangeListener = null;

			FocusChangeListener.Handler = null; 
			
			nativeView.TextChanged -= OnTextChanged;
		}

        public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateText(editor);
			(handler as IMixedGraphicsHandler)?.Invalidate();
		}

		public static void MapTextColor(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateTextColor(editor, DefaultTextColors);
		}

		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateCharacterSpacing(editor);
		}

		public static void MapFont(EditorHandler handler, IEditor editor)
		{
			// TODO: Get require service FontManager
			//IFontManager? fontManager = null;
			//handler.PlatformView?.UpdateFont(editor, fontManager);
		}

		public static void MapIsReadOnly(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateIsReadOnly(editor);
		}

		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateIsTextPredictionEnabled(editor);
		}

		public static void MapMaxLength(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateMaxLength(editor);
		}

		public static void MapKeyboard(EditorHandler handler, IEditor editor)
		{
			handler.PlatformView?.UpdateKeyboard(editor);
		}

		void OnFocusedChange(bool hasFocus)
		{
			AnimatePlaceholder();

			if (!hasFocus)
				VirtualView?.Completed();
		}

		void OnTextChanged(object? sender, TextChangedEventArgs e)
		{
			if (VirtualView is ITextInput textInput)
				textInput.UpdateText(e);
		}

		class EditorFocusChangeListener : Java.Lang.Object, IOnFocusChangeListener
		{
			public EditorHandler? Handler { get; set; }

			public void OnFocusChange(View? v, bool hasFocus)
			{
				if (Handler != null)
				{
					Handler.Drawable.HasFocus = hasFocus;

					Handler.OnFocusedChange(hasFocus);
				}
			}
		}
	}
}