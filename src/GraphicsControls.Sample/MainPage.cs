using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MainPage : ContentPage, IPage
    {
        public MainPage()
        {
            BackgroundColor = Colors.White;

            var verticalStack = new VerticalStackLayout() { Margin = 12 };

            verticalStack.Add(new Label { FontSize = 18, Text = "Microsoft.Maui.Graphics.Controls", Margin = new Thickness(0, 24) });

            // BUTTON
            verticalStack.Add(new Label { FontSize = 9, Text = "Button" });
            verticalStack.Add(new Button { Text = "Button" });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled Button" });
            verticalStack.Add(new Button { IsEnabled = false, Text = "Disabled Button" });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom Button" });
            verticalStack.Add(new Button { BackgroundColor = Colors.Red, TextColor = Colors.Yellow, Text = "Custom Button" });

            // CHECKBOX
            verticalStack.Add(new Label { FontSize = 9, Text = "CheckBox" });
            verticalStack.Add(new CheckBox { IsChecked = true });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled CheckBox" });
            verticalStack.Add(new CheckBox { IsEnabled = false, IsChecked = true });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom CheckBox" });
            verticalStack.Add(new CheckBox { BackgroundColor = Colors.Purple, IsChecked = true });

            // PROGRESSBAR
            verticalStack.Add(new Label { FontSize = 9, Text = "ProgressBar" });
            verticalStack.Add(new ProgressBar { Progress = 0.5d });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled ProgressBar" });
            verticalStack.Add(new ProgressBar { IsEnabled = false, Progress = 0.5d });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom ProgressBar" });
            verticalStack.Add(new ProgressBar { ProgressColor = Colors.Orange, Progress = 0.5d });

            // SLIDER
            verticalStack.Add(new Label { FontSize = 9, Text = "Slider" });
            verticalStack.Add(new Slider { Minimum = 0, Maximum = 10, Value = 5 });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled Slider" });
            verticalStack.Add(new Slider { IsEnabled = false, Minimum = 0, Maximum = 10, Value = 5 });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom Slider" });
            verticalStack.Add(new Slider { Minimum = 0, MinimumTrackColor = Colors.Orange, Maximum = 10, MaximumTrackColor = Colors.YellowGreen, Value = 5, ThumbColor = Colors.BlueViolet });

            // STEPPER
            verticalStack.Add(new Label { FontSize = 9, Text = "Stepper" });
            verticalStack.Add(new Stepper { Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled Stepper" });
            verticalStack.Add(new Stepper { IsEnabled = false, Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom Stepper" });
            verticalStack.Add(new Stepper { BackgroundColor = Colors.CadetBlue, Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            // SWITCH
            verticalStack.Add(new Label { FontSize = 9, Text = "Switch" });
            verticalStack.Add(new Switch { IsToggled = true });

            verticalStack.Add(new Label { FontSize = 9, Text = "Disabled Switch" });
            verticalStack.Add(new Switch { IsEnabled = false, IsToggled = true });

            verticalStack.Add(new Label { FontSize = 9, Text = "Custom Switch" });
            verticalStack.Add(new Switch { OnColor = Colors.YellowGreen, ThumbColor = Colors.Green, IsToggled = true });

            Content = verticalStack;
        }
    }
}