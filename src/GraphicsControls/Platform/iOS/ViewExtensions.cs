using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ViewExtensions
    {
        public static void UpdateIsEnabled(this UIView nativeView, IView view)
        {
            nativeView.UserInteractionEnabled = view.IsEnabled;
        }
    }
}
