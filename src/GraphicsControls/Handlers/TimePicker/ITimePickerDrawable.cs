namespace Microsoft.Maui.Graphics.Controls
{
    public interface ITimePickerDrawable : IViewDrawable<ITimePicker>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, ITimePicker view);
        void DrawBorder(ICanvas canvas, RectangleF dirtyRect, ITimePicker view);
        void DrawPlaceholder(ICanvas canvas, RectangleF dirtyRect, ITimePicker view);
        void DrawTime(ICanvas canvas, RectangleF dirtyRect, ITimePicker view);
    }
}