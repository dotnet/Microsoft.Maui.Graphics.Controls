namespace Microsoft.Maui.Graphics.Controls
{
    public class SwitchHandler : GraphicsControlHandler<ISwitchDrawable, ISwitch>
    {
		public static PropertyMapper<ISwitch> PropertyMapper = new PropertyMapper<ISwitch>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(ISwitch.IsOn)] = ViewHandler.MapInvalidate,
				[nameof(ISwitch.TrackColor)] = ViewHandler.MapInvalidate,
				[nameof(ISwitch.ThumbColor)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<ISwitchDrawable, ISwitch> DrawMapper = new DrawMapper<ISwitchDrawable, ISwitch>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Thumb"] = MapDrawThumb
		};

		public SwitchHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public override string[] LayerDrawingOrder()
        {
            throw new System.NotImplementedException();
        }

        protected override ISwitchDrawable CreateDrawable()
        {
            throw new System.NotImplementedException();
		}

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
			=> drawable.DrawThumb(canvas, dirtyRect, view);
	}
}