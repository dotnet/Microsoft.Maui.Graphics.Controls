namespace Microsoft.Maui.Graphics.Controls
{
    public interface ISliderDrawable : IViewDrawable<ISlider>
	{
		bool IsDragging { get; set; }
		RectF TrackRect { get; }
		RectF TouchTargetRect { get; }

		void DrawBackground(ICanvas canvas, RectF dirtyRect, ISlider view);
		void DrawThumb(ICanvas canvas, RectF dirtyRect, ISlider view);
		void DrawTrackProgress(ICanvas canvas, RectF dirtyRect, ISlider view);
		void DrawText(ICanvas canvas, RectF dirtyRect, ISlider view);
	}
}