using UIKit;

namespace Microsoft.Maui.Graphics.Controls
{
    public static class ViewExtensions
    {
        public static void UpdateIsEnabled(this UIView platformView, IView view)
        {
            platformView.UserInteractionEnabled = view.IsEnabled;
        }
    }
}
