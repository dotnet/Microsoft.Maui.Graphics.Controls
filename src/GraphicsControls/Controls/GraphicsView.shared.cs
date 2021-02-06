using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Graphics;
using System.Linq;
using GraphicsControls.Effects;
using Xamarin.Forms;
using Point = System.Graphics.Point;

namespace GraphicsControls
{
    public abstract class GraphicsView : ContentView, IGraphicsView, IGraphicsLayerManager, IGraphicsEffects
    {
        public GraphicsView()
        {
            GraphicsLayers = new List<string>();
            GraphicsEffects = new List<IGraphicsEffect>();
        }

        public virtual List<string> GraphicsLayers { get; }

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
            foreach (var layer in GraphicsLayers)
                DrawLayer(layer, canvas, dirtyRect);

            foreach (var graphicsEffect in GraphicsEffects)
                graphicsEffect.Draw(canvas, dirtyRect);
        }

        public virtual void DrawLayer(string layer, ICanvas canvas, RectangleF dirtyRect)
        {

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

        public int GetLayerIndex(string layerName)
        {
            for (int i = 0; i < GraphicsLayers.Count(); i++)
                if (GraphicsLayers.ElementAt(i) == layerName)
                    return i;

            return -1;
        }

        public void AddLayer(string layer)
        {
            GraphicsLayers.Add(layer);

            InvalidateDraw();
        }

        public void AddLayer(int index, string layer)
        {
            GraphicsLayers.Insert(index, layer);

            InvalidateDraw();
        }

        public void RemoveLayer(string layerName)
        {
            for (int i = 0; i < GraphicsLayers.Count(); i++)
            {
                if(GraphicsLayers.ElementAt(i) == layerName)
                {
                    GraphicsLayers.RemoveAt(i);

                    InvalidateDraw();

                    break;
                }
            }
        }

        public void RemoveLayer(int index)
        {
            GraphicsLayers.RemoveAt(index);

            InvalidateDraw();
        }
    }
}