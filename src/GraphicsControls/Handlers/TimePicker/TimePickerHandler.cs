using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class TimePickerHandler
	{
		public TimePickerHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static PropertyMapper<ITimePicker, TimePickerHandler> PropertyMapper = new PropertyMapper<ITimePicker, TimePickerHandler>(ViewHandler.Mapper)
		{
			[nameof(ITimePicker.Background)] = ViewHandler.MapInvalidate,
			[nameof(ITimePicker.Format)] = MapTime,
			[nameof(ITimePicker.Time)] = MapTime
		};

		public static DrawMapper<ITimePickerDrawable, ITimePicker> DrawMapper = new DrawMapper<ITimePickerDrawable, ITimePicker>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Placeholder"] = MapDrawPlaceholder,
			["Time"] = MapDrawTime
		};

		public static string[] DefaultTimePickerLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Border",
				"Placeholder",
				"Time",
			}, "Background").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultTimePickerLayerDrawingOrder;

		protected override ITimePickerDrawable CreateDrawable() =>
			new MaterialTimePickerDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawBorder(ICanvas canvas, RectangleF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawBorder(canvas, dirtyRect, view);

		public static void MapDrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawPlaceholder(canvas, dirtyRect, view);

		public static void MapDrawTime(ICanvas canvas, RectangleF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawTime(canvas, dirtyRect, view);
	}
}