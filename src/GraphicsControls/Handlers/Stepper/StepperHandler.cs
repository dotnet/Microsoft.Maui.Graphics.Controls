using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class StepperHandler : GraphicsControlHandler<IStepperDrawable, IStepper>
    {
		public static PropertyMapper<IStepper> PropertyMapper = new PropertyMapper<IStepper>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(IStepper.Minimum)] = ViewHandler.MapInvalidate,
				[nameof(IStepper.Maximum)] = ViewHandler.MapInvalidate,
				[nameof(IStepper.Interval)] = ViewHandler.MapInvalidate,
				[nameof(IStepper.Value)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<IStepperDrawable, IStepper> DrawMapper = new DrawMapper<IStepperDrawable, IStepper>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Separator"] = MapDrawSeparator,
			["Minus"] = MapDrawMinus,
			["Plus"] = MapDrawPlus,
			["Text"] = MapDrawText
		};

		public StepperHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static string[] DefaultStepperLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Separator",
				"Minus",
				"Plus",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultStepperLayerDrawingOrder;

		protected override IStepperDrawable CreateDrawable() =>
			new MaterialStepperDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawSeparator(ICanvas canvas, RectangleF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawSeparator(canvas, dirtyRect, view);

		public static void MapDrawMinus(ICanvas canvas, RectangleF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawMinus(canvas, dirtyRect, view);

		public static void MapDrawPlus(ICanvas canvas, RectangleF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawPlus(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, IStepperDrawable drawable, IStepper view)
			=> drawable.DrawText(canvas, dirtyRect, view);
	}
}