using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System.Diagnostics;
using Switch = Microsoft.Maui.Controls.Switch;

namespace GraphicsControls.Sample
{
    public class GalleryPageCSharp : ContentPage
    {
        public GalleryPageCSharp()
        {
            Title = "C# Gallery";

            this.SetAppThemeColor(BackgroundColorProperty, SampleColors.LightPageBackgroundColor, SampleColors.DarkPageBackgroundColor);

            var scrollView = new ScrollView();

            var verticalStack = new StackLayout() { Margin = 2 };

            verticalStack.Add(CreateButton());
            verticalStack.Add(CreateCheckBox());
            verticalStack.Add(CreateDatePicker());
            verticalStack.Add(CreateEditor());
            verticalStack.Add(CreateEntry());
            verticalStack.Add(CreateProgressBar());
            verticalStack.Add(CreateSlider());
            verticalStack.Add(CreateStepper());
            verticalStack.Add(CreateSwitch());
            verticalStack.Add(CreateTimePicker());

            scrollView.Content = verticalStack;

            Content = scrollView;
        }

        IView CreateContainer(string title, View content)
        {
            var contentContainer = new StackLayout();

            contentContainer.SetAppThemeColor(BackgroundColorProperty, SampleColors.LightSectionBackgroundColor, SampleColors.DarkSectionBackgroundColor);

            var header = new Label
            {
                Padding = 12,
                TextColor = SampleColors.LightTextColor,
                Text = title
            };

            header.SetAppThemeColor(BackgroundColorProperty, SampleColors.LightSectionHeaderBackgroundColor, SampleColors.DarkSectionHeaderBackgroundColor);
            header.SetAppThemeColor(Label.TextColorProperty, SampleColors.LightTextColor, SampleColors.DarkTextColor);

            contentContainer.Children.Add(header);
            contentContainer.Children.Add(content);

            var container = new Grid
            {
                Padding = 0,
                Margin = new Thickness(0, 6)
            };

            container.SetAppThemeColor(BackgroundColorProperty, SampleColors.LightSectionBackgroundColor, SampleColors.DarkSectionBackgroundColor);

            container.Children.Add(contentContainer);

            return container;
        }

        IView CreateButton()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Button" });

            var defaultButton = new Button { Text = "Button" };

            defaultButton.Clicked += (sender, args) =>
            {
                Debug.WriteLine("Button Clicked");
            };

            layout.Children.Add(defaultButton);

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled Button" });
            layout.Children.Add(new Button { IsEnabled = false, Text = "Disabled Button" });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom Button" });
            layout.Children.Add(new Button { BackgroundColor = Colors.Red, TextColor = Colors.Yellow, Text = "Custom Button" });

            return CreateContainer("Button", layout);
        }

        IView CreateCheckBox()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new CheckBox { IsChecked = true });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new CheckBox { IsEnabled = false, IsChecked = true });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new CheckBox { Color = Colors.Purple, IsChecked = true });

            return CreateContainer("CheckBox", layout);
        }

        IView CreateDatePicker()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new DatePicker());

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new DatePicker { IsEnabled = false });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new DatePicker { BackgroundColor = Colors.LightGreen, TextColor = Colors.White });

            return CreateContainer("DatePicker", layout);
        }

        IView CreateEditor()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new Editor { Placeholder = "Placeholder" });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new Editor { IsEnabled = false, Placeholder = "Placeholder" });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new Editor { BackgroundColor = Colors.LightPink, TextColor = Colors.DeepPink, Placeholder = "Placeholder", PlaceholderColor = Colors.HotPink });

            return CreateContainer("Editor", layout);
        }

        IView CreateEntry()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new Entry { Placeholder = "Placeholder" });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new Entry { IsEnabled = false, Placeholder = "Placeholder" });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new Entry { BackgroundColor = Colors.LightBlue, TextColor = Colors.Blue, Placeholder = "Placeholder", PlaceholderColor = Colors.DarkBlue });

            return CreateContainer("Entry", layout);
        }

        IView CreateProgressBar()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new ProgressBar { Progress = 0.5d });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "ProgressBar" });
            layout.Children.Add(new ProgressBar { IsEnabled = false, Progress = 0.5d });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "ProgressBar" });
            layout.Children.Add(new ProgressBar { ProgressColor = Colors.Orange, Progress = 0.5d });

            return CreateContainer("ProgressBar", layout);
        }

        IView CreateSlider()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new Slider { Minimum = 0, Maximum = 10, Value = 5 });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new Slider { IsEnabled = false, Minimum = 0, Maximum = 10, Value = 5 });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new Slider { Minimum = 0, MinimumTrackColor = Colors.Orange, Maximum = 10, MaximumTrackColor = Colors.YellowGreen, Value = 5, ThumbColor = Colors.BlueViolet });

            return CreateContainer("Slider", layout);
        }

        IView CreateStepper()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new Stepper { Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new Stepper { IsEnabled = false, Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new Stepper { BackgroundColor = Colors.CadetBlue, Minimum = 0, Maximum = 10, Value = 5, Increment = 1 });

            return CreateContainer("Stepper", layout);
        }

        IView CreateSwitch()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new Switch { IsToggled = true });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new Switch { IsEnabled = false, IsToggled = true });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new Switch { OnColor = Colors.YellowGreen, ThumbColor = Colors.Green, IsToggled = true });

            return CreateContainer("Switch", layout);
        }

        IView CreateTimePicker()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Default" });
            layout.Children.Add(new TimePicker());

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Disabled" });
            layout.Children.Add(new TimePicker { IsEnabled = false });

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom" });
            layout.Children.Add(new TimePicker { BackgroundColor = Colors.LightSkyBlue, TextColor = Colors.White });

            return CreateContainer("TimePicker", layout);
        }
    }
}