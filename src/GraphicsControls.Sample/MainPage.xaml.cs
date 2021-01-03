using System;
using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("GraphicsControls", "Button Clicked", "Ok");
        }
    }
}