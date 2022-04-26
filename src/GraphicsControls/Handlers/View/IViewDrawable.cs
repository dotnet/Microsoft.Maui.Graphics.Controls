namespace Microsoft.Maui.Graphics.Controls
{
	public interface IViewDrawable<TVirtualView> : IViewDrawable
		where TVirtualView : IView
	{

	}

	public interface IViewDrawable
	{
		IView View { get; set; }
		ControlState CurrentState { get; set; }

		void DrawBackground(ICanvas canvas, RectF dirtyRect, IView view);
		void DrawClip(ICanvas canvas, RectF dirtyRect, IView view);
		void DrawText(ICanvas canvas, RectF dirtyRect, IText view);
		void DrawOverlay(ICanvas canvas, RectF dirtyRect, IView view);
		void DrawBorder(ICanvas canvas, RectF dirtyRect, IView view);
		Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint);
	}
}
