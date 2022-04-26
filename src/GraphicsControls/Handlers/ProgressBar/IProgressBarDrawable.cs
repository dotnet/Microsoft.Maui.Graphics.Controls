namespace Microsoft.Maui.Graphics.Controls
{
    public interface IProgressBarDrawable : IViewDrawable<IProgress>
    {
        void DrawTrack(ICanvas canvas, RectF dirtyRect, IProgress view);
        void DrawProgress(ICanvas canvas, RectF dirtyRect, IProgress view);
    }
}