using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class ProgressBarHandler : GraphicsControlHandler<IProgressBarDrawable, IProgress>
    {
		public static PropertyMapper<IProgress> PropertyMapper = new PropertyMapper<IProgress>(ViewHandler.Mapper)
		{
			[nameof(IProgress.Progress)] = ViewHandler.MapInvalidate
		};

		public static DrawMapper<IProgressBarDrawable, IProgress> DrawMapper = new DrawMapper<IProgressBarDrawable, IProgress>(ViewHandler.DrawMapper)
		{
			["Track"] = MapDrawTrack,
			["Progress"] = MapDrawProgress
		};

		public ProgressBarHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public static string[] DefaultProgressBarLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Track",
				"Progress",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultProgressBarLayerDrawingOrder;

		protected override IProgressBarDrawable CreateDrawable() =>
			new MaterialProgressBarDrawable();

		public static void MapDrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawTrack(canvas, dirtyRect, view);

		public static void MapDrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawProgress(canvas, dirtyRect, view);
	}
}