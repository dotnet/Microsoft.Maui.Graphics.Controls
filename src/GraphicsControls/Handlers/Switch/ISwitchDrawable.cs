namespace Microsoft.Maui.Graphics.Controls
{
    public interface ISwitchDrawable : IViewDrawable<ISwitch>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ISwitch view);
        void DrawThumb(ICanvas canvas, RectangleF dirtyRect, ISwitch view);
    }
}