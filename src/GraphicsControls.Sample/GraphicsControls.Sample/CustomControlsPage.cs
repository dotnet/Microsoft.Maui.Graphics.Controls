using GraphicsControls.Sample.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace GraphicsControls.Sample
{
    public class CustomControlsPage : ContentPage
    {
        readonly Color PageBackgroundColor = Color.FromArgb("#FFFFFF");
        readonly Color SectionHeaderBackgroundColor = Color.FromArgb("#E9E9E9");
        readonly Color SectionBackgroundColor = Color.FromArgb("#FAFAFA");

        public CustomControlsPage()
        {
            BackgroundColor = PageBackgroundColor;

            var scrollView = new ScrollView();

            var verticalStack = new StackLayout() { Margin = 12 };
            
            verticalStack.Add(CreateHeader());

            verticalStack.Add(CreateDrawCustomSlider());
            //verticalStack.Add(CreateCustomSliderMapper());
            verticalStack.Add(CreateCustomSliderDrawable());
            verticalStack.Add(CreatePersona());

            scrollView.Content = verticalStack;

            Content = scrollView;
        }

        IView CreateHeader()
        {
            var container = new StackLayout();

            container.Add(new Label
            {
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                Text = "Customize using Microsoft.Maui.Graphics.Controls",
                TextColor = Colors.Black,
                Margin = new Thickness(0, 24, 0, 0)
            });

            container.Add(new Label
            {
                Text = "An example where learn how to customize existing controls as well as create new drawn controls.",
                TextColor = Colors.Black
            });

            return container;
        }

        IView CreateContainer(string title, View content)
        {
            var contentContainer = new StackLayout
            {
                BackgroundColor = SectionBackgroundColor
            };

            var header = new Label
            {
                BackgroundColor = SectionHeaderBackgroundColor,
                Padding = 12,
                Text = title,
                TextColor = Colors.Black
            };

            contentContainer.Children.Add(header);
            contentContainer.Children.Add(content);

            var container = new Grid
            {
                BackgroundColor = SectionBackgroundColor,
                Padding = 0,
                Margin = new Thickness(0, 6)
            };

            container.Children.Add(contentContainer);

            return container;
        }

        IView CreateDrawCustomSlider()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Draw custom Slider" });

            var drawCustomSlider = new DrawCustomSlider
            {
                Minimum = 0,
                Maximum = 10,
                Value = 4
            };

            layout.Children.Add(drawCustomSlider);

            return CreateContainer("DrawCustomSlider", layout);
        }

        IView CreateCustomSliderMapper()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom Slider using Mapper" });

            var customSliderMapper = new CustomSliderMapper
            {
                Minimum = 0,
                Maximum = 10,
                Value = 4
            };

            layout.Children.Add(customSliderMapper);

            return CreateContainer("CustomSliderMapper", layout);
        }

        IView CreateCustomSliderDrawable()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Custom Slider using a custom Drawable" });

            var customSliderMapper = new CustomSliderDrawable
            {
                Minimum = 0,
                Maximum = 10,
                Value = 4
            };

            layout.Children.Add(customSliderMapper);

            return CreateContainer("CustomSliderDrawable", layout);
        }

        IView CreatePersona()
        {
            var layout = new StackLayout
            {
                Margin = new Thickness(12, 6, 12, 24)
            };

            layout.Children.Add(new Label { FontSize = 9, TextColor = Colors.Gray, Text = "Fluent Persona control" });

            var persona1 = new Persona
            {
                Name = "Javier Suarez",   
                AvatarSize = AvatarSize.Small,
                Margin = new Thickness(0, 6)
            }; 
            
            var persona2 = new Persona
            {
                Name = "David Ortinau",
                AvatarSize = AvatarSize.Large,
                Margin = new Thickness(0, 6)
            }; 
            
            var persona3 = new Persona
            {
                Name = "Jhon Dick",
                AvatarSize = AvatarSize.XXLarge,
                Margin = new Thickness(0, 6)
            };

            layout.Children.Add(persona1);
            layout.Children.Add(persona2);
            layout.Children.Add(persona3);

            return CreateContainer("Persona", layout);
        }
    }
}