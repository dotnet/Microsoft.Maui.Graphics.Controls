using Android.Content;
using Android.Views;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsTimePicker : View, IMixedNativeView
    {
        public GraphicsTimePicker(Context context) : base(context)
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
