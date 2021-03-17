using System;
using Xamarin.Forms;

namespace GraphicsControls.Sample
{
    public partial class MixedControlsPage : ContentPage
    {
        public MixedControlsPage()
        {
            InitializeComponent();
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("GraphicsControls", "Button Clicked", "Ok");
        }
    }
}