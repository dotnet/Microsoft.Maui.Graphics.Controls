namespace Microsoft.Maui.Graphics.Controls
{
    public interface ITimePickerDrawable : IViewDrawable<ITimePicker>
    {
        void DrawBackground(ICanvas canvas, RectF dirtyRect, ITimePicker view);
        void DrawBorder(ICanvas canvas, RectF dirtyRect, ITimePicker view);
        void DrawPlaceholder(ICanvas canvas, RectF dirtyRect, ITimePicker view);
        void DrawTime(ICanvas canvas, RectF dirtyRect, ITimePicker view);
    }
}