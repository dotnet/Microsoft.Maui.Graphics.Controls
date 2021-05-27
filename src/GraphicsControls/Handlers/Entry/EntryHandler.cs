using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class EntryHandler : GraphicsControlHandler<IEntryDrawable, IEntry>
	{
		public static PropertyMapper<IEntry> PropertyMapper = new PropertyMapper<IEntry>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(IEntry.Placeholder)] = ViewHandler.MapInvalidate,
				[nameof(IEntry.Text)] = ViewHandler.MapInvalidate,
				[nameof(IEntry.TextColor)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<IEntryDrawable, IEntry> DrawMapper = new DrawMapper<IEntryDrawable, IEntry>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Placeholder"] = MapDrawPlaceholder,
			["Indicator"] = MapDrawIndicator
		};

		public EntryHandler() : base(DrawMapper, PropertyMapper)
		{

		}

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