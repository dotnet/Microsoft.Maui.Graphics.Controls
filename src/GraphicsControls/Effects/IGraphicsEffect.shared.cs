using System.Graphics;

namespace GraphicsControls.Effects
{
    public interface IGraphicsEffect
    {
        void AttachTo(GraphicsView graphicsView);
        void DetachFrom(GraphicsView graphicsView);
        void Draw(ICanvas canvas, RectangleF dirtyRect);
    }
}