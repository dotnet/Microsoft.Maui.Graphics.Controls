namespace Microsoft.Maui.Graphics.Controls
{
    public interface ILabelDrawable : IViewDrawable<ILabel>
    {
        void DrawBackground(ICanvas canvas, RectF dirtyRect, ILabel view);
        void DrawText(ICanvas canvas, RectF dirtyRect, ILabel view);
    }
}