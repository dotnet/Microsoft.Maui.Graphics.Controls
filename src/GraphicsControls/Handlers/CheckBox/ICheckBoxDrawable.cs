namespace Microsoft.Maui.Graphics.Controls
{
    public interface ICheckBoxDrawable : IViewDrawable<ICheckBox>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ICheckBox view);
        void DrawMark(ICanvas canvas, RectangleF dirtyRect, ICheckBox view);
        void DrawText(ICanvas canvas, RectangleF dirtyRect, ICheckBox view);
    }
}