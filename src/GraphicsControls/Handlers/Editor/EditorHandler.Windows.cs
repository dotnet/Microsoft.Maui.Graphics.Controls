namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
    {
		protected override GraphicsEditor CreateNativeView()
		{
			return new GraphicsEditor { GraphicsControl = this };
		}

		[MissingMapper]
		public static void MapText(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapTextColor(EditorHandler handler, IEditor editor) { }

		[MissingMapper]
		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor) { }

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