using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class ButtonHandler : GraphicsControlHandler<IButtonDrawable, IButton>
    {
		public static PropertyMapper<IButton> PropertyMapper = new PropertyMapper<IButton>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(IButton.Background)] = ViewHandler.MapInvalidate,
				[nameof(IButton.Text)] = ViewHandler.MapInvalidate,
				[nameof(IButton.TextColor)] = ViewHandler.MapInvalidate,
				[nameof(IButton.Font)] = ViewHandler.MapInvalidate,
				[nameof(IButton.CharacterSpacing)] = ViewHandler.MapInvalidate,
				[nameof(IButton.Padding)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<IButtonDrawable, IButton> DrawMapper = new DrawMapper<IButtonDrawable, IButton>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["Text"] = MapDrawText
		};

		public ButtonHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static string[] DefaultButtonLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"Text",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultButtonLayerDrawingOrder;

		protected override IButtonDrawable CreateDrawable() =>
			new MaterialButtonDrawable();

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, IButtonDrawable drawable, IButton view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, IButtonDrawable drawable, IButton view)
			=> drawable.DrawText(canvas, dirtyRect, view);

        public override bool StartInteraction(PointF[] points)
        {
			if (VirtualView != null && VirtualView.IsEnabled)
			{
				VirtualView.Pressed();
				VirtualView.Clicked();
			}

			return base.StartInteraction(points);
        }

        public override void EndInteraction(PointF[] points, bool inside)
        {
			if(inside && VirtualView != null && VirtualView.IsEnabled)
            {
				VirtualView.Released();
            }

            base.EndInteraction(points, inside);
        }
    }
}