namespace Microsoft.Maui.Graphics.Controls
{
    public interface ISwitchDrawable : IViewDrawable<ISwitch>
    {
        double AnimationPercent { get; set; }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view);
        void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view);
    }
}