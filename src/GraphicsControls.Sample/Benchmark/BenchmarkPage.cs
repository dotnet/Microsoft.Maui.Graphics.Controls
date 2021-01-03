using System;
using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public static class BenchmarkSettings
    {
        public static int NumberOfViews { get; } = 10;
    }

    public class BenchmarkPage : ContentPage
    {
        public BenchmarkPage()
        {
            Title = "GraphicsControls Benchmark";

            var layout = new StackLayout();

            var nativeButton = new Button
            {
                Text = "Use Native Controls"
            };

            var drawnButton = new Button
            {
                Text = "Use Drawn Controls"
            };

            layout.Children.Add(nativeButton);
            layout.Children.Add(drawnButton);

            Content = layout;

            nativeButton.Clicked += OnNativeButtonClicked;
            drawnButton.Clicked += OnDrawnButtonClicked;
        }

        void OnNativeButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NativeBenchmarkPage());
        }

        void OnDrawnButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DrawnBenchmarkPage());
        }
    }
}
