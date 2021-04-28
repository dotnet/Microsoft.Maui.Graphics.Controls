using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MainPage : ContentPage, IPage
    {
        public MainPage()
        {
            BackgroundColor = Colors.White;

            var verticalStack = new VerticalStackLayout() { Margin = 12 };

            verticalStack.Add(new Label { FontSize = 24, Text = "Microsoft.Maui.Graphics.Controls", Margin = new Thickness(0, 24) });

            verticalStack.Add(new Label { Text = "Button" });
            verticalStack.Add(new Button { Text = "Button" });

            verticalStack.Add(new Label { Text = "CheckBox" });
            verticalStack.Add(new CheckBox { IsChecked = true });

            verticalStack.Add(new Label { Text = "ProgressBar" });
            verticalStack.Add(new ProgressBar { Progress = 0.5d });

            verticalStack.Add(new Label { Text = "Slider" });
            verticalStack.Add(new Slider { Minimum = 0, Maximum = 10, Value = 5 });

            verticalStack.Add(new Label { Text = "Stepper" });
            verticalStack.Add(new Stepper { Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            verticalStack.Add(new Label { Text = "Switch" });
            verticalStack.Add(new Switch { IsToggled = true });

            Content = verticalStack;
        }
    }
}