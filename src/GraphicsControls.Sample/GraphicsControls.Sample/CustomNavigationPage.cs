using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace GraphicsControls.Sample
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page page) : base(page)
        {
            BarBackgroundColor = SampleColors.AccentColor;
            BarTextColor = Colors.White;
        }
    }
}