using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using static Android.Views.View;
using static Android.Widget.TextView;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
	{
		static ColorStateList? DefaultTextColors { get; set; }

		EditorActionListener ActionListener { get; } = new EditorActionListener();
		EntryFocusChangeListener FocusChangeListener { get; } = new EntryFocusChangeListener();

		protected override GraphicsEntry CreateNativeView()
		{
			var nativeView = new GraphicsEntry(Context!)
			{ 
				GraphicsControl = this 
			};
			
			if (Drawable is MaterialEntryDrawable)
				nativeView.SetPadding(36, 60, 0, 0);
			else if (Drawable is FluentEntryDrawable)
				nativeView.SetPadding(24, 12, 0, 0);
			else if (Drawable is CupertinoEntryDrawable)
				nativeView.SetPadding(24, 12, 0, 0);

			DefaultTextColors = nativeView.TextColors;

			return nativeView;
		}

        protected override void ConnectHandler(GraphicsEntry nativeView)
        {
			ActionListener.Handler = this;
			FocusChangeListener.Handler = this;

			nativeView.SetOnEditorActionListener(ActionListener);
			nativeView.OnFocusChangeListener = FocusChangeListener;

			base.ConnectHandler(nativeView);
        }

        protected override void DisconnectHandler(GraphicsEntry nativeView)
        {
			nativeView.SetOnEditorActionListener(null);
			nativeView.OnFocusChangeListener = null;

			ActionListener.Handler = null;
			FocusChangeListener.Handler = null;

			base.DisconnectHandler(nativeView);
        }

		public override bool StartInteraction(PointF[] points)
		{
			if (points.Length > 0)
			{
				PointF touchPoint = points[0];

				if (Drawable.IndicatorRect.Contains(touchPoint) && NativeView != null)
					NativeView.Text = string.Empty;
			}

			return base.StartInteraction(points);
		}

		public static void MapText(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateText(entry);
		}

		public static void MapCharacterSpacing(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateCharacterSpacing(entry);
		}

		public static void MapFont(EntryHandler handler, IEntry entry) 
		{
			// TODO: Get require service FontManager
			//IFontManager? fontManager = null;
			//handler.NativeView?.UpdateFont(editor, fontManager);
		}

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
			handler.NativeView?.UpdateTextColor(entry, DefaultTextColors);
		}

		public static void MapCursorPosition(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateCursorPosition(entry);
		}

		public static void MapSelectionLength(EntryHandler handler, IEntry entry)
		{
			handler.NativeView?.UpdateSelectionLength(entry);
		}

		void OnFocusedChange(bool hasFocus)
		{
			Drawable.HasFocus = hasFocus;
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