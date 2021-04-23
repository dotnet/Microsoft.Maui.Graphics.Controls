using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MainPage : ContentPage, IPage
    {
        public MainPage()
        {
            BackgroundColor = Colors.White;
            Content = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Microsoft.Maui.Graphics.Controls",
                TextColor = Colors.Black
            };
        }
    }
}