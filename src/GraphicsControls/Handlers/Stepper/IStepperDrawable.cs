namespace Microsoft.Maui.Graphics.Controls
{
    public interface IStepperDrawable : IViewDrawable<IStepper>
    {
        void DrawBackground(ICanvas canvas, RectangleF dirtyRect, IStepper view);
        void DrawSeparator(ICanvas canvas, RectangleF dirtyRect, IStepper view);
        void DrawMinus(ICanvas canvas, RectangleF dirtyRect, IStepper view);
        void DrawPlus(ICanvas canvas, RectangleF dirtyRect, IStepper view);
        void DrawText(ICanvas canvas, RectangleF dirtyRect, IStepper view);
    }
}
