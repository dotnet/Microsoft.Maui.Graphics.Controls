using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class TimePickerHandler : GraphicsControlHandler<ITimePickerDrawable, ITimePicker>
	{
		public static PropertyMapper<ITimePicker> PropertyMapper = new PropertyMapper<ITimePicker>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(ITimePicker.Format)] = ViewHandler.MapInvalidate,
				[nameof(ITimePicker.Time)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<ITimePickerDrawable, ITimePicker> DrawMapper = new DrawMapper<ITimePickerDrawable, ITimePicker>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Placeholder"] = MapDrawPlaceholder,
			["Time"] = MapDrawTime
		};

		public TimePickerHandler() : base(DrawMapper, PropertyMapper)
		{

		}

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