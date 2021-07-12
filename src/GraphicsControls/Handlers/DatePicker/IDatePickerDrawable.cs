namespace Microsoft.Maui.Graphics.Controls
{
    public interface IDatePickerDrawable : IViewDrawable<IDatePicker>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IDatePicker view);
        void DrawBorder(ICanvas canvas, RectangleF dirtyRect, IDatePicker view);
        void DrawIndicator(ICanvas canvas, RectangleF dirtyRect, IDatePicker view);
        void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, IDatePicker view);
        void DrawDate(ICanvas canvas, RectangleF dirtyRect, IDatePicker view);
    }
}