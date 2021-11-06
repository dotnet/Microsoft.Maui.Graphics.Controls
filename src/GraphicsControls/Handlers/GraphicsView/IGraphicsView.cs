using System;

namespace Microsoft.Maui.Graphics.Controls
{
    public interface IGraphicsView : IView, IDrawable
    {
        event EventHandler Loaded;
        event EventHandler Unloaded;

        event EventHandler TouchDown;
        event EventHandler TouchMove;
        event EventHandler TouchUp;

        void Invalidate();

        void Load();
        void Unload();

        void OnTouchDown(Point point);
        void OnTouchMove(Point point);
        void OnTouchUp(Point point);
    }
}