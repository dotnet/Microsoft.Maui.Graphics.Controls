namespace Microsoft.Maui.Graphics.Controls
{
	public interface IMixedGraphicsHandler : IGraphicsControlInteraction, IViewHandler, IDrawable, IInvalidate
	{
		DrawMapper DrawMapper { get; }

		string[] LayerDrawingOrder();
		void Resized(RectangleF bounds);
	}
}