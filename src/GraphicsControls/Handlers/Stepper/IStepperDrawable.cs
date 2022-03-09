namespace Microsoft.Maui.Graphics.Controls
{
    public interface IStepperDrawable : IViewDrawable<IStepper>
    {
        PointF TouchPoint { get; set; }
        double AnimationPercent { get; set; }
        RectF MinusRectangle { get; }
        RectF PlusRectangle { get; }

        void DrawBackground(ICanvas canvas, RectF dirtyRect, IStepper view);
        void DrawSeparator(ICanvas canvas, RectF dirtyRect, IStepper view);
        void DrawMinus(ICanvas canvas, RectF dirtyRect, IStepper view);
        void DrawPlus(ICanvas canvas, RectF dirtyRect, IStepper view);
        void DrawText(ICanvas canvas, RectF dirtyRect, IStepper view);
    }
}
