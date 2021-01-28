using GraphicsControls;
using GraphicsControls.Tizen;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using EEntry = ElmSharp.Entry;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace GraphicsControls.Tizen
{
    public class BorderlessEntryRenderer : EntryRenderer
    {

        protected override EEntry CreateNativeControl()
        {
            var entry = new EEntry(Forms.NativeParent)
            {
                IsSingleLine = true,
            };
            return entry;
        }
    }
}