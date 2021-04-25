using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
	public class CheckBoxHandler : GraphicsControlHandler<ICheckBoxDrawable, ICheckBox>
	{
		public static PropertyMapper<ICheckBox> PropertyMapper = new PropertyMapper<ICheckBox>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(ICheckBox.IsChecked)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<ICheckBoxDrawable, ICheckBox> DrawMapper = new DrawMapper<ICheckBoxDrawable, ICheckBox>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Mark"] = MapDrawMark,
			["Text"] = MapDrawText
		};

		public CheckBoxHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static string[] DefaultCheckBoxLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Mark",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultCheckBoxLayerDrawingOrder;

		protected override ICheckBoxDrawable CreateDrawable() =>
			new MaterialCheckBoxDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawMark(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawText(canvas, dirtyRect, view);

		public override bool StartInteraction(PointF[] points)
		{
			if (VirtualView != null)
				VirtualView.IsChecked = !VirtualView.IsChecked;

			return base.StartInteraction(points);
		}
	}
}