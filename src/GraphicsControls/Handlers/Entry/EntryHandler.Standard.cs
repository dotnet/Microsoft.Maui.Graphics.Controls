using System;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();

		public static void MapText(IViewHandler handler, IEntry entry) { }
	}
}