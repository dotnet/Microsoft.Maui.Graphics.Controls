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

		public override string[] LayerDrawingOrder()
        {
            throw new System.NotImplementedException();
        }

        protected override ICheckBoxDrawable CreateDrawable()
        {
            throw new System.NotImplementedException();
		}

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawMark(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBoxDrawable drawable, ICheckBox view)
			=> drawable.DrawText(canvas, dirtyRect, view);
	}
}