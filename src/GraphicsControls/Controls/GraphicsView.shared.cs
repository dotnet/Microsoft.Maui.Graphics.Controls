using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Graphics;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Point = System.Graphics.Point;

namespace GraphicsControls
{
    public abstract class GraphicsView : ContentView, IGraphicsView, IGraphicsEffects
    {
        public GraphicsView()
        {
            GraphicsEffects = new List<IGraphicsEffect>();
        }

        public IList<IGraphicsEffect> GraphicsEffects { get; }

        public event EventHandler Loaded;
        public event EventHandler Unloaded;

        public event EventHandler TouchDown;
        public event EventHandler TouchMove;
        public event EventHandler TouchUp;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler Invalidated;

        public void InvalidateDraw() => Invalidated?.Invoke(this, EventArgs.Empty);

        public virtual void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            foreach (var graphicsEffect in GraphicsEffects)
                graphicsEffect.Draw(canvas, dirtyRect);
        }

        public virtual void Load()
        {
            Loaded?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Unload()
        {
            Unloaded?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnTouchDown(Point point)
        {
            TouchDown?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnTouchMove(Point point)
        {
            TouchMove?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnTouchUp(Point point)
        {
            TouchUp?.Invoke(this, EventArgs.Empty);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AttachComponents()
        {
            foreach (var graphicsEffect in GraphicsEffects)
                graphicsEffect.AttachTo(this);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DetachComponents()
        {
            foreach (var graphicsEffect in GraphicsEffects)
                graphicsEffect.DetachFrom(this);
        }
    }
}