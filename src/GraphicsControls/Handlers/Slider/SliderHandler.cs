using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class SliderHandler : GraphicsControlHandler<ISliderDrawable, ISlider>
	{
		bool _isTracking;

		public static PropertyMapper<ISlider> PropertyMapper = new PropertyMapper<ISlider>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(ISlider.Value)] = ViewHandler.MapInvalidate,
				[nameof(ISlider.Value)] = ViewHandler.MapInvalidate,
				[nameof(ISlider.ThumbColor)] = ViewHandler.MapInvalidate,
				[nameof(ISlider.MaximumTrackColor)] = ViewHandler.MapInvalidate,
				[nameof(ISlider.MinimumTrackColor)] = ViewHandler.MapInvalidate,
			}
		};

		public static DrawMapper<ISliderDrawable, ISlider> DrawMapper = new DrawMapper<ISliderDrawable, ISlider>(ViewHandler.DrawMapper)
		{
			["TrackProgress"] = MapDrawTrackProgress,
			["Thumb"] = MapDrawThumb,
			["Text"] = MapDrawText
		};

		public SliderHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static string[] DefaultSliderLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"TrackProgress",
				"Thumb",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultSliderLayerDrawingOrder;

		protected override ISliderDrawable CreateDrawable() =>
			new MaterialSliderDrawable();

		public static void MapDrawThumb(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawThumb(canvas, dirtyRect, view);

		public static void MapDrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawTrackProgress(canvas, dirtyRect, view);

		public static void MapDrawText(ICanvas canvas, RectangleF dirtyRect, ISliderDrawable drawable, ISlider view)
			=> drawable.DrawText(canvas, dirtyRect, view);

		public override bool StartInteraction(PointF[] points)
		{
			_isTracking = Drawable.TouchTargetRect.Contains(points);
			return base.StartInteraction(points);
		}

		public override void DragInteraction(PointF[] points)
		{
			if (!_isTracking)
				return;

			if (VirtualView == null)
				return;

			VirtualView.DragStarted();

			// Only track the first point;
			var point = points[0];
			var TrackRect = Drawable.TrackRect;
			var progress = (point.X - TrackRect.X) / TrackRect.Width;


			var min = VirtualView.Minimum;
			var max = VirtualView.Maximum;

			var value = min + (max - min) * progress;

			VirtualView.Value = value;

			base.DragInteraction(points);
		}

		public override void EndInteraction(PointF[] points, bool inside)
		{
			_isTracking = false;
			VirtualView?.DragCompleted();

			base.EndInteraction(points, inside);
		}

		public override void CancelInteraction()
		{
			_isTracking = false;
			base.CancelInteraction();
		}
	}
}