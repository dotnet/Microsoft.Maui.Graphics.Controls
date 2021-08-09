namespace Microsoft.Maui.Graphics.Controls
{
    public interface IButtonDrawable : IViewDrawable<IButton>
    {
        PointF TouchPoint { get; set; }
        double AnimationPercent { get; set; }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton view);
        void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton view);
    }
}