namespace Microsoft.Maui.Graphics.Controls
{
    public partial class EntryHandler : MixedGraphicsControlHandler<IEntryDrawable, IEntry, GraphicsEntry>
	{
		protected override GraphicsEntry CreateNativeView()
		{
			return new GraphicsEntry(Context!);
		}

		[MissingMapper]
		public static void MapText(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapCharacterSpacing(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapClearButtonVisibility(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapFont(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapHorizontalTextAlignment(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapIsPassword(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapIsReadOnly(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapIsTextPredictionEnabled(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapKeyboard(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapMaxLength(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapReturnType(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapTextColor(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapCursorPosition(IViewHandler handler, IEntry entry) { }

		[MissingMapper]
		public static void MapSelectionLength(IViewHandler handler, IEntry entry) { }
	}
}