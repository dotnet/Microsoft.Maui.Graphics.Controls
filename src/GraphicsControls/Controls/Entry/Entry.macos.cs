using AppKit;
using GraphicsControls;
using GraphicsControls.Mac;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace GraphicsControls.Mac
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Bordered = false;
                Control.FocusRingType = NSFocusRingType.None;
            }
        }
    }
}