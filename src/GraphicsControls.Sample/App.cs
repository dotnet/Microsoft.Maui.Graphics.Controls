using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
    public class MyApp : Application
	{
		public MyApp()
		{

		}

		protected override IWindow CreateWindow(IActivationState activationState)
		{
			return new Window(new MainPage());
		}
	}
}