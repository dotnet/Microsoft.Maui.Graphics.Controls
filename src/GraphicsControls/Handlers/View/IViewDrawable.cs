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

		void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IView view);
		void DrawClip(ICanvas canvas, RectangleF dirtyRect, IView view);
		void DrawText(ICanvas canvas, RectangleF dirtyRect, IText view);
		void DrawOverlay(ICanvas canvas, RectangleF dirtyRect, IView view);
		void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IView view);
		Size GetDesiredSize(IView view, double widthConstraint, double heightConstraint);
	}
}
