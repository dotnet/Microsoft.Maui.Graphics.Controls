using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class SliderHandler : GraphicsControlHandler<ISliderDrawable, ISlider>
	{
		readonly DrawableType _drawableType;

		public SliderHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public SliderHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<ISlider> PropertyMapper = new PropertyMapper<ISlider>(ViewHandler.Mapper)
		{
			[nameof(ISlider.Value)] = ViewHandler.MapInvalidate,
			[nameof(ISlider.ThumbColor)] = ViewHandler.MapInvalidate,
			[nameof(ISlider.MaximumTrackColor)] = ViewHandler.MapInvalidate,
			[nameof(ISlider.MinimumTrackColor)] = ViewHandler.MapInvalidate,
		};

		public static DrawMapper<ISliderDrawable, ISlider> DrawMapper = new DrawMapper<ISliderDrawable, ISlider>(ViewHandler.DrawMapper)
		{
			["Background"] = MapDrawBackground,
			["TrackProgress"] = MapDrawTrackProgress,
			["Thumb"] = MapDrawThumb,
			["Text"] = MapDrawText
		};

		public static string[] DefaultSliderLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Background",
				"TrackProgress",
				"Thumb",
				"Text",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultSliderLayerDrawingOrder;

		protected override ISliderDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialSliderDrawable();
				case DrawableType.Cupertino:
					return new CupertinoSliderDrawable();
				case DrawableType.Fluent:
					return new FluentSliderDrawable();
			}
		}

		public static void MapDrawBackground(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawBackground(canvas, dirtyRect, view);

		public static void MapDrawThumb(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawThumb(canvas, dirtyRect, view);

		public static void MapDrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawTrackProgress(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawText(canvas, dirtyRect, view);

		public override bool StartInteraction(PointF[] points)
		{
			if (VirtualView == null || !VirtualView.IsEnabled)
				return false;

			Drawable.IsDragging = Drawable.TouchTargetRect.Contains(points);

			UpdateValue(points[0]);

			return base.StartInteraction(points);
		}

		public override void DragInteraction(PointF[] points)
		{
			if (!Drawable.IsDragging)
				return;

			if (VirtualView == null || !VirtualView.IsEnabled)
				return;

			VirtualView.DragStarted();

			UpdateValue(points[0]);

			base.DragInteraction(points);
		}

		public override void EndInteraction(PointF[] points, bool inside)
		{
			Drawable.IsDragging = false;
			VirtualView?.DragCompleted();

			base.EndInteraction(points, inside);
		}

		public override void CancelInteraction()
		{
			Drawable.IsDragging = false;
			base.CancelInteraction();
		}

		void UpdateValue(PointF point)
		{
			if (VirtualView == null)
				return;

			var trackRect = Drawable.TrackRect;
			VirtualView.Value = (point.X - trackRect.X) * VirtualView.Maximum / trackRect.Width;
		}
	}
}