﻿using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class TimePickerHandler
	{
		readonly DrawableType _drawableType;

		public TimePickerHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public TimePickerHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
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

		protected override ITimePickerDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialTimePickerDrawable();
				case DrawableType.Cupertino:
					return new CupertinoTimePickerDrawable();
				case DrawableType.Fluent:
					return new FluentTimePickerDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawBorder(ICanvas canvas, RectF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawBorder(canvas, dirtyRect, view);

		public static void MapDrawPlaceholder(ICanvas canvas, RectF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawPlaceholder(canvas, dirtyRect, view);

		public static void MapDrawTime(ICanvas canvas, RectF dirtyRect, ITimePickerDrawable drawable, ITimePicker view)
			=> drawable.DrawTime(canvas, dirtyRect, view);
	}
}