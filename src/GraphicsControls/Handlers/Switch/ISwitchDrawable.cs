namespace Microsoft.Maui.Graphics.Controls
{
    public interface ISwitchDrawable : IViewDrawable<ISwitch>
    {
        double AnimationPercent { get; set; }

        void DrawBackground(ICanvas canvas, RectF dirtyRect, ISwitch view);
        void DrawThumb(ICanvas canvas, RectF dirtyRect, ISwitch view);
    }
}