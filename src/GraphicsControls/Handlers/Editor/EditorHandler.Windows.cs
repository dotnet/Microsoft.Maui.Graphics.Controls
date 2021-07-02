namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
    {
		protected override GraphicsEditor CreateNativeView()
		{
			return new GraphicsEditor();
		}

		public static void MapText(EditorHandler handler, IEditor editor) { }
		public static void MapTextColor(EditorHandler handler, IEditor editor) { }
		public static void MapCharacterSpacing(EditorHandler handler, IEditor editor) { }
		public static void MapFont(EditorHandler handler, IEditor editor) { }
		public static void MapIsReadOnly(EditorHandler handler, IEditor editor) { }
		public static void MapIsTextPredictionEnabled(EditorHandler handler, IEditor editor) { }
		public static void MapMaxLength(EditorHandler handler, IEditor editor) { }
		public static void MapKeyboard(EditorHandler handler, IEditor editor) { }
	}
}