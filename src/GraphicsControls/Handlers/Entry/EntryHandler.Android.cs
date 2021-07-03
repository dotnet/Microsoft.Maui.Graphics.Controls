using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using static Android.Widget.TextView;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
	{
		static ColorStateList? DefaultTextColors { get; set; }

		EditorActionListener ActionListener { get; } = new EditorActionListener();

		protected override GraphicsEntry CreateNativeView()
		{
			return new GraphicsEntry(Context!);
		}

        protected override void ConnectHandler(GraphicsEntry nativeView)
        {
			ActionListener.Handler = this;
			nativeView.SetOnEditorActionListener(ActionListener);

			base.ConnectHandler(nativeView);
        }

        protected override void DisconnectHandler(GraphicsEntry nativeView)
        {
			nativeView.SetOnEditorActionListener(null);
			ActionListener.Handler = null;

			base.DisconnectHandler(nativeView);
        }

        protected override void SetupDefaults(GraphicsEntry nativeView)
		{
			DefaultTextColors = nativeView.TextColors;

			base.SetupDefaults(nativeView);
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
	}
}