using Android.Content.Res;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		static ColorStateList? DefaultTextColors { get; set; }

		protected override GraphicsEditor CreateNativeView()
		{
			return new GraphicsEditor(Context!);
		}

        protected override void SetupDefaults(GraphicsEditor nativeView)
        {
			DefaultTextColors = nativeView.TextColors;

			base.SetupDefaults(nativeView);
        }

        public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateText(editor);
			(handler as IMixedGraphicsHandler)?.Invalidate();
		}

		public static void MapTextColor(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateTextColor(editor, DefaultTextColors);
		}

		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateCharacterSpacing(editor);
		}

		[MissingMapper]
		public static void MapFont(EditorHandler handler, IEditor editor) { }

		public static void MapIsReadOnly(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateIsReadOnly(editor);
		}

		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateIsTextPredictionEnabled(editor);
		}

		public static void MapMaxLength(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateMaxLength(editor);
		}

		public static void MapKeyboard(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateKeyboard(editor);
		}
	}
}