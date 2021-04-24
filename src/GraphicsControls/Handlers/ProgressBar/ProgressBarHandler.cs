namespace Microsoft.Maui.Graphics.Controls
{
    public class ProgressBarHandler : GraphicsControlHandler<IProgressBarDrawable, IProgress>
    {
		public static PropertyMapper<IProgress> PropertyMapper = new PropertyMapper<IProgress>(ViewHandler.Mapper)
		{
			Actions =
			{
				[nameof(IProgress.Progress)] = ViewHandler.MapInvalidate
			}
		};

		public static DrawMapper<IProgressBarDrawable, IProgress> DrawMapper = new DrawMapper<IProgressBarDrawable, IProgress>(ViewHandler.DrawMapper)
		{
			["Track"] = MapDrawTrack,
			["Progress"] = MapDrawProgress
		};

		public ProgressBarHandler() : base(DrawMapper, PropertyMapper)
		{

		}

		public override string[] LayerDrawingOrder()
        {
            throw new System.NotImplementedException();
        }

        protected override IProgressBarDrawable CreateDrawable()
        {
            throw new System.NotImplementedException();
		}

		public static void MapDrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawTrack(canvas, dirtyRect, view);

		public static void MapDrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgressBarDrawable drawable, IProgress view)
			=> drawable.DrawProgress(canvas, dirtyRect, view);
	}
}