namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
	{
		protected override GraphicsEntry CreateNativeView()
		{
			return new GraphicsEntry();
		}

		public static void MapText(EntryHandler handler, IEntry entry) { }
		public static void MapCharacterSpacing(EntryHandler handler, IEntry entry) { }
		public static void MapClearButtonVisibility(EntryHandler handler, IEntry entry) { }
		public static void MapFont(EntryHandler handler, IEntry entry) { }
		public static void MapHorizontalTextAlignment(EntryHandler handler, IEntry entry) { }
		public static void MapIsPassword(EntryHandler handler, IEntry entry) { }
		public static void MapIsReadOnly(EntryHandler handler, IEntry entry) { }
		public static void MapIsTextPredictionEnabled(EntryHandler handler, IEntry entry) { }
		public static void MapKeyboard(EntryHandler handler, IEntry entry) { }
		public static void MapMaxLength(EntryHandler handler, IEntry entry) { }
		public static void MapReturnType(EntryHandler handler, IEntry entry) { }
		public static void MapTextColor(EntryHandler handler, IEntry entry) { }
		public static void MapCursorPosition(EntryHandler handler, IEntry entry) { }
		public static void MapSelectionLength(EntryHandler handler, IEntry entry) { }
	}
}