namespace Microsoft.Maui.Graphics.Controls
{
    public interface IEntryDrawable : IViewDrawable<IEntry>
    {
        RectangleF IndicatorRect { get; }

        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IEntry view);
        void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IEntry view);
        void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IEntry view);
        void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IEntry view);
    }
}