namespace Microsoft.Maui.Graphics.Controls
{
    public interface IDatePickerDrawable : IViewDrawable<IDatePicker>
    {
        void DrawBackground(ICanvas canvas, RectF dirtyRect, IDatePicker view);
        void DrawBorder(ICanvas canvas, RectF dirtyRect, IDatePicker view);
        void DrawIndicator(ICanvas canvas, RectF dirtyRect, IDatePicker view);
        void DrawPlaceholder(ICanvas canvas, RectF dirtyRect, IDatePicker view);
        void DrawDate(ICanvas canvas, RectF dirtyRect, IDatePicker view);
    }
}