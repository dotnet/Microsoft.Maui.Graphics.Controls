using Android.Content;
using Android.Views;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEntry : View, IMixedNativeView
    {
        public GraphicsEntry(Context context) : base(context)
        {

        }

        public string[] NativeLayers => throw new System.NotImplementedException();

        public IDrawable Drawable { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void DrawBaseLayer(RectangleF dirtyRect)
        {
            throw new System.NotImplementedException();
        }
    }
}