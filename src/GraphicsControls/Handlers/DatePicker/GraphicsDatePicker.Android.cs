using System;
using Android.Content;
using Android.Views;

namespace Microsoft.Maui.Graphics.Controls
{
    public partial class GraphicsDatePicker : View, IMixedNativeView
    {
        public GraphicsDatePicker(Context context) : base(context)
        {

        }

        public string[] NativeLayers => throw new NotImplementedException();

        public IDrawable Drawable { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void DrawBaseLayer(RectangleF dirtyRect)
        {
            throw new NotImplementedException();
        }
    }
}