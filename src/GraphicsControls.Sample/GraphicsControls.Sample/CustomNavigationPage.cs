using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace GraphicsControls.Sample
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Page page) : base(page)
        {
            this.SetAppThemeColor(BackgroundColorProperty, SampleColors.LightAccentColor, SampleColors.DarkAccentColor);
            BarTextColor = Colors.White;
        }
    }
}