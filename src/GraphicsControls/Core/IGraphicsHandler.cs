namespace Microsoft.Maui.Graphics.Controls
{
    public interface IGraphicsHandler : IGraphicsControlInteraction, IViewHandler, IDrawable, IInvalidatable
	{
		DrawMapper DrawMapper { get; }

		string[] LayerDrawingOrder();
		void Resized(RectangleF bounds);
	}
}