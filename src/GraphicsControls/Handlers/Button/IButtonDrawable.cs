namespace Microsoft.Maui.Graphics.Controls
{
    public interface IButtonDrawable : IViewDrawable<IButton>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IButton view);
        void DrawText(ICanvas canvas, RectangleF dirtyRect, IButton view);
    }
}