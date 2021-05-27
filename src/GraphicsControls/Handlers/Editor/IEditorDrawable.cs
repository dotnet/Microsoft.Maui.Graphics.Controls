namespace Microsoft.Maui.Graphics.Controls
{
    public interface IEditorDrawable : IViewDrawable<IEditor>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor view);
        void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor view);
        void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor view);
    }
}