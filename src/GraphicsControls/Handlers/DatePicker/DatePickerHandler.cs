using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class DatePickerHandler
	{
		readonly DrawableType _drawableType;

		public DatePickerHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public DatePickerHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<IDatePicker, DatePickerHandler> PropertyMapper = new PropertyMapper<IDatePicker, DatePickerHandler>(ViewHandler.Mapper)
		{
			[nameof(IDatePicker.Background)] = ViewHandler.MapInvalidate,
			[nameof(IDatePicker.MinimumDate)] = MapMinimumDate,
			[nameof(IDatePicker.MaximumDate)] = MapMaximumDate,
			[nameof(IDatePicker.Date)] = MapDate
		};

		public static DrawMapper<IDatePickerDrawable, IDatePicker> DrawMapper = new DrawMapper<IDatePickerDrawable, IDatePicker>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Border"] = MapDrawBorder,
			["Indicator"] = MapDrawIndicator,
			["Placeholder"] = MapDrawPlaceholder,
			["Date"] = MapDrawDate
		};

		public static string[] DefaultDatePickerLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Border",
				"Indicator",
				"Placeholder",
				"Date",
			}, "Background").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultDatePickerLayerDrawingOrder;

		protected override IDatePickerDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialDatePickerDrawable();
				case DrawableType.Cupertino:
					return new CupertinoDatePickerDrawable();
				case DrawableType.Fluent:
					return new FluentDatePickerDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, IDatePickerDrawable drawable, IDatePicker view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawBorder(ICanvas canvas, RectF dirtyRect, IDatePickerDrawable drawable, IDatePicker view)
			=> drawable.DrawBorder(canvas, dirtyRect, view);

		public static void MapDrawIndicator(ICanvas canvas, RectF dirtyRect, IDatePickerDrawable drawable, IDatePicker view)
		   => drawable.DrawIndicator(canvas, dirtyRect, view);

		public static void MapDrawPlaceholder(ICanvas canvas, RectF dirtyRect, IDatePickerDrawable drawable, IDatePicker view)
			=> drawable.DrawPlaceholder(canvas, dirtyRect, view);

		public static void MapDrawDate(ICanvas canvas, RectF dirtyRect, IDatePickerDrawable drawable, IDatePicker view)
			=> drawable.DrawDate(canvas, dirtyRect, view);
	}
}