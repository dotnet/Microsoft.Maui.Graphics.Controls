#nullable enable
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
	public partial class EntryHandler
	{
		public EntryHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static PropertyMapper<IEntry, EntryHandler> PropertyMapper = new PropertyMapper<IEntry, EntryHandler>(ViewMapper)
		{
			[nameof(IEntry.Background)] = ViewHandler.MapInvalidate,
			[nameof(IEntry.Text)] = MapText,
			[nameof(IEntry.CharacterSpacing)] = MapCharacterSpacing,
			[nameof(IEntry.Font)] = MapFont,
			[nameof(IEntry.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
			[nameof(IEntry.IsPassword)] = MapIsPassword,
			[nameof(IEntry.IsReadOnly)] = MapIsReadOnly,
			[nameof(IEntry.IsTextPredictionEnabled)] = MapIsTextPredictionEnabled,
			[nameof(IEntry.Keyboard)] = MapKeyboard,
			[nameof(IEntry.MaxLength)] = MapMaxLength,
			[nameof(IEntry.ReturnType)] = MapReturnType,
			[nameof(IEntry.TextColor)] = MapTextColor,
			[nameof(IEntry.CursorPosition)] = MapCursorPosition,
			[nameof(IEntry.SelectionLength)] = MapSelectionLength
		};

		public static DrawMapper<IEntryDrawable, IEntry> DrawMapper = new DrawMapper<IEntryDrawable, IEntry>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Placeholder"] = MapDrawPlaceholder,
			["Indicator"] = MapDrawIndicator
		};

		public static string[] DefaultEntryLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Border",
				"Placeholder",
				"Indicator",
			}, "Background").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultEntryLayerDrawingOrder;

		protected override IEntryDrawable CreateDrawable() =>
			new MaterialEntryDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntryDrawable drawable, IEntry view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntryDrawable drawable, IEntry view)
			=> drawable.DrawBorder(canvas, dirtyRect, view);

		public static void MapDrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntryDrawable drawable, IEntry view)
			=> drawable.DrawPlaceholder(canvas, dirtyRect, view);

		public static void MapDrawIndicator(ICanvas canvas, RectangleF dirtyRect, IEntryDrawable drawable, IEntry view)
			=> drawable.DrawIndicator(canvas, dirtyRect, view);
    }
}