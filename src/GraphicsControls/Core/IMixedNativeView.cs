namespace Microsoft.Maui.Graphics.Controls
{
	public interface IMixedNativeView : IInvalidatable
	{
		string[] NativeLayers { get; }
		IDrawable Drawable { get; set; }

		void DrawBaseLayer(RectangleF dirtyRect);
	}
}
