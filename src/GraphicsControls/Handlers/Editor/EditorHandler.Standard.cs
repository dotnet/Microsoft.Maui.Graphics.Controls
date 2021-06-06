using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EditorHandler : MixedGraphicsControlHandler<IEditorDrawable, IEditor, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}