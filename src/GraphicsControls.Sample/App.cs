using Microsoft.Maui.Controls;

namespace Microsoft.Maui.Graphics.Controls
{
	public class MyApp : Application
	{
		public MyApp()
		{
			
		}

		public override IWindow CreateWindow(IActivationState activationState)
		{
			return new MainWindow();
		}
	}
}