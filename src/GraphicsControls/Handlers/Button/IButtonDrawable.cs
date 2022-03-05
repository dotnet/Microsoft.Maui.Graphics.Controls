namespace Microsoft.Maui.Graphics.Controls
{
    public interface IButtonDrawable : IViewDrawable<IButton>
    {
        PointF TouchPoint { get; set; }
        double AnimationPercent { get; set; }

        void DrawBackground(ICanvas canvas, RectF dirtyRect, IButton view);
        void DrawText(ICanvas canvas, RectF dirtyRect, IButton view);
    }
}