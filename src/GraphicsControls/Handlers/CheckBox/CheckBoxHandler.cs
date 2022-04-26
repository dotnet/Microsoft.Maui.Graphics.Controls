using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
	public class CheckBoxHandler : GraphicsControlHandler<ICheckBoxDrawable, ICheckBox>
	{
		readonly DrawableType _drawableType;

		public CheckBoxHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public CheckBoxHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<ICheckBox> PropertyMapper = new PropertyMapper<ICheckBox>(ViewHandler.Mapper)
		{
			[nameof(ICheckBox.IsChecked)] = ViewHandler.MapInvalidate,
			[nameof(ICheckBox.Foreground)] = ViewHandler.MapInvalidate,
		};

		public static DrawMapper<ICheckBoxDrawable, ICheckBox> DrawMapper = new DrawMapper<ICheckBoxDrawable, ICheckBox>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Mark"] = MapDrawMark,
			["Text"] = MapDrawText
		};

		public static string[] DefaultCheckBoxLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Mark",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultCheckBoxLayerDrawingOrder;

		protected override ICheckBoxDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialCheckBoxDrawable();
				case DrawableType.Cupertino:
					return new CupertinoCheckBoxDrawable();
				case DrawableType.Fluent:
					return new FluentCheckBoxDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawMark(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawMark(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawText(canvas, dirtyRect, view);

		public override bool StartInteraction(PointF[] points)
		{
			if (VirtualView != null)
				VirtualView.IsChecked = !VirtualView.IsChecked;

			return base.StartInteraction(points);
		}
	}
}