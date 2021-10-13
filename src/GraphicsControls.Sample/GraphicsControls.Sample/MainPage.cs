using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using System;

namespace GraphicsControls.Sample
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Title = "GraphicsControls";

            BackgroundColor = SampleColors.PageBackgroundColor;

            var scrollView = new ScrollView();

            var layout = new Grid() { Margin = 12 };

            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            layout.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var header = CreateHeader();
            layout.Add(header);
            GridLayout.SetRow(header as BindableObject, 0);

            var content = CreateContent();
            layout.Add(content);
            GridLayout.SetRow(content as BindableObject, 1);

            var footer = CreateFooter();
            layout.Add(footer);
            GridLayout.SetRow(footer as BindableObject, 2);

            scrollView.Content = layout;

            Content = scrollView;
        }

        IView CreateHeader()
        {
            var container = new StackLayout();

            container.Add(new Label
            {
                FontSize = 24,
                FontAttributes = FontAttributes.Bold,
                Text = "Introducing Microsoft.Maui.Graphics.Controls",
                TextColor = Colors.Black,
                Margin = new Thickness(0, 12)
            });

            container.Add(new Label
            {
                FontSize = 16,
                Text = "A .NET MAUI experiment that offers drawn controls allowing to choose between Cupertino, Fluent and Material.",
                TextColor = Colors.Black
            });

            return container;
        }

        IView CreateContent()
        {
            var contentContainer = new StackLayout
            {
                Margin = new Thickness(0, 12)
            };

            var layout = new StackLayout();

            var galleryInfo = new Label
            {
                TextColor = Colors.Black,
                Text = "Next, you can access a gallery where you can test all the drawn controls. The Gallery is available in both C# and XAML so feel free to explore the option you prefer."
            };

            layout.Children.Add(galleryInfo);

            var galleryLayout = new Grid();

            galleryLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            galleryLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var cSharpFrame = new Frame
            {
                BackgroundColor = SampleColors.SectionBackgroundColor,
                CornerRadius = 12,
                HeightRequest = 150,
                WidthRequest = 150,
                Margin = new Thickness(0, 6, 6, 0)
            };

            var cSharpGesture = new TapGestureRecognizer();
            cSharpGesture.Tapped += OnCSharpGestureTapped;
            cSharpFrame.GestureRecognizers.Add(cSharpGesture);

            var cSharpLabel = new Label
            {
                Text = "C#",
                TextColor = Colors.Purple,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            cSharpFrame.Content = cSharpLabel;

            var xamlFrame = new Frame
            {
                BackgroundColor = SampleColors.SectionBackgroundColor,
                CornerRadius = 12,
                HeightRequest = 150,
                WidthRequest = 150,
                Margin = new Thickness(0, 6, 6, 0)
            }; 
            
            var xamlGesture = new TapGestureRecognizer();
            xamlGesture.Tapped += OnXamlGestureTapped;
            xamlFrame.GestureRecognizers.Add(xamlGesture);

            var xamlLabel = new Label
            {
                Text = "XAML",
                TextColor = Colors.Purple,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            xamlFrame.Content = xamlLabel;

            galleryLayout.Children.Add(cSharpFrame);
            GridLayout.SetColumn(cSharpFrame, 0);

            galleryLayout.Children.Add(xamlFrame);
            GridLayout.SetColumn(xamlFrame, 1);

            layout.Children.Add(galleryLayout);

            var customizeInfo = new Label
            {
                TextColor = Colors.Black,
                Text = "Also, there is an an example where learn how to customize existing controls as well as create new drawn controls."
            }; 
            
            layout.Children.Add(customizeInfo);

            var customizeLayout = new Grid();

            var customizeFrame = new Frame
            {
                BackgroundColor = SampleColors.SectionBackgroundColor,
                CornerRadius = 12,
                HeightRequest = 150,
                Margin = new Thickness(0, 6, 6, 0)
            };

            var customizeGesture = new TapGestureRecognizer();
            customizeGesture.Tapped += OnCustomizeGestureTapped;
            customizeFrame.GestureRecognizers.Add(customizeGesture);

            var customizeLabel = new Label
            {
                Text = "Customize or Create custom drawn controls",
                TextColor = Colors.Purple,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            customizeFrame.Content = customizeLabel;

            customizeLayout.Children.Add(customizeFrame);

            layout.Children.Add(customizeLayout);

            contentContainer.Children.Add(layout);

            return contentContainer;
        }

        IView CreateFooter()
        {
            var footer = new Label
            {
                FontSize = 10,
                Text = "Microsoft Corporation 2021",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Colors.Black
            };

            return footer;
        }

        void OnCSharpGestureTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GalleryPageCSharp());
        }

        void OnXamlGestureTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GalleryPageXAML());
        }

        void OnCustomizeGestureTapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CustomControlsPage());
        }
    }
}