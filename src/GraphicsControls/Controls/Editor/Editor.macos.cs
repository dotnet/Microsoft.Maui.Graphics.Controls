using AppKit;
using GraphicsControls;
using GraphicsControls.Mac;
using Xamarin.Forms;
using Xamarin.Forms.Platform.MacOS;

[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace GraphicsControls.Mac
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Editor> e)
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