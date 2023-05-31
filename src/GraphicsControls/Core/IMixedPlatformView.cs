namespace Microsoft.Maui.Graphics.Controls
{
	public interface IMixedPlatformView : IInvalidatable
	{
		string[] PlatformLayers { get; }
		IDrawable Drawable { get; set; }
		IMixedGraphicsHandler? GraphicsControl { get; set; }

		void DrawBaseLayer(RectF dirtyRect);
	}
}
