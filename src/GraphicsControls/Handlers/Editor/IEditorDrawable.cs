namespace Microsoft.Maui.Graphics.Controls
{
    public interface IEditorDrawable : IViewDrawable<IEditor>
    {
        // TODO: Remove when having Focus support in .NET MAUI
        bool HasFocus { get; set; }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEditor view);
        void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEditor view);
        void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEditor view);
    }
}