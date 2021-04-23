using Microsoft.Maui.Controls.Compatibility;
using Microsoft.Maui.Hosting;

namespace Microsoft.Maui.Graphics.Controls
{
    public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseFormsCompatibility()
				.UseMauiApp<MyApp>();
		}
	}
}