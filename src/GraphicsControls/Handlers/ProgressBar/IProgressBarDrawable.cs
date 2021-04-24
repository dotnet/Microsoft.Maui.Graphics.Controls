namespace Microsoft.Maui.Graphics.Controls
{
    public interface IProgressBarDrawable : IViewDrawable<IProgress>
    {
        void DrawTrack(ICanvas canvas, RectangleF dirtyRect, IProgress view);
        void DrawProgress(ICanvas canvas, RectangleF dirtyRect, IProgress view);
    }
}