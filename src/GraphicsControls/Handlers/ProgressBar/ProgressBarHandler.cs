using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public class ProgressBarHandler : GraphicsControlHandler<IProgressBarDrawable, IProgress>
    {
		readonly DrawableType _drawableType;

		public ProgressBarHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public ProgressBarHandler(DrawableType drawableType) : base(DrawMapper, PropertyMapper)
		{
			_drawableType = drawableType;
		}

		public static PropertyMapper<IProgress> PropertyMapper = new PropertyMapper<IProgress>(ViewHandler.Mapper)
		{
			[nameof(IProgress.Progress)] = ViewHandler.MapInvalidate,
			[nameof(IProgress.ProgressColor)] = ViewHandler.MapInvalidate
		};

		public static DrawMapper<IProgressBarDrawable, IProgress> DrawMapper = new DrawMapper<IProgressBarDrawable, IProgress>(ViewHandler.DrawMapper)
		{
			["Track"] = MapDrawTrack,
			["Progress"] = MapDrawProgress
		};

		public static string[] DefaultProgressBarLayerDrawingOrder =
			ViewHandler.DefaultLayerDrawingOrder.ToList().InsertAfter(new string[]
			{
				"Track",
				"Progress",
			}, "Text").ToArray();

		public override string[] LayerDrawingOrder() =>
			DefaultProgressBarLayerDrawingOrder;

		protected override IProgressBarDrawable CreateDrawable()
		{
			switch (_drawableType)
			{
				default:
				case DrawableType.Material:
					return new MaterialProgressBarDrawable();
				case DrawableType.Cupertino:
					return new CupertinoProgressBarDrawable();
				case DrawableType.Fluent:
					return new FluentProgressBarDrawable();
			}
		}

		public static void MapDrawTrack(ICanvas canvas, RectF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawTrack(canvas, dirtyRect, view);

		public static void MapDrawProgress(ICanvas canvas, RectF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawProgress(canvas, dirtyRect, view);
	}
}