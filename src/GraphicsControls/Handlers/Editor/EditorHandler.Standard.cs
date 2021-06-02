using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EditorHandler : MixedGraphicsControlHandler<IEditor, IEditorDrawable, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}