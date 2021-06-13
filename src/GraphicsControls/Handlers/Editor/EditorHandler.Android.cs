namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, GraphicsEditor>
	{
		protected override GraphicsEditor CreateNativeView()
		{
			return new GraphicsEditor(Context!);
		}

		public static void MapText(EditorHandler handler, IEditor editor)
		{
			handler.NativeView?.UpdateText(editor);
			(handler as IMixedGraphicsHandler)?.Invalidate();
		}
	}
}