namespace Microsoft.Maui.Graphics.Controls
{
    public interface IEntryDrawable : IViewDrawable<IEntry>
    {
        RectF IndicatorRect { get; }

        // TODO: Remove when having Focus support in .NET MAUI
        bool HasFocus { get; set; }

        double AnimationPercent { get; set; }

        void DrawBackground(ICanvas canvas, RectF dirtyRect, IEntry view);
        void DrawBorder(ICanvas canvas, RectF dirtyRect, IEntry view);
        void DrawPlaceholder(ICanvas canvas, RectF dirtyRect, IEntry view);
        void DrawIndicator(ICanvas canvas, RectF dirtyRect, IEntry view);
    }
}