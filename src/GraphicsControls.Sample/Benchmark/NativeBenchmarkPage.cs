using System.Diagnostics;
using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public class NativeBenchmarkPage : ContentPage
    {
        readonly Stopwatch _stopwatch;
        readonly Label _infoLabel;

        public NativeBenchmarkPage()
        {
            Title = "Use Native Controls";

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            var scrollView = new ScrollView();

            var layout = new StackLayout();

            _infoLabel = new Label
            {
                FontSize = 36,
                HorizontalOptions = LayoutOptions.Center
            };

            layout.Children.Add(_infoLabel);

            for (int i = 0; i < BenchmarkSettings.NumberOfViews; i++)
            {
                var drawnButton = new Xamarin.Forms.Button { Visual = VisualMarker.Material, Text = $"Button {i + 1}" };
                layout.Children.Add(drawnButton);
            }

            scrollView.Content = layout;

            Content = scrollView;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _stopwatch.Stop();

            _infoLabel.Text = $"{_stopwatch.ElapsedMilliseconds} ms";
        }
    }
}