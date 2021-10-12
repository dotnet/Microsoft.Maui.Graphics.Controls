namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsViewHandler
    {
		public static IPropertyMapper<IGraphicsView, GraphicsViewHandler> GraphicsViewMapper = new PropertyMapper<IGraphicsView, GraphicsViewHandler>(ViewMapper);

		public GraphicsViewHandler() : base(GraphicsViewMapper)
		{

		}

		public GraphicsViewHandler(IPropertyMapper mapper) : base(mapper ?? GraphicsViewMapper)
		{

		}
	}
}
