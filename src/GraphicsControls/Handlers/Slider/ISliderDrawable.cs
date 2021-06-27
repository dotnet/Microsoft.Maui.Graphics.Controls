namespace Microsoft.Maui.Graphics.Controls
{
    public interface ISliderDrawable : IViewDrawable<ISlider>
	{
		RectangleF TrackRect { get; }
		RectangleF TouchTargetRect { get; }

		void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISlider view);
		void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISlider view);
		void DrawTrackProgress(ICanvas canvas, RectangleF dirtyRect, ISlider view);
		void DrawText(ICanvas canvas, RectangleF dirtyRect, ISlider view);
	}
}