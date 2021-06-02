namespace Microsoft.Maui.Graphics.Controls
{
	public interface IGraphicsControlInteraction
	{
		bool TouchEnabled { get; }

		void StartHoverInteraction(PointF[] points);
		void HoverInteraction(PointF[] points);
		void EndHoverInteraction();
		bool StartInteraction(PointF[] points);
		void DragInteraction(PointF[] points);
		void EndInteraction(PointF[] points, bool inside);
		void CancelInteraction();
		bool PointsContained(PointF[] points);
	}
}