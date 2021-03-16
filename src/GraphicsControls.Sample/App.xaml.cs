using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.GTK)
                MainPage = new OnlyDrawnControlsPage();
            else
                MainPage = new MixedControlsPage();

            //MainPage = new NavigationPage(new BenchmarkPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
