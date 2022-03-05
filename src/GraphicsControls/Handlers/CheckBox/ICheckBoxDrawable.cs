namespace Microsoft.Maui.Graphics.Controls
{
    public interface ICheckBoxDrawable : IViewDrawable<ICheckBox>
    {
        void DrawBackground(ICanvas canvas, RectF dirtyRect, ICheckBox view);
        void DrawMark(ICanvas canvas, RectF dirtyRect, ICheckBox view);
        void DrawText(ICanvas canvas, RectF dirtyRect, ICheckBox view);
    }
}