using System.Linq;

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

		public static string[] DefaultSwitchLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Thumb"
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultSwitchLayerDrawingOrder;

		protected override ISwitchDrawable CreateDrawable() =>
			new MaterialSwitchDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitchDrawable drawable, ISwitch view)
			=> drawable.DrawThumb(canvas, dirtyRect, view);

        public override bool StartInteraction(PointF[] points)
		{
			if (VirtualView != null && VirtualView.IsEnabled)
			{
				VirtualView.IsOn = !VirtualView.IsOn;
				Invalidate();
			}

			return base.StartInteraction(points);
        }
    }
}