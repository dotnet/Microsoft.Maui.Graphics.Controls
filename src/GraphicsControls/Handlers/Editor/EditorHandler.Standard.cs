using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();

		public static void MapText(IViewHandler handler, IEditor editor) { }
		public static void MapTextColor(IViewHandler handler, IEditor editor) { }
		public static void MapCharacterSpacing(IViewHandler handler, IEditor editor) { }
		public static void MapFont(IViewHandler handler, IEditor editor) { }
		public static void MapIsReadOnly(IViewHandler handler, IEditor editor) { }
		public static void MapIsTextPredictionEnabled(IViewHandler handler, IEditor editor) { }
		public static void MapMaxLength(IViewHandler handler, IEditor editor) { }
		public static void MapKeyboard(IViewHandler handler, IEditor editor) { }
	}
}