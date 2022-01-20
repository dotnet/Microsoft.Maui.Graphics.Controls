#nullable disable
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Maui.Graphics.Controls
{
    public abstract class GraphicsView : ContentView, IGraphicsView, IGraphicsLayerManager
    {
        public GraphicsView()
        {
            GraphicsLayers = new List<string>();
        }

        public virtual List<string> GraphicsLayers { get; }

        public event EventHandler Loaded;
        public event EventHandler Unloaded;

        public event EventHandler TouchDown;
        public event EventHandler TouchMove;
        public event EventHandler TouchUp;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public event EventHandler Invalidated;

        public void Invalidate()
        {
            Handler?.Invoke(nameof(IGraphicsView.Invalidate));
            
            Invalidated?.Invoke(this, EventArgs.Empty);
        }

        public virtual void Draw(ICanvas canvas, RectangleF dirtyRect)
        {
            foreach (var layer in GraphicsLayers)
                DrawLayer(layer, canvas, dirtyRect);
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

            Invalidate();
        }

        public void AddLayer(int index, string layer)
        {
            GraphicsLayers.Insert(index, layer);

            Invalidate();
        }

        public void RemoveLayer(string layerName)
        {
            for (int i = 0; i < GraphicsLayers.Count(); i++)
            {
                if (GraphicsLayers.ElementAt(i) == layerName)
                {
                    GraphicsLayers.RemoveAt(i);

                    Invalidate();

                    break;
                }
            }
        }

        public void RemoveLayer(int index)
        {
            GraphicsLayers.RemoveAt(index);

            Invalidate();
        }
    }
}