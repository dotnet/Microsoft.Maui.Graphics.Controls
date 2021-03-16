using System;
using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public partial class OnlyDrawnControlsPage : ContentPage
    {
        public OnlyDrawnControlsPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("GraphicsControls", "Button Clicked", "Ok");
        }
    }
}