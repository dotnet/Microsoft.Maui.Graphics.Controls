using GraphicsControls;
using GraphicsControls.Tizen;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EEntry = ElmSharp.Entry;
using ESize = ElmSharp.Size;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace GraphicsControls.Tizen
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        protected override EEntry CreateNativeControl()
        {
            var entry = new EEntry(Forms.NativeParent)
            {
                IsSingleLine = false,
            };
            return entry;
        }

        protected override Size MinimumSize()
        {
            return new ESize(NativeView.MinimumWidth, NativeView.MinimumHeight).ToDP();
        }
    }
}