using Android.Content;
using Android.Widget;

namespace Microsoft.Maui.Graphics.Controls
{
    public class GraphicsEditor : EditText, IMixedNativeView
    {
        public GraphicsEditor(Context context) : base(context)
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
