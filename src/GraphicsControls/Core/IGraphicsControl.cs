namespace Microsoft.Maui.Graphics.Controls
{
    public interface IGraphicsControl : IViewHandler, IDrawable
	{
		DrawMapper DrawMapper { get; }
		bool TouchEnabled { get; }

		void Invalidate();
		string[] LayerDrawingOrder();
		void StartHoverInteraction(PointF[] points);
		void HoverInteraction(PointF[] points);
		void EndHoverInteraction();
		bool StartInteraction(PointF[] points);
		void DragInteraction(PointF[] points);
		void EndInteraction(PointF[] points, bool inside);
		void CancelInteraction();
		bool PointsContained(PointF[] points);
		void Resized(RectangleF bounds);
	}
}