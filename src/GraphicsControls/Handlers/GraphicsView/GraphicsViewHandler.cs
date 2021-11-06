namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsViewHandler
    {
		public static IPropertyMapper<IGraphicsView, GraphicsViewHandler> GraphicsViewMapper = new PropertyMapper<IGraphicsView, GraphicsViewHandler>(ViewMapper);

		public static CommandMapper<IGraphicsView, GraphicsViewHandler> GraphicsViewCommandMapper = new CommandMapper<IGraphicsView, GraphicsViewHandler>(ViewCommandMapper)
		{
			[nameof(IGraphicsView.Invalidate)] = MapInvalidate
		};

		public GraphicsViewHandler() : base(GraphicsViewMapper, GraphicsViewCommandMapper)
		{

		}

		public GraphicsViewHandler(IPropertyMapper? mapper = null, CommandMapper? commandMapper = null)
			: base(mapper ?? GraphicsViewMapper, commandMapper ?? GraphicsViewCommandMapper)
		{

		}
	}
}
