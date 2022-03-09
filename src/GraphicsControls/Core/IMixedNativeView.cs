namespace Microsoft.Maui.Graphics.Controls
{
	public interface IMixedNativeView : IInvalidatable
	{
		string[] NativeLayers { get; }
		IDrawable Drawable { get; set; }
		IMixedGraphicsHandler? GraphicsControl { get; set; }

		void DrawBaseLayer(RectF dirtyRect);
	}
}
