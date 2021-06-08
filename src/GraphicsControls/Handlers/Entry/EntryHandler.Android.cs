namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
	{
		protected override GraphicsEntry CreateNativeView()
		{
			return new GraphicsEntry(Context!);
		}

		public static void MapText(IViewHandler handler, IEntry entry) { }
	}
}