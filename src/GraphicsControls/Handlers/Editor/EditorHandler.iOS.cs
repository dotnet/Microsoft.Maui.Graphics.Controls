using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		protected override GraphicsEditor CreateNativeView()
        {
            return new GraphicsEditor { EdgeInsets = new UIEdgeInsets(0, 12, 0, 36) };
		}

		public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateText(editor);
			(handler as IMixedGraphicsHandler)?.Invalidate();
		}

		public static void MapTextColor(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateTextColor(editor);
		}

		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateCharacterSpacing(editor);
		}

		[MissingMapper]
		public static void MapFont(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapIsReadOnly(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapMaxLength(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapKeyboard(EditorHandler handler, IEditor editor) { }
	}
}